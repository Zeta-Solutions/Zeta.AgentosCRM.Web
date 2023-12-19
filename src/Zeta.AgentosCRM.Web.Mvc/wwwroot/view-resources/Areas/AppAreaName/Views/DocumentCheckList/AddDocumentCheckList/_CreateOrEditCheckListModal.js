(function ($) {

    app.modals.CreateOrEditDocumentCheckListHeadModal = function () {
      
        debugger
        $("#RadioProducts").hide();
        //$("#DDlProduct").show();
        $('#PartnerID,#ProductID').select2({
            multiple: true,
            width: '100%',
            // Adjust the width as needed...
        });
        $('#DocumentTypeId').select2({
            width: '100%',
            placeholder: 'Select Office',
            allowClear: true,
            minimumResultsForSearch: 10,
        });
        var _workflowsService = abp.services.app.workflows;
        var _workflowDocumentsService = abp.services.app.workflowDocuments;
        var _productsService = abp.services.app.products; 

        var GetPartnerValue = $("input[name='IsForAllPartners']:checked").val();
        var GetProductValue = $("input[name='IsForAllProducts']:checked").val();
        var clientPortalValue = $("#AllowOnClientPortal").val();
        var nextstageValue = $("#nextstage").val();
        if ($("#workflowDocumentCheckListId").val() == '') {
            var WorkFlowDocumentID = $("#workFlowStepId").val();
            debugger

            $("#workFlowStepModelId").val(WorkFlowDocumentID);

            $("#DDlPartners").hide();

            $("#DDlProduct").hide();
        }
        if ($("#workflowDocumentCheckListId").val() != '') {
            var GetPartnerValue = $("input[name='IsForAllPartners']:checked").val();
            if (GetPartnerValue == "false") {

                $("#DDlPartners").hide();
            }
            else {

                $("#DDlPartners").show();
            }

            var GetProductValue = $("input[name='IsForAllProducts']:checked").val();
            if (GetProductValue == "false") {

                $("#DDlProduct").hide();
            } else {
                $("#DDlProduct").show();

            }
        }
        
        var _documentTypesService = abp.services.app.documentTypes;
        var _workflowStepDocumentCheckListsService = abp.services.app.workflowStepDocumentCheckLists;
        var input = "";
        _documentTypesService.getAll(input)
            .done(function (data) {
                debugger
                var optionhtml = '<option value="0"> select Document Type</option>';
                $("#AddNewCheckListDocumentType").append(optionhtml);

                $.each(data.items, function (index, item) {
                    optionhtml = '<option value="' +
                        item.documentType.id + '">' + item.documentType.name + '</option>';
                    $("#AddNewCheckListDocumentType").append(optionhtml);
                    console.log(optionhtml);
                });
            });


        $.ajax({
            url: abp.appPath + 'api/services/app/Partners/GetAll',
            method: 'GET',
            dataType: 'json',

            success: function (data) {
                debugger
                populatePartnerDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });

        var dataRows = [];
        function populatePartnerDropdown(data) {

            var dropdown = $('#PartnerID');



            dropdown.empty();

            $.each(data.result.items, function (index, item) {
             
                if (item && item.partner.id !== null && item.partner.id !== undefined && item.partner.partnerName !== null && item.partner.partnerName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.partner.id).attr('data-id', item.partner.id).text(item.partner.partnerName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }


        //Fill Drop Down
        var workflowDocumentCheckListId = $("#workflowDocumentCheckListId").val();
        debugger

        if (workflowDocumentCheckListId > 0) {
            debugger

            $.ajax({
                url: abp.appPath + 'api/services/app/WorkflowStepDocumentCheckLists/GetWorkflowStepDocumentCheckListForEdit?id=' + workflowDocumentCheckListId,
                //url: abp.appPath + 'api/services/app/DocumentCheckListPartners/GetDocumentCheckListPartnerForEdit?id=' + workflowDocumentCheckListId,
                method: 'GET',
                dataType: 'json',

                success: function (data) {
                    debugger
                    // Populate the dropdown with the fetched data
                    //updatePartnerDropdown(data);
                    setTimeout(function () {
                        updatePartnerDropdown(data);
                    }, 200);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });

            function updatePartnerDropdown(data) {

                var ms_val = 0;
                debugger
                // Assuming data.result.promotionproduct is an array of objects with OwnerID property
                $.each(data.result.documentCheckListPartner, function (index, obj) {
                    debugger
                    ms_val += "," + obj.partnerId;

                });
                if (ms_val == 0) {
                    alert("There is no data")
                    // When Ms_Va get 0 then Action Perform other wise else case run...
                } else {

                    var ms_array = ms_val.split(',');
                    //alert("There is some data" + ms_array)
                    debugger
                    //$("#PartnerID").select2("val", ms_array);
                    //$("#PartnerID").empty()
                    $("#PartnerID").val(ms_array) 
                    //$("#PartnerID").trigger('change');
                    $("#PartnerID").trigger('change');

                }

            }
        }

    var _modalManager;
        var _$feeTypesInformationForm = null;
        var workflowId = $("#WorkflowId").val();
       
        if (workflowId > 0) {
            $.ajax({
                url: abp.appPath + 'api/services/app/WorkflowSteps/GetAll',
                data: {
                    WorkflowIdFilter: workflowId,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
               
                    var TotalRecord = data.result.items; 
                        $.each(TotalRecord, function (index, item) {
                    
                            var workFlowStep = `
                                <div class="col-12 timeline-item  timeline-itemRowDelete ">
                                    <div class="timeline-item col-11">
                                        <div class="timeline-circle"></div>
                                        <div class="timeline-content">
                                            <span style="display :none;">${item.workflowStep.id}</span>
                                            <a href=""><span></span></a>  
                                            <span>${item.workflowStep.name}</span>  
                                        </div>
                                    </div> 
                                </div>`;
     
                            $(".WorkFlowStepDetail").append(workFlowStep);
                              
                        }); 
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        //_workflowStepsService.getAll(WorkflowId)
        //    .done(function (data) {
        //        debugger
        //        console.log(data);
                 
        //    })
    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$feeTypesInformationForm = _modalManager.getModal().find('form[name=FeeTypeInformationsForm]');
        _$feeTypesInformationForm.validate();
    };
        $("#WorkflowId").on("change", function () {
            debugger
            var selectedWorkflowName = $(this).find("option:selected").text();
            $("#WorkFlow_Name").val(selectedWorkflowName); 
        });


        $('#PartnerID').on('change', function () {
            debugger
            if ($("#PartnerID").val() != "") {
                var PartnerId = $('#PartnerID').val();
                console.log(PartnerId);
                PartnerId = JSON.stringify(PartnerId);

                PartnerId = JSON.parse(PartnerId); 
                //var partnerIdArray = $('#PartnerID').val();

                //// Extract the string value and remove surrounding quotes
                //var PartnerId = partnerIdArray[0].replace(/^['"]|['"]$/g, '');
            }
            debugger
            $("#RadioProducts").show();
            //setTimeout(myFunction, 2000); 
            /*if (PartnerId > 0) {*/
                debugger
                var partnerIds = PartnerId
                    _productsService.getProductsByPartnerId(partnerIds)
            .done(function (data) {
                debugger
                populateProductDropdown(data);

            })
                function populateProductDropdown(data) {

                    var dropdown = $('#ProductID');


                    dropdown.empty();

                    $.each(data, function (index, item) {
                        debugger
                        if (item && item.id !== null && item.product.id !== undefined && item.product.name !== null && item.product.name !== undefined) {
                            dropdown.append($('<option></option>').attr('value', item.product.id).attr('data-id', item.product.id).text(item.product.name));
                        } else {
                            console.warn('Invalid item:', item);
                        }
                    });
                }
                ////var partnerIds = 66;
                //alert(PartnerId)
                //$.ajax({
                //    url: abp.appPath + 'api/services/app/Products/GetProductsByPartnerId?partnerIds=' + PartnerId,

                //    method: 'GET',
                //    dataType: 'json',

                //    success: function (data) {
                //        debugger
                //        populateProductDropdown(data);
                //    },
                //    error: function (error) {
                //        console.error('Error fetching data:', error);
                //    }
                //});

                //var dataRows = [];
                //function populateProductDropdown(data) {

                //    var dropdown = $('#ProductID');


                //    dropdown.empty();

                //    $.each(data.result, function (index, item) {
                //        debugger
                //        if (item && item.id !== null && item.product.id !== undefined && item.product.name !== null && item.product.name !== undefined) {
                //            dropdown.append($('<option></option>').attr('value', item.product.id).attr('data-id', item.product.id).text(item.product.name));
                //        } else {
                //            console.warn('Invalid item:', item);
                //        }
                //    });
                //}
            /*}*/


            var workflowDocumentCheckListId = $("#workflowDocumentCheckListId").val();
            debugger
            if (workflowDocumentCheckListId > 0) {
                debugger

                $.ajax({
                    url: abp.appPath + 'api/services/app/WorkflowStepDocumentCheckLists/GetWorkflowStepDocumentCheckListForEdit?id=' + workflowDocumentCheckListId,
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
                var ms_valProduct = 0;
                debugger
                $.each(data.result.documentCheckListProduct, function (index, obj) {
                    debugger
                    ms_val += "," + obj.productId;

                });
                if (ms_val == 0) { 

                } else {
                    // MS_Val Not Empty Value .,,,
                    var ms_array = ms_val.split(','); 
                    debugger
                    var $productId = $("#ProductID");
                    $productId.val(ms_array).trigger('change');
                }
            }
        });
        $(document).on('click', '#AllProduct', function () {
            $("#DDlProduct").hide();
            $('#ProductID').val(null).trigger('change');
            GetProductValue = false;
             
        });

        $(document).on('click', '#selectedProduct', function () {
            $("#DDlProduct").show()
            GetProductValue = true;


        });
        $(document).on('click', '#AllPartner', function () {
            debugger
            $("#DDlPartners").hide();
            $('#PartnerID').val(null).trigger('change');
            GetPartnerValue = false;
            $("#RadioProducts").hide();
            debugger
        });

        $(document).on('click', '#selectedPartner', function () {
            $("#DDlPartners").show();
            GetPartnerValue = true;

            debugger
        });
        $(document).on('click', '#AllowOnClientPortal', function () {
            debugger

            var clientPortalValue = $(this).prop("checked");

            alert(clientPortalValue);

        });
        $(document).on('click', '#nextstage', function () {
            debugger
            nextstageValue = $(this).prop("checked");

            alert(nextstageValue);
        });
        $(document).on('change', '#DocumentTypeId', function () {
            debugger

            var selectedDocumentTypeName = $(this).find("option:selected").text();
            $("#Name").val(selectedDocumentTypeName); 
        });
    this.save = function () {
        if (!_$feeTypesInformationForm.valid()) {
        return;
      }
        //if ($("#workflowDocumentCheckListId").val() == '' || $("#workflowDocumentCheckListId").val() == '0') {
        //    debugger
        //    var WorkFlowDocumentID = $("#workFlowStepId").val();
        //    debugger

        //    $("#workFlowStepModelId").val(WorkFlowDocumentID);
        //}
        //Partner dropdown Save


        var Partnerrows = [];
        var datarowsList = $("#PartnerID :selected").map(function (i, el) {

            return $(el).val();
        }).get();
        console.log(datarowsList);
        $.each(datarowsList, function (index, value) {
            var datarowsItem = {
                PartnerId: datarowsList[index]
            }
            Partnerrows.push(datarowsItem);
        });
        var PartnerId = JSON.stringify(Partnerrows);
        
        PartnerId = JSON.parse(PartnerId);
        //Product dropdown Save 


        var PRoductrows = [];
        var datarowsList = $("#ProductID :selected").map(function (i, el) {

            return $(el).val();
        }).get();
        console.log(datarowsList);
        $.each(datarowsList, function (index, value) {
            var datarowsItem = {
                ProductId: datarowsList[index]
            }
            PRoductrows.push(datarowsItem);
        });
        var ProductId = JSON.stringify(PRoductrows);
        
        ProductId = JSON.parse(ProductId);



        var feeType = _$feeTypesInformationForm.serializeFormToObject();

        feeType.DocumentCheckListPartner = PartnerId
        feeType.DocumentCheckListProduct = ProductId
        debugger
      _modalManager.setBusy(true);
        _workflowStepDocumentCheckListsService
            .createOrEdit(feeType)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
            _modalManager.close();
            location.reload();
          abp.event.trigger('app.createOrEditFeeTypeModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
