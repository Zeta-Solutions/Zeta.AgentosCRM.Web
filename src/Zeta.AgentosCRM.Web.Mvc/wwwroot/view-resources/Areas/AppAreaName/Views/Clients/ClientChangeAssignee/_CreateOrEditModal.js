(function ($) {
    app.modals.CreateOrEditChangeAssignee = function () {  
        var _clientsService = abp.services.app.clients;

        //$('#AssigneeID').select2({
        //    multiple: true,
        //    width: '100%',
        //    placeholder: 'Select Invitees',
        //    // Adjust the width as needed
        //});
        $.ajax({
            url: abp.appPath + 'api/services/app/AppointmentInvitees/GetAllUserForTableDropdown',
            method: 'GET',
            dataType: 'json', 
            success: function (data) { 
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function populateDropdown(data) {
             
            var dropdown = $('#AssigneeID');

            dropdown.empty();

            dropdown.append($('<option></option>').attr('value', '0').attr('data-id', '0').text('Select Assignee'));

            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
         
        var _modalManager;
        var _$countriesInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$countriesInformationForm = _modalManager.getModal().find('form[name=CountryInformationsForm]');
            _$countriesInformationForm.validate();
        };

        this.save = function () {
            //if (!_$countriesInformationForm.valid()) {
            //    return;
            //}
             
            var anyChecked = $('.custom-checkbox:checked').length > 0;

            if (anyChecked) {
                //var checkedValues = [];
         
                $('.custom-checkbox:checked').each(function () {
                    var ClientID = $(this).closest('tr').find('.clientchangeassignee').val();
                    var assignnid = $('#AssigneeID').val();

                    // Do something with ClientID and assignnid, or push them into an array
                    
                    var checkedValues = {
                        clientId: ClientID,
                        assigneeId: assignnid
                    };
                    checkedValues = JSON.stringify(checkedValues);
                     
                    checkedValues = JSON.parse(checkedValues);
                    _clientsService
                        .updateClientAssignee(checkedValues)
                        .done(function () {
                            abp.notify.info(app.localize('Assignee Change Successfully'));
                            location.reload();
                        })
                        .always(function () {
                            _modalManager.setBusy(false);
                        }); 
                });

                // Now, checkedValues array contains the ClientID and AssigneeID of the checked checkboxes in the current row
                console.log(checkedValues);
            } else {
                // No checkboxes are checked
            }

            //$.each(checkedValues, function (index, value) {
            //    var datarowsItem = {
            //        OrganizationUnitId: datarowsList[index]
            //    }
            //    officerows.push(datarowsItem);
            //});
           
            


             
          
                
        };
    };
})(jQuery);
