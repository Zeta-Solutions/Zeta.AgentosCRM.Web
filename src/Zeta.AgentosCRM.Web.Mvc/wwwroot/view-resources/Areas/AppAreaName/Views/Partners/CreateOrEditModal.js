(function () {
    $(function () {
        debugger
        $("#kt_app_sidebar_toggle").trigger("click");
        debugger
        $('#masterCategoryId').select2({
            
            width: '280px', 
        });
       
        $('#partnerTypeId').select2({
            width: '280px',

        });
        $('#workflowId').select2({
            width: '280px',

        });
        $('#countryId').select2({
            width: '400px',

        });
        $('#currencyId').select2({
            width: '250px',

        });
        var partnerId = $('input[name="id"]').val();
        var imageUrl = $.ajax({
            url: abp.appPath + 'api/services/app/PartnerProfile/GetProfilePictureByPartner',
            data: {
                partnerId: partnerId,
            },
            method: 'GET',
            dataType: 'json',
        })
            .done(function (data) {
                debugger
                console.log('Response from server:', data);
                $('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
            })
            .fail(function (error) {
                console.error('Error fetching data:', error);
            });
        var input = document.querySelector("#phone");
        const errorMsg = document.querySelector("#error-msg");
        const validMsg = document.querySelector("#valid-msg");

        const errorMap = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];


        var iti = intlTelInput(input, {
            initialCountry: "auto",
            geoIpLookup: function (success, failure) {
                // Simulate a successful IP lookup to set the initial country
                var countryCode = "PK"; // Replace with the appropriate country code
                //$.get("https://ipinfo.io/", function () { }, "jsonp").always(function (resp) {
                //    var countryCode = (resp && resp.country) ? resp.country : "";
                //    success(countryCode);
                //});
                success(countryCode);
            },
            utilsScript: "https://cdn.jsdelivr.net/npm/intl-tel-input@18.1.1/build/js/utils.js"
        });

        // Manually set the phone number and selected country code (e.g., "US")
        //var savedPhoneNumber = "123-456-7890"; // Replace with your saved phone number
        var settittle = $("#PhoneCode").val();
        var selectedCountry = settittle; // Replace with the appropriate country code

        // Set the phone number value and selected country
        //input.value = savedPhoneNumber;
        iti.setCountry(selectedCountry);

        // Change the flag and title based on a condition
        var condition = true; // Change this to your condition

        // Get the element by its class name
        var flagElement = document.querySelector(".iti__selected-flag");

        if (condition) {
            // Change the title attribute
            //flagElement.setAttribute("title", "India (भारत): +91"); // Replace "New Title" and "+XX" with your desired values

            // Change the flag by adding a new class (replace iti__us with iti__pk for Pakistan)
            // flagElement.querySelector(".iti__flag").className = "iti__flag iti__pk";
        }
        const reset = () => {
            input.classList.remove("error");
            errorMsg.innerHTML = "";
            errorMsg.classList.add("hide");
            validMsg.classList.add("hide");
        };

        input.addEventListener('blur', () => {
            reset();
            if (input.value.trim()) {
                if (iti.isValidNumber()) {
                    validMsg.classList.remove("hide");
                } else {
                    input.classList.add("error");
                    const errorCode = iti.getValidationError();
                    errorMsg.innerHTML = errorMap[errorCode];
                    errorMsg.classList.remove("hide");
                }
            }
        });
        // on keyup / change flag: reset
        input.addEventListener('change', reset);
        input.addEventListener('keyup', reset);
        
    
     
        

        



        var _partnersService = abp.services.app.partners;

        var _$partnerInformationForm = $('form[name=PartnerInformationsForm]');
        _$partnerInformationForm.validate();

        var _ClientcountryLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CountryLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientCountryLookupTableModal.js',
            modalClass: 'CountryLookupTableModal'
        }); var _ClientuserLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientUserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        }); var _ClientbinaryObjectLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/BinaryObjectLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientBinaryObjectLookupTableModal.js',
            modalClass: 'BinaryObjectLookupTableModal'
        }); var _ClientdegreeLevelLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/DegreeLevelLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientDegreeLevelLookupTableModal.js',
            modalClass: 'DegreeLevelLookupTableModal'
        }); var _ClientsubjectAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/SubjectAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientSubjectAreaLookupTableModal.js',
            modalClass: 'SubjectAreaLookupTableModal'
        }); var _ClientleadSourceLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/LeadSourceLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientLeadSourceLookupTableModal.js',
            modalClass: 'LeadSourceLookupTableModal'
        });
        var changeProfilePictureModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/_ChangePictureModal.js',
            modalClass: 'ChangeProfilePictureModal',
        });

        $('#changeProfilePicture').click(function () {
            changeProfilePictureModal.open({ userId: $('input[name=Id]').val() });
        });

        changeProfilePictureModal.onClose(function () {
            $('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId=" + $('input[name=Id]').val());
        });
        $('.date-picker').daterangepicker({
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        $('#OpenCountryLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryCodeId, displayName: client.countryDisplayProperty }, function (data) {
                _$clientInformationForm.find('input[name=countryDisplayProperty]').val(data.displayName);
                _$clientInformationForm.find('input[name=countryCodeId]').val(data.id);
            });
        });

        $('#ClearCountryDisplayPropertyButton').click(function () {
            _$clientInformationForm.find('input[name=countryDisplayProperty]').val('');
            _$clientInformationForm.find('input[name=countryCodeId]').val('');
        });

        $('#OpenUserLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientuserLookupTableModal.open({ id: client.assigneeId, displayName: client.userName }, function (data) {
                _$clientInformationForm.find('input[name=userName]').val(data.displayName);
                _$clientInformationForm.find('input[name=assigneeId]').val(data.id);
            });
        });

        $('#ClearUserNameButton').click(function () {
            _$clientInformationForm.find('input[name=userName]').val('');
            _$clientInformationForm.find('input[name=assigneeId]').val('');
        });

        $('#OpenBinaryObjectLookupTableButton').click(function () {

            var client = _$partnerInformationForm.serializeFormToObject();

            _ClientbinaryObjectLookupTableModal.open({ id: client.profilePictureId, displayName: client.binaryObjectDescription }, function (data) {
                _$partnerInformationForm.find('input[name=binaryObjectDescription]').val(data.displayName);
                _$partnerInformationForm.find('input[name=profilePictureId]').val(data.id);
            });
        });

        $('#ClearBinaryObjectDescriptionButton').click(function () {
            _$clientInformationForm.find('input[name=binaryObjectDescription]').val('');
            _$clientInformationForm.find('input[name=profilePictureId]').val('');
        });

        $('#OpenDegreeLevelLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientdegreeLevelLookupTableModal.open({ id: client.highestQualificationId, displayName: client.degreeLevelName }, function (data) {
                _$clientInformationForm.find('input[name=degreeLevelName]').val(data.displayName);
                _$clientInformationForm.find('input[name=highestQualificationId]').val(data.id);
            });
        });

        $('#ClearDegreeLevelNameButton').click(function () {
            _$clientInformationForm.find('input[name=degreeLevelName]').val('');
            _$clientInformationForm.find('input[name=highestQualificationId]').val('');
        });

        $('#OpenSubjectAreaLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientsubjectAreaLookupTableModal.open({ id: client.studyAreaId, displayName: client.subjectAreaName }, function (data) {
                _$clientInformationForm.find('input[name=subjectAreaName]').val(data.displayName);
                _$clientInformationForm.find('input[name=studyAreaId]').val(data.id);
            });
        });

        $('#ClearSubjectAreaNameButton').click(function () {
            _$clientInformationForm.find('input[name=subjectAreaName]').val('');
            _$clientInformationForm.find('input[name=studyAreaId]').val('');
        });

        $('#OpenLeadSourceLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientleadSourceLookupTableModal.open({ id: client.leadSourceId, displayName: client.leadSourceName }, function (data) {
                _$clientInformationForm.find('input[name=leadSourceName]').val(data.displayName);
                _$clientInformationForm.find('input[name=leadSourceId]').val(data.id);
            });
        });

        $('#ClearLeadSourceNameButton').click(function () {
            _$clientInformationForm.find('input[name=leadSourceName]').val('');
            _$clientInformationForm.find('input[name=leadSourceId]').val('');
        });

        $('#OpenCountry2LookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryId, displayName: client.countryName2 }, function (data) {
                _$clientInformationForm.find('input[name=countryName2]').val(data.displayName);
                _$clientInformationForm.find('input[name=countryId]').val(data.id);
            });
        });

        $('#ClearCountryName2Button').click(function () {
            _$clientInformationForm.find('input[name=countryName2]').val('');
            _$clientInformationForm.find('input[name=countryId]').val('');
        });

        $('#OpenCountry3LookupTableButton').click(function () {

            var client = _$partnerInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.passportCountryId, displayName: client.countryName3 }, function (data) {
                _$partnerInformationForm.find('input[name=countryName3]').val(data.displayName);
                _$partnerInformationForm.find('input[name=passportCountryId]').val(data.id);
            });
        });

        $('#ClearCountryName3Button').click(function () {
            _$clientInformationForm.find('input[name=countryName3]').val('');
            _$clientInformationForm.find('input[name=passportCountryId]').val('');
        });



        function save(successCallback) {
            if (!_$partnerInformationForm.valid()) {
                return;
            }
            if ($('#Partner_PartnerName').prop('required') && $('#Partner_PartnerName').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('PartnerName')));
                return;
            }
            if ($('#workflowId').prop('required') && $('#workflowId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('workflow')));
                return;
            }
            if ($('#partnerTypeId').prop('required') && $('#partnerTypeId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('partnerType')));
                return;
            }
            if ($('#masterCategoryId').prop('required') && $('#masterCategoryId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('masterCategory')));
                return;
            }
            if ($('#Partner_Email').prop('required') && $('#Partner_Email').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Email')));
                return;
            }
            if ($('#University').prop('required') && $('#University').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('University')));
                return;
            }
            //if ($('#Client_CountryId').prop('required') && $('#Client_CountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}
            //if ($('#Client_PassportCountryId').prop('required') && $('#Client_PassportCountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}



            var partner = _$partnerInformationForm.serializeFormToObject();



            abp.ui.setBusy();
            _partnersService.createOrEdit(
                partner
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditPartnerModalSaved');

                if (typeof (successCallback) === 'function') {
                    successCallback();
                }
            }).always(function () {
                abp.ui.clearBusy();
            });

        };

        function clearForm() {
            debugger
            _$partnerInformationForm[0].reset();
        }

        $('#saveBtn').click(function () {
            debugger
            var titleValue = $(".iti__selected-flag").attr("aria-activedescendant");

            var subcode = titleValue.split("-");



            // Save the title value to a field (e.g., an input field with the ID "myField")
            $("#PhoneCode").val(subcode[2]);
            save(function () {
                window.location = "/AppAreaName/Partners";
            });
        });

        $('#saveAndNewBtn').click(function () {
            var titleValue = $(".iti__selected-flag").attr("aria-activedescendant");

            var subcode = titleValue.split("-");



            // Save the title value to a field (e.g., an input field with the ID "myField")
            $("#PhoneCode").val(subcode[2]);
            save(function () {
                if (!$('input[name=id]').val()) {//if it is create page
                    clearForm();
                }
            });
        });


    });
})();