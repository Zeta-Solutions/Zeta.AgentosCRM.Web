﻿(function ($) {
    $('#UserId').select2({
        width: '100%',
    });
    app.modals.CreateOrEditClientFollwersModal = function () {
         
        var _clientfollowersService = abp.services.app.followers;
        var hiddenfield = $('input[name="Clientid"]').val();
        $("#ClientId").val(hiddenfield);
        var _modalManager;
        var _$clientFollowersInformationForm = null;

        $('input[name*="clientId"]').val(hiddenfield)
        //getClientsFollowers();
        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$clientFollowersInformationForm = _modalManager.getModal().find('form[name=ClientFollowersInformationsForm]');
            _$clientFollowersInformationForm.validate();
        };



        function getClientsFollowers() {
            var ClientFollowersId = $('input[name="Clientid"]').val();
            $.ajax({
                url: abp.appPath + 'api/services/app/Followers/GetAll',
                data: {
                    ClientId: ClientFollowersId,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    var test = data.result.items



                    // Assuming 'test' is an array of objects with 'tagName' and 'clientTag' properties
                    var uniqueFollowers = [];

                    // Extract unique tag names from the 'test' array
                    test.forEach(function (item) {
                        if (!uniqueFollowers.includes(item.userName)) {
                            uniqueFollowers.push(item.userName);
                        }
                    });

                    // Create buttons for unique tag names and their associated tag IDs
                    uniqueFollowers.forEach(function (userName) {
                        var correspondingTag = test.find(function (item) {
                            return item.userName === userName;
                        });

                        var userId = correspondingTag.follower.userId;

                        // Construct HTML for a button with the given tagId and tagName values
                        var buttonHTML = '<div class="col-12"><button style="border: none;" class="deleteUser" value="' + userId + '"><span>' + userName + '</span> ✖</button></div>';

                        $(".followersList").append(buttonHTML); // Append the button HTML to the element with class "List"
                    });
                })

                .fail(function (error) {
                     
                    console.error('Error fetching data:', error);
                });
        }


        this.save = function () {
            if (!_$clientFollowersInformationForm.valid()) {
                return;
            }


            var followers = _$clientFollowersInformationForm.serializeFormToObject();
             
            _modalManager.setBusy(true);
            _clientfollowersService
                .createOrEdit(followers)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditClientTagsModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
