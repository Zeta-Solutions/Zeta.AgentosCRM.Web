(function () {
    //alert("clientAppointment");
    $(function () {
        var _$Appointmentstable = $('#Appointmentstable');
        var _clientAppointmentsService = abp.services.app.appointments;
        debugger
        console.log(_clientAppointmentsService);
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
                getClientAppointments();
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
                getClientAppointments();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getClientAppointments();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditAppointmentModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Appointments/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditClientsAppoinmentModal',
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

        var dataTable = _$Appointmentstable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientAppointmentsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#NameFilterId').val(),
                        TimeZoneFilter: $('#TimeZoneFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
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
                    data: 'appointment.title',
                    name: 'title',
                },
                {
                    targets: 2,
                    data: 'appointment.timeZone',
                    name: 'timeZone',
                },
                {
                    targets: 3,
                    data: 'appointment.appointmentDate',
                    name: 'appointmentDate',
                },
                {
                    targets: 4,
                    data: 'appointment.appointmentTime',
                    name: 'appointmentTime',
                },

                {
                    targets: 5,
                    data: 'appointment.description',
                    name: 'description',
                },
                {
                    width: 30,
                    targets: 6,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.appointment.id;
                        console.log(rowId);
                        var rowData = data.appointment;
                        var RowDatajsonString = JSON.stringify(rowData);
                        console.log(RowDatajsonString);
                        var contaxtMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis30"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 4px 4px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action30="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action30='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';


                        return contaxtMenu;
                    }


                },

            ],
        });
        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis30', function (e) {
            e.preventDefault();
            debugger
            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });

        // Close the context menu when clicking outside of it
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });
        $(document).on('click', 'a[data-action30]', function (e) {
            e.preventDefault();
            debugger
            var rowId = $(this).data('id');
            var action = $(this).data('action30');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteAppointments(rowId);
            }
        });
        function getClientAppointments() {
            dataTable.ajax.reload();
        }

        function deleteAppointments(appointment) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientAppointmentsService
                        .delete({
                            id: appointment.id,
                        })
                        .done(function () {
                            getClientAppointments(true);
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

        $('#CreateNewAppointmentButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _clientAppointmentsService
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

        abp.event.on('app.createOrEditClientAppointmentModalSaved', function () {
            getClientAppointments();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getClientAppointments();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getClientAppointments();
            }
        });

        $('.reload-on-change').change(function (e) {
            getClientAppointments();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getClientAppointments();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getClientAppointments();
        });

    });
})();
