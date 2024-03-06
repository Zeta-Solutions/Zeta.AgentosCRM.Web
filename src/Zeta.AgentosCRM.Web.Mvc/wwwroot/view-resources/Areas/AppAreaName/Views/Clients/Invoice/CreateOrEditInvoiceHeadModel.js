(function () {
    $(function () {
        var _invoiceDetailServices = abp.services.app.invoiceDetail;
        const urlParams = new URLSearchParams(window.location.search);
        const ApplicationIdValue = urlParams.get('ApplicationId');
        var invoicetype = urlParams.get('InvoiceType');
        var idElements = urlParams.get('Id');
        $("#InvoiceType").val(invoicetype)
        handleDateInputMouseDown()
      
        //debugger
        if (invoicetype === "1") {
            var tittle = document.querySelector('.page-heading');
            var subtitlle = document.querySelector('.breadcrumb-item');
                tittle.innerHTML = '<span>Create Net Claim Invoice</span>';
            subtitlle.innerHTML = '<span class="text-muted">Net Commission Invoice</span>';
            $("#markPaidCheckbox").prop("checked", true)
            $("#NetCommission").show();
            $("#NetCommissionCard").show();
            $("#GrossCommission").remove();
            $("#GrossCommissionCard").remove();
            $("#Currencyddl").remove();
            $("#IncomeTypetd").hide();
            $("#Amounttd").hide();
            $("#TotalAmounttd").hide();
            
            //$("#IsInvoiceNetOrGross").val(1);
        }
        else if (invoicetype === "2") {
            var tittle = document.querySelector('.page-heading');
            var subtitlle = document.querySelector('.breadcrumb-item');
            tittle.innerHTML = '<span>Create Gross Claim Invoice</span>';
            subtitlle.innerHTML = '<span class="text-muted">Gross Commission Invoice</span>';
            $("#NetCommission").remove();
            $("#NetCommissionCard").remove();
            $("#Currencyddl").remove();
            $("#GrossCommission").show();
            $("#GrossCommissionCard").show();
            $("#CommissionClaimeddiv").show();
            $("#IncomeTypetd").hide();
            $("#Amounttd").hide();
            $("#TotalAmounttd").hide();
            $("#TotalAmountdiv").remove();
            //$("#IsInvoiceNetOrGross").val(2);
        }
        else {
            var tittle = document.querySelector('.page-heading');
            var subtitlle = document.querySelector('.breadcrumb-item');
            tittle.innerHTML = '<span>Create General Invoice</span>';
            subtitlle.innerHTML = '<span class="text-muted">Client General Invoice</span>';
            $("#Currencyddl").show();
            $("#NetCommission").remove();
            $("#NetCommissionCard").remove();
            $("#GrossCommissionCard").remove();
            $("#GrossCommission").show();
            $("#GernalCommissionCard").show();
            
            $("#TotalAmountdiv").show();
            $("#InvoiceDiscountdiv").remove();
            $("#TotalFeetd").hide();
            $("#CommissionPertd").hide();
            $("#CommissionAmounttd").hide();
            $("#NetAmounttd").hide();
            $("#CommissionClaimeddiv").remove();
        }
        if ($("#TotalIncomeCard").text() == "") {
            $("#TotalIncomeCard").text(0.00);
        }
        if ($("#PayablesCard").text() == "") {
            $("#PayablesCard").text(0.00);
        }
        if ($("#NetRevenueCard").text() == "") {
            $("#NetRevenueCard").text(0.00);
        }
        if ($("#NetIncomeCard").text() == "") {
            $("#NetIncomeCard").text(0.00);
        }
        if ($("#NetFeeReceivedCard").text() == "") {
            $("#NetFeeReceivedCard").text(0.00);
        }
        $('#manualPaymentDetail').select2({
            width: '100%',
           // dropdownParent: $('#Timezone').parent(),
            // Adjust the width as needed
        });
        $('#WorkFlowOfficeId').select2({
            width: '100%',
            placeholder: 'Select Office',
            allowClear: true,
            minimumResultsForSearch: 10,
        });
        $('#CurrencyId').select2({
            width: '100%',        
        });
        $.ajax({
            url: abp.appPath + 'api/services/app/Agents/GetAllOrganizationUnitForTableDropdown',
            method: 'GET',
            dataType: 'json',

            success: function (data) {

                var data = data.result;
                if (data == null) {
                    //alert("Record Not Found.");
                }
                var optionhtml = '<option value="0"> Select A Receiver</option>';
                $("#WorkFlowOfficeId").append(optionhtml);

                $.each(data, function (i) {
                    // You will need to alter the below to get the right values from your json object.  Guessing that d.id / d.modelName are columns in your carModels data
                    optionhtml = '<option value="' +
                        data[i].id + '">' + data[i].displayName + '</option>';
                    $("#WorkFlowOfficeId").append(optionhtml);
                });
                //populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        var idValue = 0;
        var clientid = 0;
        var PartnerId = 0;

        //var idElements = document.getElementsByName("id");
        
        getIdPartnerAndClient(ApplicationIdValue);

        debugger
        idValue = parseFloat(idElements);
        
        if (idValue > 0) {

            
            $.ajax({
                url: abp.appPath + 'api/services/app/InvoiceHead/GetInvoiceHeadForEdit?id=' + idValue,
                method: 'GET',
                dataType: 'json',

                success: function (data) {
                    debugger
                    // Populate the dropdown with the fetched data
                    updateinvoicetable(data);
                    if (data.result.invIncomeSharing.length > 0) {
                        $("#WorkFlowOfficeId").val(data.result.invIncomeSharing[0].organizationUnitId).trigger("change");
                        $("#IncomeSharingAmount").val(data.result.invIncomeSharing[0].incomeSharing)
                        $("#IncomeSharingId").val(data.result.invIncomeSharing[0].id)
                        //$("#IncomeSharingCheckbox").val(data.result.invIncomeSharing[0].isTax)
                        var isChecked = data.result.invIncomeSharing[0].isTax;
                        if (isChecked == true) {
                            $("#TaxincomeSharingAmount").prop("disabled", false);
                        }
                        // Set the checked state of the checkbox based on the condition
                        $("#IncomeSharingCheckbox").prop("checked", isChecked);
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + data.result.invIncomeSharing[0].taxAmount);
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + data.result.invIncomeSharing[0].totalIncludingTax);
                        GetAllTaxForTableDropdown("TaxincomeSharingAmount", data.result.invIncomeSharing[0].tax)
                    }
                    if (data.result.invPaymentReceived.length > 0) {
                        updatepaymentReceiveddiv(data.result.invPaymentReceived);
                    }
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        }
        function updatepaymentReceiveddiv(data) {
            debugger;

            var uniqueId = $(".AddPaymentReceivedDiv").children().length + 1;

            
            if (data.length == 1) {               
                $("#PaymentsReceived_1").val(data[0].paymentsReceived)
                $("#PaymentReceivedId_1").val(data[0].id)
                $("#markPaidTextarea").val(data[0].addNotes)
                var dateObject = new Date(data[0].paymentsReceivedDate);
                var day = dateObject.getDate();
                var month = dateObject.getMonth() + 1;
                var year = dateObject.getFullYear();
                var formattedDate = day + '/' + month + '/' + year;
                $("#PaymentsReceivedDate_1").val(formattedDate)
                //$("#PaymentsReceivedDate_1").val(data[0].paymentsReceivedDate)
                $("#InvoiceasPaidId_1").val(data[0].paymentMethodId).trigger("change");
                var isChecked = data[0].markInvoicePaid;
                // Set the checked state of the checkbox based on the condition
                $("#markPaidCheckbox").prop("checked", isChecked);
                return false;
            }
            else {
                $("#PaymentsReceived_1").val(data[0].paymentsReceived)
                $("#PaymentReceivedId_1").val(data[0].id)
                $("#markPaidTextarea").val(data[0].addNotes)
                var dateObject = new Date(data[0].paymentsReceivedDate);
                var day = dateObject.getDate();
                var month = dateObject.getMonth() + 1;
                var year = dateObject.getFullYear();
                day = day < 10 ? '0' + day : day;
                month = month < 10 ? '0' + month : month;

                var formattedDatefirst = day + '/' + month + '/' + year;
                $("#PaymentsReceivedDate_1").val(formattedDatefirst)
                //$("#PaymentsReceivedDate_1").val(data[0].paymentsReceivedDate)
                $("#InvoiceasPaidId_1").val(data[0].paymentMethodId).trigger("change");
                var isChecked = data[0].markInvoicePaid;
                // Set the checked state of the checkbox based on the condition
                $("#markPaidCheckbox").prop("checked", isChecked);
                debugger

                for (var i = 1; i < data.length; i++) {
                    var dateObject = new Date(data[i].paymentsReceivedDate);
                    var day = dateObject.getDate();
                    var month = dateObject.getMonth() + 1;
                    var year = dateObject.getFullYear();
                    var formattedDay = (day < 10 ? '0' : '') + day;
                    var formattedMonth = (month < 10 ? '0' : '') + month;

                    var formattedDate = formattedDay + '/' + formattedMonth + '/' + year;
                    var variableName = 'formattedDate_' + i;
                    var newTimelineItem = `
            <div class="row">
                <div class="col-lg-3">
                    <input type="hidden" name="PaymentReceivedId" id="PaymentReceivedId_${i+1}" value="${data[i].id}" />
                    <br />
                    <input class="form-control custom-small-input rounded-0 PaymentsReceived" id="PaymentsReceived_${i + 1}" value="${data[i].paymentsReceived}" type="text" name="PaymentsReceived"  />
                </div>
                <div class="col-lg-2">
                    <label> </label>
                    <input class="form-control form-control-sm rounded-0 custom-select-input date-picker PaymentsReceivedDate" id="PaymentsReceivedDate_${i + 1}" type="text" name="PaymentsReceivedDate" value="${formattedDate}" /> 
                </div>               
                <div class="col-lg-2">
                    <br/>
                    <select id="InvoiceasPaidId_${i + 1}" class="form-select select2 InvoiceasPaidId" data-id="" data-control="select2"></select>
                </div>
            </div>
        `;
                  
                    $(".AddPaymentReceivedDiv").append(newTimelineItem);
                   
                 
                    
                   
                    $('#InvoiceasPaidId_' + (i + 1)).append('<option value="1">Cheque</option>');
                    $('#InvoiceasPaidId_' + (i + 1)).append('<option value="2">Cash</option>');
                    $('#InvoiceasPaidId_' + (i + 1)).append('<option value="3">CreditCard</option>');
                    $('#InvoiceasPaidId_' + (i + 1)).append('<option value="4">BankTransfers</option>');
                    $('#InvoiceasPaidId_' + (i + 1)).select2({
                        width: '100%'
                    });
                   
                    $("#InvoiceasPaidId_" + (i + 1)).val(data[i].paymentMethodId).trigger("change");
                    //$('#PaymentsReceivedDate_' + (i + 1)).daterangepicker({
                    //    singleDatePicker: true,
                    //    locale: abp.localization.currentLanguage.name,
                    //    format: 'L'
                    //});
                }
             
            }
            handleDateInputMouseDown();
  
               
          
          
            
        }
     
        function handleDateInputMouseDown() {
            debugger
            $('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });
        }

        function updateinvoicetable(data) {
            debugger
            var invoiceDetails = data.result.invoiceDetail;
            for (var i = 0; i < data.result.invoiceDetail.length; i++) {
                var invoiceDetail = invoiceDetails[i];             
                var srCount = document.getElementById("Invoicetable").rows.length;
                var adddatatotable =
                    "<tr class='trq_" + srCount + "'>" +
                    "<td style='display: none;'><input id='DId_" + srCount + "' type='number' placeholder='' value='" + parseFloat(invoiceDetail.id).toLocaleString() + "' class='form-control border-0 input-sm DId' /></td>" +
                    "<td><textarea type='text' placeholder='Description' class='form-control border-0 input-sm Description'>" + invoiceDetail.description  + "</textarea></td>" +
                    "<td><input id='TotalFee_" + srCount + "' type='number' placeholder='' value='" + parseFloat(invoiceDetail.totalFee).toLocaleString() + "' class='form-control border-0 input-sm TotalFee' /></td>" +
                    "<td><select id='IncomeType_" + srCount + "' class='form-control border-0 input-sm IncomeType'></select></td>" +
                    "<td><input id='CommissionPer_" + srCount + "' type='number' placeholder='' value='" + parseFloat(invoiceDetail.commissionPercent).toLocaleString() + "' class='form-control border-0 input-sm CommissionPer' /></td>" +
                    "<td><input id='CommissionAmount_" + srCount + "' type='number' placeholder='' value='" + parseFloat(invoiceDetail.commissionAmount).toLocaleString() + "' class='form-control border-0 input-sm CommissionAmount' /></td>" +
                    "<td><input id='Amount_" + srCount + "' type='number' placeholder='' value='" + parseFloat(invoiceDetail.amount).toLocaleString() + "' class='form-control border-0 input-sm Amount' /></td>" +
                    "<td><select id='Tax_" + srCount + "' class='form-control border-0 input-sm Tax'></select></td>" +
                    "<td><input id='TaxAmount_" + srCount + "' type='number' placeholder='' value='" + parseFloat(invoiceDetail.taxAmount).toLocaleString() + "' class='form-control border-0 input-sm TaxAmount' /></td>" +
                    "<td><input id='NetAmount_" + srCount + "' type='number' placeholder='' value='" + parseFloat(invoiceDetail.netAmount).toLocaleString() + "' class='form-control border-0 input-sm NetAmount' /></td>" +
                    "<td><span class='Delete-icon delete' style='cursor: pointer; margin-left: 5px;'><i class='fa fa-trash' style='font-size: 10px;'></i></span></td>" +
                    "</tr>";
                $("#Invoicetable").append(adddatatotable);
                if (invoicetype === "1") {
                    $(".IncomeType").parent("td").remove();
                    $(".Amount").parent("td").remove();
                }
                else if (invoicetype === "2") {
                    $(".IncomeType").parent("td").remove();
                    $(".Amount").parent("td").remove();
                }
                else {
                    $(".TotalFee").parent("td").remove();
                    $(".CommissionPer").parent("td").remove();
                    $(".CommissionAmount").parent("td").remove();
                    var incomeTypeddl = "IncomeType_" + srCount
                    GetAllincomeTypeForTableDropdown(incomeTypeddl, invoiceDetail.incomeType);
                }
                
                var ddl = "Tax_" + srCount
                GetAllTaxForTableDropdown(ddl, invoiceDetail.tax);

            }
            
        }
       
        var globalData; // Declare the data variable in a broader scope

        function createCard(item, workflowName, countryName) {
            // Create the main container for the card
            var mainDiv = $('<div>').addClass('maincard maindivcard');

            // Create the card
            var cardDiv = $('<div>').addClass('card').css('background-color', '#f0f0f0'); // Set your desired background color

            // Create the card body
            var cardBodyDiv = $('<div>').addClass('card-body');

            // Create the card title
            var cardTitle = $('<h5>').addClass('card-title').html("Partner Details <hr>");

            // Create the paragraph for card information
            var infoParagraph = $('<p>').addClass('card-text clientName').html('<strong>Name:</strong> ' + item.partnerName + '<br>');
            var infoParagraph1 = $('<p>').addClass('card-text clientEmail').html('<strong>Address:</strong> ' + item.street +','+ item.city +','+item.state +','+ item.zipCode +','+ countryName);
            var infoParagraph2 = $('<p>').addClass('card-text clientEmail').html('<strong>Contact</strong> ' + item.phoneNo);
            var infoParagraph3 = $('<p>').addClass('card-text clientEmail').html('<strong>Service:</strong> ' + workflowName);
            $("#PartnerId").val(item.id);
            $("#PartnerName").val(item.partnerName);
            $("#PartnerContact").val(item.phoneNo);
            $("#PartnerService").val(workflowName);
            $("#PartnerAddress").val(item.street + ',' + item.city + ',' + item.state + ',' + item.zipCode + ',' + countryName);

            // Append card title and info paragraph to the card body
            cardBodyDiv.append(cardTitle, infoParagraph, infoParagraph1, infoParagraph2, infoParagraph3);

            // Append card body to the card
            cardDiv.append(cardBodyDiv);

            // Append card to the mainDiv
            mainDiv.append(cardDiv);

            var clientname = item.partnerName;
            var clientEmail = item.email;
            $("input[name='ClientEmail']").val(clientEmail);
            $("#ClientName").val(clientname);
            // Return the created card
            return mainDiv;

        }
        function createClientCard(workflowName, partnerPartnerName, productName, name, branchName, dateofBirth) {
            // Create the main container for the card
            var mainDiv = $('<div>').addClass('maincard maindivcard');

            // Create the card
            var cardDiv = $('<div>').addClass('card').css('background-color', '#f0f0f0'); // Set your desired background color

            // Create the card body
            var cardBodyDiv = $('<div>').addClass('card-body');

            // Create the card title
            var cardTitle = $('<h5>').addClass('card-title').html("Clients Details <hr>");

            // Create the paragraph for card information
            var infoParagraph = $('<p>').addClass('card-text clientName').html('<strong>Name:</strong> ' + name + '<br>');
            var infoParagraph1 = $('<p>').addClass('card-text clientEmail').html('<strong>Partner:</strong> ' + partnerPartnerName);
            var infoParagraph2 = $('<p>').addClass('card-text clientEmail').html('<strong>product</strong> ' + productName);
            var infoParagraph3 = $('<p>').addClass('card-text clientEmail').html('<strong>Branch:</strong> ' + branchName);
            var infoParagraph4 = $('<p>').addClass('card-text clientEmail').html('<strong>WorkFlow:</strong> ' + workflowName);
            var infoParagraph5 = $('<p>').addClass('card-text clientEmail').html('<strong>DOB:</strong> ' + renderDateTime(dateofBirth));
            $("#ClientName").val(name);
            $("#Product").val(productName);
            $("#Branch").val(branchName);
            $("#Workflow").val(workflowName);
            $("#dateofBirth").val(dateofBirth);
            function renderDateTime(dueDate) {
                let formattedDate = dueDate ? moment(dueDate).format('L') : "";
                return formattedDate;
            }

            // Append card title and info paragraph to the card body
            cardBodyDiv.append(cardTitle, infoParagraph, infoParagraph5, infoParagraph1, infoParagraph2, infoParagraph3, infoParagraph4);

            // Append card body to the card
            cardDiv.append(cardBodyDiv);

            // Append card to the mainDiv
            mainDiv.append(cardDiv);

            //var clientname = item.partnerName;
            //var clientEmail = item.email;
            //$("input[name='ClientEmail']").val(clientEmail);
            //$("#ClientName").val(clientname);
            // Return the created card
            return mainDiv;

        }







        function getIdPartnerAndClient(ApplicationIdValue) {


          $.ajax({
              url: abp.appPath + 'api/services/app/Applications/GetAll',
                data: {
                    ApplicationIdFilter: ApplicationIdValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    //debugger
                    clientid = data.result.items[0].application.clientId;
                    PartnerId = data.result.items[0].application.partnerId;
                    //globalData = data;
                    if (invoicetype === "2" || invoicetype === "1") {
                        getPartner(PartnerId);
                        processClientData(data);
                    }
                    else {
                        processApplication(data);
                        processClientApplicationdgross(data);
                    }
                    
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        //function getClient(clientid) {
        //    $.ajax({
        //        url: abp.appPath + 'api/services/app/Clients/GetClientForView',
        //        data: {
        //            id: clientid,
        //        },
        //        method: 'GET',
        //        dataType: 'json',
        //    })
        //        .done(function (data) {
        //            //debugger

        //            globalData = data;
        //            processClientData(globalData);

        //        })
        //        .fail(function (error) {
        //            console.error('Error fetching data:', error);
        //        });
        //}
        function getPartner(PartnerId) {


            $.ajax({
                url: abp.appPath + 'api/services/app/Partners/GetPartnerForView',
                data: {
                    id: PartnerId,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    //debugger
 
                    globalData = data; 
                    processData(globalData);
                   
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function processClientData(globalData) {
            //debugger
            var cardContainer = $('#cardContainerInvoiceClient');
            // or replace '#container' with your actual container selector..
            ///var item = globalData.result.client;
            var workflowName = globalData.result.items[0].workflowName;
            var partnerPartnerName = globalData.result.items[0].partnerPartnerName;
            var productName = globalData.result.items[0].productName;
            var name = globalData.result.items[0].application.name;
            var branchName = globalData.result.items[0].branchName || "";
            var DateofBirth = globalData.result.items[0].dateofBirth || "";
            $("#ClientId").val(globalData.result.items[0].application.clientId);
            $("#ApplicationId").val(globalData.result.items[0].application.id);
            var card = createClientCard(workflowName, partnerPartnerName, productName, name, branchName, DateofBirth);
            cardContainer.append(card);

        }
        function processData(data) {
            //debugger
            var cardContainer = $('#cardContainerInvoicePartner'); // or replace '#container' with your actual container selector
            var item = globalData.result.partner;
            var workflowName = globalData.result.workflowName;
            var countryName = globalData.result.countryName;
            var card = createCard(item, workflowName, countryName);
            cardContainer.append(card);
           
        }
        function processApplication(globalData) {
            //debugger
            var cardContainer = $('#cardContainerInvoiceClient');
            // or replace '#container' with your actual container selector..
            ///var item = globalData.result.client;
            var workflowName = globalData.result.items[0].workflowName;
            var partnerPartnerName = globalData.result.items[0].partnerPartnerName;
            var productName = globalData.result.items[0].productName;
            var branchName = globalData.result.items[0].branchName || "";
            var applicationowner = globalData.result.items[0].clientAssigneeName || "";
            $("#ClientId").val(globalData.result.items[0].application.clientId);
            $("#ApplicationId").val(globalData.result.items[0].application.id);
            var card = createApplicationCard(workflowName, partnerPartnerName, productName, branchName, applicationowner);
            cardContainer.append(card);

        }
        function processClientApplicationdgross(globalData) {
            //debugger
            var cardContainer = $('#cardContainerInvoicePartner');
            // or replace '#container' with your actual container selector..
            ///var item = globalData.result.client;
            var assignee = globalData.result.items[0].clientAssigneeName;
            var name = globalData.result.items[0].application.name;
            var email = globalData.result.items[0].email || "";
            var DateofBirth = globalData.result.items[0].dateofBirth || "";
            $("#ClientId").val(globalData.result.items[0].application.clientId);
            $("#ApplicationId").val(globalData.result.items[0].application.id);
            var card = createClientApplicationCard(name, email, DateofBirth, assignee);
            cardContainer.append(card);

        }
        function createApplicationCard(workflowName, partnerPartnerName, productName, branchName, applicationowner) {
            // Create the main container for the card
            var mainDiv = $('<div>').addClass('maincard maindivcard');

            // Create the card
            var cardDiv = $('<div>').addClass('card').css('background-color', '#f0f0f0'); // Set your desired background color

            // Create the card body
            var cardBodyDiv = $('<div>').addClass('card-body');

            // Create the card title
            var cardTitle = $('<h5>').addClass('card-title').html("Application Details <hr>");

            // Create the paragraph for card information
            var infoParagraph = $('<p>').addClass('card-text clientName').html('<strong>Service:</strong> ' + workflowName + '<br>');
            var infoParagraph1 = $('<p>').addClass('card-text clientEmail').html('<strong>Partner:</strong> ' + partnerPartnerName + '<br>' + '(' + branchName+')');
            var infoParagraph2 = $('<p>').addClass('card-text clientEmail').html('<strong>Product:</strong> ' + productName);
            var infoParagraph3 = $('<p>').addClass('card-text clientEmail').html('<strong>ApplicationOwner:</strong> ' + applicationowner);
            var infoParagraph4 = `<a href="#" id="ApplicationCardLink" style="color: blue;">Edit Application details</a>`
            $("#PartnerName").val(partnerPartnerName);
            $("#Branch").val(branchName);
            $("#Product").val(productName);
            $("#Workflow").val(workflowName);
            $("#ApplicationOwner").val(applicationowner);
            //$("#ClientName").val(name);
            //$("#dateofBirth").val(dateofBirth);
            //$("#ClientAssignee").val(assignee);
            function renderDateTime(dueDate) {
                let formattedDate = dueDate ? moment(dueDate).format('L') : "";
                return formattedDate;
            }

            // Append card title and info paragraph to the card body
            cardBodyDiv.append(cardTitle, infoParagraph, infoParagraph1, infoParagraph2, infoParagraph3, infoParagraph4);

            // Append card body to the card
            cardDiv.append(cardBodyDiv);

            // Append card to the mainDiv
            mainDiv.append(cardDiv);

            //var clientname = item.partnerName;
            //var clientEmail = item.email;
            //$("input[name='ClientEmail']").val(clientEmail);
            //$("#ClientName").val(clientname);
            // Return the created card
            return mainDiv;

        }
        function createClientApplicationCard(name, email, DateofBirth, assignee) {
            // Create the main container for the card
            var mainDiv = $('<div>').addClass('maincard maindivcard');

            // Create the card
            var cardDiv = $('<div>').addClass('card').css('background-color', '#f0f0f0'); // Set your desired background color

            // Create the card body
            var cardBodyDiv = $('<div>').addClass('card-body');

            // Create the card title
            var cardTitle = $('<h5>').addClass('card-title').html("Clients Details <hr>");

            // Create the paragraph for card information
            var infoParagraph = $('<p>').addClass('card-text clientName').html('<strong>Name:</strong> ' + name + '<br>');
            var infoParagraph1 = $('<p>').addClass('card-text clientEmail').html('<strong>Email Address:</strong> ' + email);
            var infoParagraph5 = $('<p>').addClass('card-text clientEmail').html('<strong>DOB:</strong> ' + renderDateTime(DateofBirth));
            var infoParagraph2 = $('<p>').addClass('card-text clientEmail').html('<strong>Assignee:</strong> ' + assignee);
            $("#ClientName").val(name);
            $("#dateofBirth").val(dateofBirth);
            $("#ClientAssignee").val(assignee);
            function renderDateTime(dueDate) {
                let formattedDate = dueDate ? moment(dueDate).format('L') : "";
                return formattedDate;
            }

            // Append card title and info paragraph to the card body
            cardBodyDiv.append(cardTitle, infoParagraph, infoParagraph1, infoParagraph5,  infoParagraph2);

            // Append card body to the card
            cardDiv.append(cardBodyDiv);

            // Append card to the mainDiv
            mainDiv.append(cardDiv);

            //var clientname = item.partnerName;
            //var clientEmail = item.email;
            //$("input[name='ClientEmail']").val(clientEmail);
            //$("#ClientName").val(clientname);
            // Return the created card
            return mainDiv;

        }
        var _createOrEditApplicationModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateInvoiceTypeModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Invoice/_InvoiceTypeModal.js',
            modalClass: 'CreateOrEditInvoiceTypeModal',
        });
        var _clientsInvoiceService = abp.services.app.invoiceHead;
        $("#kt_app_sidebar_toggle").trigger("click");
        $('#currencyId').select2({
            width: '100%',
            // Adjust the width as needed..
        });
        //const urlParams = new URLSearchParams(window.location.search);
        //const clientIdValue = urlParams.get('clientId');..
       // $("#ClientId").val(partnerIdValue);

        var _$clientInvoiceInformationForm = $('form[name=InvoiceInformationsForm]');
        _$clientInvoiceInformationForm.validate();

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

            var client = _$clientInvoiceInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryId, displayName: client.countryName }, function (data) {
                _$clientInvoiceInformationForm.find('input[name=countryName]').val(data.displayName);
                _$clientInvoiceInformationForm.find('input[name=countryId]').val(data.id);
            });
        });

        $('#ClearCountryNameButton').click(function () {
            _$clientInvoiceInformationForm.find('input[name=countryName]').val('');
            _$clientInvoiceInformationForm.find('input[name=countryId]').val('');
        });

        $('#OpenUserLookupTableButton').click(function () {

            var client = _$clientInvoiceInformationForm.serializeFormToObject();

            _ClientuserLookupTableModal.open({ id: client.assigneeId, displayName: client.userName }, function (data) {
                _$clientInvoiceInformationForm.find('input[name=userName]').val(data.displayName);
                _$clientInvoiceInformationForm.find('input[name=assigneeId]').val(data.id);
            });
        });

        $('#ClearUserNameButton').click(function () {
            _$clientInvoiceInformationForm.find('input[name=userName]').val('');
            _$clientInvoiceInformationForm.find('input[name=assigneeId]').val('');
        });

        $('#OpenBinaryObjectLookupTableButton').click(function () {

            var client = _$clientInvoiceInformationForm.serializeFormToObject();

            _ClientbinaryObjectLookupTableModal.open({ id: client.profilePictureId, displayName: client.binaryObjectDescription }, function (data) {
                _$clientInvoiceInformationForm.find('input[name=binaryObjectDescription]').val(data.displayName);
                _$clientInvoiceInformationForm.find('input[name=profilePictureId]').val(data.id);
            });
        });

        $('#ClearBinaryObjectDescriptionButton').click(function () {
            _$clientInvoiceInformationForm.find('input[name=binaryObjectDescription]').val('');
            _$clientInvoiceInformationForm.find('input[name=profilePictureId]').val('');
        });

        $('#OpenDegreeLevelLookupTableButton').click(function () {

            var client = _$clientInvoiceInformationForm.serializeFormToObject();

            _ClientdegreeLevelLookupTableModal.open({ id: client.highestQualificationId, displayName: client.degreeLevelName }, function (data) {
                _$clientInvoiceInformationForm.find('input[name=degreeLevelName]').val(data.displayName);
                _$clientInvoiceInformationForm.find('input[name=highestQualificationId]').val(data.id);
            });
        });

        $('#ClearDegreeLevelNameButton').click(function () {
            _$clientInvoiceInformationForm.find('input[name=degreeLevelName]').val('');
            _$clientInvoiceInformationForm.find('input[name=highestQualificationId]').val('');
        });

        $('#OpenSubjectAreaLookupTableButton').click(function () {

            var client = _$clientInvoiceInformationForm.serializeFormToObject();

            _ClientsubjectAreaLookupTableModal.open({ id: client.studyAreaId, displayName: client.subjectAreaName }, function (data) {
                _$clientInvoiceInformationForm.find('input[name=subjectAreaName]').val(data.displayName);
                _$clientInvoiceInformationForm.find('input[name=studyAreaId]').val(data.id);
            });
        });

        $('#ClearSubjectAreaNameButton').click(function () {
            _$clientInvoiceInformationForm.find('input[name=subjectAreaName]').val('');
            _$clientInvoiceInformationForm.find('input[name=studyAreaId]').val('');
        });

        $('#OpenLeadSourceLookupTableButton').click(function () {

            var client = _$clientInvoiceInformationForm.serializeFormToObject();

            _ClientleadSourceLookupTableModal.open({ id: client.leadSourceId, displayName: client.leadSourceName }, function (data) {
                _$clientInvoiceInformationForm.find('input[name=leadSourceName]').val(data.displayName);
                _$clientInvoiceInformationForm.find('input[name=leadSourceId]').val(data.id);
            });
        });

        $('#ClearLeadSourceNameButton').click(function () {
            _$clientInvoiceInformationForm.find('input[name=leadSourceName]').val('');
            _$clientInvoiceInformationForm.find('input[name=leadSourceId]').val('');
        });

        $('#OpenCountry2LookupTableButton').click(function () {

            var client = _$clientInvoiceInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.passportCountryId, displayName: client.countryName2 }, function (data) {
                _$clientInvoiceInformationForm.find('input[name=countryName2]').val(data.displayName);
                _$clientInvoiceInformationForm.find('input[name=passportCountryId]').val(data.id);
            });
        });

        $('#ClearCountryName2Button').click(function () {
            _$clientInvoiceInformationForm.find('input[name=countryName2]').val('');
            _$clientInvoiceInformationForm.find('input[name=passportCountryId]').val('');
        });




        function save(successCallback) {
            if (!_$clientInvoiceInformationForm.valid()) {
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

            ;
            var datarows = [];

            // Assuming you have some way of determining the number of rows you want to save, let's say 'rowCount'..
            //var rowCount = $(".workflowsId").length;
            var rowCount = document.getElementById("Invoicetable").rows.length;
            //debugger 
            for (var i = 1; i < rowCount; i++) {
                var row = document.getElementById("Invoicetable").rows[i]; // Get current row

                // Get values from elements within the current row
                var Description = row.getElementsByClassName("Description")[0].value || "";
                var TotalFeeElement = row.getElementsByClassName("TotalFee")[0];
                var TotalFee = TotalFeeElement ? parseFloat(TotalFeeElement.value) || 0 : 0;
                // TotalFee = parseFloat(row.getElementsByClassName("TotalFee")[0].value) || 0;
                //var CommissionPercent = parseInt(row.getElementsByClassName("CommissionPer")[0].value) || 0;
                var CommissionPercentElement = row.getElementsByClassName("CommissionPer")[0];
                var CommissionPercent = CommissionPercentElement ? parseFloat(CommissionPercentElement.value) || 0 : 0;
                var CommissionAmountElement = row.getElementsByClassName("CommissionAmount")[0];
                var CommissionAmount = CommissionAmountElement ? parseFloat(CommissionAmountElement.value) || 0 : 0;
                //var CommissionAmount = parseFloat(row.getElementsByClassName("CommissionAmount")[0].value) || 0;
                var Tax = parseInt(row.getElementsByClassName("Tax")[0].value) || 0;
                //var IncomeType = parseInt(row.getElementsByClassName("IncomeType")[0].value) || 0;
                var IncomeTypeElement = row.getElementsByClassName("IncomeType")[0];
                var IncomeType = IncomeTypeElement ? parseInt(IncomeTypeElement.value) || 0 : 0;
                var TaxAmount = parseFloat(row.getElementsByClassName("TaxAmount")[0].value) || 0;
                var NetAmount = parseInt(row.getElementsByClassName("NetAmount")[0].value) || 0;
                //var Amount = parseInt(row.getElementsByClassName("Amount")[0].value) || 0;
                var AmountElement = row.getElementsByClassName("Amount")[0];
                var Amount = AmountElement ? parseInt(AmountElement.value) || 0 : 0;
                var idElement = row.getElementsByClassName("DId")[0];
                var Id = idElement ? parseFloat(idElement.value) || 0 : 0;
                var TenantId = $("#TenantId").val() || 0;


                //var Description = $(".Description").val();
                //var TotalFee = parseFloat($(".TotalFee").val());
                //var CommissionPercent = parseInt($(".CommissionPer").val());
                //var CommissionAmount = parseFloat($(".CommissionAmount").val());
                //var Tax = parseInt($(".Tax").val());
                //var TaxAmount = parseFloat($(".TaxAmount").val());
                //var NetAmount = parseInt($(".NetAmount").val());
                //var Id = parseFloat($(".Id").val());

                var dataRowItem = {
                    Description: Description,
                    TotalFee: TotalFee,
                    CommissionPercent: CommissionPercent,
                    CommissionAmount: CommissionAmount,
                    Tax: Tax,
                    IncomeType: IncomeType,
                    Amount: Amount,
                    TaxAmount: TaxAmount,
                    NetAmount: NetAmount,
                    Id: Id,
                    TenantId: TenantId,
                };

                datarows.push(dataRowItem);
            }
            debugger
            var PaymentReceivedRows = [];
            var PaymentReceivedRowsCount = document.getElementsByClassName("PaymentsReceived").length+1;

            for (var i = 1; i < PaymentReceivedRowsCount; i++) {
                var row = document.getElementsByClassName("PaymentsReceived")[i]; // Get current row
                var PaymentsReceived = $("#PaymentsReceived_" + i).val() || 0;
                var PaymentsReceivedDate = $("#PaymentsReceivedDate_" + i).val() || 0;
                var PaymentMethodId = $("#InvoiceasPaidId_" + i).val() || 0;
                var MarkInvoicePaid = $("#markPaidCheckbox").is(":checked");
                var AddNotes = $("#markPaidTextarea").val() || "";
                var AttachmentId = $("#Attachments").text() || "";
                var Id = $("#PaymentReceivedId_"+i).val() || 0;
                var TenantId = $("#TenantId").val() || 0;


                var PaymentReceivedRowsItem = {
                    PaymentsReceived: PaymentsReceived,
                    PaymentsReceivedDate: PaymentsReceivedDate,
                    PaymentMethodId: PaymentMethodId,
                    MarkInvoicePaid: MarkInvoicePaid,
                    AddNotes: AddNotes,
                    AttachmentId: AttachmentId,                   
                    Id: Id,
                    TenantId: TenantId,
                };

                PaymentReceivedRows.push(PaymentReceivedRowsItem);
            }
            debugger
            var IncomeSharingRows = [];
            var OrganizationUnitId = $("#WorkFlowOfficeId").val() || 0;
            var IncomeSharing = $("#IncomeSharingAmount").val() || 0;
            var IsTax = $("#IncomeSharingCheckbox").is(":checked");
            var Tax = parseInt($("#TaxincomeSharingAmount").val()) || "";
            var taxAmountdecimal = $("#IncomeSharingTaxAmt").text().split(":");
            var TaxAmount = parseFloat(taxAmountdecimal[1]) || 0;
            var totaldecimal = $("#IncomeSharingAmtwithTax").text().split(":");
            var TotalIncludingTax = parseFloat(totaldecimal[1]) || 0;
            var Id = $("#IncomeSharingId").val() || 0;
            var TenantId = $("#TenantId").val() || 0;


              var IncomeSharingRowsItem = {
                  OrganizationUnitId: OrganizationUnitId,
                  IncomeSharing: IncomeSharing,
                  IsTax: IsTax,
                  Tax: Tax,
                  TaxAmount: TaxAmount,
                  TotalIncludingTax: TotalIncludingTax,
                  Id: Id,
                  TenantId: TenantId,
                };

            IncomeSharingRows.push(IncomeSharingRowsItem);

            var InvIncomeSharing = JSON.stringify(IncomeSharingRows);
            InvIncomeSharing = JSON.parse(InvIncomeSharing);

            var InvPaymentReceived = JSON.stringify(PaymentReceivedRows);
            InvPaymentReceived = JSON.parse(InvPaymentReceived);
            var taxRowCounter = $('.total').length;

            $("#ProductCount").val(taxRowCounter);
            var InvoiceDetail = JSON.stringify(datarows);
            InvoiceDetail = JSON.parse(InvoiceDetail);
            var Invoice = _$clientInvoiceInformationForm.serializeFormToObject();
            Invoice.InvoiceDetail = InvoiceDetail;
            Invoice.InvPaymentReceived = InvPaymentReceived;
            Invoice.InvIncomeSharing = InvIncomeSharing;


            abp.ui.setBusy();
            _clientsInvoiceService.createOrEdit(
                Invoice
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                //abp.event.trigger('app.createOrEditClientQuotationModalSaved');
                location.reload();
                if (typeof (successCallback) === 'function') {
                    successCallback();
                }
            }).always(function () {
                abp.ui.clearBusy();
            });
        };
        $(document).off("click", "#ApplicationCardLink").on("click", "#ApplicationCardLink", function (e) {
            //debugger
            e.preventDefault();
            _createOrEditApplicationModal.open();
  
        });
        $(document).off("change", "#DiscountGivenToClient").on("change", "#DiscountGivenToClient", function (e) {
            debugger
            e.preventDefault();
            if (invoicetype === "1") {
                var value = parseFloat($(this).val());
                if (isNaN(value)) {
                    value = 0;
                }
                //var fee = parseFloat($("#TotalFee").val());
                var feeString = $("#TotalFee").val().replace(/,/g, '');
                // Parse the value to float
                var fee = parseFloat(feeString);
                var Totalfee = parseFloat((fee - value).toFixed(2));
               
                var localizedTotalAmount = parseFloat(Totalfee).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#TotalFee").val(localizedTotalAmount)
                var paragraph = document.querySelector('#NetFeeReceivedCard');
                paragraph.textContent = localizedTotalAmount;
                CalculateFee();
            }
            else if (invoicetype === "2") {
                var value = parseFloat($(this).val());
                //debugger
                if (isNaN(value)) {
                    value = 0;
                }
                var tax = parseFloat($("#Tax").val());
                var amountinctax = parseFloat($("#TotalAmountInclTax").val());
                var fee = parseFloat((amountinctax - tax).toFixed(2));
                var Totalfee = parseFloat((fee - value).toFixed(2));
                if (isNaN(Totalfee)) {
                    Totalfee = 0;
                }
                CalculateNetFeePaidToPartner();
                //var localizedTotalAmount = parseFloat(Totalfee).toLocaleString(undefined, { minimumFractionDigits: 2 });
                //var paragraph = document.querySelector('#NetRevenueCard');
                //paragraph.textContent = localizedTotalAmount;
            }
        });
        $("#Invoicetable").on('change', '.Amount', function () {
            //debugger
            if (invoicetype === "3") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Amount = parseFloat($CalAmount.find(".Amount").val());                
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * Amount;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } 

                var NetFee = parseFloat(Amount +TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                CalculateFee();
            }
          
        });
        $("#Invoicetable").on('change', '.TotalFee', function () {
            //debugger
            if (invoicetype === "1") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(fee - CalSaleTaxDis - TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else if (invoicetype === "2") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(TaxAmount + CalSaleTaxDis).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                //var paragraph = document.querySelector('#NetRevenueCard');
                //paragraph.textContent = totalrevenue;
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
        });
        $("#Invoicetable").on('change', '.CommissionPer', function () {
            //debugger
            if (invoicetype === "1") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(fee - CalSaleTaxDis - TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else if (invoicetype === "2") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(TaxAmount + CalSaleTaxDis).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                //var paragraph = document.querySelector('#NetRevenueCard');
                //paragraph.textContent = totalrevenue;
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
        });
        $("#Invoicetable").on('change', '.CommissionAmount', function () {
            //debugger
            if (invoicetype === "1") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(fee - CalSaleTaxDis - TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else if (invoicetype === "2") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(TaxAmount + CalSaleTaxDis).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                //var paragraph = document.querySelector('#NetRevenueCard');
                //paragraph.textContent = totalrevenue;
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
        });
        $("#Invoicetable").on('change', '.Tax', function () {
            //debugger
            if (invoicetype === "1") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(fee - CalSaleTaxDis - TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                var paragraph = document.querySelector('#NetIncomeCard');
                paragraph.textContent = totalrevenue;
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else if (invoicetype === "2") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(TaxAmount + CalSaleTaxDis).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                //var paragraph = document.querySelector('#NetRevenueCard');
                //paragraph.textContent = totalrevenue;
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Amount = parseFloat($CalAmount.find(".Amount").val());
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * Amount;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                }

                var NetFee = parseFloat(Amount + TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                CalculateFee();
            }
        });
        $("#Invoicetable").on('change', '.TaxAmount', function () {
            //debugger
            if (invoicetype === "1") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(fee - CalSaleTaxDis - TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else if (invoicetype === "2") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(TaxAmount + CalSaleTaxDis).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                //var paragraph = document.querySelector('#NetRevenueCard');
                //paragraph.textContent = totalrevenue;
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Amount = parseFloat($CalAmount.find(".Amount").val());
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * Amount;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                }

                var NetFee = parseFloat(Amount + TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                CalculateFee();
            }
        });
        $("#Invoicetable").on('change', '.NetAmount', function () {
            //debugger
            if (invoicetype === "1") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(fee - CalSaleTaxDis - TaxAmount).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else if (invoicetype === "2") {
                var $CalAmount = $(this).closest('tr');
                var fee = parseFloat($CalAmount.find(".TotalFee").val());
                var Percent = parseFloat($CalAmount.find(".CommissionPer").val());
                var CalSaleTaxDis = Percent / 100 * fee;
                var CommissionAmount = $CalAmount.find(".CommissionAmount").val(parseFloat(CalSaleTaxDis).toFixed(2));
                var Tax = $CalAmount.find(".Tax option:selected").text();
                var textWord = Tax.split('(');
                var textamt = textWord[1].split("%");
                var textFinalAmount = parseFloat(textamt[0]);
                var TaxAmount = textFinalAmount / 100 * CalSaleTaxDis;
                $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                if (isNaN(fee)) {
                    fee = 0;
                }
                if (isNaN(Tax)) {
                    Tax = 0
                } if (isNaN(TaxAmount)) {
                    TaxAmount = 0
                } if (isNaN(CommissionAmount)) {
                    CommissionAmount = 0
                }

                var NetFee = parseFloat(TaxAmount + CalSaleTaxDis).toFixed(2);
                $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                if (isNaN(NetFee)) {
                    NetFee = 0;
                }
                //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                //var paragraph = document.querySelector('#NetRevenueCard');
                //paragraph.textContent = totalrevenue;
                //$CalAmount.find(".total").text(netamount);
                CalculateFee();
            }
            else {
                    var $CalAmount = $(this).closest('tr');
                    var fee = parseFloat($CalAmount.find(".TotalFee").val());
                    var Amount = parseFloat($CalAmount.find(".Amount").val());
                    var Tax = $CalAmount.find(".Tax option:selected").text();
                    var textWord = Tax.split('(');
                    var textamt = textWord[1].split("%");
                    var textFinalAmount = parseFloat(textamt[0]);
                    var TaxAmount = textFinalAmount / 100 * Amount;
                    $CalAmount.find(".TaxAmount").val(parseFloat(TaxAmount).toFixed(2));

                    if (isNaN(fee)) {
                        fee = 0;
                    }
                    if (isNaN(Tax)) {
                        Tax = 0
                    } if (isNaN(TaxAmount)) {
                        TaxAmount = 0
                    }

                    var NetFee = parseFloat(Amount + TaxAmount).toFixed(2);
                    $CalAmount.find(".NetAmount").val(parseFloat(NetFee).toFixed(2));
                    if (isNaN(NetFee)) {
                        NetFee = 0;
                    }
                    CalculateFee();
                
            }
        });
        $(document).off("click", "#IncomeSharingCheckbox").on("click", "#IncomeSharingCheckbox", function (e) {

            debugger
            if (invoicetype === "1") {
                if ($(this).prop("checked")) {
                    $("#TaxincomeSharingAmount").prop("disabled", false);

                    var incomeSharing = $("#IncomeSharingAmount").val();
                    var commission = $("#CommissionClaimed").val();
                    var incomesharingAmount = parseFloat(commission - incomeSharing).toFixed(2);
                    var paragraph = document.querySelector('#NetIncomeCard');
                    paragraph.textContent = incomesharingAmount;
                    $("#NetIncome").val(incomesharingAmount)
                    if ($("#IncomeSharingCheckbox").prop("checked")) {
                        var Tax = $("#TaxincomeSharingAmount option:selected").text();
                        var textWord = Tax.split('(');
                        var textamt = textWord[1].split("%");
                        var textFinalAmount = parseFloat(textamt[0]);
                        var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                        if (isNaN(TaxAmount)) {
                            TaxAmount = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                        var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                    else {
                        var IncomeSharingAmtwithTax = parseFloat(incomeSharing).toFixed(2)
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }

                }
                else {
                    $("#TaxincomeSharingAmount").prop("disabled", true);
                    var incomeSharing = $("#IncomeSharingAmount").val();
                    var commission = $("#CommissionClaimed").val();
                    var incomesharingAmount = parseFloat(commission - incomeSharing).toFixed(2);
                    var paragraph = document.querySelector('#NetIncomeCard');
                    paragraph.textContent = incomesharingAmount;
                    $("#NetIncome").val(incomesharingAmount)

                    if ($("#IncomeSharingCheckbox").prop("checked")) {
                        var Tax = $("#TaxincomeSharingAmount option:selected").text();
                        var textWord = Tax.split('(');
                        var textamt = textWord[1].split("%");
                        var textFinalAmount = parseFloat(textamt[0]);
                        var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                        if (isNaN(TaxAmount)) {
                            TaxAmount = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);

                        var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                    else {
                        var IncomeSharingAmtwithTax = parseFloat(incomeSharing)
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount" + 0);
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                }
            }
            else if (invoicetype === "2") {
                if ($(this).prop("checked")) {
                    $("#TaxincomeSharingAmount").prop("disabled", false);

                    var incomeSharing = $("#IncomeSharingAmount").val();
                    var commission = $("#CommissionClaimed").val();
                    var discount = $("#DiscountGivenToClient").val();
                    var commissionwithdiscount = parseFloat(commission - discount)
                    var incomesharingAmount = parseFloat(commissionwithdiscount - incomeSharing).toFixed(2);
                    var incomesharingdecimal = parseFloat(commissionwithdiscount - incomeSharing);
                    var paragraph = document.querySelector('#NetRevenueCard');
                    paragraph.textContent = incomesharingAmount;
                    $("#TotalRevenue").val(incomesharingdecimal)
                    if ($("#IncomeSharingCheckbox").prop("checked")) {
                        var Tax = $("#TaxincomeSharingAmount option:selected").text();
                        var textWord = Tax.split('(');
                        var textamt = textWord[1].split("%");
                        var textFinalAmount = parseFloat(textamt[0]);
                        var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                        if (isNaN(TaxAmount)) {
                            TaxAmount = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                        var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                    else {
                        var IncomeSharingAmtwithTax = parseFloat(incomeSharing).toFixed(2)
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }

                }
                else {
                    debugger
                    $("#TaxincomeSharingAmount").prop("disabled", true);
                    var incomeSharing = $("#IncomeSharingAmount").val();
                    var commission = $("#CommissionClaimed").val();
                    var discount = $("#DiscountGivenToClient").val();
                    var commissionwithdiscount = parseFloat(commission - discount)
                    var incomesharingAmount = parseFloat(commissionwithdiscount - incomeSharing).toFixed(2);
                    var incomesharingdecimal = parseFloat(commissionwithdiscount - incomeSharing);
                    var paragraph = document.querySelector('#NetRevenueCard');
                    paragraph.textContent = incomesharingAmount;
                    $("#TotalRevenue").val(incomesharingdecimal)

                    if ($("#IncomeSharingCheckbox").prop("checked")) {
                        var Tax = $("#TaxincomeSharingAmount option:selected").text();
                        var textWord = Tax.split('(');
                        var textamt = textWord[1].split("%");
                        var textFinalAmount = parseFloat(textamt[0]);
                        var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                        if (isNaN(TaxAmount)) {
                            TaxAmount = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);

                        var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                    else {
                        debugger
                        var IncomeSharingAmtwithTax = parseFloat(incomeSharing)
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount" + 0);
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                }
            }
            else if (invoicetype === "3") {
                if ($(this).prop("checked")) {
                    $("#TaxincomeSharingAmount").prop("disabled", false);
                    var incomeSharing = $("#IncomeSharingAmount").val();
                    var incomesharingAmount = parseFloat(incomeSharing).toFixed(2);
                    if ($("#IncomeSharingCheckbox").prop("checked")) {
                        var Tax = $("#TaxincomeSharingAmount option:selected").text();
                        var textWord = Tax.split('(');
                        var textamt = textWord[1].split("%");
                        var textFinalAmount = parseFloat(textamt[0]);
                        var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                        if (isNaN(TaxAmount)) {
                            TaxAmount = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                        var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                    else {
                        var IncomeSharingAmtwithTax = parseFloat(incomeSharing).toFixed(2)
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                }
                else {
                    debugger
                    $("#TaxincomeSharingAmount").prop("disabled", true);
                    var incomeSharing = $("#IncomeSharingAmount").val();
                    var incomesharingAmount = parseFloat(incomeSharing).toFixed(2);
                    if ($("#IncomeSharingCheckbox").prop("checked")) {
                        var Tax = $("#TaxincomeSharingAmount option:selected").text();
                        var textWord = Tax.split('(');
                        var textamt = textWord[1].split("%");
                        var textFinalAmount = parseFloat(textamt[0]);
                        var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                        if (isNaN(TaxAmount)) {
                            TaxAmount = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);

                        var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                    else {
                        var IncomeSharingAmtwithTax = parseFloat(incomeSharing)
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount" + 0);
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                
                }
            }

        });
        $(document).off("click", "#IncomeSharingAmount").on("change", "#IncomeSharingAmount", function (e) {
            debugger
           
                //$("#TaxincomeSharingAmount").prop("disabled", false)..;
            if (invoicetype === "1") {
                var incomeSharing = $("#IncomeSharingAmount").val();
                var commission = $("#CommissionClaimed").val();
                var incomesharingAmount = parseFloat(commission - incomeSharing).toFixed(2);
                var paragraph = document.querySelector('#NetIncomeCard');
                paragraph.textContent = incomesharingAmount;
                $("#NetIncome").val(incomesharingAmount)
                if ($("#IncomeSharingCheckbox").prop("checked")) {
                    var Tax = $("#TaxincomeSharingAmount option:selected").text();
                    var textWord = Tax.split('(');
                    var textamt = textWord[1].split("%");
                    var textFinalAmount = parseFloat(textamt[0]);
                    var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                    if (isNaN(TaxAmount)) {
                        TaxAmount = 0;
                    }
                    $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                    var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }
                else {
                    var IncomeSharingAmtwithTax = parseFloat(incomeSharing)
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }

            }
            else if (invoicetype === "2") {



                var incomeSharing = $("#IncomeSharingAmount").val();
                var commission = $("#CommissionClaimed").val();
                var discount = $("#DiscountGivenToClient").val();
                var commissionwithdiscount = parseFloat(commission - discount)
                var incomesharingAmount = parseFloat(commissionwithdiscount - incomeSharing).toFixed(2);
                var paragraph = document.querySelector('#NetRevenueCard');
                paragraph.textContent = incomesharingAmount;
                $("#TotalRevenue").val(incomesharingAmount)
                if ($("#IncomeSharingCheckbox").prop("checked")) {
                    var Tax = $("#TaxincomeSharingAmount option:selected").text();
                    var textWord = Tax.split('(');
                    var textamt = textWord[1].split("%");
                    var textFinalAmount = parseFloat(textamt[0]);
                    var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                    if (isNaN(TaxAmount)) {
                        TaxAmount = 0;
                    }
                    $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                    var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }
                else {
                    var IncomeSharingAmtwithTax = parseFloat(incomeSharing).toFixed(2)
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }


            }
            else if (invoicetype === "3") {
                var incomeSharing = $("#IncomeSharingAmount").val();
                var commission = $("#CommissionClaimed").val();               
                if ($("#IncomeSharingCheckbox").prop("checked")) {
                    var Tax = $("#TaxincomeSharingAmount option:selected").text();
                    var textWord = Tax.split('(');
                    var textamt = textWord[1].split("%");
                    var textFinalAmount = parseFloat(textamt[0]);
                    var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                    if (isNaN(TaxAmount)) {
                        TaxAmount = 0;
                    }
                    $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                    var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }
                else {
                    var IncomeSharingAmtwithTax = parseFloat(incomeSharing).toFixed(2)
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }

            }
           
           

        });
        $(document).off("click", "#TaxincomeSharingAmount").on("change", "#TaxincomeSharingAmount", function (e) {
            if (invoicetype === "1") {
                var incomeSharing = $("#IncomeSharingAmount").val();
                var commission = $("#CommissionClaimed").val();
                var incomesharingAmount = parseFloat(commission - incomeSharing).toFixed(2);
                var paragraph = document.querySelector('#NetIncomeCard');
                paragraph.textContent = incomesharingAmount;
                $("#NetIncome").val(incomesharingAmount)
                if ($("#IncomeSharingCheckbox").prop("checked")) {
                    var Tax = $("#TaxincomeSharingAmount option:selected").text();
                    var textWord = Tax.split('(');
                    var textamt = textWord[1].split("%");
                    var textFinalAmount = parseFloat(textamt[0]);
                    var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                   
                    $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                    var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                    debugger
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:"+IncomeSharingAmtwithTax);
                }
                else {
                    var IncomeSharingAmtwithTax = parseFloat(incomeSharing)
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }

            }
            else if (invoicetype === "2") {
               
                   

                    var incomeSharing = $("#IncomeSharingAmount").val();
                var commission = $("#CommissionClaimed").val();
                var discount = $("#DiscountGivenToClient").val();
                var commissionwithdiscount = parseFloat(commission - discount)
                var incomesharingAmount = parseFloat(commissionwithdiscount - incomeSharing).toFixed(2);  
                var incomesharingdecimal = parseFloat(commissionwithdiscount - incomeSharing);  
                    //var incomesharingAmount = parseFloat(commission - incomeSharing).toFixed(2);
                    var paragraph = document.querySelector('#NetRevenueCard');
                    paragraph.textContent = incomesharingAmount;
                $("#TotalRevenue").val(incomesharingdecimal)
                    if ($("#IncomeSharingCheckbox").prop("checked")) {
                        var Tax = $("#TaxincomeSharingAmount option:selected").text();
                        var textWord = Tax.split('(');
                        var textamt = textWord[1].split("%");
                        var textFinalAmount = parseFloat(textamt[0]);
                        var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                        if (isNaN(TaxAmount)) {
                            TaxAmount = 0;
                        }
                        $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                        var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }
                    else {
                        var IncomeSharingAmtwithTax = parseFloat(incomeSharing).toFixed(2)
                        if (isNaN(IncomeSharingAmtwithTax)) {
                            IncomeSharingAmtwithTax = 0;
                        }
                        $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                    }

             
            }
            else if (invoicetype === "3") {
                var incomeSharing = $("#IncomeSharingAmount").val();
                if ($("#IncomeSharingCheckbox").prop("checked")) {
                    var Tax = $("#TaxincomeSharingAmount option:selected").text();
                    var textWord = Tax.split('(');
                    var textamt = textWord[1].split("%");
                    var textFinalAmount = parseFloat(textamt[0]);
                    var TaxAmount = parseFloat(textFinalAmount / 100 * incomeSharing).toFixed(2);
                    if (isNaN(TaxAmount)) {
                        TaxAmount = 0;
                    }
                    $("#IncomeSharingTaxAmt").text("Tax Amount:" + TaxAmount);
                    var IncomeSharingAmtwithTax = parseFloat(parseFloat(TaxAmount) + parseFloat(incomeSharing)).toFixed(2);
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }
                else {
                    var IncomeSharingAmtwithTax = parseFloat(incomeSharing).toFixed(2)
                    if (isNaN(IncomeSharingAmtwithTax)) {
                        IncomeSharingAmtwithTax = 0;
                    }
                    $("#IncomeSharingAmtwithTax").text("Total Including Tax:" + IncomeSharingAmtwithTax);
                }


            }
        });
        $(document).off("click", "#markPaidCheckbox").on("click", "#markPaidCheckbox", function (e) {
            if ($(this).prop("checked")) {
                if (invoicetype === "1") {
                    var netfee = $("#NetFeeReceivedCard").text();
                    $("#PaymentsReceived_1").val(netfee);
                }
                else if (invoicetype === "2" || invoicetype === "3") {
                    debugger
                    var TotalDue = $("#TotalAmountInclTax").val();
                    $("#PaymentsReceived_1").val(TotalDue);
                    $("#TotalPaid").val(TotalDue);
                    $("#TotalDue").val(0);
                }
                
            }
            else {
                var TotalDue = $("#TotalAmountInclTax").val();
                $("#TotalDue").val(TotalDue);
                $("#TotalPaid").val(0);
                $("#PaymentsReceived_1").val(0);
            }
            
        });
        $(document).off("click", "#PaymentsReceived_1").on("change", "#PaymentsReceived_1", function (e) {
            debugger
            
            if (invoicetype === "1") {
                if ($("#markPaidCheckbox").prop("checked")) {
                    var netfee = $("#NetFeeReceivedCard").text();
                    $("#PaymentsReceived_1").val(netfee);
                }
                else if (parseFloat($("#PaymentsReceived_1").val()) > parseFloat($("#NetFeeReceivedCard").text())) {

                    var netfee = $("#NetFeeReceivedCard").text();
                    $("#PaymentsReceived_1").val(netfee);
                    $("#markPaidCheckbox").prop("checked", true)

                }
            }
            
            else if (invoicetype === "2" || invoicetype === "3") {
                if ($("#markPaidCheckbox").prop("checked")) {
                    var TotalDue = $("#TotalAmountInclTax").val();
                    $("#PaymentsReceived_1").val(TotalDue);
                    $("#TotalPaid").val(TotalDue);
                    $("#TotalDue").val(0);
                }
                else if ($("#markPaidCheckbox").prop("checked",false)) {
                    var PaymentsReceived= parseFloat($("#PaymentsReceived_1").val());
                    var TotalAmountInclTax = parseFloat($("#TotalAmountInclTax").val());
                    $("#TotalPaid").val(PaymentsReceived);
                    var TotalDue = parseFloat(TotalAmountInclTax - PaymentsReceived);
                    $("#TotalDue").val(TotalDue);
                    if (PaymentsReceived > TotalAmountInclTax) {
                        $("-"+"#TotalDue").val(TotalDue);
                    }
                }
            }
            else {
                $("#PaymentsReceived_1").val();
                $("#markPaidCheckbox").prop("checked", false)
            }
        });
        function CalculateFee() {
            if (invoicetype === "1") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.TotalFee').each(function () {

                    // Assuming there is an input field with class 'total' in each row..
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var discount = $("#DiscountGivenToClient").val();
                var totalamountwithdiscount = totalAmount - discount;
                var localizedTotalAmount = parseFloat(totalamountwithdiscount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#TotalFee").val(totalamountwithdiscount);
                $("#TotalAmountInclTax").val(totalamountwithdiscount);
                var paragraph = document.querySelector('#NetFeeReceivedCard');
                paragraph.textContent = totalamountwithdiscount;
                $("#NetFeeReceived").val(totalamountwithdiscount);
                $("#PaymentsReceived_1").val(totalamountwithdiscount);
                $("#markPaidCheckbox").prop("checked",true);

                CalculateCommission();
            }
            else if (invoicetype === "2") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.TotalFee').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                //var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                //$("#TotalAmountInclTax").val(localizedTotalAmount);

                CalculateCommission();
            }
            else {
                CalculateTax();
            }
        }
        function CalculateCommission() {
            if (invoicetype === "1") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.CommissionAmount').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#CommissionClaimed").val(localizedTotalAmount);
                var paragraph = document.querySelector('#NetIncomeCard');
                paragraph.textContent = localizedTotalAmount;
                $("#NetIncome").val(localizedTotalAmount);
                
                CalculateTax();
            }
            else if (invoicetype === "2") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.CommissionAmount').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#CommissionClaimed").val(localizedTotalAmount);
                CalculateTax();
            }
            
        }
        function CalculateTax() {
            if (invoicetype === "1") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.TaxAmount').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#Tax").val(localizedTotalAmount);
                CalculateNetFeePaidToPartner();
            }
            else if (invoicetype === "2") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.TaxAmount').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#Tax").val(localizedTotalAmount);
                CalculateNetFeePaidToPartner();
            }
            else {
                //debugger
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.TaxAmount').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#Tax").val(localizedTotalAmount);
                CalculateNetFeePaidToPartner();
            }
        }
        function CalculateNetFeePaidToPartner() {
            debugger
            if (invoicetype === "1") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.NetAmount').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#NetFeePaidToPartner").val(localizedTotalAmount);
                
                
            }
            else if (invoicetype === "2") {
                var total = 0;

                var totalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.NetAmount').each(function () {

                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;
                });
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#TotalAmountInclTax").val(localizedTotalAmount);
                $("#TotalDue").val(localizedTotalAmount);
                var NetFee = $("#TotalAmountInclTax").val();
                var TaxAmount = $("#Tax").val();
                var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                var discount = $("#DiscountGivenToClient").val();
                var totalrevenuewithdiscount = parseFloat(totalrevenue - discount).toFixed(2);
                var totalrevenuewithdiscountforsave = parseFloat(totalrevenue - discount);
                var localizedTotalAmountRevenue = parseFloat(totalrevenuewithdiscount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                var paragraph = document.querySelector('#NetRevenueCard');
                paragraph.textContent = localizedTotalAmountRevenue;
                $("#TotalRevenue").val(totalrevenuewithdiscountforsave);
            }
            else {
                var total = 0;

                var totalAmount = 0;
                var cardtotalAmount = 0;
                var payabletotalAmount = 0;
                var incometotalAmount = 0;
                var taxRowCounter = document.getElementById("Invoicetable").rows.length;
                $('.NetAmount').each(function () {
                    // Assuming there is an input field with class 'total' in each row
                    var amount = parseFloat($(this).val()) || 0;
                    totalAmount += amount;                    
                });
                $('.Amount').each(function () {
                    
                    var CardAmount = parseFloat($(this).val()) || 0;
                    cardtotalAmount += CardAmount;
                    var incomeType = $(this).closest('tr').find('.IncomeType option:selected').text();
                    if (incomeType === "Payables") {
                        debugger
                        payabletotalAmount += CardAmount;
                    }
                    if (incomeType === "Income") {
                        incometotalAmount += CardAmount;
                    }
                });
                debugger
                var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                $("#TotalAmountInclTax").val(localizedTotalAmount);
                $("#TotalDue").val(localizedTotalAmount);
                var NetFee = $("#TotalAmountInclTax").val();
                var TaxAmount = $("#Tax").val();
                //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
                
                var totalrevenue = parseFloat(cardtotalAmount).toFixed(2);
                var localizedTotalAmountRevenue = parseFloat(totalrevenue).toLocaleString(undefined, { minimumFractionDigits: 2 });
                
                $("#TotalAmount").val(localizedTotalAmountRevenue);
                var localizedpayabletotalAmount = parseFloat(payabletotalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                var paragraph = document.querySelector('#PayablesCard');
                paragraph.textContent = localizedpayabletotalAmount;
                $("#TotalPayables").val(localizedpayabletotalAmount);
                var localizedincometotalAmount = parseFloat(incometotalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
                var paragraph = document.querySelector('#TotalIncomeCard');
                paragraph.textContent = localizedincometotalAmount;
                $("#TotalIncome").val(localizedincometotalAmount);
            }
        }
        //$("#Invoicetable").on('change', '.Amount', function () {
        //    debugger
        //    var total = 0;

        //    var totalAmount = 0;
        //    var payabletotalAmount = 0;
        //    var incometotalAmount = 0;
        //    var taxRowCounter = document.getElementById("Invoicetable").rows.length;
           
        //    $('.Amount').each(function () {
        //        debugger
        //        var CardAmount = parseFloat($(this).val()) || 0;
        //        totalAmount += CardAmount;
        //        var incomeType = $(this).closest('tr').find('.IncomeType option:selected').text();
        //        if (incomeType === "Payables") {
        //            debugger
        //            payabletotalAmount += CardAmount;
        //        }
        //        if (incomeType === "Income") {
        //            incometotalAmount += CardAmount;
        //        }
        //    });
        //    var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
        //    $("#TotalAmountInclTax").val(localizedTotalAmount);
        //    $("#TotalDue").val(localizedTotalAmount);
        //    var NetFee = $("#TotalAmountInclTax").val();
        //    var TaxAmount = $("#Tax").val();
        //    //var totalrevenue = parseFloat(NetFee - TaxAmount).toFixed(2);
        //    var totalrevenue = totalAmount;
        //    var localizedTotalAmountRevenue = parseFloat(totalrevenue).toLocaleString(undefined, { minimumFractionDigits: 2 });
        //    if (isNaN(localizedTotalAmountRevenue)) {
        //        localizedTotalAmountRevenue = 0;
        //    }
        //    $("#TotalAmount").val(localizedTotalAmountRevenue);
        //    var localizedpayabletotalAmount = parseFloat(payabletotalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
        //    var paragraph = document.querySelector('#PayablesCard');
        //    paragraph.textContent = localizedpayabletotalAmount;
        //    var localizedincometotalAmount = parseFloat(incometotalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
        //    var paragraph = document.querySelector('#TotalIncomeCard');
        //    paragraph.textContent = localizedincometotalAmount;
        //});
        function clearForm() {
            _$clientInvoiceInformationForm[0].reset();
        }
        $(document).on('click', '.delete', function () {

            closestTr = $(this).closest('tr')
            var Invoicedetailid = closestTr.find('.DId').val();

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _invoiceDetailServices
                        .delete({
                            id: Invoicedetailid,
                        })
                        .done(function () {

                            closestTr.remove();

                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        });
        $('#saveInvoiceBtn').click(function () {
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
                if (!$('input[name=id]').val()) {//if it is create page..
                    clearForm();
                }
            });
        });
        $(document).ready(function () {
            $(document).on("click", "#addInvoiceBtn", function () {
                //$('#updatequotationBtn').click(function () {

                // Get form data
                var srCount = document.getElementById("Invoicetable").rows.length;
                //debugger
                //var rowCount = 0;//parseInt($('#rowCount').val())+0;..
                            
                    //resetFormFields();
                    //$("workflowId").val("selectedIndex", 0);
                    var adddatatotable =
                        "<tr class='trq_" + srCount + "'>" +
                        "<td><textarea type='text' placeholder='Description' class='form-control border-0 input-sm Description'>" + '' + "</textarea></td>" +
                        "<td><input id='TotalFee_" + srCount + "' type='number' placeholder='' value='" + '' + "' class='form-control border-0 input-sm TotalFee' /></td>" +
                        "<td><select id='IncomeType_" + srCount + "' class='form-control border-0 input-sm IncomeType'></select></td>" +
                        "<td><input id='CommissionPer_" + srCount + "' type='number' placeholder='' value='" + '' + "' class='form-control border-0 input-sm CommissionPer' /></td>" +
                        "<td><input id='CommissionAmount_" + srCount + "' type='number' placeholder='' value='" + '' + "' class='form-control border-0 input-sm CommissionAmount' /></td>" +
                        "<td><input id='Amount_" + srCount + "' type='number' placeholder='' value='" + '' + "' class='form-control border-0 input-sm Amount' /></td>" +
                        "<td><select id='Tax_" + srCount + "' class='form-control border-0 input-sm Tax'></select></td>"+
                        "<td><input id='TaxAmount_" + srCount + "' type='number' placeholder='' value='" + '' + "' class='form-control border-0 input-sm TaxAmount' /></td>" +
                        "<td><input id='NetAmount_" + srCount + "' type='number' placeholder='' value='" + '' + "' class='form-control border-0 input-sm NetAmount' /></td>" +
                        "<td><span class='Delete-icon delete' style='cursor: pointer; margin-left: 5px;'><i class='fa fa-trash' style='font-size: 10px;'></i></span></td>" +
                        "</tr>";

                $("#Invoicetable").append(adddatatotable);
                if (invoicetype === "1") {
                    $(".IncomeType").parent("td").remove();
                    $(".Amount").parent("td").remove();
                }
                else if (invoicetype === "2") {
                    $(".IncomeType").parent("td").remove();
                    $(".Amount").parent("td").remove();
                }
                else {
                    $(".TotalFee").parent("td").remove();
                    $(".CommissionPer").parent("td").remove();
                    $(".CommissionAmount").parent("td").remove();
                    var incomeTypeddl = "IncomeType_" + srCount
                    var incometypedata = 0;
                    GetAllincomeTypeForTableDropdown(incomeTypeddl, incometypedata);
                }
                var ddl = "Tax_" + srCount
                var Taxdata = 0;
                GetAllTaxForTableDropdown(ddl, Taxdata);
            });
        });
        function GetAllincomeTypeForTableDropdown(incomeTypeddl,incometypedata) {
            //debugger
            $.ajax({
                url: abp.appPath + 'api/services/app/InvoiceDetail/GetAllIncomeTypeForTableDropdown',
                method: 'GET',
                dataType: 'json',
                //data: {
                //    PartnerIdFilter: dynamicValue,
                //},
                success: function (data) {
                    
                    // Populate the dropdown with the fetched data
                    populateIncometypeddlDropdown(data, incomeTypeddl, incometypedata)
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        }
        function GetAllTaxForTableDropdown(ddl, Taxdata) {
            //debugger
            $.ajax({
                url: abp.appPath + 'api/services/app/InvoiceDetail/GetAllTaxForTableDropdown',
                method: 'GET',
                dataType: 'json',
                //data: {
                //    PartnerIdFilter: dynamicValue,
                //},
                success: function (data) {
                    
                    // Populate the dropdown with the fetched data
                    populateDropdown(data, ddl, Taxdata);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        }
        function populateDropdown(data, ddl, Taxdata) {
            //debugger
            var dropdown = $('#' + ddl);
            var IncomeSharingdropdown = $('#TaxincomeSharingAmount');

            dropdown.empty();
            IncomeSharingdropdown.empty();

            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                    IncomeSharingdropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });

            if (Taxdata > 0) {
                dropdown.val(Taxdata).trigger("change.select2")
            }

        }
        function populateIncometypeddlDropdown(data, incomeTypeddl, incometypedata) {
            //debugger
            var dropdown = $('#' + incomeTypeddl);

            dropdown.empty();

            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
            if (incometypedata > 0) {
                dropdown.val(incometypedata).trigger("change.select2")
            }
        }
        $(document).off("click", "#savePaymentRecivedBtn").on("click", "#savePaymentRecivedBtn", function (e) {
            var uniqueId = $(".AddPaymentReceivedDiv").children().length; // Generate a unique identifier based on the number of existing elements
            uniqueId = uniqueId+2; // Generate a unique identifier based on the number of existing elements
            var addPaymentRecive = 0;
            debugger
            if (invoicetype === "1") {
                $('.PaymentsReceived').each(function () {
                    var CardAmount = parseFloat($(this).val()) || 0;
                    addPaymentRecive += CardAmount;
                }); // Added closing parenthesis here

                if (addPaymentRecive >= parseFloat($("#NetFeeReceivedCard").text())) {
                    abp.notify.error(app.localize('ReceivedAmountExceededTotalFee'));
                }
                else if ($("#markPaidCheckbox").prop("checked")) {
                    abp.notify.error(app.localize('ReceivedAmountExceededTotalFee'));
                }
                else {
                    var newTimelineItem = `
            <div class="row">
                <div class="col-lg-3">
                    <br />
                    <input class="form-control custom-small-input rounded-0 PaymentsReceived" id="PaymentsReceived_${uniqueId}" value="" type="text" name="PaymentsReceived"  />
                </div>
                <div class="col-lg-2">
                    <label> </label>
                    <input class="form-control form-control-sm rounded-0 custom-select-input date-picker PaymentsReceivedDate" id="PaymentsReceivedDate_${uniqueId}" type="text" name="PaymentsReceivedDate" value="" /> 
                </div>               
                <div class="col-lg-2">
                    <br/>
                    <select id="InvoiceasPaidId_${uniqueId}" class="form-select select2 InvoiceasPaidId" data-id="" data-control="select2"></select>
                </div>
            </div>
        `;

                    $(".AddPaymentReceivedDiv").append(newTimelineItem);
                    setTimeout(function () {
                        handleDateInputMouseDown();
                    }, 100);
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="1">Cheque</option>');
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="2">Cash</option>');
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="3">CreditCard</option>');
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="4">BankTransfers</option>');
                    $('#InvoiceasPaidId_' + uniqueId).select2({
                        width: '100%'
                    });
                }
            }
            else if (invoicetype === "2" || invoicetype === "3") {
                $('.PaymentsReceived').each(function () {
                    var CardAmount = parseFloat($(this).val()) || 0;
                    addPaymentRecive += CardAmount;
                }); // Added closing parenthesis here

                if (addPaymentRecive >= parseFloat($("#TotalAmountInclTax").val())) {
                    abp.notify.error(app.localize('ReceivedAmountExceededTotalFee'));
                }
                else if ($("#markPaidCheckbox").prop("checked", true)) {
                    abp.notify.error(app.localize('ReceivedAmountExceededTotalFee'));
                }
                else {
                    var newTimelineItem = `
            <div class="row">
                <div class="col-lg-3">
                    <br />
                    <input class="form-control custom-small-input rounded-0 PaymentsReceived" id="PaymentsReceived_${uniqueId}" value="" type="text" name="PaymentsReceived"  />
                </div>
                <div class="col-lg-2">
                    <label> </label>
                    <input class="form-control form-control-sm rounded-0 custom-select-input date-picker PaymentsReceivedDate" id="PaymentsReceivedDate_${uniqueId}" type="text" name="PaymentsReceivedDate" value="" /> 
                </div>               
                <div class="col-lg-2">
                    <br/>
                    <select id="InvoiceasPaidId_${uniqueId}" class="form-select select2 InvoiceasPaidId" data-id="" data-control="select2"></select>
                </div>
            </div>
        `;

                    $(".AddPaymentReceivedDiv").append(newTimelineItem);
                    setTimeout(function () {
                        handleDateInputMouseDown();
                    }, 100);
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="1">Cheque</option>');
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="2">Cash</option>');
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="3">CreditCard</option>');
                    $('#InvoiceasPaidId_' + uniqueId).append('<option value="4">BankTransfers</option>');
                    $('#InvoiceasPaidId_' + uniqueId).select2({
                        width: '100%'
                    });
                }
            }
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
