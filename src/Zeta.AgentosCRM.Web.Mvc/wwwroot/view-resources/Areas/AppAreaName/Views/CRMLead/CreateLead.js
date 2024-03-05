(function () {
    $(function () {
        $(".app-header").css("display", "none");
        $("#kt_app_sidebar").css("display", "none");
        $("#kt_app_wrapper").css("margin", "0");
        //...
        var _leadsService = abp.services.app.leadHead;
        var _cRMInquiriessService = abp.services.app.cRMInquiries;
        var demoUiComponentsService = abp.services.app.demoUiComponents;
        var img;
        const urlParams = new URLSearchParams(window.location.search);
        const FormName = urlParams.get('FormName');
        const id = urlParams.get('id');
      
        getLeadsreload(id);
        var globalData; // Declare the data variable in a broader scope..
     
        function createCard(item) {
            // Example: Creating a card with Bootstrap classes
            var card = $('<div>').addClass('card');
            var cardBody = $('<div>').addClass('card-body');
            var cardTitle = $('<h5>').addClass('card-title').text(item.displayName);
            var cardSubtitle = $('<h6>').addClass('card-subtitle mb-2 text-muted').text('ID: ' + item.id);
            var cardText = $('<p>').addClass('card-text').text('Type: ' + item.inputtype);

            cardBody.append(cardTitle, cardSubtitle, cardText);
            card.append(cardBody);

            return card;
        }



        //var changeProfilePictureModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/CRMLead/_ChangePictureModal.js',
        //    modalClass: 'ChangeProfilePictureModal',
        //});


        function getLeadsreload(id) {
            //debugger


            $.ajax({
                url: abp.appPath + 'api/services/app/LeadHead/GetLeadHeadForEdit?id=' + id,
                //data: {
                //    FormName: FormName,
                //},
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    //debugger
                    // console.log('Response from server:', data);  
                    globalData = data; // Assign data to the global variable
                    //processData(); // Call processData function after data is available
                    if (globalData.result.leadHead.logo !== null && globalData.result.leadHead.coverImage === null || globalData.result.leadHead.logo != "" && globalData.result.leadHead.coverImage == "") {


                        $.ajax({
                            url: abp.appPath + 'api/services/app/ClientProfile/GetProfilePictureByPictireId?fileTokkenId=' + globalData.result.leadHead.logo,
                            //data: {
                            //    pictureId: globalData.result.leadHead.logo,
                            //},
                            method: 'GET',
                            dataType: 'json',
                        })
                            .done(function (data) {
                                //debugger
                                //$('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
                                img = "data:image/png;base64," + data.result.profilePicture
                                coverimg =''
                                processData(globalData, img, coverimg);

                            })
                    }
                    else if (globalData.result.leadHead.logo == "" && globalData.result.leadHead.coverImage != "" || globalData.result.leadHead.logo === null && globalData.result.leadHead.coverImage !== null)
                    {
                        $.ajax({
                            url: abp.appPath + 'api/services/app/ClientProfile/GetProfilePictureByPictireId?fileTokkenId=' + globalData.result.leadHead.coverImage,
                            //data: {
                            //    pictureId: globalData.result.leadHead.logo,
                            //},
                            method: 'GET',
                            dataType: 'json',
                        })
                            .done(function (data) {
                                //debugger
                                //$('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
                                    img = ''; 
                                    coverimg = "data:image/png;base64," + data.result.profilePicture
                                processData(globalData, img, coverimg);

                            })
                    }
                    else if (
                        (globalData.result.leadHead.logo !== null && globalData.result.leadHead.coverImage !== null) &&
                        (globalData.result.leadHead.logo !== "" && globalData.result.leadHead.coverImage !== "")
                    ) {
                        $.ajax({
                            url: abp.appPath + 'api/services/app/ClientProfile/GetProfilePictureByPictireId?fileTokkenId=' + globalData.result.leadHead.coverImage,
                            //data: {
                            //    pictureId: globalData.result.leadHead.logo,
                            //},
                            method: 'GET',
                            dataType: 'json',
                        })
                            .done(function (data) {
                                //debugger
                                //$('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
                                img = '';
                                coverimg = "data:image/png;base64," + data.result.profilePicture
                               
                                $.ajax({
                                    url: abp.appPath + 'api/services/app/ClientProfile/GetProfilePictureByPictireId?fileTokkenId=' + globalData.result.leadHead.logo,
                                    //data: {
                                    //    pictureId: globalData.result.leadHead.logo,
                                    //},
                                    method: 'GET',
                                    dataType: 'json',
                                })
                                    .done(function (data) {
                                        //debugger
                                        //$('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
                                        img = "data:image/png;base64," + data.result.profilePicture
                                        coverimg = coverimg;
                                        processData(globalData, img, coverimg);

                                    })
                            })
                    }
                    else {
                        img = "";
                        coverimg = "";
                        processData(globalData, img, coverimg);
                    }
                });
        }
        //function processData() {

        //    // Set the background color of the entire page to blue
        //    $('body').css('background-color', '#9bb0db');

        //    var container = $('#cardContainerLead'); // or replace '#container' with your actual container selector

        //    // Check if globalData.result.items is an array before attempting to iterate
        //    if (Array.isArray(globalData.result.items)) {
        //        // Create a card div
        //        var cardDiv = $('<div>').addClass('card mt-3');

        //        // Set card background color to white
        //        cardDiv.css('background-color', 'white');

        //        // Create a card body div
        //        var cardBodyDiv = $('<div>').addClass('card-body');
        //        var cardTitle = $('<h3>')
        //            .addClass('card-title text-center font-weight-bold mt-6') // Added text-center and font-weight-bold classes
        //            .text( globalData.result.items[0].lead.formName);

        //        // Append the card title to the card body div
        //        //cardBodyDiv.append(cardTitle);
        //        cardBodyDiv.css('margin-top', '10px');
        //        // Iterate through items and create rows with divs
        //        for (var i = 0; i < globalData.result.items.length; i += 4) {
        //            // Create a row div for each set of four items
        //            var rowDiv = $('<div>').addClass('row');
        //            //debugger
        //            // Iterate for four items in each row
        //            for (var j = 0; j < 4 && (i + j) < globalData.result.items.length; j++) {
        //                var item = globalData.result.items[i + j];

        //                // Create a div for the item
        //                var itemDiv = $('<div>').addClass('col-md-3 form-group'); // Adjust the class and column width as needed
        //                var label = $('<label>').append($('<strong>').text(item.lead.displayName + ':'));

        //                var input;

        //                if (item.lead.inputtype === 'Select') {
        //                    // Create a select element
        //                    input = $('<select>').attr({
        //                        'id': 'input_' + item.lead.displayName, // Add a unique ID based on item ID
        //                        'style': 'width: 100%'
        //                    });

        //                    // Add options to the select element, you can modify this based on your data
        //                    input.append($('<option>').text('Option 1').val('value1'));
        //                    input.append($('<option>').text('Option 2').val('value2'));

        //                    // Initialize select2 after appending to the DOM

        //                    // Add more options as needed
        //                } else {
        //                    // Create an input element
        //                    input = $('<input>').attr({
        //                        'type': item.lead.inputtype,
        //                        'id': 'input_' + item.lead.displayName, // Add a unique ID based on item ID
        //                        'placeholder': item.lead.displayName,
        //                        'style': 'width: 100%', // Adjust the width as needed
        //                        'class': 'form-control custom-small-input rounded-0 input-sm' // Adjust the class as needed
        //                    });
        //                }

        //                // Append label and input to the item div
        //                itemDiv.append(label, '<br>', input);

        //                // Append the item div to the row div
        //                rowDiv.append(itemDiv);
        //            }

        //            // Append the row div to the card body div
        //            cardBodyDiv.append(rowDiv);
        //        }

        //        // Append the card body div to the card div
        //        cardDiv.append(cardTitle);
        //        cardDiv.append(cardBodyDiv);

        //        // Append the card div to the container..
        //        container.append(cardDiv);
        //    } else {
        //        console.error('globalData.result.items is not an array:', globalData.result.items);
        //    }
        //}
        function processData(globalData, img, coverimg) {
            //debugger
            var ddl = 0;
            var ddlid;
            var ddlidArray = [];
            // Set the background color of the entire page to blue
            $('body').css('background-color', '#9bb0db');
            var imgElement = $(`<img id="CoverImage" src=${coverimg} width="" height="" class="img-thumbnail img-rounded client-edit-dialog-profile-image" />`);
            var container = $('#cardContainerLead');
            imgElement.css({
                'height': '',
                'display': 'block',
                'margin': '0 auto', // Center the image horizontally with no margin on top and bottom
                'padding': '0', // Remove any padding
                'border-bottom': '1px solid',
                'border-radius': '0'
            });
            container.css('padding', '0');
            container.append(imgElement);
            
            // Check if globalData.result.items is an array before attempting to iterate
            if (Array.isArray(globalData.result.leadDetail)) {
                // Group items by sectionName
                var groupedItems = groupBy(globalData.result.leadDetail, 'sectionName');

                // Create a card div for all sections
                var cardDiv = $('<div>').addClass('card mt-0').css({
                    'border-radius': '0' // Remove rounded corners
                });;

                // Set card background color to white
                cardDiv.css('background-color', 'white');

                // Create a card body div
                var cardBodyDiv = $('<div>').addClass('card-body');
                var retrievedText = globalData.result.leadHead.headerNote.replace(/\\n/g, '<br>');
              /*  var hardHeaderDiv = $('<div>').addClass('text-center font-weight-bold mb-3 border-bottom pb-2 p-2').css('background-color', 'red');*/
                var hardHeaderTitle = `<div class="row p-5">
                    <div class="col-sm-2  margin-top-15 margin-bottom-15">
                        <img id="profileImage" src=${img} width="100" height="100" class="img-thumbnail img-rounded client-edit-dialog-profile-image" />
                    </div>
                    <div class="col-md-7 mb-3 d-flex align-items-center justify-content-center">
                        <h1>${globalData.result.leadHead.formtittle}</h1>
                    </div>
                    <div class="col-md-3 mb-3">
                        ${retrievedText}
                    </div>
                </div>`

                // Append the hard-coded header title to the header div
                //hardHeaderDiv.append(hardHeaderTitle);
                cardBodyDiv.append(hardHeaderTitle);
                // Iterate through the grouped items
                //debugger
                $.each(groupedItems, function (sectionName, sectionItems) {
                    //debugger
                    // Create a card title div for the section name with a border
                    //var cardTitleDiv = $('<div>').addClass('text-center font-weight-bold mb-3 border-bottem pb-2 p-2').css('background-color', '#9bb0db');
                    //var borderDiv = $('<div>').addClass('border border-secondary');
                    var cardTitleDiv = $('<div>').addClass(' font-weight-bold mb-3 border pb-2 p-2 border-secondary');
                    var cardTitle = $('<h3>').css('display', 'flex').css('align-items', 'left').text(sectionName);

                    // Append the card title to the card title div
                    cardTitleDiv.append(cardTitle).append('<hr/>');

                    // Append the card title div to the card body div
                   // cardBodyDiv.append(cardTitleDiv);

                    // Create a row div for each set of four items
                    var rowDiv = $('<div>').addClass('row');

                    // Iterate through items in the current section
                    for (var i = 0; i < sectionItems.length; i++) {
                        var item = sectionItems[i];
                        // Create a div for the item with a border
                        //var itemDiv = $('<div>').addClass('col-md-3 form-group border p-3'); // Added border and padding
                        var itemDiv = $('<div>').addClass('col-md-3 form-group  p-3'); // Added border and padding
                        var label = $('<label>').append($('<strong>').text(item.propertyName + ':'));

                        var input;
                        var removespace = item.propertyName.replace(/[\s!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]/g, '');
                       
                        if (sectionName == "INTERESTED SERVICES") {
                            var checkedValues = [];

                            var removeintrestedspace = item.propertyName.replace(/[\s!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]/g, '');
                            var index = removeintrestedspace.indexOf('0');
                            var firstPart = index !== -1 ? removeintrestedspace.substr(0, index) : removeintrestedspace;
                            var secondPart = index !== -1 ? removeintrestedspace.substr(index + 1) : '';
                            var label = $('<label>').append($('<strong>').text(item.propertyName.replace(item.propertyName, firstPart)));
                            var input = $('<input>').attr({
                                'type': 'checkbox',
                                'id': secondPart,
                                'class': 'form-check-input custom-checkbox' // Add your additional classes here

                            });
                            input.click(function () {
                                if ($(this).is(':checked')) {
                                    // If checked, add its value to the checkedValues array
                                    checkedValues.push($(this).attr('id'));
                                } else {
                                    // If unchecked, remove its value from the checkedValues array
                                    var index = checkedValues.indexOf($(this).attr('id'));
                                    if (index !== -1) {
                                        checkedValues.splice(index, 1);
                                    }
                                }

                                // Update the comma-separated list in your desired location
                                var commaSeparatedValues = checkedValues.join(',');
                                // Replace 'targetElementId' with the ID of the element where you want to display the comma-separated list
                                $('#IntrestedService').val(commaSeparatedValues);
                            });
                        }
                        else {
                            if (item.inputtype === 'Select') {
                                // Create a select element
                                input = $('<select>').attr({
                                    'id': removespace,
                                    'style': 'width: 100%'
                                });

                                // Add options to the select element, you can modify this based on your data
                                input.append($('<option>').text('Option 1').val('value1'));
                                input.append($('<option>').text('Option 2').val('value2'));

                                // Initialize select2 after appending to the DOM

                                // Add more options as needed

                            } else {
                                //debugger
                                if (item.propertyName == "DateOfBrith" || item.propertyName == "Visa Expiry Date" || item.propertyName == "Visa Expiry Date" || item.propertyName == "Course Start" || item.propertyName == "Course End" || item.propertyName == "PrefferedIntake") {
                                    input = $('<input>').attr({
                                        'type': "text",
                                        'id': removespace,
                                        'placeholder': item.propertyName,
                                        'style': 'width: 100%',
                                        'class': 'form-control form-control-sm rounded-0 custom-select-input date-picker'
                                    });

                                }
                                else if (item.propertyName == "Country" || item.propertyName == "Country of Passport" || item.propertyName == "Degree Level" || item.propertyName == "Subject Area" || item.propertyName == "Subject") {
                                    
                                    input = $('<select>').attr({
                                        'id': removespace,
                                        'style': 'width: 100%'
                                    })
                                    //ddl = 1;
                                    ddlidArray.push(removespace);
                                   // ddlid = removespace;
                                }
                                else if (item.propertyName == "Contact Preferences") {

                                    var contactPreferences = ["Email", "Phone"]; // Replace with your preferences

                                    // Create a div element to contain the buttons
                                     input = document.createElement("div");

                                    // Loop through the contactPreferences array and create buttons dynamically
                                    contactPreferences.forEach(function (preference) {
                                        var button = document.createElement("button");
                                        button.setAttribute("type", "button");
                                        button.setAttribute("class", "btn btn-secondary btn-sm contact-preference-button");
                                        button.setAttribute("data-value", preference);
                                        button.textContent = preference;
                                        button.addEventListener("click", function () {
                                            document.querySelectorAll('.contact-preference-button').forEach(function (btn) {
                                                btn.classList.remove('active');
                                                // Remove the background color of all buttons
                                                btn.style.backgroundColor = "";
                                            });

                                            // Add the 'active' class to the clicked button
                                            button.classList.add('active');
                                            // Set the background color of the clicked button to blue
                                            button.style.backgroundColor = "blue";
                                            var dataValue = button.getAttribute("data-value");
                                            $("#contactPreference").val(dataValue);
                                        });
                                        // Append each button to the div
                                        input.appendChild(button);
                                    });
                                }
                                else if (item.propertyName == "UpdateProfileImage") {

                                    input = document.createElement("div");
                                    input.setAttribute("class", "col-sm-11 text-center margin-top-15 margin-bottom-15");

                                    // Create the image element
                                    var image = document.createElement("img");
                                    image.setAttribute("id", "profileImage");
                                    image.setAttribute("src", ""); // Set the src attribute according to your requirements
                                    image.setAttribute("width", "128");
                                    image.setAttribute("height", "128");
                                    image.setAttribute("class", "img-thumbnail img-rounded client-edit-dialog-profile-image");

                                    // Append the image element to the outer div
                                    input.appendChild(image);

                                    // Create the button element
                                    var button1 = document.createElement("button");
                                    button1.setAttribute("class", "btn btn-light btn-sm mb-5");
                                    button1.setAttribute("id", "changeProfilePicture");
                                    button1.textContent = "Change Profile Picture"; // Set the button text according to your requirements
                                    button1.addEventListener("click", function () {
                                        debugger
                                        event.preventDefault();
                                        changeProfilePictureModal.open({ userId: 0 });
                                        changeProfilePictureModal.onClose(function () {
                                            $('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId=" + $('input[name=Id]').val());
                                        });
                                    });
                                    // Append the button element to the outer div
                                    input.appendChild(button1);
                                }
                                else if (item.propertyName == "Phone") {
                                     input =` <div class="" style="display: flex; flex-direction: column;">
     <input type="hidden" value="" id="PhoneCode" name="PhoneCode" />
     <input type="tel" class="form-control custom-small-input  rounded-0" id="phone" name="PhoneNo" value="">
     <span id="valid-msg" class="hide">✓ Valid</span>
     <span id="error-msg" class="hide"></span>
 </div>`
                                    
                                    // Append the button element to the outer div
                                    //input.appendChild(inputphone);
                                }
                                else if (item.propertyName == "Academic Score") {
                                    input = `<input type="number" name="acadmicScore" class="form-control  custom-small-input rounded-0 input-sm" value="0">
                                    <label class="form-check  form-check-inline">
                        <input type="radio" class="form-check-input" id="IsGpa" name="IsGpa" value="false" checked="&quot;checked&quot;">
                        <span class="form-check-label">
                            Percentage
                        </span>
                    </label><label class="form-check  form-check-inline ml-3 mt-3">
                        <input id="IsGpa" type="radio" name="IsGpa" class="form-check-input" value="true">
                        <span class="form-check-label">
                            GPA
                        </span>
                    </label>`

                                    // Append the button element to the outer div
                                    //input.appendChild(inputphone);
                                }
                                else {
                                    // Create an input element
                                    input = $('<input>').attr({
                                        'type': item.inputtype,
                                        'id': removespace,
                                        'placeholder': item.propertyName,
                                        'style': 'width: 100%',
                                        'class': 'form-control custom-small-input rounded-0 input-sm'
                                    });
                                }
                            }
                        }
                        // Append label and input to the item div
                        itemDiv.append(label, '<br>', input,);

                        // Append the item div to the row div
                        rowDiv.append(itemDiv);
                        cardTitleDiv.append(rowDiv)
                    }

                    // Append the row div to the card body div
                    cardBodyDiv.append(cardTitleDiv);
                   
                });
                if (globalData.result.leadHead.isPrivacyShown == true) {
                    var cardFooterDiv = $('<div>');
                    var cardFooterheading = $('<div>');
                    //var cardConsentheading = $('<div>');
                     cardFooterheading.html("<h5>PrivacyInformation:<h5>");
                    //cardFooterDiv.html(globalData.result.leadHead.privacyInfo).css(style = "margin-bottom: 0px;");
                    var cardConsentheading = `<span style="margin: 0; padding: 0;">${globalData.result.leadHead.privacyInfo}</span><div className="terms-checkbox d-flex" style="margin: 0; padding: 0;">
     <label style="display: flex; align-items: center; margin: 0; padding: 0;">
         <input type="checkbox" id="consentCheckbox" required style="transform: scale(1.5); margin-top: 0;"/> 
         <span style="margin-left: 5px; margin-top: 7px; margin-bottom: 0; padding: 0;">${globalData.result.leadHead.consent}</span>
     </label>
 </div>`;

                    //cardConsentheading.css('display', 'inline-block').html('<label style="display: inline;"><input type="checkbox" id="consentCheckbox">' + globalData.result.leadHead.consent + '</label>');

                    cardBodyDiv.append(cardFooterheading,cardConsentheading);
                }
                var button = `<div style="display: flex; justify-content: center;">
    <button type="button" id="saveEmailSetupBtn" class="btn btn-primary save-button rounded-0" style="margin-right:25px; width: 234px; height: 52px;">
        <i class="fa fa-save"></i> 
        Submit Form
    </button>  
</div>`;

                var poweredBy = `<br><div class="text-muted" style="display: flex; justify-content: center; align-items: center; ">
    Powered By &nbsp; 
  <img alt="Logo" src="/Common/Images/app-logo-on-light.svg?&amp;t=638424805980745571" class="h-35px h-25px app-sidebar-logo-default" >
</div>`;


                cardBodyDiv.append(button)
                cardBodyDiv.append(poweredBy)
                cardDiv.append(cardBodyDiv);
                
                
                // Append the card div to the container
                container.append(cardDiv);

                for (var i = 0; i < ddlidArray.length; i++) {
                    (function (index) { // Create a closure to capture the value of i
                        $('#' + ddlidArray[index]).select2({
                            width: '100%',
                            // Add other Select2 options as needed
                        });

                        if (ddlidArray[index] === "Country" || ddlidArray[index] === "CountryofPassport") {
                            $.ajax({
                                url: abp.appPath + 'api/services/app/Countries/GetAll',
                                method: 'GET',
                                dataType: 'json',
                                success: function (data) {
                                    //debugger
                                    var dropdown = $('#' + ddlidArray[index]);

                                    dropdown.empty();
                                    dropdown.append($('<option></option>').attr('value', '').text('Select an option'));
                                    $.each(data.result.items, function (innerIndex, item) {
                                        if (item.country && item.country.id !== null && item.country.id !== undefined && item.country.name !== null && item.country.name !== undefined) {
                                            dropdown.append($('<option></option>').attr('value', item.country.id).attr('data-id', item.country.id).text(item.country.name));
                                        } else {
                                            console.warn('Invalid item:', item);
                                        }
                                    });
                                },
                            });
                        }
                        else if (ddlidArray[i] === "DegreeLevel") {
                            $.ajax({
                                url: abp.appPath + 'api/services/app/DegreeLevels/GetAll',
                                method: 'GET',
                                dataType: 'json',
                                success: function (data) {
                                    //debugger
                                    var dropdown = $('#' + ddlidArray[index]);

                                    dropdown.empty();
                                    dropdown.append($('<option></option>').attr('value', '').text('Select an Degree Level'));
                                    $.each(data.result.items, function (innerIndex, item) {
                                        if (item.degreeLevel && item.degreeLevel.id !== null && item.degreeLevel.id !== undefined && item.degreeLevel.name !== null && item.degreeLevel.name !== undefined) {
                                            dropdown.append($('<option></option>').attr('value', item.degreeLevel.id).attr('data-id', item.degreeLevel.id).text(item.degreeLevel.name));
                                        } else {
                                            console.warn('Invalid item:', item);
                                        }
                                    });
                                },
                            });
                        }
                        else if (ddlidArray[i] === "SubjectArea") {
                            $.ajax({
                                url: abp.appPath + 'api/services/app/SubjectAreas/GetAll',
                                method: 'GET',
                                dataType: 'json',
                                success: function (data) {
                                    //debugger
                                    var dropdown = $('#' + ddlidArray[index]);

                                    dropdown.empty();
                                    dropdown.append($('<option></option>').attr('value', '').text('Select an Subject Area'));
                                    $.each(data.result.items, function (innerIndex, item) {
                                        if (item.subjectArea && item.subjectArea.id !== null && item.subjectArea.id !== undefined && item.subjectArea.name !== null && item.subjectArea.name !== undefined) {
                                            dropdown.append($('<option></option>').attr('value', item.subjectArea.id).attr('data-id', item.subjectArea.id).text(item.subjectArea.name));
                                        } else {
                                            console.warn('Invalid item:', item);
                                        }
                                    });
                                },
                            });
                        }
                        else if (ddlidArray[i] === "Subject") {
                            $.ajax({
                                url: abp.appPath + 'api/services/app/Subjects/GetAll',
                                method: 'GET',
                                dataType: 'json',
                                success: function (data) {
                                    //debugger
                                    var dropdown = $('#' + ddlidArray[index]);

                                    dropdown.empty();
                                    dropdown.append($('<option></option>').attr('value', '').text('Select an Subject'));
                                    $.each(data.result.items, function (innerIndex, item) {
                                        if (item.subject && item.subject.id !== null && item.subject.id !== undefined && item.subject.name !== null && item.subject.name !== undefined) {
                                            dropdown.append($('<option></option>').attr('value', item.subject.id).attr('data-id', item.subject.id).text(item.subject.name));
                                        } else {
                                            console.warn('Invalid item:', item);
                                        }
                                    });
                                },
                            });
                        }
                    })(i); // Pass the current value of i to the closure
                }

                setTimeout(function () {
                    handleDateInputMouseDown();
                }, 100);
                
            } else {
                console.error('globalData.result.items is not an array:', globalData.result.items);
            }
        }
  
        function handleDateInputMouseDown() {
            $('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });
            SelectphoneNo();
        }
        function SelectphoneNo() {
            var input = document.querySelector("#phone");
            const errorMsg = document.querySelector("#error-msg");
            const validMsg = document.querySelector("#valid-msg");

            const errorMap = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];

            var iti = intlTelInput(input, {
                initialCountry: "auto",
                geoIpLookup: function (success, failure) {
                    var countryCode = "PK"; // Replace with the appropriate country code
                    success(countryCode);
                },
                utilsScript: "https://cdn.jsdelivr.net/npm/intl-tel-input@18.1.1/build/js/utils.js"
            });

            var settittle = $("#PhoneCode").val();
            var selectedCountry = settittle;

            iti.setCountry(selectedCountry);

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

            input.addEventListener('change', reset);
            input.addEventListener('keyup', reset);
        }

  
    //    $(document).off("click", ".date-picker").on("click", ".date-picker", function (e) {
    //        $('.date-picker').daterangepicker({
    //            singleDatePicker: true,
    //            locale: abp.localization.currentLanguage.name,
    //            format: 'L'
    //        });
    //});

        // Helper function to group array items by a specific property
        function groupBy(array, property) {
            return array.reduce(function (acc, obj) {
               
                var key = obj.sectionName; // Adjust the property based on your data structure
                if (!acc[key]) {
                    acc[key] = [];
                }
                acc[key].push(obj);
                return acc;
            }, {});
        }
        $(document).off("click", "#saveEmailSetupBtn").on("click", "#saveEmailSetupBtn", function (e) {
            debugger
            var phoneNo = $("#phone").val() !== undefined ? $("#phone").val() : "";
            if (phoneNo !== null && phoneNo !== "" && phoneNo !== undefined) {
                var titleValue = $(".iti__selected-flag").attr("aria-activedescendant");

                var subcode = titleValue.split("-");

                var phonecode = subcode[2];
            }
            var consentCheckbox = $("#consentCheckbox").val() !== undefined ? $("#consentCheckbox").val() : "";
            if (consentCheckbox !== null && consentCheckbox !== "" && consentCheckbox !== undefined) {
                var checkbox = document.getElementById('consentCheckbox');

                // Check if the checkbox is checked
                var isChecked = checkbox.checked;
            }
            // Save the title value to a field (e.g., an input field with the ID "myField")
           

            var inputData = {
                firstName: $("#FirstName").val(),
                lastName: $("#LastName").val(),
                dateofBirth: $("#DateOfBrith").val(),
                phoneCode: phonecode,
                phoneNo: $("#phone").val(),
                email: $("#Email").val(),
                secondaryEmail: $("#SecondryEmail").val(),
                contactPreference: $("#contactPreference").val(),
                street: $("#Street").val(),
                city: $("#City").val(),
                state: $("#State").val(),
                postalCode: $("#ZipCode").val(),
                visaType: $("#VisaType").val(),
                visaExpiryDate: $("#VisaExpiryDate").val(),
                preferedInTake: $("#PrefferedIntake").val(),
                degreeTitle: $("#DegreeTitle").val(),
                institution: $("#Institution").val(),
                courseStartDate: $("#CourseStart").val(),
                courseEndDate: $("#CourseEnd").val(),
                academicScore: $("input[name='acadmicScore']").val(),
                isGpa: $("input[name='IsGpa']:checked").val(),
                toefl: $("#TOEFl").val(),
                ielts: $("#IELTS").val(),
                pte: $("#PTE").val(),
                sat1: $("#SATI").val(),
                sat2: $("#SATII").val(),
                gre: $("#GRE").val(),
                gMat: $("#GMAT").val(),
                documentId: $("#UPLOADDOCUMENETS").val(),
                documentIdToken: $("#UPLOADDOCUMENETS").val(),
                pictureId: $("#UpdateProfileImage").val(),
                pictureIdToken: $("#UpdateProfileImage").val(),
                comments: $("#COMMENTS").val(),
                status: $("#status").val(),
                isArchived: $("#isArchived").val(),
                countryId: $("#Country").val(),
                passportCountryId: $("#CountryofPassport").val(),
                degreeLevelId: $("#DegreeLevel").val(),
                subjectId: $("#Subject").val(),
                subjectAreaId: $("#SubjectArea").val(),
                interstedService: $('#IntrestedService').val(),
                isPrivacy: isChecked,
                //organizationUnitId: $("#FirstName").val(),
                //leadSourceId: $("#FirstName").val(),
               // tagId: $("#FirstName").val(),
            };
            var Steps = JSON.stringify(inputData);
            Steps = JSON.parse(Steps);
            _cRMInquiriessService
                .createOrEdit(Steps)
                .done(function () {
                    location.reload();
                    //$('#cardContainerapplicationnotes').remove();
                    //abp.event.trigger('app.createOrEditNoteModalSaved');

                })
                .always(function () {
                    _modalManager.setBusy(false);
                });

        });
        
        
    });
})();
