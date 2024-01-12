(function ($) {
    app.modals.CreateOrEditEmailConfigurationModal = function () {
        var _emailConfigurationsService = abp.services.app.emailConfigurations;
      
        var _modalManager;
        var _$emailConfigurationsInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$emailConfigurationsInformationForm = _modalManager.getModal().find('form[name=EmailConfigurationInformationsForm]');
            _$emailConfigurationsInformationForm.validate();
        };

        this.save = function () {
            if (!_$emailConfigurationsInformationForm.valid()) {
                return;
            }

            var emailConfiguration = _$emailConfigurationsInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _emailConfigurationsService
                .createOrEdit(emailConfiguration)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditEmailConfigurationModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };

    };
})(jQuery);
