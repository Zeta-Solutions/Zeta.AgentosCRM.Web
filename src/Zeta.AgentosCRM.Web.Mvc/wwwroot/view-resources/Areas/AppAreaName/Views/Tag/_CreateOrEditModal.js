(function ($) {
  app.modals.CreateOrEditTagsModal = function () {
      var _tagsService = abp.services.app.tags;

    var _modalManager;
      var _$tagsInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$tagsInformationForm = _modalManager.getModal().find('form[name=TagInformationsForm]');
        _$tagsInformationForm.validate();
    };

    this.save = function () {
        if (!_$tagsInformationForm.valid()) {
        return;
      }

        var tags = _$tagsInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _tagsService
            .createOrEdit(tags)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditTagsModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
