(function ($) {
  app.modals.CreateOrEditPartnerTypeModal = function () {
      var _partnerTypesService = abp.services.app.partnerTypes;
      debugger 
      //$('#masterCategoryId').select2({
      //    width: '750px',
      //    //placeholder: 'Select a Master Category',
      //    //allowClear: true
      //    // Adjust the width as needed
      //});
      $('#masterCategoryId').select2({
          width: '755px', 
          placeholder: 'Select a Master Category',
          allowClear: true,
          minimumResultsForSearch: 10,   
      });
     
    var _modalManager;
    var _$partnerTypeInformationForm = null;

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

      _$partnerTypeInformationForm = _modalManager.getModal().find('form[name=PartnerTypeInformationsForm]');
      _$partnerTypeInformationForm.validate();
    };

    $('#OpenMasterCategoryLookupTableButton').click(function () {
      var partnerType = _$partnerTypeInformationForm.serializeFormToObject();

      _PartnerTypemasterCategoryLookupTableModal.open(
        { id: partnerType.masterCategoryId, displayName: partnerType.masterCategoryName },
        function (data) {
          _$partnerTypeInformationForm.find('input[name=masterCategoryName]').val(data.displayName);
          _$partnerTypeInformationForm.find('input[name=masterCategoryId]').val(data.id);
        }
      );
    });

    $('#ClearMasterCategoryNameButton').click(function () {
      _$partnerTypeInformationForm.find('input[name=masterCategoryName]').val('');
      _$partnerTypeInformationForm.find('input[name=masterCategoryId]').val('');
    });

    this.save = function () {
      if (!_$partnerTypeInformationForm.valid()) {
        return;
      }
      if ($('#PartnerType_MasterCategoryId').prop('required') && $('#PartnerType_MasterCategoryId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('MasterCategory')));
        return;
      }

      var partnerType = _$partnerTypeInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _partnerTypesService
        .createOrEdit(partnerType)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditPartnerTypeModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
