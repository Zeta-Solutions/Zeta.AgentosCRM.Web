(function ($) {
  app.modals.CreateOrEditInterestedServicesModal = function () {
      var _clientInterstedServices = abp.services.app.clientInterstedServices;
      var ClientName = $("#clientAppID").val();
      $("#Name").val(ClientName);
      var hiddenfield = $("#ID").val();
      $("#ClientId").val(hiddenfield);
      var _modalManager;
      var _$clientTagsInformationForm = null;
      $('#WorkflowId').select2({
          width: '100%',
          placeholder: 'Select Workflow',
          allowClear: true,
          minimumResultsForSearch: Infinity,
      });
      $('#PartnerId').select2({
          width: '100%',
          placeholder: 'Select Partner',
          allowClear: true,
          minimumResultsForSearch: 10,
      });
      $('#ProductId').select2({
          width: '100%',
          placeholder: 'Select Product',
          allowClear: true,
          minimumResultsForSearch: Infinity,
      });
      $('#BranchId').select2({
          width: '100%',
          placeholder: 'Select Branch',
          allowClear: true,
          minimumResultsForSearch: Infinity,
      });
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

        _$_clientInterstedInformationForm = _modalManager.getModal().find('form[name=IntrestedServiceInformationsForm]');
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
            abp.event.trigger('app.createOrEditInterstedInformationFormModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
