(function ($) {
  app.modals.CreateOrEditSubjectModal = function () {
    var _subjectsService = abp.services.app.Subjects;

    var _modalManager;
      var _$SubjectInformationForm = null;

    var _PartnerTypemasterCategoryLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/PartnerTypes/MasterCategoryLookupTableModal',
      scriptUrl:
        abp.appPath +
        'view-resources/Areas/AppAreaName/Views/PartnerTypes/_PartnerTypeMasterCategoryLookupTableModal.js',
      modalClass: 'MasterCategoryLookupTableModal',
    });

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

    $('#OpenMasterCategoryLookupTableButton').click(function () {
      var Subject = _$SubjectInformationForm.serializeFormToObject();

      _PartnerTypemasterCategoryLookupTableModal.open(
        { id: partnerType.masterCategoryId, displayName: partnerType.masterCategoryName },
        function (data) {
            _$SubjectInformationForm.find('input[name=masterCategoryName]').val(data.displayName);
            _$SubjectInformationForm.find('input[name=masterCategoryId]').val(data.id);
        }
      );
    });

    $('#ClearMasterCategoryNameButton').click(function () {
      _$SubjectInformationForm.find('input[name=masterCategoryName]').val('');
      _$SubjectInformationForm.find('input[name=masterCategoryId]').val('');
    });

    this.save = function () {
        if (!_$SubjectInformationForm.valid()) {
        return;
      }
      if ($('#PartnerType_MasterCategoryId').prop('required') && $('#PartnerType_MasterCategoryId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('MasterCategory')));
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
