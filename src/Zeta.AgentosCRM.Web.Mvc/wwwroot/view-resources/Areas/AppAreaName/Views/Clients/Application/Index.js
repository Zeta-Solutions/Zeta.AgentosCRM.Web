(function () {
    $(function () {
        var _$applicationsTable = $('#Applicationstable');
        console.log(_$applicationsTable);
        var _applicationsService = abp.services.app.applications;
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
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditApplicationModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Application/_CreateOrEditModal.js',
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
        var hiddenfield = $('input[name="Clientid"]').val();

        var dataTable = _$applicationsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _applicationsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SubjectsTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
                        clientIdFilter: hiddenfield,
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
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        let productName = row.productName;
                        let branchName = row.branchName;
                         
                        let workflowId = data.application.workflowId;
                        let appId = data.application.id;

                        let displayBranchName = branchName ? branchName : '-';
                        //let clientDetailUrl = `/AppAreaName/products/ProductsDetail?id=${row.application.productId}`;

                        //let fullName = ` <a href="${clientDetailUrl}" class="text-truncate" title="${productName}"> <span style="color:blue; font-size: 14px;">${productName}</span> </a><br> ${displayBranchName}`;
                        //return `${fullName}`;
                        var contaxtMenu = `
                                <div class="d-flex align-items-center">
             
                                    <div class="d-flex flex-column">
                                    <div id="search" style="cursor: pointer;" data-id="${workflowId}" data-action="${appId}"><span style="color:blue; font-size: 14px;">${productName}</span><br>${displayBranchName}</div>

                                    </div>
                                </div>
                            `;
                        return contaxtMenu;





                    },
                    name: 'concatenatedData',
                },
                {
                    targets: 2,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        let workflowName = data.workflowName;
                        let workflowId = data.application.workflowId;
                        let appId = data.application.id;
                        var contaxtMenu = `
                                <div class="d-flex align-items-center">
             
                                    <div class="d-flex flex-column">
                                    <div id="search">${workflowName}</div>

                                    </div>
                                </div>
                            `;
                        return contaxtMenu;
                    }

                },
                {
                    targets: 3,
                    data: 'applicationName',
                    name: 'applicationName',
                },
                {
                    targets: 4,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        let isDiscontinue = row.application.isDiscontinue;

                        if (isDiscontinue == true) {
                            return `<span style="color: red; font-size: 14px;">&#8226; Discontinue</span>`;
                        } else {
                            return `<span style="color:blue; font-size: 14px;">&#8226; InProgress</span>`;
                        }
                    },

                    name: 'concatenatedData',
                    //data: 'application.isDiscontinue',
                    //name: 'application.isDiscontinue', 

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
                        var rowId = data.application.id;
                        var rowData = data.application;
                        var RowDatajsonString = JSON.stringify(rowData);
                        var contaxtMenu = '<div class="context-menu Applicationmenu" style="position:relative;">' +
                            '<div class="Applicationellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 4px 4px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action1="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action1='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';
                        return contaxtMenu;
                    }
                },
            ],
        });

        // Add a click event handler for the ellipsis icons
        $(document).on('click', '#search', function (e) {
            //
            var workflowId = $(this).data('id');
            var appId = $(this).data('action');
            $("#ApplicationWorkflowId").val(workflowId)
            $("#ApplicationId").val(appId)

            // Show the selected tab
            var selectedTab = document.getElementById("ApplicationDetailTab");

            var pane = selectedTab.attributes[0];
            var src = selectedTab.dataset.url;
            abp.ui.setBusy();
            $("#ApplicationDetailTabDiv").load(src + "/" + appId);

            selectedTab.click();

            abp.ui.clearBusy();
            var id = appId;
            //setTimeout(function () {
            //    _applicationsService.getApplicationForView(id)
            //        .done(function (data) {
            //             ;
            //            if (data.application.isDiscontinue === true) {
            //                $('#AppDiscontinueBtn').hide();
            //                $('#AppActiveBtn').show();
            //                $('#smartwizard').smartWizard("disable");
            //                $("#AppPreviousBtn").addClass('disabled');
            //                $("#AppNextBtn").addClass('disabled');
                           
            //            } else {
            //                $('#AppDiscontinueBtn').show();
            //                $('#AppActiveBtn').hide();
            //                $("#AppPreviousBtn").removeClass('disabled');
            //                $("#AppNextBtn").removeClass('disabled');
            //            }
            //        })
            //        .fail(function (error) {
            //            // Handle errors here, if needed
            //            console.error("Error fetching application data:", error);
            //        });
            //}, 100);
                
        });
        $(document).on('click', '.Applicationellipsis', function (e) {
            e.preventDefault();
            //
            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });

        // Close the contextcontext menu when clicking outside of it
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });
        $(document).on('click', 'a[data-action1]', function (e) {
            e.preventDefault();
            //
            var rowId = $(this).data('id');
            var action = $(this).data('action1');
            //
            // Handle the selected action based on the rowId
            if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                deleteApplications(rowId);
            }
        });
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
})(jQuery);
