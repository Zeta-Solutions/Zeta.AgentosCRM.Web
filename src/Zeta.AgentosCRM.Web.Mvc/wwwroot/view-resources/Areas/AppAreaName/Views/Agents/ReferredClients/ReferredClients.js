(function () {
    $(function () {
        var _$ClientTable = $('#ClientReferedtable');
        var _clientsService = abp.services.app.clients;

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
                getSubjects();
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
            viewUrl: abp.appPath + 'AppAreaName/Agents/CreateOrEditReferredClientsModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Agents/ReferredClients/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditReferredClientsModal',
        });

        //var _viewSubjectModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/ApplicationClient/ViewApplicationModal',
        //    modalClass: 'ViewApplicationModal',
        //});

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
        var dataTable = _$ClientTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientsService.getAll,
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
                        let firstNameInitial = row.client.firstName.charAt(0).toUpperCase();
                        let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                        let initials = `${firstNameInitial}${lastNameInitial}`;
                        let fullName = `${row.client.firstName} ${row.client.lastName}`;
                        /*      console.log(row);*/
                        debugger
                        // Generate the URLs using JavaScript variables
                        let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.client.id}`;
                        //let clientEmailComposeUrl = _createOrEditModalEmail.open(row.client.id);
                        let clientEmailComposeUrl = `/AppAreaName/Clients/ClientEmailCompose?id=${row.client.id}`;
                        /*             console.log(clientEmailComposeUrl);*/

                        return `
        <div class="d-flex align-items-center">
            <span class="rounded-circle bg-primary text-white p-2 me-2" title="${fullName}">
                <b>${initials}</b>
            </span>
            <div class="d-flex flex-column">
                <a href="${clientDetailUrl}" class="text-truncate" title="${fullName}">
                    ${fullName}
                </a> 
                 <a href="#" class="EmailForm" data-id="${row.client.id}">${row.client.email}</a>
            </div>
        </div>
    `;
                    },

                    name: 'concatenatedData',

                },

                {
                    targets: 2,
                    data: function (row) {
                        return row.client.city + '/' +row.passportCountry;
                    },
                    name: 'passportCountryFk.name'
                },
                //{
                //    targets: 3,
                //    data: 'workflowName',
                //    name: 'workflowNameFk.name',
                //},
                //{
                //    targets: 3,
                //    //data: 'application.productName',
                //    //name: 'productName',

                //    data: 'productName',
                //    name: 'productNameFk.name',
                //},
                {
                    width: 30,
                    targets: 3,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.client.id;
                        console.log(rowId);
                        var rowData = data.client;
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

        function getSubjects() {
            dataTable.ajax.reload();
        }

        function deletePartnerType(subject) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _subjectsService
                        .delete({
                            id: subject.id,
                        })
                        .done(function () {
                            getSubjects(true);
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


        $('#ExportToExcelButton').click(function () {
            _subjectsService
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

        abp.event.on('app.createOrEditPartnerTypeModalSaved', function () {
            getSubjects();
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
            getSubjects();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getSubjects();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getSubjects();
        });
    });
})();
