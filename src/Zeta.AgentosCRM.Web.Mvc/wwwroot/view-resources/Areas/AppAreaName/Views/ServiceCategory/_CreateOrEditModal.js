(function ($) {
    app.modals.CreateOrEditServiceCategoryModal = function () {
        var _serviceCategoriesAppService = abp.services.app.serviceCategories;

    var _modalManager;
    var _$serviceCategoryInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$serviceCategoryInformationForm = _modalManager.getModal().find('form[name=ServiceCategoryInformationsForm]');
        _$serviceCategoryInformationForm.validate();
    };

    this.save = function () {
        if (!_$serviceCategoryInformationForm.valid()) {
        return;
      }

        var serviceCategory = _$serviceCategoryInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _serviceCategoriesAppService
            .createOrEdit(serviceCategory)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
            abp.event.trigger('app.createOrEditServiceCategoryModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
