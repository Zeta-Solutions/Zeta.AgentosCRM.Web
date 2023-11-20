(function ($) {
    app.modals.CreateOrEditTaskModal = function () {
        $('#taskCategoryId').select2({
            width: '350px',
            // Adjust the width as needed
        });
        $('#assigneeId').select2({
            width: '350px',
            // Adjust the width as needed
        });
        $('#taskPriorityId').select2({
            width: '350px',
            // Adjust the width as needed
        });
        $('#followerId').select2({
            width: '750px',
            // Adjust the width as needed
        });
        var hiddenfield = $("#ClientId").val();

        $("#ClientId").val(hiddenfield);
      var _cRMTasksService = abp.services.app.cRMTasks;

    var _modalManager;
      var _$tasksInformationForm = null;



    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$tasksInformationForm = _modalManager.getModal().find('form[name=TaskInformationsForm]');
        _$tasksInformationForm.validate();
    };

    

 

    this.save = function () {
        if (!_$tasksInformationForm.valid()) {
        return;
      }


        var Subject = _$tasksInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _cRMTasksService
          .createOrEdit(Subject)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditTaskModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
