(function ($) {
  app.modals.CreateOrEditSubjectModal = function () {
    var _subjectsService = abp.services.app.subjects;
      $('#SubjectAreaId').select2({
          width: '755px',
          placeholder: 'Select A Subject Area',
          allowClear: true,
          minimumResultsForSearch: 10,
      });
    var _modalManager;
      var _$SubjectInformationForm = null;



    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$SubjectInformationForm = _modalManager.getModal().find('form[name=InformationsForm]');
        _$SubjectInformationForm.validate();
    };

    

 

    this.save = function () {
        if (!_$SubjectInformationForm.valid()) {
        return;
      }


        var Subject = _$SubjectInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _subjectsService
          .createOrEdit(Subject)
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
