(function ($) {
    app.modals.CreateOrEditClientsAppoinmentModal = function () {
        debugger;
        $('#inviteesId').select2({
            multiple: true,
            width: '100%',
            // Adjust the width as needed
        });
        $.ajax({
            url: abp.appPath + 'api/services/app/AppointmentInvitees/GetAllUserForTableDropdown',
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
            var dropdown = $('#inviteesId');

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
                url: abp.appPath + 'api/services/app/Appointments/GetAppointmentForEdit?id=' + idValue,
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
            $.each(data.result.appointmentinvitees, function (index, obj) {
                ms_val += "," + obj.userId;

            });

            //var ms_array = ms_val.length > 0 ? ms_val.substring(1).split(',') : [];
            var ms_array = ms_val.split(',');
            var $productId = $("#inviteesId");

          
            $productId.val(ms_array).trigger('change');
           
        }
        var _clientAppointmentsService = abp.services.app.appointments;
      //var hiddenfield = $("#clientId").val();
      var hiddenfield = $("#clientAppID").val();
      //$("#clientId").val(hiddenfield);
      $("#Applointment_ClietName").val(hiddenfield);
      //$("#applointment_Invitees").select2()...;
        var hiddenfield = $("#ID").val();
        $("#ClientId").val(hiddenfield);
        var _modalManager;
        var _$clientTagsInformationForm = null;

        $('input[name*="clientId"]').val(hiddenfield)
        $('input[name*="AddedById"]').val(1);
    var _modalManager;
      var _$clientAppointmentsInformationForm = null;



    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$clientAppointmentsInformationForm = _modalManager.getModal().find('form[name=AppointmentInformationsForm]');
        _$clientAppointmentsInformationForm.validate();
    };

    


        $(document).on('blur', '#applointment_Invitees', function ()
        {
            var applointment_InviteesID=$("#applointment_Invitees").val();
            console.log(applointment_InviteesID);
            $("#AddedById").val(applointment_InviteesID);
        });
    this.save = function () {
        if (!_$clientAppointmentsInformationForm.valid()) {
        return;
        }
        var datarows = [];
        var datarowsList = $("#inviteesId :selected").map(function (i, el) {
            debugger
            return $(el).val();
        }).get();
        $.each(datarowsList, function (index, value) {
            var datarowsItem = {
                UserId: datarowsList[index]
            }
            datarows.push(datarowsItem);
        });
        var Steps = JSON.stringify(datarows);

        Steps = JSON.parse(Steps);
        debugger
        var ClientAppointment = _$clientAppointmentsInformationForm.serializeFormToObject();
        ClientAppointment.Steps = Steps;
       
      _modalManager.setBusy(true);
        _clientAppointmentsService
            .createOrEdit(ClientAppointment)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
            abp.event.trigger('app.createOrEditClientAppointmentModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
