(function ($) {
    app.modals.CreateOrEditcheckInLogsModal = function () {
        var _clientInterstedServices = abp.services.app.checkInLogs;
        var AssigneeId = $("#AssigneeId").val();
        var hiddenfield = $("#ID").val();
        $("#CheckInAssigneeId").val(AssigneeId);
        var _modalManager;
        var _$clientTagsInformationForm = null;

        $('input[name*="clientId"]').val(hiddenfield)
        var _modalManager;
        var _$_clientInterstedInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$_clientInterstedInformationForm = _modalManager.getModal().find('form[name=CheckIngLogsInformationsForm]');
            _$_clientInterstedInformationForm.validate();
        };





        this.save = function () {
            if (!_$_clientInterstedInformationForm.valid()) {
                return;
            }


            var InterstedServices = _$_clientInterstedInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _clientInterstedServices
                .createOrEdit(InterstedServices)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app._createOrEditModal');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
