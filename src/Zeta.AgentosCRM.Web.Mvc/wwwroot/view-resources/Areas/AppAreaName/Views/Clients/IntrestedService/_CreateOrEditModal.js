(function ($) {
  app.modals.CreateOrEditInterestedServicesModal = function () {
      var _clientInterstedServices = abp.services.app.clientInterstedServices;
      var ClientName = $("#clientAppID").val();
      $("#Name").val(ClientName);
      var hiddenfield = $("#ID").val();
      $("#ClientId").val(hiddenfield);
      var _modalManager;
      var _$clientTagsInformationForm = null;
      $('#WorkflowId').select2({
          width: '100%',
          dropdownParent: $('#WorkflowId').parent(),
      });
      $('#partnerId').select2({
          width: '100%',
          dropdownParent: $('#partnerId').parent(),
      });
      $('#productId').select2({
          width: '100%',
          dropdownParent: $('#productId').parent(),
      });
      $('#branchId').select2({
          width: '100%',
          dropdownParent: $('#branchId').parent(),
      });
      $('input[name*="clientId"]').val(hiddenfield)

      var idValue = $("#WorkflowId").val();
      $.ajax({
          url: abp.appPath + 'api/services/app/Partners/GetAll?WorkFlowIdFilter=' + idValue,
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
      $(document).on("change", "#WorkflowId", function () {

          var idValue = $(this).val();
          $.ajax({
              url: abp.appPath + 'api/services/app/Partners/GetAll?WorkFlowIdFilter=' + idValue,
              method: 'GET',
              dataType: 'json',
              //data: {
              //    PartnerIdFilter: dynamicValue,
              //},
              success: function (data) {

                  // Populate the dropdown with the fetched data....
                  populateDropdown(data);

              },
              error: function (error) {
                  console.error('Error fetching data:', error);
              }
          });


      });
      $(document).on("change", "#partnerId", function () {

          var idValue = $(this).val();
          $.ajax({
              url: abp.appPath + 'api/services/app/Products/GetAll?PartnerIdFilter=' + idValue,
              method: 'GET',
              dataType: 'json',
              //data: {
              //    PartnerIdFilter: dynamicValue,
              //},
              success: function (data) {

                  // Populate the dropdown with the fetched data....
                  populateProductDropdown(data);

              },
              error: function (error) {
                  console.error('Error fetching data:', error);
              }
          });


      });
      $(document).on("change", "#productId", function () {
          debugger
          var idValue = $(this).val();
          $.ajax({
              url: abp.appPath + 'api/services/app/ProductBranches/GetAll?ProductIdFilter=' + idValue,
              method: 'GET',
              dataType: 'json',
              //data: {
              //    PartnerIdFilter: dynamicValue,
              //},
              success: function (data) {

                  // Populate the dropdown with the fetched data....
                  populatebranchDropdown(data);

              },
              error: function (error) {
                  console.error('Error fetching data:', error);
              }
          });


      });
      function populateDropdown(data) {
          debugger
          var dropdown = $('#partnerId');

          dropdown.empty();
          dropdown.prepend($('<option></option>').attr('value', '').text('Select Partner'));
          $.each(data.result.items, function (index, item) {
              if (item && item.partner && item.partner.partnerName !== null && item.partner.partnerName !== undefined) {
                  dropdown.append($('<option></option>').attr('value', item.partner.id).attr('data-id', item.partner.id).text(item.partner.partnerName));
              } else {
                  console.warn('Invalid item:', item);
              }
          });
      }
      function populateProductDropdown(data) {
          debugger
          var dropdown = $('#productId');

          dropdown.empty();
          dropdown.prepend($('<option></option>').attr('value', '').text('Select Partner'));
          $.each(data.result.items, function (index, item) {
              if (item && item.product && item.product.name !== null && item.product.name !== undefined) {
                  dropdown.append($('<option></option>').attr('value', item.product.id).attr('data-id', item.product.id).text(item.product.name));
              } else {
                  console.warn('Invalid item:', item);
              }
          });
      }
      function populatebranchDropdown(data) {
          debugger;
          var dropdown = $('#branchId');

          dropdown.empty();
          dropdown.prepend($('<option></option>').attr('value', '').text('Select Product'));

          $.each(data.result.items, function (index, item) {
              debugger
              if (item && item.productBranch && item.productBranch.id !== null && item.productBranch.id !== undefined && item.branchName !== null && item.branchName !== undefined) {
                  dropdown.append($('<option></option>').attr('value', item.productBranch.id).attr('data-id', item.productBranch.id).text(item.branchName));
              } else {
                  console.warn('Invalid item:', item);
              }
          });
      }
      debugger
      if ($('input[name="id"]').val() > 0) {
          id = $('input[name="id"]').val()
          $.ajax({
              url: abp.appPath + 'api/services/app/ClientInterstedServices/GetClientInterstedServiceForEdit?Id=' + id,
              method: 'GET',
              dataType: 'json',
              //data: {
              //    PartnerIdFilter: dynamicValue,
              //},
              success: function (responseData) {

                  // Populate the dropdown with the fetched data....
                  updateDropdown(responseData);

                  // Assign the response data to the outer 'data' variable
                  data = responseData;
                 // updatebranchDropdown(data);
              },
              error: function (error) {
                  console.error('Error fetching data:', error);
              },
              complete: function () {
                  // After the first dropdown is fully completed, initiate the second AJAX call
                  setTimeout(function () {
                      updatebranchDropdown(data);
                  }, 3000);
              }
          });
      }
      function updateDropdown(data) {
   
          setTimeout(function () {
          var ms_val = 0;

          $('#partnerId').val(data.result.clientInterstedService.partnerId).trigger('change.select2');
          var idpartnerValue = $("#partnerId").val();
          debugger
          setTimeout(function () {
              $.ajax({
                  url: abp.appPath + 'api/services/app/Products/GetAll?PartnerIdFilter=' + idpartnerValue,
                  method: 'GET',
                  dataType: 'json',
                  success: function (data1) {
                      // Populate the dropdown with the fetched data......
                      populateProductDropdown(data1);
                      $('#productId').val(data.result.clientInterstedService.productId).trigger('change.select2');
                  },
                  error: function (error) {
                      console.error('Error fetching data:', error);
                  }
              });
          }, 1000);

          // Assuming data.result.promotionproduct is an array of objects with OwnerID property

      }, 1000);


      }
      function updatebranchDropdown(data) {
          debugger;
       
              var ms_val = 0;

              
              var idproductValue = $("#productId").val();

                  $.ajax({
                      url: abp.appPath + 'api/services/app/ProductBranches/GetAll?ProductIdFilter=' + idproductValue,
                      method: 'GET',
                      dataType: 'json',
                      success: function (data2) {
                          // Populate the dropdown with the fetched data......
                          populatebranchDropdown(data2);
                          $('#branchId').val(data.result.clientInterstedService.branchId).trigger('change.select2');
                      },
                      error: function (error) {
                          console.error('Error fetching data:', error);
                      }
                  });
      
              // Assuming data.result.promotionproduct is an array of objects with OwnerID property



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
