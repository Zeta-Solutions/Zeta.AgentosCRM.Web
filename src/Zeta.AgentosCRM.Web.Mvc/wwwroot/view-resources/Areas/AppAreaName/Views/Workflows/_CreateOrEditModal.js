(function ($) {
  app.modals.CreateOrEditWorkflowModal = function () {
    var _workflowsService = abp.services.app.workflows;

    var _modalManager;
    var _$workflowInformationForm = null;
      var _$WorkflowInformationSetupsForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$workflowInformationForm = _modalManager.getModal().find('form[name=WorkflowInformationsForm]');
        _$workflowInformationForm.validate();
// WorkflowInformationsetupsForm Form Data 
        _$WorkflowInformationSetupsForm = _modalManager.getModal().find('form[name=WorkflowInformationSetupsForm]');
        _$WorkflowInformationSetupsForm.validate();
    };

    this.save = function () {
      if (!_$workflowInformationForm.valid()) {
        return;
      }

      var workflow = _$workflowInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _workflowsService
        .createOrEdit(workflow)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditWorkflowModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
