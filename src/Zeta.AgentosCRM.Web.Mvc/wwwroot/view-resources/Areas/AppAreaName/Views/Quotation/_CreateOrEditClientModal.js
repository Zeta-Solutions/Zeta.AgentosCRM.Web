(function ($) {
    app.modals.CreateOrEditClientModal = function () {
        $('#ClientId').select2({

            width: '100%',
            dropdownParent: $('#ClientId').parent(),
            // Adjust the width as needed
        });
        
        
        $.ajax({
            url: abp.appPath + 'api/services/app/Clients/GetAll',
            method: 'GET',
            dataType: 'json',
            //data: {
            //    PartnerIdFilter: dynamicValue,
            //},
            success: function (data) {

                // Populate the dropdown with the fetched data
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function populateDropdown(data) {
            debugger
            var dropdown = $('#ClientId');

            dropdown.empty();
            dropdown.prepend($('<option></option>').attr('value', '').text('Select Client'));
            $.each(data.result.items, function (index, item) {
                if (item && item.client && item.client.id !== null && item.client.id !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.client.id).attr('data-id', item.client.id).text(item.client.firstName  +  item.client.lastName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        

        var _cRMTasksService = abp.services.app.cRMTasks;

        var _modalManager;
        var _$tasksInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$tasksInformationForm = _modalManager.getModal().find('form[name=TaskApplicationInformationsForm]');
            _$tasksInformationForm.validate();
        };


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
                        }
                    },
                    error: function (error) {

                        console.error('Error fetching data:', error);
                    }
                });

            } 
        });


        $('#saveClientBtn').click(function () {
            debugger
            var hiddenfield = $("#ClientId").val();


            var baseUrl = "/AppAreaName/Clients/CreateOrEditClientQuotationModal/";
            var url = baseUrl + "?clientId=" + hiddenfield;

            // Redirect to the constructed URL
            window.location.href = url;
            // _createOrEditModal.open();


        });

    };
})(jQuery);
