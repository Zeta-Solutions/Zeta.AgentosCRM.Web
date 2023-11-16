(function ($) {

    app.modals.CreateOrEditPromotionsModal = function () {
        debugger
        $('input[name="ApplyTo"]').change(function () {
            if ($(this).val() === "false") {
                document.getElementById("field1").style.display = 'block';

                //$("#field1").show();
            } else  {
                // Hide the label and field for option1
                //$("#field1 label, #field1 input").hide();
                document.getElementById("field1").style.display = 'none';
                // Show the label and field for option2
                // $("#field2 label, #field2 input").show();
            }
        });
        $("#EditUser_IsActive").prop("checked", true);
        document.getElementById("field1").style.display = 'none';
        var hiddenfield = $("#PartnerId").val();

        $("#partnerId").val(hiddenfield);

        var _partnerPromotionsService = abp.services.app.partnerPromotions;

        var _modalManager;
        var _$partnerPromotionsInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$partnerPromotionsInformationForm = _modalManager.getModal().find('form[name=PromotionsInformationsForm]');
            console.log(_$partnerPromotionsInformationForm);
            _$partnerPromotionsInformationForm.validate();
        };

        this.save = function () {
            if (!_$partnerPromotionsInformationForm.valid()) {
                return;
            }

            var leadSources = _$partnerPromotionsInformationForm.serializeFormToObject();
            debugger
            _modalManager.setBusy(true);
            _partnerPromotionsService
                .createOrEdit(leadSources)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditPromotionModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
