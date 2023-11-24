(function ($) {
    app.modals.CreateOrEditEnglishTestScoreModal = function () {
        var _englisTestScoresService = abp.services.app.englisTestScores;

        var ClientName = $("#clientAppID").val();
        $("#Name").val(ClientName);
        var hiddenfield = $("#ID").val();
        $("#ClientId").val(hiddenfield);
        var _modalManager;
        var _$clientTagsInformationForm = null;

        $('input[name*="clientId"]').val(hiddenfield)

        var _modalManager;
        var _$englisTestScoresInformationsForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$englisTestScoresInformationsForm = _modalManager.getModal().find('form[name=EnglishTestScoreInformationsForm]');
            _$englisTestScoresInformationsForm.validate();
        };





        this.save = function () {
            if (!_$englisTestScoresInformationsForm.valid()) {
                return;
            }


            var englisTestScores = _$englisTestScoresInformationsForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _englisTestScoresService
                .createOrEdit(englisTestScores)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditSubjectModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
