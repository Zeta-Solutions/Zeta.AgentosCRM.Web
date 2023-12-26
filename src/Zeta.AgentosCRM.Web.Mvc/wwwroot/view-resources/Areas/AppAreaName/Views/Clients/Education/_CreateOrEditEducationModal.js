(function ($) {
    app.modals.CreateOrEditEducationModal = function () {
        var _clientEducationsService = abp.services.app.clientEducations;
       $('#DegreeLevelId').select2({
           width: '100%',
           dropdownParent: $('#DegreeLevelId').parent(),
        });
        $('#SubjectAreaId').select2({
            width: '100%',
            dropdownParent: $('#SubjectAreaId').parent(),
        });
        $('#SubjectId').select2({
            width: '100%',
            dropdownParent: $('#SubjectId').parent(),
        });
        var _modalManager;
        var _$educationForm = null;
        var hiddenfield = $('input[name="Clientid"]').val();

        $('input[name="clientId"]').val(hiddenfield)
        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$educationForm = _modalManager.getModal().find('form[name=EducationInformationsForm]');
            _$educationForm.validate();
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
            if (!_$educationForm.valid()) {
                return;
            }

            var countries = _$educationForm.serializeFormToObject();
            debugger
            _modalManager.setBusy(true);
            _clientEducationsService
                .createOrEdit(countries)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditEducationModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
