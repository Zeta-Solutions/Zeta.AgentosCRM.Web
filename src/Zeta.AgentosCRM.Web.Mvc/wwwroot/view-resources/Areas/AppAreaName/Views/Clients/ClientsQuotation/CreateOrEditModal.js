(function () {
    $(function () {
        var _clientsQuotationService = abp.services.app.clientQuotationHeads;
        $("#kt_app_sidebar_toggle").trigger("click");
        $('#currencyId').select2({
            width: '420px',
            // Adjust the width as needed
        });
        const urlParams = new URLSearchParams(window.location.search);
        const clientIdValue = urlParams.get('clientId');
        $("#ClientId").val(clientIdValue);
        var _$clientQuotationInformationForm = $('form[name=QuotationInformationsForm]');
        _$clientQuotationInformationForm.validate();

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



        $('.date-picker').daterangepicker({
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        $('#OpenCountryLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.countryId, displayName: client.countryName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=countryName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=countryId]').val(data.id);
            });
        });

        $('#ClearCountryNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=countryName]').val('');
            _$clientQuotationInformationForm.find('input[name=countryId]').val('');
        });

        $('#OpenUserLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientuserLookupTableModal.open({ id: client.assigneeId, displayName: client.userName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=userName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=assigneeId]').val(data.id);
            });
        });

        $('#ClearUserNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=userName]').val('');
            _$clientQuotationInformationForm.find('input[name=assigneeId]').val('');
        });

        $('#OpenBinaryObjectLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientbinaryObjectLookupTableModal.open({ id: client.profilePictureId, displayName: client.binaryObjectDescription }, function (data) {
                _$clientQuotationInformationForm.find('input[name=binaryObjectDescription]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=profilePictureId]').val(data.id);
            });
        });

        $('#ClearBinaryObjectDescriptionButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=binaryObjectDescription]').val('');
            _$clientQuotationInformationForm.find('input[name=profilePictureId]').val('');
        });

        $('#OpenDegreeLevelLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientdegreeLevelLookupTableModal.open({ id: client.highestQualificationId, displayName: client.degreeLevelName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=degreeLevelName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=highestQualificationId]').val(data.id);
            });
        });

        $('#ClearDegreeLevelNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=degreeLevelName]').val('');
            _$clientQuotationInformationForm.find('input[name=highestQualificationId]').val('');
        });

        $('#OpenSubjectAreaLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientsubjectAreaLookupTableModal.open({ id: client.studyAreaId, displayName: client.subjectAreaName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=subjectAreaName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=studyAreaId]').val(data.id);
            });
        });

        $('#ClearSubjectAreaNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=subjectAreaName]').val('');
            _$clientQuotationInformationForm.find('input[name=studyAreaId]').val('');
        });

        $('#OpenLeadSourceLookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientleadSourceLookupTableModal.open({ id: client.leadSourceId, displayName: client.leadSourceName }, function (data) {
                _$clientQuotationInformationForm.find('input[name=leadSourceName]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=leadSourceId]').val(data.id);
            });
        });

        $('#ClearLeadSourceNameButton').click(function () {
            _$clientQuotationInformationForm.find('input[name=leadSourceName]').val('');
            _$clientQuotationInformationForm.find('input[name=leadSourceId]').val('');
        });

        $('#OpenCountry2LookupTableButton').click(function () {

            var client = _$clientQuotationInformationForm.serializeFormToObject();

            _ClientcountryLookupTableModal.open({ id: client.passportCountryId, displayName: client.countryName2 }, function (data) {
                _$clientQuotationInformationForm.find('input[name=countryName2]').val(data.displayName);
                _$clientQuotationInformationForm.find('input[name=passportCountryId]').val(data.id);
            });
        });

        $('#ClearCountryName2Button').click(function () {
            _$clientQuotationInformationForm.find('input[name=countryName2]').val('');
            _$clientQuotationInformationForm.find('input[name=passportCountryId]').val('');
        });



        function save(successCallback) {
            if (!_$clientQuotationInformationForm.valid()) {
                return;
            }
            //if ($('#Client_CountryId').prop('required') && $('#Client_CountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}
            //if ($('#Client_AssigneeId').prop('required') && $('#Client_AssigneeId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
            //    return;
            //}
            //if ($('#Client_ProfilePictureId').prop('required') && $('#Client_ProfilePictureId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('BinaryObject')));
            //    return;
            //}
            //if ($('#Client_HighestQualificationId').prop('required') && $('#Client_HighestQualificationId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('DegreeLevel')));
            //    return;
            //}
            //if ($('#Client_StudyAreaId').prop('required') && $('#Client_StudyAreaId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('SubjectArea')));
            //    return;
            //}
            //if ($('#Client_LeadSourceId').prop('required') && $('#Client_LeadSourceId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('LeadSource')));
            //    return;
            //}
            //if ($('#Client_PassportCountryId').prop('required') && $('#Client_PassportCountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}


            var datarows = [];
            closestTr = $(this).closest('tr') 
            var WorkflowId = closestTr.find(".workflowsId").val();
            var ProductId = closestTr.find(".productsId").val();
            var BranchId = closestTr.find(".branchsId").val();
            var PartnerId = closestTr.find(".partnersId").val();
            var Description = closestTr.find(".Description").val();
            var ServiceFee = closestTr.find(".fee").val();
            var NetFee = closestTr.find(".discount").val();
            var ExchangeRate = closestTr.find(".Rate").val();
            var TotalAmount = closestTr.find(".total").val();

            var dataRowItem = {
                WorkflowId: WorkflowId,
                ProductId: ProductId,
                BranchId: BranchId,
                PartnerId: PartnerId,
                Description: Description,
                ServiceFee: ServiceFee,
                NetFee: NetFee,
                ExchangeRate: ExchangeRate,
                TotalAmount: TotalAmount
            };

            datarows.push(dataRowItem);

            // Convert the array to a JSON string
            var Steps = JSON.stringify(datarows);

            var client = _$clientQuotationInformationForm.serializeFormToObject();
            client.Steps = Steps;


            abp.ui.setBusy();
            console.log(client);
            _clientsQuotationService.createOrEdit(
                client
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditClientModalSaved');

                if (typeof (successCallback) === 'function') {
                    successCallback();
                }
            }).always(function () {
                abp.ui.clearBusy();
            });
        };

        function clearForm() {
            _$clientQuotationInformationForm[0].reset();
        }

        $('#saveBtn').click(function () {
            save(function () {
                window.location = "/AppAreaName/Clients";
            });
        });

        $('#saveAndNewBtn').click(function () {
            save(function () {
                if (!$('input[name=id]').val()) {//if it is create page
                    clearForm();
                }
            });
        });

        $('.accordion-header').click(function () {
            if ($(this).hasClass('active')) {
                $(this).removeClass('active');
            } else {
                $(this).addClass('active');
            }
        });
    });
})();