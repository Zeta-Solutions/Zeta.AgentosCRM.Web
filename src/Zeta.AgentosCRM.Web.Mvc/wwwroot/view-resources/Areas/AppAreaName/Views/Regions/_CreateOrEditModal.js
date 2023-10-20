(function ($) {
  app.modals.CreateOrEditRegionModal = function () {
      var _regionsService = abp.services.app.regions;

    var _modalManager;
    var _$regionInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$regionInformationForm = _modalManager.getModal().find('form[name=RegionInformationsForm]');
        _$regionInformationForm.validate();
    };

    this.save = function () {
        if (!_$regionInformationForm.valid()) {
        return;
      }

        var region = _$regionInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _regionsService
            .createOrEdit(region)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditRegionModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
