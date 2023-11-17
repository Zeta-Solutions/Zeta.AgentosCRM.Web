(function ($) {
  app.modals.CreateOrEditEducationModal = function () {
      var _clientEducationsService = abp.services.app.clientEducations;

     
      var hiddenfield = $("#ID").val();
      $("#ClientId").val(hiddenfield);
      var _modalManager;
      var _$clientTagsInformationForm = null;

      $('input[name*="clientId"]').val(hiddenfield)

    var _modalManager;
      var _$EducationInformationsForm = null;



    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$EducationInformationsForm = _modalManager.getModal().find('form[name=EducationInformationsForm]');
        _$EducationInformationsForm.validate();
    };

    

 

    this.save = function () {
        if (!_$EducationInformationsForm.valid()) {
        return;
      }


        var ClientEducation= _$EducationInformationsForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _clientEducationsService
            .createOrEdit(ClientEducation)
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
