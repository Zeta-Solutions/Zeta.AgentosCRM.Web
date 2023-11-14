(function ($) {
    app.modals.CreateOrEditTestAattachmentModal = function () {

        var _testAattachmentsService = abp.services.app.testAattachments;

        var _modalManager;
        var _$testAattachmentInformationForm = null;

		
		        var _fileUploading = [];
		        var _attachmentToken;

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$testAattachmentInformationForm = _modalManager.getModal().find('form[name=TestAattachmentInformationsForm]');
            _$testAattachmentInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$testAattachmentInformationForm.valid()) {
                return;
            }

            
                        if (_fileUploading != null && _fileUploading.length > 0) {
                            abp.notify.info(app.localize('WaitingForFileUpload'));
                            return;
                        }
					

            var testAattachment = _$testAattachmentInformationForm.serializeFormToObject();
            
                        testAattachment.attachmentToken = _attachmentToken;
					
            
			
			 _modalManager.setBusy(true);
			 _testAattachmentsService.createOrEdit(
				testAattachment
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditTestAattachmentModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
                            $("#TestAattachment_Attachment").change(function () {
                                var file = $(this)[0].files[0];
                                if (!file) {
                                    _attachmentToken = null;
                                    return;
                                }
                    
                                var formData = new FormData();
                                formData.append('file', file);
                                _fileUploading.push(true);
                    
                                $.ajax({
                                    url: '/App/TestAattachments/UploadAttachmentFile',
                                    type: 'POST',
                                    data: formData,
                                    processData: false,
                                    contentType: false
                                }).done(function (resp) {
                                    if (resp.success && resp.result.fileToken) {
                                        _attachmentToken = resp.result.fileToken;
                                    } else {
                                        abp.message.error(resp.result.message);
                                    }
                                }).always(function () {
                                    _fileUploading.pop();
                                });
                            });
                    
                            $("#TestAattachment_Attachment_Remove").click(function () {
                                abp.message.confirm(
                                    app.localize('DoYouWantToRemoveTheFile'),
                                    app.localize('AreYouSure'),
                                    function (isConfirmed) {
                                        if (isConfirmed) {
                                            var TestAattachment = _$testAattachmentInformationForm.serializeFormToObject();
                                            _testAattachmentsService.removeAttachmentFile({
                                                id: TestAattachment.id
                                            }).done(function () {
                                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                                                _$testAattachmentInformationForm.find("#div_current_file").css("display", "none");
                                            });
                                        }
                                    }
                                );
                            });
                            
                            $('#TestAattachment_Attachment').change(function () {
                                var fileName = app.localize('ChooseAFile');
                                if (this.files && this.files[0]) {
                                    fileName = this.files[0].name;
                                }
                                $('#TestAattachment_AttachmentLabel').text(fileName);
                            });	
					
    };
})(jQuery);