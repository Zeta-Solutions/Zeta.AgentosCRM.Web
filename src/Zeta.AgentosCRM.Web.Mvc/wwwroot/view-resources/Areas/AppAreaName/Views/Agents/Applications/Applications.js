﻿(function () {
    $(function () {
        var _$applicationsTable = $('#Applicationstable');
        var _applicationsService = abp.services.app.applications;
        debugger
        console.log(_applicationsService)
        var $selectedDate = {
            startDate: null,
            endDate: null,
        };


        $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('.startDate')
            .daterangepicker({
                autoUpdateInput: false,
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            })
            .on('apply.daterangepicker', (ev, picker) => {
                $selectedDate.startDate = picker.startDate;
                getSubjects();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getSubjects();
            });

        $('.endDate')
            .daterangepicker({
                autoUpdateInput: false,
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            })
            .on('apply.daterangepicker', (ev, picker) => {
                $selectedDate.endDate = picker.startDate;
                getApplications();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getSubjects();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditApplicationModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Application/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditApplicationModal',
        });


        var getDateFilter = function (element) {
            if ($selectedDate.startDate == null) {
                return null;
            }
            return $selectedDate.startDate.format('YYYY-MM-DDT00:00:00Z');
        };

        var getMaxDateFilter = function (element) {
            if ($selectedDate.endDate == null) {
                return null;
            }
            return $selectedDate.endDate.format('YYYY-MM-DDT23:59:59Z');
        };
        var hiddenfield = $("#AgentId").val();
        var dynamicValue = hiddenfield;

        var dataTable = _$applicationsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _applicationsService.getAll,
                inputFilter: function () {
                    return {
                        agentIdFilter: dynamicValue,
                    };
                },
            },
            columnDefs: [
                {
                    className: ' responsive',
                    /* className: 'control responsive',*/
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0,
                },
                {
                    width: 100,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    // Assuming 'row' contains the client data with properties 'firstName', 'lastName', and 'email'
                    render: function (data, type, row) {
                        let firstNameInitial = row.clientFirstName.charAt(0).toUpperCase();
                        let lastNameInitial = row.clientLastName.charAt(0).toUpperCase();
                        let initials = `${firstNameInitial}` + ' ' + `${lastNameInitial}`;
                        let fullName = `${row.clientFirstName}` + ' ' + `${row.clientLastName}`; 
                        let ClientEmail = row.clientEmail;
                        debugger
                        // Generate the URLs using JavaScript variables
                        let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.application.clientId}`;
                        //let clientEmailComposeUrl = _createOrEditModalEmail.open(row.client.id);
                        // let clientEmailComposeUrl = `/AppAreaName/Clients/ClientEmailCompose?id=${row.client.id}`;
                        /*             console.log(clientEmailComposeUrl);*/

                        return `
        <div class="d-flex align-items-center">
            <span class="rounded-circle bg-primary text-white p-2 me-2" title="${fullName}">
                <b>${initials}</b>
            </span>
            <div class="d-flex flex-column">
                <a href="${clientDetailUrl}" class="text-truncate" title="${fullName}">
                    ${fullName}<br>   ${ClientEmail} 
                </a> 
              
            </div>
        </div>
    `;
                    },

                    name: 'concatenatedData',

                },

                {
                    width: 100,
                    targets: 2,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    // Assuming 'row' contains the client data with properties 'firstName', 'lastName', and 'email'
                    render: function (data, type, row) {
                        let branchName = row.branchName;//.charAt(0).toUpperCase();
                        //let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                        //let initials = `${firstNameInitial}`;
                        let fullName = `${row.workflowName}`;

                        let displayBranchName = branchName ? branchName : '-';
                        /*      console.log(row);*/
                        debugger
                        // Generate the URLs using JavaScript variables
                        let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.application.clientId}`;
                        //let clientEmailComposeUrl = _createOrEditModalEmail.open(row.client.id);
                        // let clientEmailComposeUrl = `/AppAreaName/Clients/ClientEmailCompose?id=${row.client.id}`;
                        /*             console.log(clientEmailComposeUrl);*/

                        return `
        <div class="d-flex align-items-center">
            <div class="d-flex flex-column">
                <a href="${clientDetailUrl}" class="text-truncate" title="${fullName}">
                    ${fullName}
                </a> 
              <span>${displayBranchName}</span>
            </div>
        </div>
    `;
                    },

                    name: 'concatenatedData',

                },
                //{
                //    targets: 2,
                //    data: 'workflowName',
                //    name: 'workflowNameFk.name',
                //},
                {
                    targets: 3,

                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        let IsCurrent = row.isCurrent;
                        if (IsCurrent == false) {
                            return `<span>` + row.applicationName + `</span>`;
                        } else if (IsCurrent == true) {
                            return `<span>` + row.applicationName + `</span>`;
                        }
                    },

                    name: 'concatenatedData',
                },
                {
                    targets: 4,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        let isDiscontinue = row.application.isDiscontinue;

                        if (isDiscontinue == false) {
                            return `<span style="color: red; font-size: 14px;">&#8226;Discontinue</span>`;
                        } else if (isDiscontinue == true) {
                            return `<span style="color:blue; font-size: 14px;">&#8226;InProgress</span>`;
                        }
                    },

                    name: 'concatenatedData',
                },

                {
                    targets: 5,

                    data: 'application.creationTime',
                    name: 'creationTime',
                    render: function (creationTime) {
                        if (creationTime) {
                            return moment(creationTime).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 6,

                    data: 'application.lastModificationTime',
                    name: 'lastModificationTime',
                    render: function (lastModificationTime) {
                        if (lastModificationTime) {
                            return moment(lastModificationTime).format('L');
                        }
                        return "";
                    }

                },
                {
                    width: 30,
                    targets: 7,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.application.id;
                        console.log(rowId);
                        var rowData = data.application;
                        var RowDatajsonString = JSON.stringify(rowData);
                        console.log(RowDatajsonString);
                        var contaxtMenu = '<div class="context-menu Applicationmenu" style="position:relative;">' +
                            '<div class="Applicationellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 4px 4px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<li ><a href="#" style="color: black;" data-action60="edit" data-id="' + rowId + '">Edit</a></li>' +
                            "<a href='#' style='color: black;' data-action60='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';


                        return contaxtMenu;
                    }


                },


            ],
        });
        // Add a click event handler for the ellipsis icons
        //$(document).on('click', '.Applicationellipsis', function (e) {
        //    e.preventDefault();
        //    debugger
        //    var options = $(this).closest('.context-menu').find('.options');
        //    var allOptions = $('.options');  // Select all options

        //    // Close all other open options
        //    allOptions.not(options).hide();

        //    // Toggle the visibility of the options
        //    options.toggle();
        //});

        //// Close the contextcontext menu when clicking outside of it
        //$(document).on('click', function (event) {
        //    if (!$(event.target).closest('.context-menu').length) {
        //        $('.options').hide();
        //    }
        //});
        //$(document).on('click', 'a[data-action60]', function (e) {
        //    e.preventDefault();
        //    debugger
        //    var rowId = $(this).data('id');
        //    var action = $(this).data('action60');
        //    debugger
        //    // Handle the selected action based on the rowId
        //    if (action === 'edit') {
        //        _createOrEditModal.open({ id: rowId });
        //    } else if (action === 'delete') {
        //        console.log(rowId);
        //        deleteApplications(rowId);
        //    }
        //});
        function getApplications() {
            dataTable.ajax.reload();
        }

        function deleteApplications(application) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _applicationsService
                        .delete({
                            id: application.id,
                        })
                        .done(function () {
                            getApplications(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        }

        $('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewApplicationButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _applicationsService
                .getPartnerTypesToExcel({
                    filter: $('#SubjectsTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                    subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditApplicationsModalSaved', function () {
            getApplications();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getSubjects();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getSubjects();
            }
        });

        $('.reload-on-change').change(function (e) {
            getApplications();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getApplications();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getApplications();
        });
    });
})();
