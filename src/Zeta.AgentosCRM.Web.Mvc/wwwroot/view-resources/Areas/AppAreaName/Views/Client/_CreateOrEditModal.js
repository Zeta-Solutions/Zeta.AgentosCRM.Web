(function ($) {
  app.modals.CreateOrEditClientModal = function () {
      var _clientService = abp.services.app.clients;

    var _modalManager;
      var _$ClientInformationsForm = null;



    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$ClientInformationsForm = _modalManager.getModal().find('form[name=ClientInformationsForm]');
        _$ClientInformationsForm.validate();
    };

    

 

    this.save = function () {
        if (!_$ClientInformationsForm.valid()) {
        return;
      }


        var Client = _$ClientInformationsForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _clientService
            .createOrEdit(Client)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
            abp.event.trigger('app.createOrEditClientModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
