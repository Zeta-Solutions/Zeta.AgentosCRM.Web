(function () {
    $(function () {
        const urlParams = new URLSearchParams(window.location.search);
        const partnerIdValue = urlParams.get('clientId');
        var idValue = 0;
        var idElements = document.getElementsByName("id");
        debugger
      

        if (idElements.length > 0) {
            // Check if at least one element with the name "id" is found
            var idElement = idElements[0];

            if (idElement.value !== undefined) {
                // Check if the value property is defined
                idValue = idElement.value;
            } else {
                console.error("Element with name 'id' does not have a value attribute.");
            }
        } else {
            console.error("Element with name 'id' not found.");
        }
        if (idValue > 0) {

            debugger
            $.ajax({
                url: abp.appPath + 'api/services/app/ClientQuotationHeads/GetClientQuotationHeadForEdit?id=' + idValue,
                method: 'GET',
                dataType: 'json',

                success: function (data) {
                    debugger
                    // Populate the dropdown with the fetched data
                    updatetable(data);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        }
        function updatetable(data) {
            debugger;
            var clientQuotationDetails = data.result.clientQuotationDetail;
            for (var i = 0; i < data.result.clientQuotationDetail.length; i++) {
                var clientQuotationDetail = clientQuotationDetails[i];
              
                var rowCount = $("#ClientsQuotationDetailtable tbody tr").length;
                //var rowCount = 0;//parseInt($('#rowCount').val())+0;
                var srlno = $('#rowCount').val();
              
                var TrData = '<div class="Card" style="background-color: #f0f0f0;">';
                TrData += '<div class="Card-head">';
                TrData += '<span><input id="workflowsId"class="workflowsId" type="hidden" value="' + clientQuotationDetail.workflowId + '"/></span>';
                TrData += '<span class="workflowsName">' + clientQuotationDetail.workflowName + '</span>' + '<span class="Edit-icon" style="float: right; cursor: pointer;"><i class="fa fa-edit" style="font-size: 10px;"></i></span><br>';
                TrData += '<span><input id="productsId"class="productsId" type="hidden" value="' + clientQuotationDetail.productId + '"/></span>';
                TrData += '<span class="productsName">' + clientQuotationDetail.productName + '</span><br>';
                TrData += '<span><input id="partnersId" class="partnersId" type="hidden" value="' + clientQuotationDetail.partnerId + '"/></span>';
                TrData += '<span class="partnerName">' + clientQuotationDetail.partnerName + '</span><br>';
                TrData += '<span><input id="branchsId"  class="branchsId" type="hidden" value="' + clientQuotationDetail.branchId + '"/></span>';
                TrData += '<span><input id="branchName"  class="branchName" type="hidden" value="' + clientQuotationDetail.branchName + '"/></span>';
                TrData += '<span><input id="Id"  class="Id" type="hidden" value="' + clientQuotationDetail.id + '"/></span>';
                TrData += '<span><input id="rowCount" type="hidden" value="' + rowCount + '"/></span>';
                TrData += '</div></div>';


                var srCount = rowCount + 2;

                debugger
                var mainDiv = $('<div>').addClass('maincard maindivcard');


                mainDiv.append(TrData);

                // Return the created card
                var cardHtml = mainDiv.html();
               
                //$("workflowId").val("selectedIndex", 0);
                var adddatatotable =
                    "<tr class='trq_" + srCount + "'>" +
                    "<td>" + cardHtml + "</td>" +
                    "<td><textarea type='text' placeholder='Description' class='form-control border-0 input-sm Description'>" + clientQuotationDetail.description + "</textarea></td>" +
                    "<td><input id='fee_" + srCount + "' type='text' placeholder='' value='" + clientQuotationDetail.serviceFee + "' class='form-control border-0 input-sm fee' /></td>" +
                    "<td><input id='discount_" + srCount + "' type='text' placeholder='' value='" + clientQuotationDetail.discount + "' class='form-control border-0 input-sm discount' /></td>" +
                    "<td><input id='NetFee_" + srCount + "' type='text' placeholder='' value='" + clientQuotationDetail.netFee + "' class='form-control border-0 input-sm NetFee'readonly /></td>" +
                    "<td><input id='Rate_" + srCount + "' type='text' placeholder='' value='" + clientQuotationDetail.exchangeRate + "' class='form-control border-0 input-sm Rate' /></td>" +
                    "<td><input id='total_" + srCount + "' type='text' placeholder='' value='" + clientQuotationDetail.totalAmount + "' class='form-control border-0 input-sm total'readonly /></td>" +
                    "<td><span class='Delete-icon delete' style='cursor: pointer; margin-left: 5px;'><i class='fa fa-trash' style='font-size: 10px;'></i></span></td>" +
                    "</tr>";

                $("#ClientsQuotationDetailtable").append(adddatatotable);

            }
        }
        getclientsreload(partnerIdValue);
        var globalData; // Declare the data variable in a broader scope

        function createCard(item) {
            // Create the main container for the card
            var mainDiv = $('<div>').addClass('maincard maindivcard');

            // Create the card
            var cardDiv = $('<div>').addClass('card').css('background-color', '#f0f0f0'); // Set your desired background color

            // Create the card body
            var cardBodyDiv = $('<div>').addClass('card-body');

            // Create the card title
            var cardTitle = $('<h5>').addClass('card-title').html("Client Details <hr>");

            // Create the paragraph for card information
            var infoParagraph = $('<p>').addClass('card-text clientName').html(item.firstName + '  ' + item.lastName + '<br>');
            var infoParagraph1 = $('<p>').addClass('card-text clientEmail').html(item.email);
               

            // Append card title and info paragraph to the card body
            cardBodyDiv.append(cardTitle, infoParagraph, infoParagraph1);

            // Append card body to the card
            cardDiv.append(cardBodyDiv);

            // Append card to the mainDiv
            mainDiv.append(cardDiv);
            debugger
            var clientname = item.firstName + item.lastName;
            var clientEmail = item.email;
            $("input[name='ClientEmail']").val(clientEmail);
            $("#ClientName").val(clientname);
            // Return the created card
            return mainDiv;
            
        }








        function getclientsreload(partnerIdValue) {
            debugger

            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/Clients/GetClientForView',
                data: {
                    id: partnerIdValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    console.log('Response from server:', data);
                    globalData = data; // Assign data to the global variable
                    processData(data); // Call processData function after data is available
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function processData(data) {
            debugger
            var cardContainer = $('#cardContainerContact'); // or replace '#container' with your actual container selector
            var item = globalData.result.client;
            var card = createCard(item);
            cardContainer.append(card);
            // Check if globalData.result.items is an array before attempting to iterate
            //if (Array.isArray(globalData.result.items)) {
            //    // Iterate through items and create cards
            //    for (var i = 0; i < globalData.result.client.length; i += 3) {
            //        var rowDiv = $('<div>').addClass('row mt-3');

            //        for (var j = 0; j < 3 && (i + j) < globalData.result.client.length; j++) {
            //            var item = globalData.result.items[i + j];
            //            var card = createCard(item);

            //            var colDiv = $('<div>').addClass('col-md-4'); // Set the column size to 4 for three columns in a row
            //            colDiv.append(card);
            //            rowDiv.append(colDiv);
            //        }

            //        cardContainer.append(rowDiv);
            //    }
            //} 
        }

        var _clientsQuotationService = abp.services.app.clientQuotationHeads;
        $("#kt_app_sidebar_toggle").trigger("click");
        $('#currencyId').select2({
            width: '100%',
            // Adjust the width as needed
        });
        //const urlParams = new URLSearchParams(window.location.search);
        //const clientIdValue = urlParams.get('clientId');..
        $("#ClientId").val(partnerIdValue);
       
        var _$clientQuotationInformationForm = $('form[name=QuotationInformationsForm]');
        _$clientQuotationInformationForm.validate();

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

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryId, displayName: client.countryName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=countryName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=countryId]').val(data.id);
            });
        });

        $('#ClearCountryNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=countryName]').val('');
            _$clientQuotationInformationForm.find('input[name=countryId]').val('');
        });

        $('#OpenUserLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientuserLookupTableModal.open({ id: client.assigneeId, displayName: client.userName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=userName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=assigneeId]').val(data.id);
            });
        });

        $('#ClearUserNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=userName]').val('');
            _$clientQuotationInformationForm.find('input[name=assigneeId]').val('');
        });

        $('#OpenBinaryObjectLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientbinaryObjectLookupTableModal.open({ id: client.profilePictureId, displayName: client.binaryObjectDescription }, function (data) {
                _$clientQuotationInformationForm.find('input[name=binaryObjectDescription]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=profilePictureId]').val(data.id);
            });
        });

        $('#ClearBinaryObjectDescriptionButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=binaryObjectDescription]').val('');
            _$clientQuotationInformationForm.find('input[name=profilePictureId]').val('');
        });

        $('#OpenDegreeLevelLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientdegreeLevelLookupTableModal.open({ id: client.highestQualificationId, displayName: client.degreeLevelName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=degreeLevelName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=highestQualificationId]').val(data.id);
            });
        });

        $('#ClearDegreeLevelNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=degreeLevelName]').val('');
            _$clientQuotationInformationForm.find('input[name=highestQualificationId]').val('');
        });

        $('#OpenSubjectAreaLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientsubjectAreaLookupTableModal.open({ id: client.studyAreaId, displayName: client.subjectAreaName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=subjectAreaName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=studyAreaId]').val(data.id);
            });
        });

        $('#ClearSubjectAreaNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=subjectAreaName]').val('');
            _$clientQuotationInformationForm.find('input[name=studyAreaId]').val('');
        });

        $('#OpenLeadSourceLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientleadSourceLookupTableModal.open({ id: client.leadSourceId, displayName: client.leadSourceName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=leadSourceName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=leadSourceId]').val(data.id);
            });
        });

        $('#ClearLeadSourceNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=leadSourceName]').val('');
            _$clientQuotationInformationForm.find('input[name=leadSourceId]').val('');
        });

        $('#OpenCountry2LookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.passportCountryId, displayName: client.countryName2 }, function (data) {
                _$clientQuotationInformationForm.find('input[name=countryName2]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=passportCountryId]').val(data.id);
            });
        });

        $('#ClearCountryName2Button').click(function () {
            _$clientQuotationInformationForm.find('input[name=countryName2]').val('');
            _$clientQuotationInformationForm.find('input[name=passportCountryId]').val('');
        });

 
  

        function save(successCallback) {
            if (!_$clientQuotationInformationForm.valid()) {
                return;
            }
            //if ($('#Client_CountryId').prop('required') && $('#Client_CountryId').val() == '') {
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
            //if ($('#Client_PassportCountryId').prop('required') && $('#Client_PassportCountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}

            debugger; 
            var datarows = [];

            // Assuming you have some way of determining the number of rows you want to save, let's say 'rowCount'.
            var rowCount = $(".workflowsId").length;
            debugger
            for (var i = 0; i < rowCount; i++) {
                var WorkflowId = parseInt($(".workflowsId").eq(i).val(), 10);
              
                var WorkflowName = $(".workflowsName").eq(i).text();
                var ProductId = parseInt($(".productsId").eq(i).val(), 10);
                var ProductName = $(".productsName").eq(i).text();
                var BranchId = parseInt($(".branchsId").eq(i).val(), 10);
                var BranchName = $(".branchName").eq(i).text();
                var PartnerId = parseInt($(".partnersId").eq(i).val(), 10);
                var PartnerName = $(".partnerName").eq(i).text();
                var Description = $(".Description").eq(i).val();
                var ServiceFee = parseFloat($(".fee").eq(i).val());
                var Discount = parseFloat($(".discount").eq(i).val());
                var NetFee = parseFloat($(".NetFee").eq(i).val());
                var ExchangeRate = parseFloat($(".Rate").eq(i).val());
                var TotalAmount = parseFloat($(".total").eq(i).val());
                var Id = parseFloat($(".Id").eq(i).val());

                var dataRowItem = {
                    WorkflowId: WorkflowId,
                    ProductId: ProductId,
                    BranchId: BranchId,
                    PartnerId: PartnerId,
                    Description: Description,
                    ServiceFee: ServiceFee,
                    Discount: Discount,
                    NetFee: NetFee,
                    ExchangeRate: ExchangeRate,
                    TotalAmount: TotalAmount,
                    Id: Id,
                    WorkflowName: WorkflowName,
                    ProductName: ProductName,
                    BranchName: BranchName,
                    PartnerName: PartnerName
                };

                datarows.push(dataRowItem);
            }

            var taxRowCounter = $('.total').length;

            $("#ProductCount").val(taxRowCounter);
            // Convert the array to a JSON string
            var QuotationDetails = JSON.stringify(datarows);
            QuotationDetails = JSON.parse(QuotationDetails);
            var client = _$clientQuotationInformationForm.serializeFormToObject();
            client.QuotationDetails = QuotationDetails;


            abp.ui.setBusy();   
            console.log(client);
            _clientsQuotationService.createOrEdit(
                client
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditClientQuotationModalSaved');

                if (typeof (successCallback) === 'function') {
                    successCallback();
                }
            }).always(function () {
                abp.ui.clearBusy();
            });
        };

        function clearForm() {
            _$clientQuotationInformationForm[0].reset();
        }

        $('#saveBtn').click(function () {
            save(function () {
                var taxRowCounter = $('.total').length;

                $("#ProductCount").val(taxRowCounter);
                window.location = "/AppAreaName/Quotation";
                //location.reload();
            });
        });

        $('#saveAndNewBtn').click(function () {
            save(function () {
                var taxRowCounter = $('.total').length;

                $("#ProductCount").val(taxRowCounter);
                if (!$('input[name=id]').val()) {//if it is create page
                    clearForm();
                }
            });
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
