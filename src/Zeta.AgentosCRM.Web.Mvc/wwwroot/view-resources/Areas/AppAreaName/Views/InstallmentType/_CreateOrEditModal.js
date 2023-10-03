(function ($) {
  app.modals.CreateOrEditMasterCategoryModal = function () {
    var _masterCategoriesService = abp.services.app.masterCategories;

    var _modalManager;
    var _$masterCategoryInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$masterCategoryInformationForm = _modalManager.getModal().find('form[name=InformationsTab]');
      _$masterCategoryInformationForm.validate();
    };

    this.save = function () {
      if (!_$masterCategoryInformationForm.valid()) {
        return;
      }

      var masterCategory = _$masterCategoryInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _masterCategoriesService
        .createOrEdit(masterCategory)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditMasterCategoryModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
