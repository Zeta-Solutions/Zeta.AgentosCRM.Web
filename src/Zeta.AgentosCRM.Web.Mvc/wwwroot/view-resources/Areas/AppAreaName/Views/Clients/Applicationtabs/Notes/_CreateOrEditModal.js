(function ($) {
    app.modals.CreateOrEditNotesAndTermsModal = function () {
        debugger

        var _notesService = abp.services.app.notes;
        

        $(document).off("click", "#saveNoteBtn").on("click", "#saveNoteBtn", function (e) {
            debugger

            var hiddenfield = $('input[name="Clientid"]').val();
            var hiddenapplicationfield = $("#ApplicationId").val()
                var srno = $('.nav-link.default.active .num').text().trim();
                var workflowStepId = $("#StageID-" + srno).val();

            $("#clientId").val(hiddenfield);
            $("#applicationId").val(hiddenapplicationfield);
            $("#ApplicationStageId").val(workflowStepId);
                

                var branches = _$noteInformationForm.serializeFormToObject();

                _modalManager.setBusy(true);
                _notesService
                    .createOrEdit(branches)
                    .done(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        _modalManager.close();
                        $('#cardContainerapplicationnotes').remove();
                        //abp.event.trigger('app.createOrEditNoteModalSaved');
                        location.reload();
                       
                    })
                    .always(function () {
                        _modalManager.setBusy(false);
                    });
           
        });
        $(document).off("click", "#closeNoteBtn").on("click", "#closeNoteBtn", function (e) {
            _modalManager.close();
        });
        
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

            _$noteInformationForm = _modalManager.getModal().find('form[name=NotesAndTermsApplicationInformationsForm]');
            _$noteInformationForm.validate();
        };

        //this.save = function () {
        //    if (!_$noteInformationForm.valid()) {
        //        return;
        //    }

        //    var branches = _$noteInformationForm.serializeFormToObject();

        //    _modalManager.setBusy(true);
        //    _notesService
        //        .createOrEdit(branches)
        //        .done(function () {
        //            abp.notify.info(app.localize('SavedSuccessfully'));
        //            _modalManager.close();
        //            abp.event.trigger('app.createOrEditNoteModalSaved');
        //        })
        //        .always(function () {
        //            _modalManager.setBusy(false);
        //        });
        //};
       
    };
})(jQuery);
