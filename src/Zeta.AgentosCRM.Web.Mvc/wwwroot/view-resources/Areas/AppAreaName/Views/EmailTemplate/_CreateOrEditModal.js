(function ($) {
  app.modals.CreateOrEditEmailTemplateModal = function () {
      var _emailTemplatesService = abp.services.app.emailTemplates;

    var _modalManager;
    var _$emailTemplatesInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$emailTemplatesInformationForm = _modalManager.getModal().find('form[name=EmailTemplateInformationsForm]');
        _$emailTemplatesInformationForm.validate();
    };

    this.save = function () {
        if (!_$emailTemplatesInformationForm.valid()) {
        return;
      }

        var feeType = _$emailTemplatesInformationForm.serializeFormToObject();
        debugger
      _modalManager.setBusy(true);
        _emailTemplatesService
            .createOrEdit(feeType)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
            abp.event.trigger('app.createOrEditEmailTemplateModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
