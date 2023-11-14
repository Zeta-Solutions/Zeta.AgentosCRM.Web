(function ($) {
    app.modals.CreateOrEditClientsAppoinmentModal = function () {
        debugger;
        var _clientAppointmentsService = abp.services.app.appointments;
      //var hiddenfield = $("#clientId").val();
      var hiddenfield = $("#clientAppID").val();
      //$("#clientId").val(hiddenfield);
      $("#Applointment_ClietName").val(hiddenfield);
      //$("#applointment_Invitees").select2();
        var hiddenfield = $("#ID").val();
        $("#ClientId").val(hiddenfield);
        var _modalManager;
        var _$clientTagsInformationForm = null;

        $('input[name*="clientId"]').val(hiddenfield)
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

    

 

    this.save = function () {
        if (!_$clientAppointmentsInformationForm.valid()) {
        return;
      }

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
