(function ($) {
    app.modals.CreateOrEditContactsModal = function () {
        $('#branchId').select2({
            width: '350px',
            // Adjust the width as needed
        });
        var hiddenfield = $("#PartnerId").val();

        $("#partnerId").val(hiddenfield);

      var _partnerContactsService = abp.services.app.partnerContacts;

    var _modalManager;
    var _$partnerContactsInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$partnerContactsInformationForm = _modalManager.getModal().find('form[name=ContactsInformationsForm]');
        console.log(_$partnerContactsInformationForm);
        _$partnerContactsInformationForm.validate();
    };

    this.save = function () {
        if (!_$partnerContactsInformationForm.valid()) {
        return;
      }

        var leadSources = _$partnerContactsInformationForm.serializeFormToObject();
        debugger
      _modalManager.setBusy(true);
        _partnerContactsService
            .createOrEdit(leadSources)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
            abp.event.trigger('app.createOrEditContactModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
