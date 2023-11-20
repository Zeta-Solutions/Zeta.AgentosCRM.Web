(function ($) {
  app.modals.CreateOrEditEmailTemplateModal = function () {
      //var _feeTypesService = abp.services.app.feeTypes;

    var _modalManager;
    var _$feeTypesInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$feeTypesInformationForm = _modalManager.getModal().find('form[name=FeeTypeInformationsForm]');
        _$feeTypesInformationForm.validate();
    };

    this.save = function () {
        if (!_$feeTypesInformationForm.valid()) {
        return;
      }

        var feeType = _$feeTypesInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _feeTypesService
            .createOrEdit(feeType)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditFeeTypeModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
