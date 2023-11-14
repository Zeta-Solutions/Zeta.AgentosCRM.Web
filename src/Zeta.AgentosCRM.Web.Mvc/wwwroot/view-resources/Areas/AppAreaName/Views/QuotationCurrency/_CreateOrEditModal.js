(function ($) {
    app.modals.CreateOrEditQuotationCurrencyModal = function () {
        var _QuotationCurrenciesService = abp.services.app.cRMCurrencies;

        var _modalManager;
        var _$quotationCurrencyInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$quotationCurrencyInformationForm = _modalManager.getModal().find('form[name=CRMCurrencyInformationsForm]');
            _$quotationCurrencyInformationForm.validate();
        };

        this.save = function () {
            if (!_$quotationCurrencyInformationForm.valid()) {
                return;
            }

            var masterCategory = _$quotationCurrencyInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _QuotationCurrenciesService
                .createOrEdit(masterCategory)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.CreateOrEditQuotationCurrencyModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
