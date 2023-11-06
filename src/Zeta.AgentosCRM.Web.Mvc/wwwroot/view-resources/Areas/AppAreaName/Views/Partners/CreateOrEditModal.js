(function () {
    $(function () {
        $('#masterCategoryId').select2();
        $('#partnerTypeId').select2();
        $('#serviceWorkflowId').select2();
        $('#countryId').select2();
        $('#currencyId').select2();

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
        var selectedCountry = "IN"; // Replace with the appropriate country code

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
            //if ($('#Client_CountryCodeId').prop('required') && $('#Client_CountryCodeId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}
            //if ($('#Client_AssigneeId').prop('required') && $('#Client_AssigneeId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
            //    return;
            //}
            //if ($('#Client_ProfilePictureId').prop('required') && $('#Client_ProfilePictureId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('BinaryObject')));
            //    return;
            //}
            //if ($('#Client_HighestQualificationId').prop('required') && $('#Client_HighestQualificationId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('DegreeLevel')));
            //    return;
            //}
            //if ($('#Client_StudyAreaId').prop('required') && $('#Client_StudyAreaId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('SubjectArea')));
            //    return;
            //}
            //if ($('#Client_LeadSourceId').prop('required') && $('#Client_LeadSourceId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('LeadSource')));
            //    return;
            //}
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
            save(function () {
                window.location = "/AppAreaName/Partners";
            });
        });

        $('#saveAndNewBtn').click(function () {
            save(function () {
                if (!$('input[name=id]').val()) {//if it is create page
                    clearForm();
                }
            });
        });


    });
})();