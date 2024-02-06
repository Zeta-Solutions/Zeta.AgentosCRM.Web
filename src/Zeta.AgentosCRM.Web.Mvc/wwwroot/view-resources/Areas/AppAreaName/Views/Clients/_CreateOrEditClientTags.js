(function ($) {

    $('#TagId').select2({
        width: '100%',
        // Adjust the width as needed
    });
    app.modals.CreateOrEditClientTagsModal = function () {
         
        var _clientTagsService = abp.services.app.clientTags;
        var hiddenfield = $('input[name="Clientid"]').val();
        $("#ClientId").val(hiddenfield);
        var _modalManager;
        var _$clientTagsInformationForm = null;

        $('input[name*="clientId"]').val(hiddenfield)
        //getClientsTags();
        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$clientTagsInformationForm = _modalManager.getModal().find('form[name=ClientTagsInformationsForm]');
            _$clientTagsInformationForm.validate();
        };


        function getClientsTags() {
            var ClientTagId = $('input[name="Clientid"]').val();

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
                        var buttonHTML = '<div class="col-12"><button class="deleteTag" value="' + tagId + '"><span>' + tagName + '</span> ?</button></div>';

                        $(".List").append(buttonHTML); // Append the button HTML to the element with class "List"
                    });

                })

                .fail(function (error) {
                     
                    console.error('Error fetching data:', error);
                });
        }




        this.save = function () {
            if (!_$clientTagsInformationForm.valid()) {
                return;
            }


            var Tags = _$clientTagsInformationForm.serializeFormToObject();
             
            console.log(Tags);
            _modalManager.setBusy(true);
            _clientTagsService
                .createOrEdit(Tags)
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
