(function ($) {
    app.modals.CreateOrEditAcadamicRequirementModal = function () {
       
        $('#degreeLevelId').select2({

            width: '100%',
            // Adjust the width as needed
        });
       
        var hiddenfield = $("#ProductId").val();

        $("#productId").val(hiddenfield);
        var _productAcadamicRequirements = abp.services.app.productAcadamicRequirements;

        var _modalManager;
        var _$othersInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$othersInformationForm = _modalManager.getModal().find('form[name=ProductRequirementForm]');
            _$othersInformationForm.validate();
        };





        this.save = function () {
            if (!_$othersInformationForm.valid()) {
                return;
            }

            debugger
            var Subject = _$othersInformationForm.serializeFormToObject();
            _modalManager.setBusy(true);
            _productAcadamicRequirements
                .createOrEdit(Subject)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditAcadamicRequirementModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
