(function ($) {
    app.modals.CreateOrEditMasterCategoryModal = function () {
        var _masterCategoriesService = abp.services.app.masterCategories;

        var _modalManager;
        var _$CategoryInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$CategoryInformationForm = _modalManager.getModal().find('form[name=CategoryInformationsForm]');
            _$CategoryInformationForm.validate();
        };

        this.save = function () {
            if (!_$CategoryInformationForm.valid()) {
                return;
            }

            var Category = _$CategoryInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _masterCategoriesService
                .createOrEdit(Category)
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
