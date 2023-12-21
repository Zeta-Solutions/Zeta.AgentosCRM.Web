(function ($) {
    app.modals.CreateOrEditAgreementsModal = function () {
        debugger
        $('#regionId').select2({
            width: '100%',
            dropdownParent: $('#regionId').parent(),
            // Adjust the width as needed
        });
        $('#agentId').select2({
            width: '100%',
            dropdownParent: $('#agentId').parent(),
            // Adjust the width as needed
        });

        var _partnerContractsService = abp.services.app.partnerContracts;
        var hiddenfield = $("#PartnerId").val();
        //var hiddenfield = $('[name="partnerid"]').val();
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
        $(document).on('select2:open', function () {
            var $searchField = $('.select2-search__field');
            $searchField.on('keydown', function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        });
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
