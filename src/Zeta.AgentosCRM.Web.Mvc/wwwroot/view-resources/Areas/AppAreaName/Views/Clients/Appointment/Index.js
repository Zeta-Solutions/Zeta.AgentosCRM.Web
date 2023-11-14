(function () {
    //alert("clientAppointment");
    $(function () {
        var _$Appointmentstable = $('#Appointmentstable');
        var _clientAppointmentsService = abp.services.app.clientAppointments;
        debugger
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
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditClientAppointmentModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Appointment/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditClientsAppoinmentModal',
        });
        var _createOrEditModalEmail = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/ClientEmailCompose',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Client/ApplicationClient/_CreateOrEditModal.js',
            modalClass: 'ClientEmailCompose',
        });
        var _viewSubjectModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ApplicationClient/ViewApplicationModal',
            modalClass: 'ViewApplicationModal',
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
                //{
                //    targets: 1, // The column index (zero-based) where you want to add the "View" button
                //    data: 'subject.abbrivation',
                //    name: 'abbrivation',
                //    render: function (data, type, row) {
                //        return '<a href="' + abp.appPath + 'AppAreaName/Client/ClientDetail/' + row.subject.id + '" class="btn btn-primary">View</a>';
                //    }
                //},
                {
                    targets: 1,
                    data: 'clientAppointments.name',
                    name: 'name',
                },
                {
                    targets: 2,
                    data: 'clientAppointments.timeZone',
                    name: 'timeZone',
                },
                {
                    targets: 3,
                    data: 'clientAppointments.appointmentDate',
                    name: 'appointmentDate',
                },
                {
                    targets: 4,
                    data: 'clientAppointments.appointmentTime',
                    name: 'appointmentTime',
                },
                {
                    targets: 5,
                    data: 'clientAppointments.title',
                    name: 'title',
                },
                {
                    targets: 6,
                    data: 'clientAppointments.description',
                    name: 'description',
                },
               
            ],
        });

        function getClientAppointments() {
            dataTable.ajax.reload();
        }

        function deletePartnerType(subject) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientAppointmentsService
                        .delete({
                            id: subject.id,
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
            getSubjects();
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
