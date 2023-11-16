(function ($) {
    app.modals.CreateOrEditAgreementsModal = function () {
        debugger
        $('#regionId').select2({
            width: '350px',
            // Adjust the width as needed
        });
        $('#agentId').select2({
            width: '350px',
            // Adjust the width as needed
        });

        var _partnerContractsService = abp.services.app.partnerContracts;
        var hiddenfield = $("#PartnerId").val();

        $("#partnerId").val(hiddenfield);


        var _modalManager;
        var _$partnerContractsInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$partnerContractsInformationForm = _modalManager.getModal().find('form[name=ContractsInformationsForm]');
            _$partnerContractsInformationForm.validate();
        };

        this.save = function () {
            if (!_$partnerContractsInformationForm.valid()) {
                return;
            }

            var branches = _$partnerContractsInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _partnerContractsService
                .createOrEdit(branches)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditPartnerContractsModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
