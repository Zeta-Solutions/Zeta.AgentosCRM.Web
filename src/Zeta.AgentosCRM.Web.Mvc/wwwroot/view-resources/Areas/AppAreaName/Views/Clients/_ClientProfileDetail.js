(function () {

    $(function () {

        var _clientsTagsService = abp.services.app.clientTags;
        var _clientsFollowersService = abp.services.app.followers;
        var _entityTypeFullName = 'Zeta.AgentosCRM.CRMClient.Client';
        // alert(_clientsService);
        var $selectedDate = {
            startDate: null,
            endDate: null,
        } 
        getClientsTags();
        getClientsFollowers();
        $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('.startDate').daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L',
        })
            .on("apply.daterangepicker", (ev, picker) => {
                $selectedDate.startDate = picker.startDate;
                getClients();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val("");
                $selectedDate.startDate = null;
                getClients();
            });

        $('.endDate').daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L',
        })
            .on("apply.daterangepicker", (ev, picker) => {
                $selectedDate.endDate = picker.startDate;
                getClients();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val("");
                $selectedDate.endDate = null;
                getClients();
            });






        var _createOrEditClientTagModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditClientTags',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_CreateOrEditClientTags.js',
            modalClass: 'CreateOrEditClientTagsModal'
        });
        var _createOrEditClientFllowersModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditClientFllowers',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_CreateOrEditClientFollowers.js',
            modalClass: 'CreateOrEditClientFollwersModal'
        });





        //Client Tags Modal
        $('#CreateNewClientsTagsButton').click(function () {

            _createOrEditClientTagModal.open();
        });
      
        $('#CreateNewClientsFllowersButton').click(function () {

            _createOrEditClientFllowersModal.open();

        });
      
     




        $(document).keypress(function (e) {
            if (e.which === 13) {
                getClients();
            }
        });

        $('.reload-on-change').change(function (e) {
            getClients();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getClients();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getClients();
        });


        function getClientsTags() {
            var ClientTagId = $("#ID").val();
             
            $.ajax({
                url: abp.appPath + 'api/services/app/ClientTags/GetAll',
                data: {
                    ClientId: ClientTagId,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) { 
                    var test = data.result.items
                  
                    
                    // Assuming 'test' is an array of objects with 'tagName' and 'clientTag' properties
                    var uniqueTags = [];

                    // Extract unique tag names from the 'test' array
                    test.forEach(function (item) {
                        if (!uniqueTags.includes(item.tagName)) {
                            uniqueTags.push(item.tagName);
                        }
                    });

                    // Create buttons for unique tag names and their associated tag IDs
                    uniqueTags.forEach(function (tagName) {
                        var correspondingTag = test.find(function (item) {
                            return item.tagName === tagName;
                        });

                        var tagId = correspondingTag.clientTag.tagId;

                        // Construct HTML for a button with the given tagId and tagName values
                        var buttonHTML = '<div class="col-12 f-2"><button class="deleteTag"  value="' + tagId + '" style="display: inline-block; margin-right: 10px;"><span style="font-size: 11px;">' + tagName + '</span> ✖</button></div>';

                        $(".List").append(buttonHTML); // Append the button HTML to the element with class "List"
                    });
                      
                })
         
                .fail(function (error) {
                    debugger
                    console.error('Error fetching data:', error);
                });
        }
        $(document).on('click', '.deleteTag', function () {

            debugger
            var tagId = 2;// $(this).val(); // Retrieve the value of the clicked button

            // Use the retrieved tagId value as needed
            //console.log("Tag ID:", tagId); // For example, log the tagId to the console
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientsTagsService
                        .delete({
                            id: tagId,
                        })
                        .done(function () {
                            //getClientss(true);
                            debugger
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        });

        function getClientsFollowers() {
            var ClientFollowersId = $("#ID").val();
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
                        var buttonHTML = '<button class="deleteUser" value="' + userId + '" style="display: inline-block; margin-right: 10px;"><span style="font-size: 11px;">' + userName + '</span> ✖</button>';

                        // Append the button HTML to the element with class "followersList"
                        $(".followersList").append(buttonHTML);

                        //var userId = correspondingTag.follower.userId;

                        //// Construct HTML for a button with the given tagId and tagName values
                        //var buttonHTML = '<div class="col-12"><button class="deleteUser" value="' + userId + '"><span style="font-size: 11px;">' + userName + '</span> ✖</button></div>';

                        ////$(".followersList").append(buttonHTML);
                        //$(".followersList").append('<div class="col-12" >' + buttonHTML + '</div>');// Append the button HTML to the element with class "List"

                    });
                })

                .fail(function (error) {
                    debugger
                    console.error('Error fetching data:', error);
                });
        }
        $(document).on('click', '.deleteUser', function () {

            debugger
            var UserId =  $(this).val(); // Retrieve the value of the clicked button

            // Use the retrieved tagId value as needed
            //console.log("User ID:", UserId); // For example, log the tagId to the console
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientsFollowersService
                        .delete({
                            id: UserId,
                        })
                        .done(function () {
                            //getClientss(true);
                            debugger
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        });
    });
})();
