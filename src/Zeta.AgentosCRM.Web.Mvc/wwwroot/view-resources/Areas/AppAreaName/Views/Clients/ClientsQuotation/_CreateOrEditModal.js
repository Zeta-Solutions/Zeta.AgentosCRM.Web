(function ($) {
    app.modals.CreateOrEditClientQuotationModal = function () {
    var _subjectsService = abp.services.app.subjects;

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

        _$SubjectInformationForm = _modalManager.getModal().find('form[name=QuotationInformationsForm]');
        _$SubjectInformationForm.validate();
    };
        $('#updatequotationBtn').click(function () {
            debugger
            var updatedData = {};  // Variable to store the updated data as an object

           
                // Serialize the form data as an array
                var formDataArray = $('form[name=QuotationDetailInformationsForm]').serializeArray();

                // Convert the array to an object with select names and selected text included
                formDataArray.forEach(function (item) {
                    var element = $('[name=' + item.name + ']');
                    var selectedText = element.find('option:selected').text();

                    // Add the data to the object
                    updatedData[item.name] = item.value;
                    updatedData[item.name + 'Text'] = selectedText;
                });

                // Optionally, you can log the data to the console for verification
                console.log('Updated Data:', updatedData);

                // You can perform other actions or manipulations with the updatedData object here
          

            // You can perform other actions or manipulations with the updatedData variable here
        });

 

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
            abp.event.trigger('app.createOrEditQuotationModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
