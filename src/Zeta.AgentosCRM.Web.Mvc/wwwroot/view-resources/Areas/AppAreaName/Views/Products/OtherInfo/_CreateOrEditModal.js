(function ($) {
    app.modals.CreateOrEditotherinfoModal = function () {
        $('#degreeLevelId').select2({

            width: '100%',
            dropdownParent: $('#degreeLevelId').parent(),
            // Adjust the width as needed
        });
        $('#subjectId').select2({
            width: '100%',
            dropdownParent: $('#subjectId').parent(),
            // Adjust the width as needed
        });
        $('#subjectAreaId').select2({
            width: '100%',
            dropdownParent: $('#subjectAreaId').parent(),
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

         
        $(document).on('select2:open', function () {
            var $searchField = $('.select2-search__field');
            $searchField.on('keydown', function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        });


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
