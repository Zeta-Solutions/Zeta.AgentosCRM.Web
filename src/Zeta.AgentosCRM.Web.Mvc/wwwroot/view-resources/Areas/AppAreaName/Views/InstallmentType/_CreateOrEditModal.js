(function ($) {
  app.modals.CreateOrEditInstallmentTypeModal = function () {
      var _installmentTypesService = abp.services.app.installmentTypes;

    var _modalManager;
      var _$installmentTypesInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$installmentTypesInformationForm = _modalManager.getModal().find('form[name=InstallmentTypeInformationsForm]');
        _$installmentTypesInformationForm.validate();
    };

    this.save = function () {
        if (!_$installmentTypesInformationForm.valid()) {
        return;
      }

        var masterCategory = _$installmentTypesInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _installmentTypesService
        .createOrEdit(masterCategory)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditInstallmentTypeModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
