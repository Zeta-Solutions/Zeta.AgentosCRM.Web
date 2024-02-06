(function () { 
     
    $("#DDlPartners").hide();
    $("#RadioProducts").hide();
    $("#DDlProduct").hide();
    $('#PartnerID,#ProductID').select2({
        multiple: true,
        width: '100%',
        // Adjust the width as needed
    });
    $(function () {
        var GetPartnerValue = $("input[name='PartnerType']:checked").val();
        var GetProductValue = $("input[name='ProductType']:checked").val(); 
        var clientPortalValue = $("#clientportal").val();
        var nextstageValue = $("#nextstage").val();
        var WorkFlowDocumentID = $("#workFlowStepId").val();
         
        $("#workFlowStepModelId").val(WorkFlowDocumentID); 
        var _documentTypesService = abp.services.app.documentTypes;
        var _workflowStepDocumentCheckListsService = abp.services.app.workflowStepDocumentCheckLists; 
        var input = "";
        _documentTypesService.getAll(input)
            .done(function (data) {
                 
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
                 
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });

        var dataRows = [];
        function populateDropdown(data) {

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

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };

        $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('.startDate')
            .daterangepicker({
                autoUpdateInput: false,
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            })
            .on('apply.daterangepicker', (ev, picker) => {
                $selectedDate.startDate = picker.startDate;
                getDocumentCheckList();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getDocumentCheckList();
            });

        $('.endDate')
            .daterangepicker({
                autoUpdateInput: false,
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            })
            .on('apply.daterangepicker', (ev, picker) => {
                $selectedDate.endDate = picker.startDate;
                getDocumentCheckList();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getDocumentCheckList();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
        //    edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
        //    delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/AddCheckList',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DocumentCheckList/_CreateOrEditModal.js',
            modalClass: 'AddCheckList',
        });

        //var _viewFeeTypeModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/ViewLeadFormModal',
        //    modalClass: 'ViewLeadFormModal',
        //});

        //$(document).on('click', '#ProductID', function () {
        //   
        //  $("#RadioProducts").show();
        //});
        $('#PartnerID').on('change', function () {
             
            var PartnerId = $('#PartnerID').val();

            var partnerIdArray = $('#PartnerID').val();

            // Extract the string value and remove surrounding quotes
            var PartnerId = partnerIdArray[0].replace(/^['"]|['"]$/g, '');

             
            $("#RadioProducts").show();

            if (PartnerId > 0) {
                $.ajax({
                    url: abp.appPath + 'api/services/app/Products/GetAll',
                    data: {
                        PartnerIdFilter: PartnerId
                    },
                    method: 'GET',
                    dataType: 'json',

                    success: function (data) {
                         
                        populateDropdown(data);
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });

                var dataRows = [];
                function populateDropdown(data) {

                    var dropdown = $('#ProductID');


                    dropdown.empty();

                    $.each(data.result.items, function (index, item) {
                         
                        if (item && item.id !== null && item.product.id !== undefined && item.product.name !== null && item.product.name !== undefined) {
                            dropdown.append($('<option></option>').attr('value', item.product.id).attr('data-id', item.product.id).text(item.product.name));
                        } else {
                            console.warn('Invalid item:', item);
                        }
                    });
                }
            }
        });
        $(document).on('click', '#AllProduct', function () {
            $("#DDlProduct").hide();
            GetProductValue = false;

        });

        $(document).on('click', '#selectedProduct', function () {
            $("#DDlProduct").show()
            GetProductValue = true;


        });
        $(document).on('click', '#AllPartner', function () {
            $("#DDlPartners").hide();
            GetPartnerValue = false;

             
        });

        $(document).on('click', '#selectedPartner', function () {
            $("#DDlPartners").show();
            GetPartnerValue = true;

             
        });
        $(document).on('click', '#clientportal', function () {
             

            var clientPortalValue = $(this).prop("checked");
             
            alert(clientPortalValue);
            
        });
        $(document).on('click', '#nextstage', function () {
             
             nextstageValue = $(this).prop("checked");

            alert(nextstageValue);
        });

        //$('#NewCheckList').click(function () {
        //     
        //    _createOrEditModal.open();
        //});

        abp.event.on('app.createOrEditFeeTypeModalSaved', function () {
             
            getDocumentCheckList();
        }); 
        $(document).on('click', '.save-button', function () {
             
            var jsonData = {
                Name : $("#Description").val(),
                Description : $("#Description").val(),
                 IsForAllPartners : GetPartnerValue,
                 IsForAllProducts : GetProductValue,
                 AllowOnClientPortal : clientPortalValue, 
                WorkflowStepId: $("#workFlowStepModelId").val(),
                DocumentTypeId : $("#AddNewCheckListDocumentType").val()
            }
            var jsonData = JSON.stringify(jsonData);
            console.log(jsonData);
            jsonData = JSON.parse(jsonData);


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

            jsonData.DocumentCheckListPartner = PartnerId
            jsonData.DocumentCheckListProduct = ProductId


            _workflowStepDocumentCheckListsService
                .createOrEdit(jsonData)
                .done(function () {
                     
                    abp.notify.info(app.localize('SavedSuccessfully'));
                     
                    location.reload();
                    abp.event.trigger('app.createOrEditWorkflowModalSaved');
                })
                .always(function () {
                    alert("Error")
                    //_modalManager.setBusy(false);
                });
        });
    });
})();
