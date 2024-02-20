(function ($) {
    app.modals.CreateOrEditInvoiceTypeModal = function () {
        var _clientInterstedServices = abp.services.app.clientInterstedServices;
        var ClientName = $("#clientAppID").val();
        $("#Invoice_ClietName").val(ClientName);
        var _modalManager;
        var _$clientTagsInformationForm = null;
        var invoicetype = $("#invoicetype").val();
        if (invoicetype === "1") {
            $("#radioGroupContainer").show();
        } else {
            $("#radioGroupBContainer").show();
            var modalTitle = document.querySelector('.modal-title');

            // Set the inner HTML of the <h5> element
            modalTitle.innerHTML = '<span>General Invoice</span>';
        }
        $('#ApplicationInvoiceTypeId').select2({
            width: '100%',
            //dropdownParent: $('#Timezone').parent(),
            // Adjust the width as needed
            templateResult: formatSearch,
            templateSelection: formatSelected
        });
        function formatSearch(item) {
            var selectionText = item.text.split("|");
            var $returnString = $('<span style="justify-content:space-between;display: flex;">' + selectionText[0] + '</br><b>' + selectionText[1] + '<b style=""></span>' + selectionText[2] + '</span>');
            return $returnString;
        }; 
        function formatSelected(item) {
            var selectionText = item.text.split("|");
            var $returnString = $('<span>' + selectionText[0].substring(0, 21) + '</span>');
            return $returnString;
        };
        var clientID=$('input[name="Clientid"]').val()

        var idValue = $("#ApplicationInvoiceTypeId").val();
       
            $.ajax({
                url: abp.appPath + 'api/services/app/Applications/GetAll?ClientIdFilter=' + clientID,
                method: 'GET',
                dataType: 'json',
                //data: {
                //    PartnerIdFilter: dynamicValue,
                //},
                success: function (data) {
                    debugger
                    // Populate the dropdown with the fetched data....
                    populateDropdown(data);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
       
      
        function populateDropdown(data) {
            
            var dropdown = $('#ApplicationInvoiceTypeId');

            dropdown.empty();
            dropdown.prepend($('<option></option>').attr('value', '').text('Select Application| | '));
           /* <option value="1">Pacheco|Fmcia Pacheco|11443322551</option>*/
            $.each(data.result.items, function (index, item) {
                debugger
                var optionText = '<span class="text-semi-bold">' + item.productName + '</span><br>|' + item.workflowName + '|' + item.partnerPartnerName;
                if (item && item.applicationName && item.applicationName !== null && item.applicationName !== undefined) {
                    dropdown.append(
                        $('<option></option>').attr('value', item.application.id)
                            .attr('data-id', item.application.id)
                            .html(optionText)
                    );
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }

  
   
        var _modalManager;
        var _$_clientInterstedInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$_clientInterstedInformationForm = _modalManager.getModal().find('form[name=IntrestedServiceInformationsForm]');
            _$_clientInterstedInformationForm.validate();
        };

        $(document).on('select2:open', function () {
            var $searchField = $('.select2-search__field');
            $searchField.on('keydown', function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        });
        $('#saveInvoiceTypeBtn').click(function () {
            debugger
            var hiddenfield = $("#ClientId").val();


            var baseUrl = "/AppAreaName/Clients/CreateOrEditInvoiceHeadModal/";
            var url = baseUrl + "?clientId=" + hiddenfield;

            // Redirect to the constructed URL
            window.location.href = url;
            // _createOrEditModal.open();


        });

  
        this.save = function () {
            if (!_$_clientInterstedInformationForm.valid()) {
                return;
            }

            branch = $("#branchId").val()
            $("#BranchId").val(branch)
            product = $("#productId").val()
            $("#ProductId").val(product)
            Partner = $("#partnerId").val()
            $("#PartnerId").val(Partner)
            var InterstedServices = _$_clientInterstedInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _clientInterstedServices
                .createOrEdit(InterstedServices)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditInterstedInformationFormModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
