(function ($) {
    app.modals.CreateOrEditManualPaymentModal = function () {
        debugger
        var _documentTypesService = abp.services.app.manualPaymentDetails;
        var _paymentInvoiceTypesService = abp.services.app.paymentInvoiceTypes;
        var hiddenfield = $("#WorkFlowOfficeId").val();
        //$("#OrganizationUnitId").val(hiddenfield);
        $("input[name='OrganizationUnitId']").val(hiddenfield);

        // multi select dropdown List 
        $('#InvoiceTypeId').select2({
            multiple: true,
            width: '650px',
            // Adjust the width as needed
        });

        $.ajax({
            url: abp.appPath + 'api/services/app/InvoiceTypes/GetAll',
            method: 'GET',
            dataType: 'json',

            success: function (data) {
                debugger
                var data = data.result.items;
                if (data == null) {
                    //alert("Record Not Found.");
                }
                //var optionhtml = '<option value="0"> ---Select Office ---</option>';
                //$("#InvoiceTypeId").append(optionhtml);

                $.each(data, function (i) {
                    optionhtml = '<option value="' +
                        data[i].invoiceType.id + '">' + data[i].invoiceType.name + '</option>';
                    $("#InvoiceTypeId").append(optionhtml);
                });
                //populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        //Fill Drop Down
        var ManualPaymentId=$("#ManualPaymentDetailId").val();
        if (ManualPaymentId > 0) {


            $.ajax({
                url: abp.appPath + 'api/services/app/ManualPaymentDetails/GetManualPaymentDetailForEdit?id=' + ManualPaymentId,
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
            $.each(data.result.paymentInvoiceType, function (index, obj) {
                debugger
                ms_val += "," + obj.invoiceTypeId;

            });

            //var ms_array = ms_val.length > 0 ? ms_val.substring(1).split(',') : [];
            var ms_array = ms_val.split(',');
            var $productId = $("#InvoiceTypeId");
            $productId.val(ms_array).trigger('change');
        }
        //var dataRows = [];
        //function populateDropdown(data) {
        //    var dropdown = $('#InvoiceTypeId');
         
        //    dropdown.empty();

        //    $.each(data.result, function (index, item) {
        //        if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
        //            dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
        //        } else {
        //            console.warn('Invalid item:', item);
        //        }
        //    });
        //}



        var _modalManager;
        var _$documentTypesInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$documentTypesInformationForm = _modalManager.getModal().find('form[name=DocumentTypeInformationsForm]');
            _$documentTypesInformationForm.validate();
        };

        this.save = function () {
            if (!_$documentTypesInformationForm.valid()) {
                return;
            }
            debugger
            var InvoiceTyperows = [];
            var datarowsList = $("#InvoiceTypeId :selected").map(function (i, el) {
                return $(el).val();
            }).get();
            console.log(datarowsList);

            $.each(datarowsList, function (index, value) {
                var datarowsItem = {
                    InvoiceTypeId: datarowsList[index] 

                };
                InvoiceTyperows.push(datarowsItem);
            });

            var PaymentInvoiceTypes = JSON.stringify(InvoiceTyperows);
            PaymentInvoiceTypes = JSON.parse(PaymentInvoiceTypes);
            var feeType = _$documentTypesInformationForm.serializeFormToObject();
             
            feeType.PaymentInvoiceTypeRecord = PaymentInvoiceTypes;
            _modalManager.setBusy(true);
            _documentTypesService
                .createOrEdit(feeType)
                .done(function () {
                    debugger
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditFeeTypeModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
            //debugger
            //_paymentInvoiceTypesService.createOrEdit(PaymentInvoiceTypes)
            //    .done(function () {
            //        debugger
            //        abp.event.trigger('app.createOrEditFeeTypeModalSaved');
            //    })
            //    .always(function () {
            //        _modalManager.setBusy(false);
            //    });

        };
    };
})(jQuery);
