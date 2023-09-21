(function ($) {
  app.modals.CreateOrEditWorkflowStepModal = function () {
    var _workflowStepsService = abp.services.app.workflowSteps;

    var _modalManager;
    var _$workflowStepInformationForm = null;

    var _WorkflowStepworkflowLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/WorkflowSteps/WorkflowLookupTableModal',
      scriptUrl:
        abp.appPath + 'view-resources/Areas/AppAreaName/Views/WorkflowSteps/_WorkflowStepWorkflowLookupTableModal.js',
      modalClass: 'WorkflowLookupTableModal',
    });

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

    $('#OpenWorkflowLookupTableButton').click(function () {
      var workflowStep = _$workflowStepInformationForm.serializeFormToObject();

      _WorkflowStepworkflowLookupTableModal.open(
        { id: workflowStep.workflowId, displayName: workflowStep.workflowName },
        function (data) {
          _$workflowStepInformationForm.find('input[name=workflowName]').val(data.displayName);
          _$workflowStepInformationForm.find('input[name=workflowId]').val(data.id);
        }
      );
    });

    $('#ClearWorkflowNameButton').click(function () {
      _$workflowStepInformationForm.find('input[name=workflowName]').val('');
      _$workflowStepInformationForm.find('input[name=workflowId]').val('');
    });

    this.save = function () {
      if (!_$workflowStepInformationForm.valid()) {
        return;
      }
      if ($('#WorkflowStep_WorkflowId').prop('required') && $('#WorkflowStep_WorkflowId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('Workflow')));
        return;
      }

      var workflowStep = _$workflowStepInformationForm.serializeFormToObject();

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
