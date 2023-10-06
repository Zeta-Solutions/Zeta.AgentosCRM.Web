(function ($) {
    app.modals.CreateOrEditTaskPriorityModal = function () {
        var _taskPrioritiesService = abp.services.app.taskPriorities;

        var _modalManager;
        var _$PriorityInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$PriorityInformationForm = _modalManager.getModal().find('form[name=TasknformationsForm]');
            _$PriorityInformationForm.validate();
        };

        this.save = function () {
            if (!_$PriorityInformationForm.valid()) {
                return;
            }

            var taskPriority = _$PriorityInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _taskPrioritiesService
                .createOrEdit(taskPriority)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.CreateOrEditTaskPriorityModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
