(function ($) {
    app.modals.CreateOrEditSubjectAreaModal = function () {
        var _subjectAreasService = abp.services.app.subjectAreas;

        var _modalManager;
        var _$subjectAreaInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$subjectAreaInformationForm = _modalManager.getModal().find('form[name=InformationsForm]');
            _$subjectAreaInformationForm.validate();
        };

        this.save = function () {
            if (!_$subjectAreaInformationForm.valid()) {
                return;
            }

            var subjectArea = _$subjectAreaInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _subjectAreasService
                .createOrEdit(subjectArea)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditSubjectAreaModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
