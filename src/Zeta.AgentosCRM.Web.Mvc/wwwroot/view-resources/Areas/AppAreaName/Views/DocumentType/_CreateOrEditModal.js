(function ($) {
  app.modals.CreateOrEditDocumentTypeModal = function () {
      var _documentTypesService = abp.services.app.documentTypes;
      debugger
    var _modalManager;
    var _$documentTypesInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$documentTypesInformationForm = _modalManager.getModal().find('form[name=DocumentTypeInformationsForm]');
        _$documentTypesInformationForm.validate();
    };

    this.save = function () {
        if (!_$documentTypesInformationForm.valid()) {
        return;
      }

        var feeType = _$documentTypesInformationForm.serializeFormToObject();
        debugger
      _modalManager.setBusy(true);
        _documentTypesService
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
