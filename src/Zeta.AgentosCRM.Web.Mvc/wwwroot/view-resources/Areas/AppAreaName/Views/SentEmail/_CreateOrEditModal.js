(function ($) {
    app.modals.CreateOrEditSentEmailModal = function () {
        var _sentemailsService = abp.services.app.sentEmails;
        var demoUiComponentsService = abp.services.app.demoUiComponents;
        var EmailBody;
        var quill = new Quill('#kt_docs_quill_basic12', {
            modules: {
                toolbar: [
                    [{
                        header: [1, 2, false]
                    }],
                    //['bold', 'italic', 'underline'],
                    //['image', 'code-block']
                    ['bold', 'italic', 'underline'],
                    ['code-block']
                ]
            },
            placeholder: 'Type your text here...',
            theme: 'snow' // or 'bubble'
        });
        $('#EmailTemplateId').select2({
            width: '100%',

        });
        $('#EmailConfigurationId').select2({
            width: '100%',

        });
        //$('#ToEmails').select2({
        //    width: '100%',
        //    multiple: true,
        //});
        var GetEmail = $("#GetEmail").val();
        $("input[name='ToEmail']").val(GetEmail);
        $(document).on("change", "#EmailConfigurationId", function () {
            var EmailConfiguration = $(this).val();
            if (EmailConfiguration > 0) {
                $.ajax({
                    url: abp.appPath + 'api/services/app/EmailConfigurations/GetEmailConfigurationForView?id=' + EmailConfiguration,
                    method: 'GET',
                    dataType: 'json',
                    //data: {
                    //    PartnerIdFilter: dynamicValue,
                    //},
                    success: function (data) {
                        debugger
                        port = data.result.emailConfiguration.smtpPort
                        Host = data.result.emailConfiguration.smtpServer
                        username = data.result.emailConfiguration.name
                        password = data.result.emailConfiguration.senderPassword
                        EnableSsl = data.result.emailConfiguration.isEnableSsl
                        senderEmail = data.result.emailConfiguration.senderEmail
                        $("#Host").val(Host)
                        $("#Port").val(port)
                        $("#username").val(username)
                        $("#password").val(password)
                        $("#EnableSsl").val(EnableSsl)
                        $("#senderEmail").val(senderEmail)
                        $("#FromEmail").val(senderEmail)
                        $("input[name='Title']").val(username);
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
            }


        });
        //$.ajax({
        //    url: abp.appPath + 'api/services/app/Clients/GetAll',
        //    method: 'GET',
        //    dataType: 'json',
        //    //data: {
        //    //    PartnerIdFilter: dynamicValue,
        //    //},
        //    success: function (data) {

        //        // Populate the dropdown with the fetched data
        //        populateDropdown(data);
        //    },
        //    error: function (error) {
        //        console.error('Error fetching data:', error);
        //    }
        //});
        //function populateDropdown(data) {
        //    debugger
        //    var dropdown = $('#ToEmails');

        //    dropdown.empty();

        //    $.each(data.result.items, function (index, item) {
        //        if (item && item.client && item.client.id !== null && item.client.id !== undefined) {
        //            dropdown.append($('<option></option>').attr('value', item.client.id).attr('data-email', item.client.email).attr('data-id', item.client.id).text(item.client.firstName + item.client.lastName));
        //        } else {
        //            console.warn('Invalid item:', item);
        //        }
        //    });
        //}
        //$(document).on("change", "#ToEmails", function () {
        //var selectedValues = $("#ToEmails :selected").map(function (i, el) {
        //    debugger
        //    return $(el).attr('data-email');
        //}).get().join(",");
        //    $("#ToEmail").val(selectedValues);
        //});
        $(document).on("change", "#EmailTemplateId", function () {
            var EmailTemplateId = $(this).val();
            if (EmailTemplateId > 0) {
                $.ajax({
                    url: abp.appPath + 'api/services/app/EmailTemplates/GetEmailTemplateForView?id=' + EmailTemplateId,
                    method: 'GET',
                    dataType: 'json',
                    //data: {
                    //    PartnerIdFilter: dynamicValue,
                    //},
                    success: function (data) {
                        debugger
                        EmailBody =data.result.emailTemplate.emailBody
                        quill.root.innerHTML = EmailBody;
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
            }
            

        });
        var _modalManager;
        var _$sentemailsInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$sentemailsInformationForm = _modalManager.getModal().find('form[name=SentEmailInformationsForm]');
            _$sentemailsInformationForm.validate();
        };
        $(document).off("click", "#saveSentEmailBtn").on("click", "#saveSentEmailBtn", function (e) {
            debugger
            $("#saveSentEmailBtn").prop('disabled', true);
            //this.save = function () {
                if (!_$sentemailsInformationForm.valid()) {
                    return;
                }


                demoUiComponentsService.sendAndGetValue(quill.root.innerHTML).done(function (data) {
                    $("input[name='EmailBody']").val(data.output);
                    abp.libs.sweetAlert.config.info.html = true;
                    var args = {

                        Host: $("#Host").val(),
                        Port: $("#Port").val(),
                        username: $("input[name='Title']").val(),
                        password: $("#password").val(),
                        EnableSsl: $("#EnableSsl").val(),
                        mailaddressto: $("input[name='ToEmail']").val(),
                        mailaddressfrom: $("#senderEmail").val(),
                        Subject: $("#Subject").val(),
                        Body: $("input[name='EmailBody']").val(),
                        CCEmail: $("#CCEmail").val(),
                    };
                    debugger

                    var Steps = JSON.stringify(args);
                    Steps = JSON.parse(Steps);
                    _sentemailsService
                        .sentEmail(Steps)
                        .done(function (data) {
                            debugger
                            if (data = true) {
                                var sentemail = _$sentemailsInformationForm.serializeFormToObject();

                                _modalManager.setBusy(true);
                                _sentemailsService
                                    .createOrEdit(sentemail)
                                    .done(function () {
                                        abp.notify.info(app.localize('SavedSuccessfully'));
                                        _modalManager.close();
                                        abp.event.trigger('app.createOrEditSentEmailModalSaved');
                                    })
                                    .always(function () {
                                        _modalManager.setBusy(false);
                                    });
                            }

                        })



                });
            //};

        });
        $(document).off("click", "#closeSentEmailBtn").on("click", "#closeSentEmailBtn", function (e) {
            _modalManager.close();
        });
        
    };
})(jQuery);
