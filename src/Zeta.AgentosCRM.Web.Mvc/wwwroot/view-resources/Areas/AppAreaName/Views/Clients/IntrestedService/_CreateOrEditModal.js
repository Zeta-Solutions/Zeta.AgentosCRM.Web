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
          dropdownParent: $('#WorkflowId').parent(),
      });
      $('#PartnerId').select2({
          width: '100%',
          dropdownParent: $('#WorkflowId').parent(),
      });
      $('#ProductId').select2({
          width: '100%',
          dropdownParent: $('#WorkflowId').parent(),
      });
      $('#BranchId').select2({
          width: '100%',
          dropdownParent: $('#WorkflowId').parent(),
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
     
      $(document).on('select2:open', function () {
          var $searchField = $('.select2-search__field');
          $searchField.on('keydown', function (e) {
              if (e.which == 13) {
                  return false;
              }
          });
      });

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
