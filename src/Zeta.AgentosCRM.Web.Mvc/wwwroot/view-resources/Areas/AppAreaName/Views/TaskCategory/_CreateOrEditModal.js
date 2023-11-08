﻿(function ($) {
    app.modals.CreateOrEditTaskCategoryModal = function () {
        var _taskCategoriesService = abp.services.app.taskCategories;

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
            _taskCategoriesService
                .createOrEdit(Category)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditTaskCategoryModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
