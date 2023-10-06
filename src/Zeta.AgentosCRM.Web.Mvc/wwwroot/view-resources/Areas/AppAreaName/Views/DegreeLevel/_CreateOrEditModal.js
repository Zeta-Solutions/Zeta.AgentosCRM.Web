(function ($) {
    app.modals.CreateOrEditDegreeLevelModal = function () {
        var _degreeLevelsService = abp.services.app.degreeLevels;

        var _modalManager;
        var _$degreeInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$degreeInformationForm = _modalManager.getModal().find('form[name=InformationsForm]');
            _$degreeInformationForm.validate();
        };

        this.save = function () {
            if (!_$degreeInformationForm.valid()) {
                return;
            }

            var DegreeLevel = _$degreeInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _degreeLevelsService
                .createOrEdit(DegreeLevel)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditDegeeLevelModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
