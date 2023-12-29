

(function () {
    $(function () {
        var _clientsService = abp.services.app.clients;
        var _applicationsService = abp.services.app.applications;
        $("#kt_app_sidebar_toggle").trigger("click");

        $('#applicationId').select2({
            multiple: true,
            width: '100%',
            // Adjust the width as needed
        });
        $('#passportCountryId, #countryId, #highestQualificationId, #studyAreaId, #leadSourceId, #applicationId, #agentId').select2({

            width: '100%', 
            // Adjust the width as needed
        });
        var LeadSource = $("#leadSourceId option:selected").text();
        {
            if (LeadSource == "Agent") {
                document.getElementById("field1").style.display = 'block';
            }
            else {
                document.getElementById("field1").style.display = 'none';
            }
        }
        $("#leadSourceId").change(function () {
            debugger;
            if ($("#leadSourceId option:selected").text() === "Agent") {
                document.getElementById("field1").style.display = 'block';
                //$("#field1").show();
            } else {
                document.getElementById("field1").style.display = 'none';
            }
        });
        var initialValue = $("#ContactPreferences").val();

        // Select the button based on the initial value
        $(".contact-preference-button[data-value='" + initialValue + "']").addClass("selected").css({
            'background-color': '#007bff', // Adjust the background color as needed
            'color': '#fff' // Adjust the text color as needed
        });
        $(document).ready(function () {
            $(".contact-preference-button").click(function () {
                debugger
                var value = $(this).data('value');
                setContactPreference(value); // Change 'Email' to the desired value
            });
        });
        function setContactPreference(value) {
            debugger;
            $(".contact-preference-button").removeClass("selected").css({
                'background-color': '',
                'color': ''
            });

            // Add the selected class and set styles to the button corresponding to the value
            $(".contact-preference-button[data-value='" + value + "']")
                .addClass("selected")
                .css({
                    'background-color': '#007bff', // Adjust the background color as needed
                    'color': '#fff' // Adjust the text color as needed
                });
            var numericValue = 0; // Default value

            switch (value) {
                case "Email":
                    numericValue = 1;
                    break;
                case "Phone":
                    numericValue = 2;
                    break;
                // Add more cases as needed for other enum values
                // default case is not necessary since you've already set a default value
            }

            $("#ContactPreferences").val(numericValue);
            // Add any additional logic or visual feedback here
        }
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

        /* Application Dropdown ... */
         
        _applicationsService.getAll("") 
            .done(function (data) {
                var Record = data.items;
                $("#applicationId").empty();
                optionhtml = '<option value="0">Select Application</option>';
               
                $("#applicationId").append(optionhtml);

                $.each(Record, function (index, item) {
                    debugger
                    optionhtml = '<option value="' +
                        item.application.productId + '">' + item.productName + '</option>';
                    console.log(optionhtml);
                    $("#applicationId").append(optionhtml);
                });
                debugger 
            }) 


        var _$clientInformationForm = $('form[name=ClientInformationsForm]');
        _$clientInformationForm.validate();
        var clientId = $('input[name="id"]').val();
        var imageUrl = $.ajax({
            url: abp.appPath + 'api/services/app/ClientProfile/GetProfilePictureByClient',
            data: {
                clientId: clientId,
            },
            method: 'GET',
            dataType: 'json',
        })
            .done(function (data) {
                debugger 
                if (data.result.profilePicture != "") {
                    $('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
                }
                else {
                    // Assuming you have an image element with the ID 'profileImage'
                    $('#profileImage').attr('src', '/Profile/GetProfilePictureByUser?userId=5&profilePictureId=null');
                }
                
            })
            .fail(function (error) {
                debugger 
                // Assuming you have an image element with the ID 'profileImage'
                $('#profileImage').attr('src', '/Profile/GetProfilePictureByUser?userId=5&profilePictureId=null');

              
            });
        

		        var _ClientcountryLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CountryLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientCountryLookupTableModal.js',
            modalClass: 'CountryLookupTableModal'
        });        var _ClientuserLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientUserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        });        var _ClientbinaryObjectLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/BinaryObjectLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientBinaryObjectLookupTableModal.js',
            modalClass: 'BinaryObjectLookupTableModal'
        });        var _ClientdegreeLevelLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/DegreeLevelLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientDegreeLevelLookupTableModal.js',
            modalClass: 'DegreeLevelLookupTableModal'
        });        var _ClientsubjectAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/SubjectAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientSubjectAreaLookupTableModal.js',
            modalClass: 'SubjectAreaLookupTableModal'
        });        var _ClientleadSourceLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/LeadSourceLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientLeadSourceLookupTableModal.js',
            modalClass: 'LeadSourceLookupTableModal'
        });
        var changeProfilePictureModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ChangePictureModal.js',
            modalClass: 'ChangeProfilePictureModal',
        });
        $('#changeProfilePicture').click(function () {
            changeProfilePictureModal.open({ userId: $('input[name=Id]').val() });
        });

        changeProfilePictureModal.onClose(function () {
            $('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId=" + $('input[name=Id]').val());
        });

        //_modalManager
        //    .getModal()
        //    .find('#changeProfilePicture')
        //    .click(function () {
        //        changeProfilePictureModal.open({ userId: _modalManager.getModal().find('input[name=Id]').val() });
        //    });

        //changeProfilePictureModal.onClose(function () {
        //    _modalManager.getModal().find('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId=" + _modalManager.getModal().find('input[name=Id]').val())
        //});
    
        

        $('.date-picker').daterangepicker({
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	            $('#OpenCountryLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryId, displayName: client.countryName }, function (data) {
                _$clientInformationForm.find('input[name=countryName]').val(data.displayName); 
                _$clientInformationForm.find('input[name=countryId]').val(data.id); 
            });
        });
		
		$('#ClearCountryNameButton').click(function () {
                _$clientInformationForm.find('input[name=countryName]').val(''); 
                _$clientInformationForm.find('input[name=countryId]').val(''); 
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

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientbinaryObjectLookupTableModal.open({ id: client.profilePictureId, displayName: client.binaryObjectDescription }, function (data) {
                _$clientInformationForm.find('input[name=binaryObjectDescription]').val(data.displayName); 
                _$clientInformationForm.find('input[name=profilePictureId]').val(data.id); 
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

            _ClientcountryLookupTableModal.open({ id: client.passportCountryId, displayName: client.countryName2 }, function (data) {
                _$clientInformationForm.find('input[name=countryName2]').val(data.displayName); 
                _$clientInformationForm.find('input[name=passportCountryId]').val(data.id); 
            });
        });
		
		$('#ClearCountryName2Button').click(function () {
                _$clientInformationForm.find('input[name=countryName2]').val(''); 
                _$clientInformationForm.find('input[name=passportCountryId]').val(''); 
        });
		


        function save(successCallback) {
            if (!_$clientInformationForm.valid()) {
                return;
            }
            if ($('#Client_CountryId').prop('required') && $('#Client_CountryId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
                return;
            }
            if ($('#Client_AssigneeId').prop('required') && $('#Client_AssigneeId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
                return;
            }
            if ($('#Client_ProfilePictureId').prop('required') && $('#Client_ProfilePictureId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('BinaryObject')));
                return;
            }
            if ($('#Client_HighestQualificationId').prop('required') && $('#Client_HighestQualificationId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('DegreeLevel')));
                return;
            }
            if ($('#Client_StudyAreaId').prop('required') && $('#Client_StudyAreaId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('SubjectArea')));
                return;
            }
            if ($('#Client_LeadSourceId').prop('required') && $('#Client_LeadSourceId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('LeadSource')));
                return;
            }
            if ($('#Client_PassportCountryId').prop('required') && $('#Client_PassportCountryId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
                return;
            }
            

            
           
            ProfilePictureId = $('input[name="ProfilePictureId"]').val()
            
            var client = _$clientInformationForm.serializeFormToObject();
            
			
			
            abp.ui.setBusy(); 
			 _clientsService.createOrEdit(
				client
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditClientModalSaved');
               
               if(typeof(successCallback)==='function'){
                    successCallback();
               }
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$clientInformationForm[0].reset();
            location.reload();
        }
        
        $('#saveBtn').click(function () {
            var titleValue = $(".iti__selected-flag").attr("aria-activedescendant");

            var subcode = titleValue.split("-");



            // Save the title value to a field (e.g., an input field with the ID "myField")
            $("#PhoneCode").val(subcode[2]);
            save(function(){
                window.location="/AppAreaName/Clients";
            });
        });
        
        $('#saveAndNewBtn').click(function () {
            var titleValue = $(".iti__selected-flag").attr("aria-activedescendant");

            var subcode = titleValue.split("-");



            // Save the title value to a field (e.g., an input field with the ID "myField")
            $("#PhoneCode").val(subcode[2]);
            save(function(){
                if (!$('input[name=id]').val()) {//if it is create page...
                    clearForm();
                
                }
            });
        });
        $(document).on("change", "#applicationId", function () {
            
            var ProductIdFilter = $("#applicationId").val();
            debugger
            alert(ProductIdFilter);
            $.ajax({
                url: abp.appPath + 'api/services/app/Applications/GetAll?ProductIdFilter=' + ProductIdFilter,
                 
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    debugger
                    var html = '<div class=row>';
                    html += '<div class="col-lg-4">' + data.result.items[0].productName +'</div>'
                    html += '<div class="col-lg-4">' + data.result.items[0].workflowName +'</div>'
                    html += '<div class="col-lg-4">' + data.result.items[0].productName +'</div>'
                    html += '</div>'
                    $(".ApplicationGridData").append(html);
                        
                    })
                    .fail(function (Error) { });
             
        });
        $('.accordion-header').click(function () {
            if ($(this).hasClass('active')) {
                $(this).removeClass('active');
            } else {
                $(this).addClass('active');
            }
        });
    });
})();