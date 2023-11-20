(function ($) {
    app.modals.CreateOrEditClientsAppoinmentModal = function () {
        debugger;
        var _clientAppointmentsService = abp.services.app.appointments;
      
      
        var hiddenfield2 = $('[name="partnerName"]').val();
        $("#Applointment_PartnerName").val(hiddenfield2);
        //$("#applointment_Invitees").select2();
        
        var hiddenfield = $("#PartnerId").val();
        $("#partnerId").val(hiddenfield);
        //$('[name="PartnerId"]').val(hiddenfield);
        var _modalManager;
        var _$clientTagsInformationForm = null;

       
        var _modalManager;
        var _$clientAppointmentsInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$clientAppointmentsInformationForm = _modalManager.getModal().find('form[name=AppointmentInformationsForm]');
            _$clientAppointmentsInformationForm.validate();
        };




        $(document).on('blur', '#applointment_Invitees', function () {
            var applointment_InviteesID = $("#applointment_Invitees").val();
            console.log(applointment_InviteesID);
            $("#AddedById").val(applointment_InviteesID);
        });
        this.save = function () {
            if (!_$clientAppointmentsInformationForm.valid()) {
                return;
            }
            debugger
            var ClientAppointment = _$clientAppointmentsInformationForm.serializeFormToObject();
            console.log(ClientAppointment);
            _modalManager.setBusy(true);
            _clientAppointmentsService
                .createOrEdit(ClientAppointment)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditClientAppointmentModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
