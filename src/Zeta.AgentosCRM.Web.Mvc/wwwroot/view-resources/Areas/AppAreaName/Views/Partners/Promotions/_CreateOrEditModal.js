(function ($) { 
    app.modals.CreateOrEditPromotionsModal = function () {
        debugger
    
        $('#productId').select2({
            multiple: true,
            width: '100%',
            placeholder: 'Select Product'

            // Adjust the width as needed
        });
        //if (result.ContactOwner.length > 0) {
        //    var ms_val = 0, Degree = 0, Course = 0;
        //    $.each(result.ContactOwner, function (index, obj) {
        //        ms_val += "," + obj.OwnerID;
        //    });
        //    var ms_array = ms_val.split(',');
        //    $("#ddlContactOwner").val(ms_array).trigger("chosen:updated");
        //}
        if ($('input[name="ApplyTo"]:checked').val() === "false") {

            document.getElementById("field1").style.display = 'block';

        } else {
            document.getElementById("field1").style.display = 'none';
        }

        //var selectId = <text>@Html.Raw(Model.SelectId)</text>;

        //$('#productId').val(selectId).trigger('change');
        var hiddenfield = $("#PartnerId").val();
        var dynamicValue = hiddenfield;

        $.ajax({
            url: abp.appPath + 'api/services/app/PromotionProducts/GetAllProductForTableDropdown',
            method: 'GET',
            dataType: 'json',
            data: {
                PartnerIdFilter: dynamicValue,
            },
            success: function (data) {
                debugger
                // Populate the dropdown with the fetched data
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });

        var dataRows = []; // Declare dataRows outside the function to maintain its state
          //Declare Steps outside the function to make it accessible

        function populateDropdown(data) {
            var dropdown = $('#productId');

            //dropdown.on('change', function () {
            //    var selectedOption = $('option:selected', this);
            //    var dataIdValue = selectedOption.data('id');

            //    if (!dataRows.find(row => row.ddlValue === dataIdValue)) {
            //        dataRows.push({
            //            ddlValue: dataIdValue
            //        });
            //    } else {
            //        // Remove the item if already exists in dataRows
            //        dataRows = dataRows.filter(row => row.ddlValue !== dataIdValue);
            //    }

            //    // Now you can use or log the updated dataRows array
            //    console.log('Updated dataRows:', dataRows);

            //    // Update the global Steps array
            //    Steps = dataRows.slice(); // Copy the array to avoid reference issues

            //    // Now Steps contains the updated array
            //    console.log('Updated Steps:', Steps);
            //});

            dropdown.empty();

            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        var idValue = 0;
        var idElements = document.getElementsByName("id");

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

     
        $.ajax({
            url: abp.appPath + 'api/services/app/PartnerPromotions/GetPartnerPromotionForEdit?id=' + idValue,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                debugger
                // Populate the dropdown with the fetched data
                updateProductDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        }
        function updateProductDropdown(data) {
            debugger;
            var ms_val = 0;

            // Assuming data.result.promotionproduct is an array of objects with OwnerID property
            $.each(data.result.promotionProduct, function (index, obj) {
                ms_val += "," + obj.productId;
               
            });

            //var ms_array = ms_val.length > 0 ? ms_val.substring(1).split(',') : [];
            var ms_array = ms_val.split(',');
            var $productId = $("#productId");

            // Clear existing options in Select2
            //$productId.empty();
            $productId.val(ms_array).trigger('change');
            // Add new options
            //$.each(ms_array, function (index, value) {
            //    debugger
            //    var newOption = new Option(value, value, true, true);
            //    $productId.append(newOption).trigger('change');
            //    //$productId.val(newOption).trigger("chosen:updated");
            //});
            // Assuming #ddlContactOwner is a multiple-select dropdown using a library like Chosen
           // $("#productId").val(ms_array).trigger("chosen:updated");
          /*  $('#productId').val(selectId).trigger('change');*/
        }
        // Call populateDropdown with your data
        //populateDropdown(yourData);

        // You can use or log the initial state of dataRows
        $('input[name="ApplyTo"]').change(function () {
            if ($(this).val() === "false") {
                document.getElementById("field1").style.display = 'block';

                //$("#field1").show();
            } else  {
                // Hide the label and field for option1
                //$("#field1 label, #field1 input").hide();
                document.getElementById("field1").style.display = 'none';
                // Show the label and field for option2
                // $("#field2 label, #field2 input").show();
            }
        });
       // $("#EditUser_IsActive").prop("checked", true);
        //document.getElementById("field1").style.display = 'none';
        var hiddenfield = $("#PartnerId").val();

        $("#partnerId").val(hiddenfield);

        var _partnerPromotionsService = abp.services.app.partnerPromotions;

        var _modalManager;
        var _$partnerPromotionsInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$partnerPromotionsInformationForm = _modalManager.getModal().find('form[name=PromotionsInformationsForm]');
            console.log(_$partnerPromotionsInformationForm);
            _$partnerPromotionsInformationForm.validate();
        };

        this.save = function () {
            if (!_$partnerPromotionsInformationForm.valid()) {
                return;
            }
            var datarows = [];
            var datarowsList = $("#productId :selected").map(function (i, el) {
                debugger
                return $(el).val();
            }).get();
            $.each(datarowsList, function (index, value) {
                var datarowsItem = {
                    ProductId: datarowsList[index]
                }
                datarows.push(datarowsItem);
            });
            var Steps = JSON.stringify(datarows);

            Steps = JSON.parse(Steps);
       

            var leadSources = _$partnerPromotionsInformationForm.serializeFormToObject();
            leadSources.Steps = Steps;
            debugger
            _modalManager.setBusy(true);
            _partnerPromotionsService
                .createOrEdit(leadSources)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditPromotionModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
