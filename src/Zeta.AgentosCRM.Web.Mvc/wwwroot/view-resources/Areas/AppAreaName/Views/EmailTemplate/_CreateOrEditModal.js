(function ($) {
  app.modals.CreateOrEditEmailTemplateModal = function () {
      var _emailTemplatesService = abp.services.app.emailTemplates;
      var demoUiComponentsService = abp.services.app.demoUiComponents;
      var quill = new Quill('#kt_docs_quill_basic1', {
          modules: {
              toolbar: [
                  [{
                      header: [1, 2, false]
                  }],
                  //['bold', 'italic', 'underline'],
                  //['image', 'code-block']
                  ['bold', 'italic', 'underline'],
                  [ 'code-block']
              ]
          },
          placeholder: 'Type your text here...',
          theme: 'snow' // or 'bubble'
      });
      //$('#kt_docs_quill_basic1').change(function () {
      //    demoUiComponentsService.sendAndGetValue(quill.root.innerHTML).done(function (data) {
      //         
      //        $("input[name='EmailBody']").val(data.output);
      //          abp.libs.sweetAlert.config.info.html = true;
      //          abp.message.info(data.output, app.localize('PostedValue'), { isHtml: true });
      //          abp.notify.info(app.localize('SavedSuccessfully'));
      //      });
      //  });
      //quill.on('text-change', function (delta, oldDelta, source) {
      //    // Your existing code here...
      //    demoUiComponentsService.sendAndGetValue(quill.root.innerHTML).done(function (data) {
      //         ;
      //        $("input[name='EmailBody']").val(data.output);
      //        abp.libs.sweetAlert.config.info.html = true;
      //        abp.message.info(data.output, app.localize('PostedValue'), { isHtml: true });
      //        abp.notify.info(app.localize('SavedSuccessfully'));
      //    });
      var enterKeyPressed = false;
      $(document).keyup(function (e) {
          if (e.which === 13) {
               
              // Enter key is pressed
              // Call your function here
              e.preventDefault();
              e.stopPropagation();
              var enterKeyPressed = true;
          }
      });
    var _modalManager;
    var _$emailTemplatesInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$emailTemplatesInformationForm = _modalManager.getModal().find('form[name=EmailTemplateInformationsForm]');
        _$emailTemplatesInformationForm.validate();
    };
      
      $(document).off("click", "#saveEmailSetupBtn").on("click", "#saveEmailSetupBtn", function (e) {
           

          //this.save = function () {

              if (!_$emailTemplatesInformationForm.valid()) {
                  return;
              }

              demoUiComponentsService.sendAndGetValue(quill.root.innerHTML).done(function (data) {
                   ;
                  $("input[name='EmailBody']").val(data.output);
                  abp.libs.sweetAlert.config.info.html = true;

                  var feeType = _$emailTemplatesInformationForm.serializeFormToObject();
                   
                  _modalManager.setBusy(true);
                  _emailTemplatesService
                      .createOrEdit(feeType)
                      .done(function () {
                          abp.notify.info(app.localize('SavedSuccessfully'));
                          _modalManager.close();
                          abp.event.trigger('app.createOrEditEmailTemplateModalSaved');
                      })
                      .always(function () {
                          _modalManager.setBusy(false);
                      });
                  //abp.notify.info(app.localize('SavedSuccessfully'));
              });



         // };

      });
      $(document).off("click", "#closeEmailSetupBtn").on("click", "#closeEmailSetupBtn", function (e) {
          _modalManager.close();
      });
     
  };
})(jQuery);
