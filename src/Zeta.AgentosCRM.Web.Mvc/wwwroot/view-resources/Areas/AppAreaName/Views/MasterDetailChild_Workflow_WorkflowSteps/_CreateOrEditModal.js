(function ($) {
  app.modals.MasterDetailChild_Workflow_CreateOrEditWorkflowStepModal = function () {
    var _workflowStepsService = abp.services.app.workflowSteps;

    var _modalManager;
    var _$workflowStepInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$workflowStepInformationForm = _modalManager.getModal().find('form[name=WorkflowStepInformationsForm]');
      _$workflowStepInformationForm.validate();
    };

    this.save = function () {
      if (!_$workflowStepInformationForm.valid()) {
        return;
      }

      var workflowStep = _$workflowStepInformationForm.serializeFormToObject();

      workflowStep.workflowId = $('#MasterDetailChild_Workflow_WorkflowStepsId').val();

      _modalManager.setBusy(true);
      _workflowStepsService
        .createOrEdit(workflowStep)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditWorkflowStepModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
