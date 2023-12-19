(function ($) {
  app.modals.CreateOrEditCountriesModal = function () {
      var _countriesService = abp.services.app.countries;
      $('#regionId').select2({
          width: '100%',
          placeholder: 'Select Office',
          allowClear: true,
          minimumResultsForSearch: 10,
      });
    var _modalManager;
      var _$countriesInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$countriesInformationForm = _modalManager.getModal().find('form[name=CountryInformationsForm]');
        _$countriesInformationForm.validate();
    };

    this.save = function () {
        if (!_$countriesInformationForm.valid()) {
        return;
      }

        var countries = _$countriesInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
        _countriesService
            .createOrEdit(countries)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditCountryModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
