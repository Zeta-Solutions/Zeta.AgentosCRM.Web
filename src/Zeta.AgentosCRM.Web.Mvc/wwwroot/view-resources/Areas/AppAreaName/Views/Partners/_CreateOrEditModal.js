(function ($) {
    app.modals.CreateOrEditPartnersModal = function () {
      var _leadSourcesService = abp.services.app.leadSources;

    var _modalManager;
    var _$leadSourceInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$leadSourceInformationForm = _modalManager.getModal().find('form[name=PartnersInformationsForm]');
        _$leadSourceInformationForm.validate();
    };

    this.save = function () {
        if (!_$leadSourceInformationForm.valid()) {
        return;
      }

        var leadSources = _$leadSourceInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _leadSourcesService
            .createOrEdit(leadSources)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
            abp.event.trigger('app.createOrEditPartnerModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
