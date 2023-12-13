(function ($) {
    app.modals.CreateOrEditotherinfoModal = function () {
        $('#degreeLevelId').select2({

            width: '350px',
            // Adjust the width as needed
        });
        $('#subjectId').select2({
            width: '350px',
            // Adjust the width as needed
        });
        $('#subjectAreaId').select2({
            width: '720px',
            // Adjust the width as needed
        });

        var hiddenfield = $("#ProductId").val();

        $("#productId").val(hiddenfield);
        var _productOtherInformationsService = abp.services.app.productOtherInformations;

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

            _$othersInformationForm = _modalManager.getModal().find('form[name=OtherInformationsForm]');
            _$othersInformationForm.validate();
        };





        this.save = function () {
            if (!_$othersInformationForm.valid()) {
                return;
            }
     

            var Subject = _$othersInformationForm.serializeFormToObject();
            _modalManager.setBusy(true);
            _productOtherInformationsService
                .createOrEdit(Subject)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditOtherInfoModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
