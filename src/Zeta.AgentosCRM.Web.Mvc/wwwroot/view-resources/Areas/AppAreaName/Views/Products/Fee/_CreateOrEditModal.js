(function ($) {
    app.modals.CreateOrEditFeeModal = function () {
        $('#installmentTypeId').select2({

            width: '100%',
            dropdownParent: $('#installmentTypeId').parent(),
            // Adjust the width as needed
        });
        $('#countryId').select2({
            width: '100%',
            dropdownParent: $('#countryId').parent(),
            // Adjust the width as needed
        });
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
                url: abp.appPath + 'api/services/app/ProductFees/GetProductFeeForEdit?id=' + idValue,
                method: 'GET',
                dataType: 'json',
                success: function (data) {
                     
                    // Populate the dropdown with the fetched data
                    updateProductFee(data);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        }
 
        function updateProductFee(data) {
             ;
            // Assuming data.result.promotionproduct is an array of objects with OwnerID property
            $.each(data.result.feeDetail, function (index, obj) {
                var newTimelineItem = `
        <div class="row TaxRow">
            <div class="col-lg-3">
                <br />
                  <select class="form-control FeeType${index}"></select>  
                <input class="form-control ID" type="hidden" value="${obj.id}"/>
            </div>
            <div class="col-lg-2">
                <br />
                <input class="form-control Amount" type="text" value="${obj.installmentAmount}"/>
            </div>
            <div class="col-lg-2">
                <br />
                <input class="form-control installment" type="text" value="${obj.installments}"/>
            </div>
            <div class="col-lg-2">
                <br />
                <input class="form-control TotalFee" disabled="disabled" type="text" value="${obj.totalFee}"/>
            </div>
            <div class="col-lg-1">
                <br />
                <input class="IsPayable" type="checkbox" ${obj.isPayable ? 'checked' : ''}/>
            </div> 
            <div class="col-lg-1">
                <br />
                <input class="AddInQuotation" type="checkbox" ${obj.addInQuotation ? 'checked' : ''}/>
            </div>
            <div class="col-lg-1">
                <br />
                <span class="TaxRowDelete"><i class="fa fa-trash" style="font-size: 20px;"></i></span>
            </div>
        </div>
    `;

                $(".AddTaxDiv").append(newTimelineItem);
                $.ajax({
                    url: abp.appPath + 'api/services/app/ProductFeeDetails/GetAllFeeTypeForTableDropdown',
                    method: 'GET',
                    dataType: 'json',

                    success: function (data) {
                         
                        var $currentDropdown = $(".FeeType" + index);
                        $currentDropdown.empty();
                        var data = data.result;
                        if (data == null) {
                            //alert("Record Not Found.");
                        }
                        var optionhtml = '<option value="0"> select Fee Type</option>';
                        $currentDropdown.append(optionhtml);

                        $.each(data, function (i) {
                            // You will need to alter the below to get the right values from your json object.  Guessing that d.id / d.modelName are columns in your carModels data
                            optionhtml = '<option value="' +
                                data[i].id + '">' + data[i].displayName + '</option>';
                            $currentDropdown.append(optionhtml);
                         
                        });
                        $currentDropdown.val(obj.feeTypeId);
                        //populateDropdown(data);
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
            });
            

            //var ms_array = ms_val.length > 0 ? ms_val.substring(1).split(',') : [];

        }
   

        var hiddenfield = $("#ProductId").val();

        $("#productId").val(hiddenfield);
        var _productsFeeService = abp.services.app.productFees;
        var _productFeeDetailsService = abp.services.app.productFeeDetails;

        var _modalManager;
        var _$feesInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$feesInformationForm = _modalManager.getModal().find('form[name=FeeInformationsForm]');
            _$feesInformationForm.validate();
        };

      
        $(document).on('click', '.addTax', function () {
             
            var taxRowCounter = $('.TaxRow').length;
            var newTimelineItem = `
    <div class="row TaxRow">
        <div class="col-lg-3">
            <br />
            <select id="FeeType" class="form-control FeeType${taxRowCounter}"></select>  
            <input class="form-control ID"value="0" type="hidden"/>
        </div>
        <div class="col-lg-2">
            <br />
            <input class="form-control Amount" type="text"/>
        </div>
        <div class="col-lg-2">
            <br />
            <input class="form-control installment" type="text"/>
        </div>
        <div class="col-lg-2">
            <br />
            <input class="form-control TotalFee" disabled=disabled  type="text"/>
        </div>
        <div class="col-lg-1">
            <br />
   <input  class="IsPayable" type="checkbox"/>        </div> 
        <div class="col-lg-1">
           <br />
            <input class="AddInQuotation"  type="checkbox"/>
        </div>
        <div class="col-lg-1">
           <br />  <span class="TaxRowDelete"><i class="fa fa-trash" style="font-size: 20px;"></i></span>
        </div>
    </div>
`;

            $(".AddTaxDiv").append(newTimelineItem);
            taxRowCounter++;
        });
      
        //$(document).on('click', '.addTax', function () {
        //    taxRowCounter = 0;
        //$.ajax({
        //    url: abp.appPath + 'api/services/app/ProductFeeDetails/GetAllFeeTypeForTableDropdown',
        //    method: 'GET',
        //    dataType: 'json',
          
        //    success: function (data) {
        //         
        //        var data = data.result;
        //        if (data == null) {
        //            //alert("Record Not Found.");
        //        }
        //        var optionhtml = '<option value="0"> select Office</option>';
        //        $("#FeeType" + taxRowCounter).html(optionhtml);
        //        $.each(data, function (i) {
        //            // You will need to alter the below to get the right values from your json object.  Guessing that d.id / d.modelName are columns in your carModels data
        //            optionhtml = '<option value="' +
        //                data[i].id + '">' + data[i].displayName + '</option>';
        //            $("#FeeType" + taxRowCounter).append(optionhtml);
                   
        //        });
        //        taxRowCounter++;
        //        //populateDropdown(data);
        //    },
        //    error: function (error) {
        //        console.error('Error fetching data:', error);
        //    }
        //});
        //});
        $(document).on('change', '.Amount', function () {
             ;
            var Amount = $(this).closest('.TaxRow').find('.Amount').val();
            var installment = $(this).closest('.TaxRow').find('.installment').val();
            if (isNaN(Amount)) {
                Amount = 0;
            }
            if (isNaN(installment)) {
                installment = 0
            }
            var NetFee = parseFloat(Amount * installment).toFixed(2);
            $(this).closest('.TaxRow').find(".TotalFee").val(parseFloat(NetFee).toFixed(2));
           
        });
        $(document).on('change', '.installment', function () {
             ;
            var Amount = $(this).closest('.TaxRow').find('.Amount').val();
            var installment = $(this).closest('.TaxRow').find('.installment').val();
            if (isNaN(Amount)) {
                Amount = 0;
            }
            if (isNaN(installment)) {
                installment = 0
            }
            var NetFee = parseFloat(Amount * installment).toFixed(2);
            $(this).closest('.TaxRow').find(".TotalFee").val(parseFloat(NetFee).toFixed(2));

        });
        $(document).on('click', '.addTax', function () {
            var taxRowCounter1 = Math.max(0, $('.TaxRow').length)-1;
            $.ajax({
                url: abp.appPath + 'api/services/app/ProductFeeDetails/GetAllFeeTypeForTableDropdown',
                method: 'GET',
                dataType: 'json',
                success: function (data) {
                     ;
                    $(".FeeType" + taxRowCounter1).empty();
                    var dropdownData = data.result;

                    if (dropdownData == null) {
                        //alert("Record Not Found.");
                    }

                    var optionhtml = '<option value="0"> select Office</option>';
                    $(".FeeType" + taxRowCounter1).html(optionhtml);

                    $.each(dropdownData, function (i) {
                        var optionhtml = '<option value="' + dropdownData[i].id + '">' + dropdownData[i].displayName + '</option>';
                        $(".FeeType" + taxRowCounter1).append(optionhtml);
                    });

                    // Increment the counter for the next TaxRow
                    taxRowCounter1++;
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        });
        $(document).on('click', '.TaxRowDelete', function () {
             
            var closestTr = $(this).closest('.row');
            var feedetailsid = closestTr.find('.ID').val();

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _productFeeDetailsService
                        .delete({
                            id: feedetailsid,
                        })
                        .done(function () {
                             
                            closestTr.remove();

                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        });


        $(document).on('select2:open', function () {
            var $searchField = $('.select2-search__field');
            $searchField.on('keydown', function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        });


        this.save = function () {
            if (!_$feesInformationForm.valid()) {
                return;
            }
            //var  Id = $("#TaxID").val();
            var datarows = [];

            // Assuming you have some way of determining the number of rows you want to save, let's say 'rowCount'.
            var rowCount = $(".Amount").length;
             
            for (var i = 0; i < rowCount; i++) {
                var InstallmentAmount = $('.TaxRow').eq(i).find('.Amount').val();
                var Installments = $('.TaxRow').eq(i).find('.installment').val();
                var TotalFee = $('.TaxRow').eq(i).find('.TotalFee').val();
                var IsPayable = $('.TaxRow').eq(i).find('.IsPayable').is(':checked'); // Use is(':checked') to get the boolean value
                var AddInQuotation = $('.TaxRow').eq(i).find('.AddInQuotation').is(':checked'); // Use is(':checked') to get the boolean value
                var FeeTypeId = $('.TaxRow').eq(i).find('.FeeType' + i).val();
                var Id = $('.TaxRow').eq(i).find('.ID').val();
                var CommissionPer = 0;
                var ProductFeeId =0;

                var dataRowItem = {
                    InstallmentAmount: InstallmentAmount,
                    Installments: Installments,
                    TotalFee: TotalFee,
                    IsPayable: IsPayable,
                    FeeTypeId: FeeTypeId,
                    AddInQuotation: AddInQuotation,
                    CommissionPer: CommissionPer,
                    ProductFeeId: ProductFeeId,
                    Id:Id
                };

                datarows.push(dataRowItem);
            }


            // Convert the array to a JSON string
            var FeeDetails = JSON.stringify(datarows);
            FeeDetails = JSON.parse(FeeDetails);
            var Subject = _$feesInformationForm.serializeFormToObject();
            Subject.FeeDetails = FeeDetails;
            _modalManager.setBusy(true);
            _productsFeeService
                .createOrEdit(Subject)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditFeeModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
        $(document).on('click', '.close-button,.btn-close', function () {

            location.reload();
        });
    };
})(jQuery);
