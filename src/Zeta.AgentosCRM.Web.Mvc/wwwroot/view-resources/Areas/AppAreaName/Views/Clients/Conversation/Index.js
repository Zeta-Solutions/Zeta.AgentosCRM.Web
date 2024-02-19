(function () {
    //alert("clientAppointment");
    $(function () {
        //var _$Appointmentstable = $('#Appointmentstable');
       // var _clientAppointmentsService = abp.services.app.appointments;

        var _sentEmailService = abp.services.app.sentEmails;
        var _$clientEmailTable = $('#ClientEmailTable');

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };

        var clientId = $('input[name="Clientid"]').val();

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

        var _createOrEditSmsModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/SendSms',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Conversation/_SendSmsConversationModal.js',
            modalClass: 'SendSmsModal',
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
   
        function getsmsconversation() {
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
                            getsmsconversation(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        }


        var dataTable = _$clientEmailTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _sentEmailService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EmailsTableFilter').val(),
                        subjectFilter: $('#SubjectFilter').val(),
                        clientIdFilter: clientId,//$('#EmailClientIdFilter').val(),
                        
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                }, 
                {
                    targets: 1,
                    data: 'sentEmail.subject',
                    name: 'sentEmail.subject',
                },
                {
                    targets: 2,
                    data: 'sentEmail.toEmail',
                    name: 'sentEmail.toEmail',
                }, 
                {
                    targets: 3,
                    data: "userName",
                    name: "userName"
                },
                {
                    targets: 4,
                    data: 'sentEmail.creationTime',
                    name: 'sentEmail.creationTime',
                }, 
            ]
        });


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

        $('#SmsButton').click(function () {
            //_createOrEditSmsModal.open();
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
            getsmsconversation();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getsmsconversation();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getsmsconversation();
            }
        });

        $('.reload-on-change').change(function (e) {
            getsmsconversation();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getsmsconversation();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getsmsconversation();
        });

    });
})(jQuery);
