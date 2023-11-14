(function ($) {
    app.modals.CreateOrEditBranchesModal = function () {
        debugger
        $('#countryId').select2({
            width: '350px',
            // Adjust the width as needed
        });

      var _branchesService = abp.services.app.branches;
        var hiddenfield = $("#PartnerId").val();

        $("#partnerId").val(hiddenfield);

       
    var _modalManager;
        var _$branchInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$branchInformationForm = _modalManager.getModal().find('form[name=BranchesTabInformationsForm]');
        _$branchInformationForm.validate();
    };

    this.save = function () {
        if (!_$branchInformationForm.valid()) {
        return;
      }

        var branches = _$branchInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _branchesService
            .createOrEdit(branches)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
            abp.event.trigger('app.createOrEditBranchModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
