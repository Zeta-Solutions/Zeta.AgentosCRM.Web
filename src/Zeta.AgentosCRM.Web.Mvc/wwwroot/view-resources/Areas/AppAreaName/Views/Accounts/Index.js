(function () {
    $(function () {
        var _$DocumentTypeTable = $('#DocumentTypeTable');
        var _documentTypesService = abp.services.app.manualPaymentDetails;
        var _taxSettingsService = abp.services.app.taxSettings;
        var _businessRegNummbersService = abp.services.app.businessRegNummbers;
        var _invoiceAddressesService = abp.services.app.invoiceAddresses;
        debugger
        var $selectedDate = {
            startDate: null,
            endDate: null,
        };
        $(".btnbusinessRegNummbers,.btnbusinessAddress,.addMaunalTax ,.addTax,.TaxSettingSave").prop("disabled", true);
         

        $.ajax({
            url: abp.appPath + 'api/services/app/Agents/GetAllOrganizationUnitForTableDropdown',
            method: 'GET',
            dataType: 'json',

            success: function (data) {
                debugger
                var data = data.result;
                if (data == null) {
                //alert("Record Not Found.");
            }
            var optionhtml = '<option value="0"> select Office</option>';
                $("#WorkFlowOfficeId").append(optionhtml);

            $.each(data, function (i) {
                // You will need to alter the below to get the right values from your json object.  Guessing that d.id / d.modelName are columns in your carModels data
                optionhtml = '<option value="' +
                    data[i].id + '">' + data[i].displayName + '</option>';
                $("#WorkFlowOfficeId").append(optionhtml);
            });
                //populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
 
        
        $.ajax({
            url: abp.appPath + 'api/services/app/Countries/GetAll',
            method: 'GET',
            dataType: 'json',

            success: function (data) {
                debugger

                var ManualPayment = data.result.items;

                var optionhtml = '<option value="0">---Select Country---</option>';
                $.each(ManualPayment, function (index, item) {
                    debugger
                    optionhtml = '<option value="' +
                        item.country.id + '">' + item.country.name + '</option>';
                    $("#B_I_Country").append(optionhtml);
                     
                });
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        }); 
         
        $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('.startDate')
            .daterangepicker({
                autoUpdateInput: false,
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            })
            .on('apply.daterangepicker', (ev, picker) => {
                $selectedDate.startDate = picker.startDate;
                getDocumentType();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getDocumentType();
            });

        $('.endDate')
            .daterangepicker({
                autoUpdateInput: false,
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            })
            .on('apply.daterangepicker', (ev, picker) => {
                $selectedDate.endDate = picker.startDate;
                getDocumentType();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getDocumentType();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
        //    edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
        //    delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Accounts/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Accounts/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditManualPaymentModal',
        });

        var _viewFeeTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Accounts/ViewDocumentTypeModal',
            modalClass: 'ViewDocumentTypeModal',
        });

        var getDateFilter = function (element) {
            if ($selectedDate.startDate == null) {
                return null;
            }
            return $selectedDate.startDate.format('YYYY-MM-DDT00:00:00Z');
        };

        var getMaxDateFilter = function (element) {
            if ($selectedDate.endDate == null) {
                return null;
            }
            return $selectedDate.endDate.format('YYYY-MM-DDT23:59:59Z');
        };
        var dataTable = _$DocumentTypeTable.DataTable({

            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _documentTypesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#MasterCategoriesTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                    };
                },
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0,
                },
                {
                    targets: 1,
                    data: 'documentType.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'documentType.name',
                    name: 'name',
                },
                {
                    targets: 3,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.documentType.id;
                        var rowData = data.documentType;
                        var RowDatajsonString = JSON.stringify(rowData);

                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
                            '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }
                },
            ],
        });
        function getDocumentType() {
            dataTable.ajax.reload();
        }



        $(document).on('click', '.ellipsis', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');
            allOptions.not(options).hide();

            options.toggle();
        });

        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });

        // Handle menu item clicks
        $(document).on('click', 'a[data-action]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'view') {
                debugger
                _viewFeeTypeModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteFeeType(rowId);
            }
        });


        $(".btnbusinessAddress").click(function () {
            var ID = $("#businessInvoicenumber").val();
            var Street = $("#B_I_Address").val();
            var City = $("#B_I_City").val();
            var State = $("#B_I_State").val();
            var ZipCode = $("#B_I_ZipCode").val();
            var CountryId = $("#B_I_Country").val(); 
            var OrganizationUnitId = $("#WorkFlowOfficeId").val();
            
            debugger
            var requestData = {
                id: ID,
                Street: Street,
                City: City,
                State: State,
                ZipCode: ZipCode,
                CountryId: CountryId,
                OrganizationUnitId: OrganizationUnitId
            };
            var jsonData = JSON.stringify(requestData);
            jsonData = JSON.parse(jsonData);
            debugger
            _invoiceAddressesService.createOrEdit(jsonData)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));

                    location.reload();
                    abp.event.trigger('app.createOrEditWorkflowModalSaved');
                })

             
             
        });
        $(".btnbusinessRegNummbers").click(function () {
            var ID = $("#businessregistrationID").val();
            var businessregistrationnumber = $("#businessregistrationnumber").val();
            var OrganizationUnitId = $("#WorkFlowOfficeId").val();
            debugger
            var requestData = {
                id: ID,
                RegistrationNo: businessregistrationnumber,
                OrganizationUnitId: OrganizationUnitId
            };
             
            var jsonData = JSON.stringify(requestData);
            jsonData = JSON.parse(jsonData);
            debugger
            _businessRegNummbersService.createOrEdit(jsonData)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));

                    location.reload();
                    abp.event.trigger('app.createOrEditWorkflowModalSaved');
                })
             
        });
        function deleteFeeType(feeType) {
            debugger
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _documentTypesService
                        .delete({
                            id: feeType,
                        })
                        .done(function () {
                            debugger
                            getDocumentType(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        }
        $(document).on("click", ".delete-icon", function () {

            var ManualPaymentDetailID = $(this).closest('.ManualPaymentData');
            var EmailID = ManualPaymentDetailID.find("#ManualPaymentDetailID").val();
            debugger
            deleteFeeType(EmailID);
        });

        $(document).on("click", ".Edit-icon", function () {
            debugger
            var ManualPaymentDetailID = $(this).closest('.ManualPaymentData');
            var EmailID = ManualPaymentDetailID.find("#ManualPaymentDetailID").val();

            console.log(EmailID);
            debugger
            _createOrEditModal.open({ id: EmailID });
            debugger
        });
        $('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });
        $(".TaxSettingSave").click(function () {
            var dataRows = [];
            var OrganizationUnitId =$("#WorkFlowOfficeId").val();
            //var  Id = $("#TaxID").val();
            $(".AddTaxDiv .TaxRow").each(function () {
                debugger
                var Id = $(this).find('.TaxSettingID').val();
                var TaxCode = $(this).find('.TaxCode').val();
                var TaxRate = $(this).find('.TaxRate').val();
                var IsDefault = $(this).find('.TaxSettingValue').val();
                 
                dataRows.push({
                    id: Id,
                    taxCode: TaxCode,
                    taxRate: TaxRate,
                    isDefault: IsDefault,
                    organizationUnitId: OrganizationUnitId,
                });

            }); 
            $.each(dataRows, function (index, row) {
                var jsonData = JSON.stringify(row); 
                jsonData = JSON.parse(jsonData);
                debugger
                _taxSettingsService.createOrEdit(jsonData)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                     
                    location.reload();
                    abp.event.trigger('app.createOrEditWorkflowModalSaved');
                })
            }); 
        });

        $('.addMaunalTax').click(function () {
            debugger
            //var OrganizationUnitId = $("#WorkFlowOfficeId").val();
            _createOrEditModal.open();
        });

        $(document).on('click', '.addTax', function () {
            debugger
            var newTimelineItem = `
    <div class="row TaxRow">
        <div class="col-lg-3">
            <br />
            <input class="form-control TaxSettingID" hidden id="TaxID" value="0" type="text" name="name"   />

            <input class="form-control TaxCode"  type="text"/>
        </div>
        <div class="col-lg-3">
            <br />
            <input class="form-control TaxRate" type="text"/>
        </div>
        <div class="col-lg-3">
            <br />
            <button type="button" value="false" class="btn btnclose btn-sm btn-outline text-white save-button rounded-0 TaxSettingValue"><span>Set as Default</span></button>
        </div>
        <div class="col-lg-3">
            <br />
            <span class="TaxRowDelete"><i class="fa fa-trash" style="font-size: 20px;"></i></span>
        </div>
    </div>
`;

            $(".AddTaxDiv").append(newTimelineItem);

        });
        $(document).on('click', '.TaxRowDelete', function () {
            debugger; 
            var row = $(this).closest(".TaxRow");
            var TaxRowid=row.find('.TaxSettingID').val();
            debugger; 
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _taxSettingsService
                        .delete({
                            id: TaxRowid,
                        })
                        .done(function () {
                            debugger 

                            row.remove();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });

        });
        $(document).on('click', '.TaxSettingValue', function () {
            debugger;

            var row = $(this).closest(".TaxRow");
            var taxSettingValue = row.find('.TaxSettingValue');

            if (taxSettingValue.val() === "false") {
             
                taxSettingValue.val('true');
                $(this).html('<span>Default Tax</span>'); 
            } else {
                taxSettingValue.val('false');
                $(this).html('<span>Set as Default</span>');
            }
        });


        $(document).on("change", "#WorkFlowOfficeId", function () {
            filterRcordwithOfficeId();
 
        });
        function filterRcordwithOfficeId() {
            var organizationUnitIdFilter = $("#WorkFlowOfficeId").val();
            if (organizationUnitIdFilter > 0) {
                debugger
                $(".btnbusinessRegNummbers,.btnbusinessAddress,.addMaunalTax ,.addTax,.TaxSettingSave").prop("disabled", false);

                //_businessRegNummbersService.getAll(organizationUnitIdFilter)
                //    .done(function (data) {
                //        debugger
                //        $("#businessregistrationID").val(data.items[0].businessRegNummber.id);
                //        $("#businessregistrationnumber").val(data.items[0].businessRegNummber.registrationNo);  

                //    })
                $.ajax({
                    url: abp.appPath + 'api/services/app/BusinessRegNummbers/GetAll',
                    data: {
                        OrganizationUnitIdFilter: organizationUnitIdFilter,
                    },
                    method: 'GET',
                    dataType: 'json',

                    success: function (data) {
                        var ManualPayment = data.result.items;

                        $("#businessregistrationID").val(ManualPayment[0].businessRegNummber.id);
                        $("#businessregistrationnumber").val(ManualPayment[0].businessRegNummber.registrationNo);
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
                $.ajax({
                    url: abp.appPath + 'api/services/app/InvoiceAddresses/GetAll',
                    data: {
                        OrganizationUnitIdFilter: organizationUnitIdFilter,
                    },
                    method: 'GET',
                    dataType: 'json',

                    success: function (data) {

                        var ManualPayment = data.result.items;
                        $("#businessInvoicenumber").val(ManualPayment[0].invoiceAddress.id);
                        $("#B_I_Address").val(ManualPayment[0].invoiceAddress.street);
                        $("#B_I_City").val(ManualPayment[0].invoiceAddress.city);
                        $("#B_I_State").val(ManualPayment[0].invoiceAddress.state);
                        $("#B_I_ZipCode").val(ManualPayment[0].invoiceAddress.zipCode);
                        $("#B_I_Country").val(ManualPayment[0].invoiceAddress.countryId);

                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
                $.ajax({
                    url: abp.appPath + 'api/services/app/ManualPaymentDetails/GetAll',
                    data: {
                        OrganizationUnitIdFilter: organizationUnitIdFilter,
                    }, method: 'GET',
                    dataType: 'json',

                    success: function (data) {


                        $(".ManualDetail").empty();
                        var ManualPayment = data.result.items;
                        $.each(ManualPayment, function (index, item) {
                            debugger
                            var ManualPaymentDetail = '<div class="row ManualPaymentData">' +
                                '<div class="col-lg-4" hidden><input type="hidden" id="ManualPaymentDetailID" value="' + item.manualPaymentDetail.id + '"/></div>' +
                                '<div class="col-lg-4">' + item.manualPaymentDetail.name + '</div>' +
                                '<div class="col-lg-4">' + item.manualPaymentDetail.paymentDetail + '</div>' +
                                '<div class="col-lg-4"><span class="Edit-icon" style = "cursor: pointer;" > <i class="fa fa-edit" style="font-size: 20px;"></i></span >' +
                                '<span class="delete-icon" style="cursor: pointer; margin-left: 10px;"><i class="fa fa-trash" style="font-size: 20px;"></i></span></div>' +
                                '</div>'

                            $(".ManualDetail").append(ManualPaymentDetail);
                        });
                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });

                $.ajax({
                    url: abp.appPath + 'api/services/app/TaxSettings/GetAll',
                    data: {
                        OrganizationUnitIdFilter: organizationUnitIdFilter,
                    },
                    method: 'GET',
                    dataType: 'json',

                    success: function (data) {

                        $(".AddTaxDiv").empty();
                        var ManualPayment = data.result.items;
                        $.each(ManualPayment, function (index, item) {

                            var newTimelineItem = `
    <div class="row TaxRow">
        <div class="col-lg-3">
            <br />
            <input class="form-control TaxSettingID" hidden id="TaxID" value="`+ item.taxSetting.id + `" type="text" name="name"   />
            <input class="form-control TaxCode" value="`+ item.taxSetting.taxCode + `" type="text"/>
        </div>
        <div class="col-lg-3">
            <br />
            <input class="form-control TaxRate" value="`+ item.taxSetting.taxRate + `" type="text"/>
        </div>
        <div class="col-lg-3">
            <br />`

                            if (item.taxSetting.isDefault == true) {
                                newTimelineItem += `<button type="button" value="` + item.taxSetting.isDefault + `" class="btn btnclose btn-sm btn-outline text-white save-button rounded-0 TaxSettingValue"><span>Default Tax</span></button>`
                            } else {
                                newTimelineItem += `<button type="button" value="` + item.taxSetting.isDefault + `" class="btn btnclose btn-sm btn-outline text-white save-button rounded-0 TaxSettingValue"><span>Set as Default</span></button>`
                            }
                            newTimelineItem += `
            
        </div>
        <div class="col-lg-3">
            <br />
            <span class="TaxRowDelete"><i class="fa fa-trash" style="font-size: 20px;"></i></span>
        </div>
    </div>
`;

                            $(".AddTaxDiv").append(newTimelineItem);
                        })

                    },
                    error: function (error) {
                        console.error('Error fetching data:', error);
                    }
                });
            } else {
                location.reload();
            }
        }
         
        abp.event.on('app.createOrEditFeeTypeModalSaved', function () {
            debugger
            getDocumentType();
        });

        $('#GetMasterCategoriesButton').click(function (e) {
            e.preventDefault();
            getDocumentType();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getDocumentType();
            }
        });

        $('.reload-on-change').change(function (e) {
            getDocumentType();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getDocumentType();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getDocumentType();
        });
    });
})();
