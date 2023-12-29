(function ($) { 
    app.modals.CreateOrEditApplicationModal = function () {
        var _applicationsService = abp.services.app.applications;
        var ClientName = $("#clientAppID").val();
        $("#Name").val(ClientName);
        var hiddenfield = $("#ID").val();
        $("#ClientId").val(hiddenfield);
        var _modalManager;
        var _$clientTagsInformationForm = null;
        debugger
        $('input[name*="clientId"]').val(hiddenfield)
        var _modalManager;
        var _$applicationsInformationForm = null;

        // Initialize Select2 for local search
        $('#WorkflowId').select2({
            width: '100%',
            dropdownParent: $('#WorkflowId').parent(),
        });
         
        //$('#PartnerId').select2({
        //    width: '100%',
        //    dropdownParent: $('#PartnerId').parent(),
        //});
        $('#branchId').select2({
            width: '100%',
            dropdownParent: $('#branchId').parent(),
      
        });
        $('#productId').select2({
            width: '100%',
            dropdownParent: $('#productId').parent(),
        });
        var idValue = $("#WorkflowId").val();
        if (idValue > 0) { 
            $.ajax({
                url: abp.appPath + 'api/services/app/Branches/GetBranchbyWorkflowId?workflowId=' + idValue,
                method: 'GET',
                dataType: 'json',
                //data: {
                //    PartnerIdFilter: dynamicValue,
                //},
                success: function (data) {
                    debugger
                    // Populate the dropdown with the fetched data....
                    $("#PartnerId").val(data.result[0].branch.partnerId)
                    populateDropdown(data);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        } else { 
            $('#productId').prepend($('<option></option>').attr('value', '').text('Select Product'));
            $('#branchId').prepend($('<option></option>').attr('value', '').text('Select Branch'));

        }
        function populateDropdown(data) {
            debugger
            var dropdown = $('#branchId');

            dropdown.empty();
            dropdown.prepend($('<option></option>').attr('value', '').text('Select Branch'));
            $.each(data.result, function (index, item) {
                if (item && item.branch && item.branch.name !== null && item.branch.name !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.branch.id).attr('data-id', item.branch.id).text(item.branch.name));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        
        $(document).on("change", "#WorkflowId", function () {
        debugger
            var idValue = $(this).val();
            if (idValue > 0) {

                $.ajax({
                    url: abp.appPath + 'api/services/app/Branches/GetBranchbyWorkflowId?workflowId=' + idValue,
                    method: 'GET',
                    dataType: 'json',
                    //data: {
                    //    PartnerIdFilter: dynamicValue,
                    //},
                    success: function (data) {
                        debugger


                        if (data == null || data == 0 || data == undefined || data.result.length == 0) {
                            $('#branchId').empty();

                            $('#productId').empty();  
                            $('#productId').prepend($('<option></option>').attr('value', '').text('Select Product'));
                            $('#branchId').prepend($('<option></option>').attr('value', '').text('Select Branch'));
 
                        }
                        else {
                            populateDropdown(data);
                            $("input[name='PartnerId']").val(data.result[0].branch.partnerId)
                        }
                    },
                    error: function (error) {

                        console.error('Error fetching data:', error);
                    }
                });

            } else {
                $('#branchId').empty();
                $('#productId').empty();
                  
                    $('#productId').prepend($('<option></option>').attr('value', '').text('Select Product'));
                    $('#branchId').prepend($('<option></option>').attr('value', '').text('Select Branch'));

                
            }
        });
        $(document).on("change", "#branchId", function () {
            debugger
            var idBranchValue = $(this).val();
            if (idBranchValue > 0) {
                $.ajax({
                    url: abp.appPath + 'api/services/app/ProductBranches/GetAll?BranchIdFilter=' + idBranchValue,
                    method: 'GET',
                    dataType: 'json',
                    //data: {
                    //    PartnerIdFilter: dynamicValue,
                    //},
                    success: function (data) {

                        // Populate the dropdown with the fetched data......
                        populateProductDropdown(data);
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
            }
            else {

                $('#productId').empty();
              
                    $('#productId').prepend($('<option></option>').attr('value', '').text('Select Product')); 

                
            }

        });
        function populateProductDropdown(data) {
            debugger;
            var dropdown = $('#productId');

            dropdown.empty();
            dropdown.prepend($('<option></option>').attr('value', '').text('Select Product'));

            $.each(data.result.items, function (index, item) {
                debugger
                if (item && item.productBranch && item.productBranch.id !== null && item.productBranch.id !== undefined && item.productName !== null && item.productName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.productBranch.id).attr('data-id', item.productBranch.id).text(item.productName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }


        if ($('input[name="id"]').val() > 0) {
            id = $('input[name="id"]').val()
            $.ajax({
                url: abp.appPath + 'api/services/app/Applications/GetApplicationForEdit?Id=' + id,
                method: 'GET',
                dataType: 'json',
                //data: {
                //    PartnerIdFilter: dynamicValue,
                //},
                success: function (data) {

                    // Populate the dropdown with the fetched data....
                    updateDropdown(data);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        }
        function updateDropdown(data) {
            debugger;
            var ms_val = 0;
        
            $('#branchId').val(data.result.application.branchId).trigger('change.select2');
            var idBranchValue = $("#branchId").val();
            debugger
            setTimeout(function () {
                $.ajax({
                    url: abp.appPath + 'api/services/app/ProductBranches/GetAll?BranchIdFilter=' + idBranchValue,
                    method: 'GET',
                    dataType: 'json',
                    success: function (data1) {
                        // Populate the dropdown with the fetched data......
                        populateProductDropdown(data1);
                        $('#productId').val(data.result.application.productId).trigger('change.select2');
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
            }, 100);
    
            // Assuming data.result.promotionproduct is an array of objects with OwnerID property
           

           

        }

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$applicationsInformationForm = _modalManager.getModal().find('form[name=ApplicationsInformationsForm]');
            _$applicationsInformationForm.validate();
        };

        $(document).on('select2:open', function () {
            var $searchField = $('.select2-search__field');
            $searchField.on('keydown', function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        });
      
    
       
        this.save = function () {  
            debugger

            if (!_$applicationsInformationForm.valid()) {
                return;
            }
            branch = $("#branchId").val()
            $("#BranchId").val(branch)
            product = $("#productId").val()
            $("#ProductId").val(product)

            var application = _$applicationsInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _applicationsService
                .createOrEdit(application)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditApplicationsModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
