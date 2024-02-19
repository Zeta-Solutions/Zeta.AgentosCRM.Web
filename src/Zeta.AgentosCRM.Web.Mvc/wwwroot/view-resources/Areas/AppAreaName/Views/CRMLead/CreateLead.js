(function () {
    $(function () {
        $(".app-header").css("display", "none");
        $("#kt_app_sidebar").css("display", "none");
        $("#kt_app_wrapper").css("margin", "0");
        //...
        var _leadsService = abp.services.app.leadHead;

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
            var imgElement = $(`<img id="CoverImage" src=${coverimg} width="700" height="" class="img-thumbnail img-rounded client-edit-dialog-profile-image" />`);
            var container = $('#cardContainerLead');
            imgElement.css({
                'display': 'block',
                'margin-left': 'auto',
                'margin-right': 'auto',
                'border': 'none',
                'background-color': '#9bb0db',
            });
            //$('#CoverImage').attr('src', "data:image/png;base64," + data.result.profilePicture).css({
            //    'width': '100px',
            //    'height': '200px',
            //    'object-fit': 'contain',  // Ensure the entire image fits within the specified dimensions
            //    // Additional styles as needed
            //});
            container.append(imgElement);
            
            // Check if globalData.result.items is an array before attempting to iterate
            if (Array.isArray(globalData.result.leadDetail)) {
                // Group items by sectionName
                var groupedItems = groupBy(globalData.result.leadDetail, 'sectionName');

                // Create a card div for all sections
                var cardDiv = $('<div>').addClass('card mt-3');

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
                        if (sectionName == "INTERESTED SERVICES") {
                            var input = $('<input>').attr({
                                'type': 'checkbox',
                                'id':item.propertyName,
                                'class': 'form-check-input custom-checkbox' // Add your additional classes here
                            });
                        }
                        else {
                            if (item.inputtype === 'Select') {
                                // Create a select element
                                input = $('<select>').attr({
                                    'id':item.propertyName,
                                    'style': 'width: 100%'
                                });

                                // Add options to the select element, you can modify this based on your data
                                input.append($('<option>').text('Option 1').val('value1'));
                                input.append($('<option>').text('Option 2').val('value2'));

                                // Initialize select2 after appending to the DOM

                                // Add more options as needed

                            } else {
                                //debugger
                                if (item.propertyName == "DateOfBrith" || item.propertyName == "Visa Expiry Date" || item.propertyName == "Visa Expiry Date" || item.propertyName == "Course Start" || item.propertyName == "Course End") {
                                    input = $('<input>').attr({
                                        'type': "text",
                                        'id':item.propertyName,
                                        'placeholder': item.propertyName,
                                        'style': 'width: 100%',
                                        'class': 'form-control form-control-sm rounded-0 custom-select-input date-picker'
                                    });

                                }
                                else if (item.propertyName == "Country" || item.propertyName == "Country of Passport" || item.propertyName == "Degree Level" || item.propertyName == "Subject Area" || item.propertyName == "Subject") {
                                    var removespace = item.propertyName.replace(/[\s!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]/g, '');
                                    input = $('<select>').attr({
                                        'id': removespace,
                                        'style': 'width: 100%'
                                    })
                                    //ddl = 1;
                                    ddlidArray.push(removespace);
                                   // ddlid = removespace;
                                }
                                else {
                                    // Create an input element
                                    input = $('<input>').attr({
                                        'type': item.inputtype,
                                        'id':item.propertyName,
                                        'placeholder': item.propertyName,
                                        'style': 'width: 100%',
                                        'class': 'form-control custom-small-input rounded-0 input-sm'
                                    });
                                }
                            }
                        }
                        // Append label and input to the item div
                        itemDiv.append(label, '<br>', input);

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
    <button type="button" id="saveEmailSetupBtn" class="btn btn-primary save-button rounded-0" style="margin-right:25px;">
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
        }
        function Selectdropdown() {
            $('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });
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



    });
})();
