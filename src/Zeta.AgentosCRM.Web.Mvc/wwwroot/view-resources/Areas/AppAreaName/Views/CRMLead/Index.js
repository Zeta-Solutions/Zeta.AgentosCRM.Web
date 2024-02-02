(function () {
    $(function () {
        //...
        var _leadsService = abp.services.app.leadHead;
        var demoUiComponentsService = abp.services.app.demoUiComponents;
        var globalData;
        var Tags;
        $('#TagName').select2({
            multiple: true,
            width: '100%',
            placeholder: 'Select Tag',
            // Adjust the width as needed
        });
        $('#leadSourceId').select2({
            width: '100%',
            //dropdownParent: $('#Timezone').parent(),
            // Adjust the width as needed
        });
        $('#organizationUnitId').select2({
            width: '100%',
            //dropdownParent: $('#Timezone').parent(),
            // Adjust the width as needed
        });
        $.ajax({
            url: abp.appPath + 'api/services/app/Tags/GetAll',
            method: 'GET',
            dataType: 'json',
            //data: {
            //    PartnerIdFilter: dynamicValue,
            //},
            success: function (data) {

                // Populate the dropdown with the fetched data
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function populateDropdown(data) {
           
            var dropdown = $('#TagName');

            dropdown.empty();

            $.each(data.result.items, function (index, item) {
              
                // Check if item and its 'tag' property are not null or undefined
                if (item && item.tag && item.tag.id !== null && item.tag.id !== undefined && item.tag.name !== null && item.tag.name !== undefined) {
                    // Assuming you want to use the 'tag' property for the dropdown
                    dropdown.append($('<option></option>').attr('value', item.tag.id).attr('data-id', item.tag.id).text(item.tag.name));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        $.ajax({
            url: abp.appPath + 'api/services/app/ClientInterstedServices/GetAll',
            method: 'GET',
            dataType: 'json',
            //data: {
            //    PartnerIdFilter: dynamicValue,
            //},
            success: function (data) {
                
                var globalData = data
                // Populate the dropdown with the fetched data
                createintrestedservice(globalData);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function createintrestedservice(globalData) {
            debugger
            var cardContainer = $('#cardContainerInterested'); // or replace '#container' with your actual container selector

            // Check if globalData.result.items is an array before attempting to iterate
            if (Array.isArray(globalData.result.items)) {
                // Iterate through items and create cards
                for (var i = 0; i < globalData.result.items.length; i++) {
                    var item = globalData.result.items[i];
                    // Generate unique identifiers for each dynamically created element
                    var uniqueIdSuffix = i + 1;

                    // Access the workflowName from the current item
                    var workflowName = item.workflowName || '';
                    var workflowNameid = (item.workflowName || '').replace(/\s+/g, ''); // Remove spaces and special characters
                    var intrested = `<div class="col-md-3 text-container mb-2 p-2">
                <label class="checkbox-bootstrap checkbox-sm checkboxLabel mb-2">
                    <input class="save_data custom-checkboxIntererstedService" id="save_title" type="checkbox" />
                    <span class="checkbox-placeholder"></span>
                </label>
                <label for="title" hidden>${workflowName}</label>
                <input type="text" class="form-control custom-small-input rounded-0 input-sm mt-2" id="${workflowNameid}${uniqueIdSuffix}" placeholder="${workflowName}">
            </div>`;

                    cardContainer.append(intrested);
                }
            } else {
                console.error('globalData.result.items is not an array:', globalData.result.items);
            }
        }




        var quill = new Quill('#kt_docs_quill_basic1', {
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
    
        var quill2 = new Quill('#kt_docs_quill_basic2', {
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
        $("#exampleCheckbox").change(function () {
            var inputField = $("#exampleInput");
            var inputType;
            debugger
            // Set the initial label text
            var Test = $("#exampleInputLabel").text();
            alert(Test);
            if ($(this).is(":checked")) {
                inputField.attr("type", "text");
                inputType = "text";
                alert(inputType);
            }

        });
        //$('#saveSentLeadBtn').click(function () {
        //    $("[id^='save_title']").each(function () {
        //        debugger
        //        if ($(this).is(':checked')) {
        //            var parentDiv = $(this).closest('div');

        //            // Find the text input field within the parent div

        //            var textInput = parentDiv.find(':text');
        //            if (textInput>0) {
        //                var inputId = textInput.attr('id');

        //                // Get the type of the found input field
        //                var inputType = textInput.attr('type');
        //            }
        //           else if (textInput.length === 0) {
        //                var textInput = parentDiv.find('select');
        //                var inputId = textInput.attr('id');

        //                // Get the type of the found input field
        //                var inputType = textInput.attr('type');
        //            }
        //            else if (textInput.length === 0) {
        //               debugger
        //                parentDiv.find('input[type="datetime-local"]');
        //               var inputId = textInput.attr('id');

        //               // Get the type of the found input field
        //               var inputType = textInput.attr('type');
        //           }


        //            // Toggle the 'auto-save' class based on the checkbox state
        //            textInput.toggleClass('auto-save', this.checked);
        //        }
        //        else {
        //            $(this).parent().next().find('input').removeClass('auto-save');
        //        }
        //    });

        //});
        //$('#saveSentLeadBtn').click(function () {
        //    var checkedInputs = [];
        //    $("[id^='save_title']").each(function () {
        //        debugger
        //        if ($(this).is(':checked')) {
        //            var parentDiv = $(this).closest('div');

        //            // Find the text input field within the parent div
        //            var textInput = parentDiv.find(':text');

        //            if (textInput.length === 0) {
        //                // If no text input is found, find a select input
        //                textInput = parentDiv.find('select');
        //            }

        //            // If still no input is found, find a datetime input
        //            if (textInput.length === 0) {
        //                textInput = parentDiv.find('input[type="datetime"]');
        //            }

        //            // Check if the input field is found
        //            if (textInput.length > 0) {
        //                var inputId = textInput.attr('id');

        //                // Get the type of the found input field
        //                //var inputType = textInput.attr('type');
        //                var inputType = textInput.attr('type') || "Select";
        //                var associatedLabel = textInput.closest('div').find('label');
        //                var labelText = associatedLabel.text().trim();
        //                // Toggle the 'auto-save' class based on the checkbox state
        //                textInput.toggleClass('auto-save', this.checked);

        //                var inputData = {
        //                    propertyName: labelText,
        //                    inputtype: inputType,
        //                    formName: $("#FormName").val(),
        //                    displayName: labelText
        //                };

        //                    processInputData(inputData);


        //                //setTimeout(function () {
        //                //    var Steps = JSON.stringify(inputData);
        //                //    Steps = JSON.parse(Steps);

        //                //    _leadsService.createOrEdit(Steps)
        //                //        .done(function () {
        //                //            // Introduce a delay before reloading the page
        //                //            setTimeout(function () {
        //                //                abp.notify.info(app.localize('SavedSuccessfully'));
        //                //                location.reload();
        //                //            }, 100); // Adjust the delay time as needed
        //                //        });
        //                //}, 100);
        //            }
        //        } else {
        //            // Uncheck: Remove 'auto-save' class from all input fields
        //            //parentDiv.find(':text, select, input[type="datetime-local"]').removeClass('auto-save');
        //        }
        //    });
        //});

        //$('#saveSentLeadBtn').click(function () {
        //  // Array to store checked values
        //    var checkedValues = [];
        //    $("[id^='save_title']").each(function () {
        //        if ($(this).is(':checked')) {
        //            debugger
        //            var parentDiv = $(this).closest('div');

        //            var textInput = parentDiv.find(':text');
        //            if (textInput.length === 0) {
        //                textInput = parentDiv.find('select');
        //            }

        //            if (textInput.length === 0) {
        //                textInput = parentDiv.find('input[type="datetime"]');
        //            }

        //            if (textInput.length > 0) {
        //                var inputId = textInput.attr('id');

        //                var inputType = textInput.attr('type') || "Select";
        //                var associatedLabel = textInput.closest('div').find('label');
        //                var labelText = associatedLabel.text().trim();

        //                textInput.toggleClass('auto-save', true);
        //                var inputData = {
        //                    propertyName: labelText,
        //                    inputtype: inputType,
        //                    formName: $("#FormName").val(),
        //                    displayName: labelText
        //                };
        //                checkedValues.push(inputData);
        //                console.log(checkedValues)

        //                //checkedValues.forEach(function (checkedValue) {
        //                    $.each(checkedValues, function (index, item) { //});
        //                    debugger;
        //                    processInputData(item);
        //                });
        //            }
        //        }
        //    });
        //});


        var changeProfilePictureModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/CRMLead/_ChangePictureModal.js',
            modalClass: 'ChangeProfilePictureModal',
        });
        $('#changeProfilePicture').click(function () {
            changeProfilePictureModal.open({ userId: $('input[name=Id]').val() });
        });

        changeProfilePictureModal.onClose(function () {
            $('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId=" + $('input[name=Id]').val());
        });
        var changeCoverPictureModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/CRMLead/_ChangeCoverPictureModal.js',
            modalClass: 'ChangeProfilePictureModal',
        });
        $('#changeCoverPicture').click(function () {
            changeCoverPictureModal.open({ userId: $('input[name=Id]').val() });
        });

        changeProfilePictureModal.onClose(function () {
            $('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId=" + $('input[name=Id]').val());
        });
        var HeaderType = "";
        $(document).on('click', '#SelectAllCheckBox', function () {
            HeaderType = "Comments";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckDocument', function () {
            HeaderType = "UploadDocument";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckOtherTestScore', function () {
            debugger
            HeaderType = "OtherTestScore";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckEnglishTestScore', function () {
            debugger
            HeaderType = "EnglishTestScore";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckEduBackGround', function () {

            HeaderType = "EduBackground";
            chkAll();

        });

        $(document).on('click', '#SelectAllCheckIntererstedService', function () {

            HeaderType = "IntererstedService";
            chkAll();

        });

        $(document).on('click', '#SelectAllCheckotherDetail', function () {

            HeaderType = "otherDetail";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckCurrentVisaInfo', function () {

            HeaderType = "CurrentVisaInfo";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckAddressDetail', function () {

            HeaderType = "AddressDetail";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckContactDetail', function () {

            HeaderType = "ContactDetail";
            chkAll();

        });
        $(document).on('click', '#SelectAllCheckPersonalDetail', function () {

            HeaderType = "PersonalDetail";
            chkAll();

        });



        function chkAll() {
            debugger;
            if (HeaderType == "Comments") {
                var selectAllCheckbox = $("#SelectAllCheckBox");

                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkbox");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkbox");
                    checkboxesInTextContainer.prop("checked", false);
                }

            }
            else if (HeaderType == "UploadDocument") {
                var selectAllCheckbox = $("#SelectAllCheckDocument");

                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxDocument");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxDocument");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }
            else if (HeaderType == "OtherTestScore") {
                var selectAllCheckbox = $("#SelectAllCheckOtherTestScore");
                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxOtherTestScore");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxOtherTestScore");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }
            else if (HeaderType == "EnglishTestScore") {
                var selectAllCheckbox = $("#SelectAllCheckEnglishTestScore");
                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxEnglishTestScore");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxEnglishTestScore");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }
            else if (HeaderType == "EduBackground") {
                var selectAllCheckbox = $("#SelectAllCheckEduBackGround");
                // Check if the checkbox is checked...
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxEduBackground");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxEduBackground");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }
            else if (HeaderType == "IntererstedService") {
                var selectAllCheckbox = $("#SelectAllCheckIntererstedService");
                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxIntererstedService");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxIntererstedService");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }

            else if (HeaderType == "otherDetail") {
                var selectAllCheckbox = $("#SelectAllCheckotherDetail");
                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxOtherDetail");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxOtherDetail");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }
            else if (HeaderType == "CurrentVisaInfo") {
                var selectAllCheckbox = $("#SelectAllCheckCurrentVisaInfo");
                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxCurrentVisaInfo");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxCurrentVisaInfo");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }
            else if (HeaderType == "AddressDetail") {
                var selectAllCheckbox = $("#SelectAllCheckAddressDetail");
                // Check if the checkbox is checked..
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxAddressDetail");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxAddressDetail");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }
            else if (HeaderType == "ContactDetail") {
                var selectAllCheckbox = $("#SelectAllCheckContactDetail");
                // Check if the checkbox is checked..
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxContactDetail");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxContactDetail");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }

            else if (HeaderType == "PersonalDetail") {
                var selectAllCheckbox = $("#SelectAllCheckPersonalDetail");
                // Check if the checkbox is checked
                if (selectAllCheckbox.is(":checked")) {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxPersonalDetail");
                    checkboxesInTextContainer.prop("checked", true);
                } else {
                    var checkboxesInTextContainer = $(".text-container .custom-checkboxPersonalDetail");
                    checkboxesInTextContainer.prop("checked", false);
                }
            }

        }
        $('#saveSentLeadBtn').click(function () {
            // Array to store checked values
            var checkedValues = [];
            var uniqueInputIds = []; // Array to store unique input IDs
            var allCheckedValues = [];
            $("[id^='save_title']").each(function () {
                if ($(this).is(':checked')) {
                    
                    var parentDiv = $(this).closest('div');

                    var textInput = parentDiv.find(':text');
                    if (textInput.length === 0) {
                        textInput = parentDiv.find('select');
                    }

                    //var sectionDiv = parentDiv.parents('.MainHeader').first();

                    // Extract the section name from the h5 element inside MainHeader
                    // var sectionName = $(this).closest('.MainHeader').find('.SectionName').text().trim();
                    //var sectionName = $(this).closest('.SectionName').text().trim();

                    if (textInput.length === 0) {
                        textInput = parentDiv.find('input[type="datetime"]');
                    }

                    if (textInput.length > 0) {
                        var inputId = textInput.attr('id');

                        // Check for duplicate input IDs
                        if (uniqueInputIds.indexOf(inputId) === -1) {
                            uniqueInputIds.push(inputId);

                            var inputType = textInput.attr('type') || "Select";
                            var associatedLabel = textInput.closest('div').find('label');
                            var labelText = associatedLabel.text().trim();
                           
                            //var sectionName = $(this).closest('.text-container').prev('.MainHeader').find('.SectionName').text().trim();
                            var sectionName = $(this).closest('.text-container').prevAll('.MainHeader').first().find('.SectionName').text().trim() ||"INTERESTED SERVICES";
                            textInput.toggleClass('auto-save', true);
                            var inputData = {
                                propertyName: labelText,
                                inputtype: inputType,
                                sectionName: sectionName,
                                status:1,
                            };
                            checkedValues.push(inputData);

                            //console.log(checkedValues);
                        }
                    }
                }
            });
        
            //checkedValues.forEach(function (checkedValue) {

            //    processInputData(checkedValue);
            //});
            checkedValues.forEach(function (checkedValue) {
                allCheckedValues.push(checkedValue);
            });
            processInputData(allCheckedValues);
        });
        $(document).off("click", "#IsPrivacyShown").on("click", "#IsPrivacyShown", function (e) {
        demoUiComponentsService.sendAndGetValue(quill.root.innerHTML).done(function (data) {
            debugger;
            if ($("#IsPrivacyShown").prop("checked") == true) {
                $("input[name='PrivacyInfo']").val(data.output);
                abp.libs.sweetAlert.config.info.html = true;
            }
            demoUiComponentsService.sendAndGetValue(quill2.root.innerHTML).done(function (data) {
                debugger;
                if ($("#IsPrivacyShown").prop("checked") == true) {
                    $("input[name='Consent']").val(data.output);
                    abp.libs.sweetAlert.config.info.html = true;
                }

            });
        });
        
        });
       
        $(document).off("change", "#TagName").on("change", "#TagName", function (e) {
            var datarows = [];
        var datarowsList = $("#TagName :selected").map(function (i, el) {
            debugger
            return $(el).val();
        }).get();
        $.each(datarowsList, function (index, value) {
            var datarowsItem = {
                TagId: datarowsList[index]
            }
            debugger
            datarows.push(datarowsItem);
        });
            Tags = "";
             Tags = datarows.map(row => row.TagId).join(',');
        });
        function processInputData(checkedValue) {

          debugger
            var LeadDetails = JSON.stringify(checkedValue);
            LeadDetails = JSON.parse(LeadDetails);
            var savedText = $("#CardHeader").val().replace(/\n/g, '\\n');
           debugger
            var LeadHead = {
                formName: $("#FormName").val(),
                formtittle: $("#formTitle").val(),
                headerNote: savedText,
                organizationUnitId: $("#organizationUnitId").val(),
                leadSourceId: $("#leadSourceId").val(),
                //tagName: $("#TagName").val(),
                //isPrivacyShown: $("#IsPrivacyShown").val(),
                isPrivacyShown: $("#IsPrivacyShown").prop("checked"),
                privacyInfo: $("input[name='PrivacyInfo']").val(),
                consent: $("input[name='Consent']").val(),
                logo: $('input[name="ProfilePictureId"]').val(),
                coverImage: $('input[name="CoverPictureId"]').val(),
                tagName: Tags,
            };
           
            LeadHead.LeadDetails = LeadDetails;
            //var LeadHeads = LeadHead.serializeFormToObject();
            console.log(LeadHead);
            _leadsService.createOrEdit(LeadHead)
                .done(function () {
                    // Introduce a delay before reloading the page..

                    abp.notify.info(app.localize('SavedSuccessfully'));
                    location.reload();
                    // Adjust the delay time as needed
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    // Error handler..

                    //console.error('Error:', textStatus, errorThrown);

                    // Check if the responseJSON contains error details
                    if (jqXHR.responseJSON && jqXHR.responseJSON.error) {
                        var errorDetails = jqXHR.responseJSON.error;

                        // Check for a specific condition or property in the errorDetails
                        if (errorDetails.innerException && errorDetails.innerException.message) {
                            console.error('Inner exception:', errorDetails.innerException.message);
                            // Handle the inner exception as needed
                        }
                    }
                });
        }

        //    function processInputData(inputData) {
        //    debugger
        //    // Perform any action with the inputData
        //    console.log(inputData);

        //    // Example: Push inputData to an array
        //    // inputDataArray.push(inputData);

        //    // Example: Call the service with inputData
        //    //setTimeout(function () {
        //        var Steps = JSON.stringify(inputData);
        //        Steps = JSON.parse(Steps);

        //        _leadsService.createOrEdit(Steps)
        //            .done(function () {
        //                // Introduce a delay before reloading the page
        //               // setTimeout(function () {
        //                    abp.notify.info(app.localize('SavedSuccessfully'));
        //                    location.reload();
        //                //}, 1000); // Adjust the delay time as needed
        //            });
        //   // }, 1000);
        //}
    });
})();
