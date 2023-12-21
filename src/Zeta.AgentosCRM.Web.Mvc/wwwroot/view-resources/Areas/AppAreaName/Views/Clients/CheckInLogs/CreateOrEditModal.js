(function ($) {
    app.modals.CreateOrEditcheckInLogsModal = function () {
        var _clientInterstedServices = abp.services.app.checkInLogs; 
        $('#checkInAssigneeId').select2({
            width: '100%',
            placeholder: 'Select Assignee',
            allowClear: true,
            minimumResultsForSearch: Infinity,
        });
        function getCurrentTime() {
            const now = new Date();
            const hours = now.getHours().toString().padStart(2, '0');
            const minutes = now.getMinutes().toString().padStart(2, '0');
            return `${hours}:${minutes}`;
        }

        // Set the current time in the StartTime field
        $(document).ready(function () {
            if ($('input[name="id"]').val() < 1 || $('input[name="id"]').val() == undefined) {
                const startTimeField = $("#StartTime");
                const endTimeField = $("#EndTime");
                if (startTimeField.length || endTimeField) {
                    startTimeField.val(getCurrentTime());
                    endTimeField.val(getCurrentTime());
                }
            }
        });
        var AssigneeId = $("#AssigneeId").val();
        var hiddenfield = $('input[name="Clientid"]').val();
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
                    abp.event.trigger('app.createOrEditCheckInInformationFormModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
