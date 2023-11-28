(function () {
    $(function () {
        var _$checkInLogstableTable = $('#CheckInLogstable');
        var _checkInLogsServices = abp.services.app.checkInLogs;

        var dateTimeString = "2023-01-01T00:00:00Z"; // Replace this with your date-time string

        var date = new Date(dateTimeString);
        var formattedDate = date.toISOString().split('T')[0]; // Extracting only the date part

        $('#dateDisplay').text(formattedDate);

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
                getclientInterstedService();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getclientInterstedService();
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
                getclientInterstedService();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getclientInterstedService();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditCheckInLogsModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/CheckInLogs/CreateOrEditModal.js',
            modalClass: 'CreateOrEditcheckInLogsModal',
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

        var dataTable = _$checkInLogstableTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _checkInLogsServices.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SubjectsTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
                    };
                },
            },
            columnDefs: [
                {
                    className: ' responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0,
                },
                {
                    targets: 1,
                    data: 'checkInLog.title',
                    name: 'title',
                },
                {
                    targets: 2,

                    data: 'checkInLog.checkInPurpose',
                    name: 'checkInPurpose',

                },
                {
                    targets: 3,
                    //data: 'application.productName',
                    //name: 'productName',
                    data: 'checkInLog.checkInStatus',
                    name: 'checkInStatus',
                },
                {
                    targets: 4,
                    data: 'checkInLog.checkInDate',
                    name: 'checkInDate',
                },
                {
                    targets: 5,
                    data: 'checkInLog.startTime',
                    name: 'startTime',
                },
                {
                    targets: 6,
                    data: 'checkInLog.endTime',
                    name: 'endTime',
                },
                {
                    width: 30,
                    targets: 7,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                       // console.log(data);
                        var rowId = data.checkInLog.id;
                       // console.log(rowId);
                        var rowData = data.checkInLog;
                        var RowDatajsonString = JSON.stringify(rowData);
                       // console.log(RowDatajsonString);
                        var contaxtMenu = '<div class="context-menu Applicationmenu" style="position:relative;">' +
                            '<div class="Checkellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 4px 4px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<li ><a href="#" style="color: black;" data-action6="edit" data-id="' + rowId + '">Edit</a></li>' +
                            "<a href='#' style='color: black;' data-action6='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';


                        return contaxtMenu;
                    }


                },

            ],
        });

        function getclientInterstedService() {
            dataTable.ajax.reload();
        }
        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.Checkellipsis', function (e) {
            e.preventDefault();
            debugger
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
        $(document).on('click', 'a[data-action6]', function (e) {
            e.preventDefault();
            debugger
            var rowId = $(this).data('id');
            var action = $(this).data('action6');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteclientInterstedService(rowId);
            }
        });
        function deleteclientInterstedService(checkInLogs) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _checkInLogsServices
                        .delete({
                            id: checkInLogs.id,
                        })
                        .done(function () {
                            getclientInterstedService(true);
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

        $('#CreateNewCheckInLogsButton').click(function () {
            _createOrEditModal.open();
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

        abp.event.on('app.createOrEditCheckInInformationFormModalSaved', function () {
            getclientInterstedService();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getclientInterstedService();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getclientInterstedService();
            }
        });

        $('.reload-on-change').change(function (e) {
            getclientInterstedService();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getclientInterstedService();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getclientInterstedService();
        });
    });
})(jQuery);
