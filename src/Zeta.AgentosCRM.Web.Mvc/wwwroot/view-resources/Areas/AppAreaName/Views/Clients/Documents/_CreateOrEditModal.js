
(function ($) {
    app.modals.CreateOrEditDocumentsModal = function () {
        debugger
        var _clientAttachmentsService = abp.services.app.clientAttachments;
        var _modalManager;
        var _$clientDocumentsInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$clientDocumentsInformationForm = _modalManager.getModal().find('form[name=ClientDocumentForm]');
            _$clientDocumentsInformationForm.validate();
        };
        //Appearance/Logo
        var attachmentnames;
        $('#ClientDocumentForm').ajaxForm({
            beforeSubmit: function (formData, jqForm, options) {
                var $fileInput = $('#ClientDocumentForm input[name=ApplicationLogoImage]');
                var files = $fileInput.get()[0].files;

                if (!files.length) {
                    return false;
                }
                debugger
                attachmentnames = files[0].name;
                var file = files[0];
                debugger
                //File type check
                var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                if ('|jpg|jpeg|png|gif|doc|pdf|docx|txt|text|plain|vnd.openxmlformats-officedocument.wordprocessingml.document|'.indexOf(type) === -1) {
                    abp.message.warn(app.localize('File_Invalid_Type_Error'));
                    return false;
                }


                //File size check...
                if (file.size > 5242880) {
                    //30KB
                    abp.message.warn(app.localize('File_SizeLimit_Error'));
                    return false;
                }

                return true;
            },
            success: function (response) {
                debugger
                //if (response.success) {
                //    refreshLogo(
                //        abp.appPath +
                //        'TenantCustomization/GetTenantLogo?skin=light&tenantId=' +
                //        response.result.tenantId +
                //        '&t=' +
                //        new Date().getTime(),
                //        'light'
                //    );
                //    abp.notify.info(app.localize('SavedSuccessfully'));
                //} else {
                //    abp.message.error(response.error.message);
                //}
                var clientId = $('input[name="Clientid"]').val();
                var attachmentId = response.result.id
                var attachmentIdToken = response.result.id
                var name = attachmentnames;
                inputData = {
                    clientId: clientId,
                    name: name,
                    /* attachmentIdToken: attachmentIdToken,*/
                    attachmentId: attachmentId
                }
                var Steps = JSON.stringify(inputData);
                Steps = JSON.parse(Steps);
                _clientAttachmentsService
                    .createOrEdit(Steps)
                    .done(function () {
                        //debugger..
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        _modalManager.close();
                        abp.event.trigger('app.createOrEditClientDocumentsModalSaved');
                    })
            },
        });

    };
})(jQuery);
