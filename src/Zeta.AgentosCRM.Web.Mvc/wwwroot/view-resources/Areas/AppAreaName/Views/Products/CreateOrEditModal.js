(function () {
    $(function () {
        debugger
        //const urlParams = new URLSearchParams(window.location.search);
        //const partnerIdValue = urlParams.get('partnerId');

        // For example, if you want to set it in an input field with the id 'productId'
        //$("#productId").val(partnerIdValue);
        $('#partnerId').select2({
            width: '100%',
            // Adjust the width as needed
        });
        //if (partnerIdValue != null) {
        //    $('#partnerId').val(partnerIdValue).trigger('change');
        //}
        //$('#partnerId').prop('disabled', true);
        $('#branchId').select2({
            multiple: true,
            width: '100%',
            // Adjust the width as needed
        });
        $('#partnerTypeId').select2({
            width: '100%',
            // Adjust the width as needed
        });
        $('#intakeMonth').select2({
            width: '100%',
            // Adjust the width as needed
        });
        var productId = $('input[name="id"]').val();
        var imageUrl = $.ajax({
            url: abp.appPath + 'api/services/app/ProductProfile/GetProfilePictureByProduct',
            data: {
                productId: productId,
            },
            method: 'GET',
            dataType: 'json',
        })
            .done(function (data) {
                debugger
                console.log('Response from server:', data);
                $('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
            })
            .fail(function (error) {
                console.error('Error fetching data:', error);
            });
        var hiddenfield = 38;

        $("#BranchId").val(hiddenfield);

        $.ajax({
            url: abp.appPath + 'api/services/app/Products/GetAllBranchForTableDropdown',
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
            var dropdown = $('#branchId');

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
                url: abp.appPath + 'api/services/app/Products/GetProductForEdit?id=' + idValue,
                method: 'GET',
                dataType: 'json',
                success: function (data) {
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

            // Assuming data.result.promotionproduct is an array of objects with OwnerID property..
            $.each(data.result.branches, function (index, obj) {
                ms_val += "," + obj.branchId;

            });

            //var ms_array = ms_val.length > 0 ? ms_val.substring(1).split(',') : [];
            var ms_array = ms_val.split(',');
            var $productId = $("#branchId");


            $productId.val(ms_array).trigger('change');

        }





        var _productsService = abp.services.app.products;

        var _$productInformationForm = $('form[name=ProductInformationsForm]');
        _$productInformationForm.validate();

        var _ClientcountryLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CountryLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientCountryLookupTableModal.js',
            modalClass: 'CountryLookupTableModal'
        }); var _ClientuserLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientUserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        }); var _ClientbinaryObjectLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/BinaryObjectLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientBinaryObjectLookupTableModal.js',
            modalClass: 'BinaryObjectLookupTableModal'
        }); var _ClientdegreeLevelLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/DegreeLevelLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientDegreeLevelLookupTableModal.js',
            modalClass: 'DegreeLevelLookupTableModal'
        }); var _ClientsubjectAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/SubjectAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientSubjectAreaLookupTableModal.js',
            modalClass: 'SubjectAreaLookupTableModal'
        }); var _ClientleadSourceLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/LeadSourceLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/_ClientLeadSourceLookupTableModal.js',
            modalClass: 'LeadSourceLookupTableModal'
        });
        var changeProfilePictureModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Products/_ChangePictureModal.js',
            modalClass: 'ChangeProfilePictureModal',
        });

        $('#changeProfilePicture').click(function () {
            changeProfilePictureModal.open({ userId: $('input[name=Id]').val() });
        });

        changeProfilePictureModal.onClose(function () {
            $('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId=" + $('input[name=Id]').val());
        });


        $('.date-picker').daterangepicker({
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        $('#OpenCountryLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryCodeId, displayName: client.countryDisplayProperty }, function (data) {
                _$clientInformationForm.find('input[name=countryDisplayProperty]').val(data.displayName);
                _$clientInformationForm.find('input[name=countryCodeId]').val(data.id);
            });
        });

        $('#ClearCountryDisplayPropertyButton').click(function () {
            _$clientInformationForm.find('input[name=countryDisplayProperty]').val('');
            _$clientInformationForm.find('input[name=countryCodeId]').val('');
        });

        $('#OpenUserLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientuserLookupTableModal.open({ id: client.assigneeId, displayName: client.userName }, function (data) {
                _$clientInformationForm.find('input[name=userName]').val(data.displayName);
                _$clientInformationForm.find('input[name=assigneeId]').val(data.id);
            });
        });

        $('#ClearUserNameButton').click(function () {
            _$clientInformationForm.find('input[name=userName]').val('');
            _$clientInformationForm.find('input[name=assigneeId]').val('');
        });

        $('#OpenBinaryObjectLookupTableButton').click(function () {

            var client = _$productInformationForm.serializeFormToObject();

            _ClientbinaryObjectLookupTableModal.open({ id: client.profilePictureId, displayName: client.binaryObjectDescription }, function (data) {
                _$productInformationForm.find('input[name=binaryObjectDescription]').val(data.displayName);
                _$productInformationForm.find('input[name=profilePictureId]').val(data.id);
            });
        });

        $('#ClearBinaryObjectDescriptionButton').click(function () {
            _$clientInformationForm.find('input[name=binaryObjectDescription]').val('');
            _$clientInformationForm.find('input[name=profilePictureId]').val('');
        });

        $('#OpenDegreeLevelLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientdegreeLevelLookupTableModal.open({ id: client.highestQualificationId, displayName: client.degreeLevelName }, function (data) {
                _$clientInformationForm.find('input[name=degreeLevelName]').val(data.displayName);
                _$clientInformationForm.find('input[name=highestQualificationId]').val(data.id);
            });
        });

        $('#ClearDegreeLevelNameButton').click(function () {
            _$clientInformationForm.find('input[name=degreeLevelName]').val('');
            _$clientInformationForm.find('input[name=highestQualificationId]').val('');
        });

        $('#OpenSubjectAreaLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientsubjectAreaLookupTableModal.open({ id: client.studyAreaId, displayName: client.subjectAreaName }, function (data) {
                _$clientInformationForm.find('input[name=subjectAreaName]').val(data.displayName);
                _$clientInformationForm.find('input[name=studyAreaId]').val(data.id);
            });
        });

        $('#ClearSubjectAreaNameButton').click(function () {
            _$clientInformationForm.find('input[name=subjectAreaName]').val('');
            _$clientInformationForm.find('input[name=studyAreaId]').val('');
        });

        $('#OpenLeadSourceLookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientleadSourceLookupTableModal.open({ id: client.leadSourceId, displayName: client.leadSourceName }, function (data) {
                _$clientInformationForm.find('input[name=leadSourceName]').val(data.displayName);
                _$clientInformationForm.find('input[name=leadSourceId]').val(data.id);
            });
        });

        $('#ClearLeadSourceNameButton').click(function () {
            _$clientInformationForm.find('input[name=leadSourceName]').val('');
            _$clientInformationForm.find('input[name=leadSourceId]').val('');
        });

        $('#OpenCountry2LookupTableButton').click(function () {

            var client = _$clientInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryId, displayName: client.countryName2 }, function (data) {
                _$clientInformationForm.find('input[name=countryName2]').val(data.displayName);
                _$clientInformationForm.find('input[name=countryId]').val(data.id);
            });
        });

        $('#ClearCountryName2Button').click(function () {
            _$clientInformationForm.find('input[name=countryName2]').val('');
            _$clientInformationForm.find('input[name=countryId]').val('');
        });

        $('#OpenCountry3LookupTableButton').click(function () {

            var client = _$productInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.passportCountryId, displayName: client.countryName3 }, function (data) {
                _$productInformationForm.find('input[name=countryName3]').val(data.displayName);
                _$productInformationForm.find('input[name=passportCountryId]').val(data.id);
            });
        });

        $('#ClearCountryName3Button').click(function () {
            _$clientInformationForm.find('input[name=countryName3]').val('');
            _$clientInformationForm.find('input[name=passportCountryId]').val('');
        });



        function save(successCallback) {
            if (!_$productInformationForm.valid()) {
                return;
            }
            //if ($('#Partner_PartnerName').prop('required') && $('#Partner_PartnerName').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('PartnerName')));
            //    return;
            //}
            //if ($('#workflowId').prop('required') && $('#workflowId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('workflow')));
            //    return;
            //}
            //if ($('#partnerTypeId').prop('required') && $('#partnerTypeId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('partnerType')));
            //    return;
            //}
            //if ($('#masterCategoryId').prop('required') && $('#masterCategoryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('masterCategory')));
            //    return;
            //}
            //if ($('#Partner_Email').prop('required') && $('#Partner_Email').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Email')));
            //    return;
            //}
            //if ($('#University').prop('required') && $('#University').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('University')));
            //    return;
            //}
            //if ($('#Client_CountryId').prop('required') && $('#Client_CountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}
            //if ($('#Client_PassportCountryId').prop('required') && $('#Client_PassportCountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}
            var datarows = [];
            var datarowsList = $("#branchId :selected").map(function (i, el) {
                debugger
                return $(el).val();
            }).get();
            $.each(datarowsList, function (index, value) {
                var datarowsItem = {
                    BranchId: datarowsList[index]
                }
                datarows.push(datarowsItem);
            });
            var Branches = JSON.stringify(datarows);

            Branches = JSON.parse(Branches);


            var partner = _$productInformationForm.serializeFormToObject();
            partner.Branches = Branches;


            abp.ui.setBusy();
            _productsService.createOrEdit(
                partner
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditProductModalSaved');

                if (typeof (successCallback) === 'function') {
                    successCallback();
                }
            }).always(function () {
                abp.ui.clearBusy();
            });

        };

        function clearForm() {
            debugger
            _$productInformationForm[0].reset();
        }

        $('#saveBtn').click(function () {
            debugger
            //var titleValue = $(".iti__selected-flag").attr("aria-activedescendant");

            //var subcode = titleValue.split("-");



            //// Save the title value to a field (e.g., an input field with the ID "myField")
            //$("#PhoneCode").val(subcode[2]);
            save(function () {
                window.location = "/AppAreaName/Products";
            });
        });

        $('#saveAndNewBtn').click(function () {
            //var titleValue = $(".iti__selected-flag").attr("aria-activedescendant");

            //var subcode = titleValue.split("-");



            //// Save the title value to a field (e.g., an input field with the ID "myField")
            //$("#PhoneCode").val(subcode[2]);
            save(function () {
                if (!$('input[name=id]').val()) {//if it is create page
                    clearForm();
                }
            });
        });


    });
})();