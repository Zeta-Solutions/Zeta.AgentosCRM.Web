(function ($) {
    app.modals.CreateOrEditApplicationModal = function () {
        var _applicationsService = abp.services.app.applications;
        var ClientName = $("#clientAppID").val();
        $("#Name").val(ClientName);
        var hiddenfield = $("#ID").val();
        $("#ClientId").val(hiddenfield);
        var _modalManager;
        var _$clientTagsInformationForm = null;

        $('input[name*="clientId"]').val(hiddenfield)
        var _modalManager;
        var _$applicationsInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$applicationsInformationForm = _modalManager.getModal().find('form[name=ApplicationsInformationsForm]');
            _$applicationsInformationForm.validate();
        };





        this.save = function () {
            if (!_$applicationsInformationForm.valid()) {
                return;
            }


            var application = _$applicationsInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _applicationsService
                .createOrEdit(application)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditApplicationsModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
