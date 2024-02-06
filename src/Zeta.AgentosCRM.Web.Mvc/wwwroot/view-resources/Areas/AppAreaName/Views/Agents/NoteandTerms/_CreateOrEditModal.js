(function ($) {
    app.modals.CreateOrEditNotesAndTermsModal = function () {
         


        var _notesService = abp.services.app.notes;
        var hiddenfield = $("#AgentId").val();

        $("input[name='agentId']").val(hiddenfield);


        var _modalManager;
        var _$noteInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$noteInformationForm = _modalManager.getModal().find('form[name=NotesAndTermsInformationsForm]');
            _$noteInformationForm.validate();
        };

        this.save = function () {
            if (!_$noteInformationForm.valid()) {
                return;
            }

            var branches = _$noteInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _notesService
                .createOrEdit(branches)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditNoteModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);

