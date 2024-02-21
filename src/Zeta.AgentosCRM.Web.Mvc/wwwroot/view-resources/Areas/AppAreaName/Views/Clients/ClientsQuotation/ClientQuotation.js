
(function () {
    $(function () {
        var _$clientQuotationHeadsTable = $('#ClientsQuotationtable');

        var _clientQuotationHeadsService = abp.services.app.clientQuotationHeads;
        var _clientQuotationDetailService = abp.services.app.clientQuotationDetails;
        //$('#branchId').select2({
        //    width: '450px',
        //    // Adjust the width as needed
        //});
        //$('#partnerId').select2({
        //    width: '450px',
        //    // Adjust the width as needed
        //});
        //$('#productId').select2({
        //    width: '450px',
        //    // Adjust the width as needed
        //});
        //$('#workflowId').select2({
        //    width: '450px',
        //    dropdownCss: { zIndex: 0 }
        //    // Adjust the width as needed
        //});


            // Add your other document-ready code here
            // ...
        //});
        $.ajax({
            url: abp.appPath + 'api/services/app/ClientQuotationDetails/GetAllPartnerForTableDropdown',
            method: 'GET',
            dataType: 'json',
            //data: {
            //    PartnerIdFilter: dynamicValue,
            //},
            success: function (data) {

                // Populate the dropdown with the fetched data
                populatePartnerDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function populatePartnerDropdown(data) {
             
            var dropdown = $('#partnerId');

            dropdown.empty();
            dropdown.append($('<option></option>').attr('value', '0').text('Select A Partner'));
            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        $.ajax({
            url: abp.appPath + 'api/services/app/ClientQuotationDetails/GetAllBranchForTableDropdown',
            method: 'GET',
            dataType: 'json',
            //data: {
            //    PartnerIdFilter: dynamicValue,
            //},
            success: function (data) {

                // Populate the dropdown with the fetched data
                populateBranchDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function populateBranchDropdown(data) {
             
            var dropdown = $('#branchId');

            dropdown.empty();
            dropdown.append($('<option></option>').attr('value', '0').text('Select A Branch'));
            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        $.ajax({
            url: abp.appPath + 'api/services/app/ClientQuotationDetails/GetAllProductForTableDropdown',
            method: 'GET',
            dataType: 'json',
            //data: {
            //    PartnerIdFilter: dynamicValue,
            //},
            success: function (data) {

                // Populate the dropdown with the fetched data
                populateProductDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function populateProductDropdown(data) {
             
            var dropdown = $('#productId');

            dropdown.empty();
            dropdown.append($('<option></option>').attr('value', '0').text('Select A Product'));
            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        $.ajax({
            url: abp.appPath + 'api/services/app/ClientQuotationDetails/GetAllWorkflowForTableDropdown',
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
             
            var dropdown = $('#workflowId');

            dropdown.empty();
            dropdown.append($('<option></option>').attr('value', '0').text('Select A Workflow'));
            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        function getFormData() {
            var formData = {
                workflowId: $('#workflowId').val(),
                partnerId: $('#partnerId').val(),
                productId: $('#productId').val(),
                branchId: $('#branchId').val(),
                workflowName: $('#workflowId option:selected').text(),
                productName: $('#productId option:selected').text(),
                brnachName: $('#branchId option:selected').text(),
                partnerName: $('#partnerId option:selected').text()
                // Add other fields as needed
            };
            return formData;
        }
        //$('#updatequotationBtn').click(function () {
        //     ;
        //    var updatedData = {};  // Variable to store the updated data as an object

        //    // Manually collect data from each form element
        //    $('form[name=QuotationDetailInformationsForm] :input').each(function () {
        //         
        //        var element = $(this);
        //        var name = element.attr('name');

        //        // Skip elements without a name attribute
        //        if (!name) {
        //            return;
        //        }

        //        console.log('Element Name:', name);

        //        // Check if the element is a select (dropdown)
        //        if (element.is('select')) {
        //            console.log('Element is a select:', element);

        //            var selectedValue = element.val();
        //            var selectedText = element.find('option[value="' + selectedValue + '"]').text();

        //            updatedData[name] = selectedValue;
        //            updatedData[name + 'Text'] = selectedText;
        //        } else {
        //            console.log('Element is not a select:', element);

        //            // For other input types, directly get the value
        //            updatedData[name] = element.val();
        //        }
        //    });

        //    // If the "Select A Workflow" option is selected, set the value to an empty string
        //    if (updatedData['workflowId'] === '0') {
        //        updatedData['workflowId'] = '';
        //        updatedData['workflowIdText'] = '';
        //    }

        //    // Optionally, you can log the data to the console for verification
        //    console.log('Updated Data:', updatedData);

        //    // You can perform other actions or manipulations with the updatedData object here
        //});

        $(document).ready(function () {
        $(document).on("click", "#updatequotationBtn", function () {
        //$('#updatequotationBtn').click(function () {
             
            // Get form data
            var formData = getFormData();
            var rowCount = $("#ClientsQuotationDetailtable tbody tr").length;
            //var rowCount = 0;//parseInt($('#rowCount').val())+0;..
            var srlno = $('#rowCount').val();
            console.log(closestTr)
            if (closestTr && closestTr.length > 0) {
                var currentRowNumber = parseInt(closestTr.attr('class').match(/\d+/)[0]);
                //var targetRowNumber = parseInt(tr.attr('class').match(/\d+/)[0]);
                if (currentRowNumber > 0) {
                    //var existingRow = $("#ClientsQuotationDetailtable").length;
                     

                    // Update the content of the existing row
                    closestTr.find('.workflowsName').text(formData.workflowName);
                    closestTr.find('.productsName').text(formData.productName);
                    closestTr.find('.partnerName').text(formData.partnerName);
                    closestTr.find('.branchName').text(formData.brnachName);

                    closestTr.find('.workflowsId').val(formData.workflowId);
                    closestTr.find('.productsId').val(formData.productId);
                    closestTr.find('.partnersId').val(formData.partnerId);
                    closestTr.find('.branchsId').val(formData.branchId);




                    // You may need to update other cells based on your table structure

                }
            }
            else {
               // $("#ClientsQuotationDetailtable").length()
              
                //var rowCount = $('#ClientsQuotationDetailtable tbody tr').length;
                var TrData = '<div class="Card" style="background-color: #f0f0f0;">';
                TrData += '<div class="Card-head">';
                TrData += '<span><input id="workflowsId"class="workflowsId" type="hidden" value="' + formData.workflowId + '"/></span>';
                TrData += '<span class="workflowsName">' + formData.workflowName + '</span>' + '<span class="Edit-icon" style="float: right; cursor: pointer;"><i class="fa fa-edit" style="font-size: 10px;"></i></span><br>';
                TrData += '<span><input id="productsId"class="productsId" type="hidden" value="' + formData.productId + '"/></span>';
                TrData += '<span class="productsName">' + formData.productName + '</span><br>';
                TrData += '<span><input id="partnersId" class="partnersId" type="hidden" value="' + formData.partnerId + '"/></span>';
                TrData += '<span class="partnerName">' + formData.partnerName + '</span><br>';
                TrData += '<span><input id="branchsId"  class="branchsId" type="hidden" value="' + formData.branchId + '"/></span>';
                TrData += '<span><input id="branchName"  class="branchName" type="hidden" value="' + formData.branchName + '"/></span>';
                TrData += '<span><input id="Id"  class="Id" type="hidden" value="0"/></span>';
                TrData += '<span><input id="rowCount" type="hidden" value="' + rowCount + '"/></span>';
               
                TrData += '</div></div>';


                var srCount = rowCount + 2;

                 
                var mainDiv = $('<div>').addClass('maincard maindivcard');

 
                mainDiv.append(TrData);

                // Return the created card
                var cardHtml = mainDiv.html();
                //resetFormFields();
                //$("workflowId").val("selectedIndex", 0);
                var adddatatotable =
                    "<tr class='trq_" + srCount + "'>" +
                    "<td>" + cardHtml + "</td>" +
                    "<td><textarea type='text' placeholder='Description' class='form-control border-0 input-sm Description'>" + '' + "</textarea></td>" +
                    "<td><input id='fee_" + srCount + "' type='text' placeholder='' value='" + '' + "' class='form-control border-0 input-sm fee' /></td>" +
                    "<td><input id='discount_" + srCount + "' type='text' placeholder='' value='" + '' + "' class='form-control border-0 input-sm discount' /></td>" +
                    "<td><input id='NetFee_" + srCount + "' type='text' placeholder='' value='" + '' + "' class='form-control border-0 input-sm NetFee'readonly /></td>" +
                    "<td><input id='Rate_" + srCount + "' type='text' placeholder='' value='" + '' + "' class='form-control border-0 input-sm Rate' /></td>" +
                    "<td><input id='total_" + srCount + "' type='text' placeholder='' value='" + '' + "' class='form-control border-0 input-sm total'readonly /></td>" +
                    "<td><span class='Delete-icon delete' style='cursor: pointer; margin-left: 5px;'><i class='fa fa-trash' style='font-size: 10px;'></i></span></td>" +
                    "</tr>";

                $("#ClientsQuotationDetailtable").append(adddatatotable);
            }
            closestTr.length = 0;
            closestTr = 0;
            $('#QuotationDetailModal').modal('hide');
        });
        });
        var closestTr = "";
        $(document).on("click", ".Edit-icon", function () {
             
            var modal = $(this)
            closestTr = $(this).closest('tr') 
            var trClass = closestTr.attr('class');
            var currentRowNumber = parseInt(trClass.match(/\d+/)[0]);
            console.log('Row number:', currentRowNumber);

            var workflow = closestTr.find(".workflowsId").val();
            var product = closestTr.find(".productsId").val();
            var branch = closestTr.find(".branchsId").val();
            var partner = closestTr.find(".partnersId").val();
            modal.find('.modal-body #workflowId').val(workflow)
            modal.find('.modal-body #productId').val(product)
            modal.find('.modal-body #branchId').val(branch)
            modal.find('.modal-body #partnerId').val(partner)
            $('#workflowId').val(workflow);
            $('#productId').val(product);
            $('#branchId').val(branch);
            $('#partnerId').val(partner);
            $('#QuotationDetailModal').modal('show');
        });
        $(document).on('click', '.delete', function () {
             
            closestTr = $(this).closest('tr') 
            var quotationdetailsid = closestTr.find('.Id').val();

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientQuotationDetailService
                        .delete({
                            id: quotationdetailsid,
                        })
                        .done(function () {
                             
                            closestTr.remove();

                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        });



        // Function to handle the button click event for closing
        //$('#closedquotationBtn').click(function () {
        //     
        //    // Get form data
        //    var formData = getFormData();

        //    // Do something with the form data
        //    console.log(formData);
          
        //    // If you want to store it in a variable, you can do something like this..:
        //    // var myVariable = formData;
        //});
        function resetFormFields() {
    // Reset or clear the values of your form fields
    // For example, assuming you have input fields with IDs 'workflowName', 'workflowId', 'productId', 'branchId', 'partnerId'
    $('#workflowName').val('');
    $('#workflowId').val('');
    $('#productId').val('');
    $('#branchId').val('');
    $('#partnerId').val('');

    // You may need to reset other form fields based on your actual form structure
    // ...

    // If you have textarea and input fields in the dynamically added row, you can reset them like this:
    $(".Description").val('');
    $(".fee").val('');
    $(".discount").val('');
    $(".NetFee").val('');
    $(".Rate").val('');
    $(".total").val('');
}
            $('#closedquotationBtn').click(function () {
                 
                //$('#workflowId').val(0),
                //    $('#partnerId').val(0),
                //    $('#productId').val(0),
                //    $('#branchId').val(0)
            $('#QuotationDetailModal').modal('hide');

            // _createOrEditQuotationDetailModal.open();


        });



  
        var hiddenfield = $('input[name="Clientid"]').val();
        var dataTable = _$clientQuotationHeadsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientQuotationHeadsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#NameFilterId').val(),
                        TimeZoneFilter: $('#TimeZoneFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        clientIdFilter: hiddenfield,
                    };
                },
            },
            columnDefs: [
                {
                    className: ' responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0,
                },
                {
                    targets: 1,
                    data: 'clientFirstName',
                    name: 'ClientFirstNameFk',
                },
                {
                    targets: 2,
                    data: 'clientQuotationHead.productCount',
                    name: 'productCount',
                },
                {
                    targets: 3,
                    data: 'clientQuotationHead.totalAmount',
                    name: 'totalAmount',
                    render: function (data, type, row) {
                        // Format the totalAmount with commas
                        if (type === 'display' || type === 'filter') {
                            return parseFloat(data).toLocaleString();
                        }
                        return data;
                    }
                },
                {
                    targets: 4,
                    data: 'clientQuotationHead.dueDate',
                    name: 'dueDate',
                    render: function (dueDate) {
                        if (dueDate) {
                            return moment(dueDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 5,
                    data: 'clientQuotationHead.creationTime',
                    name: 'creationTime',
                    render: function (creationTime) {
                        if (creationTime) {
                            return moment(creationTime).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 6,
                    data: 'userName',
                    name: 'UserNameFk',
                },
                
                {
                    width: 30,
                    targets: 7,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                        //console.log(data);
                        var rowId = data.clientQuotationHead.id;
                        //console.log(rowId);
                        var rowData = data.clientQuotationHead;
                        var RowDatajsonString = JSON.stringify(rowData);
                        //console.log(RowDatajsonString);
                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis60"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            /* '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +*/
                            '<a href="#" style="color: black;" data-action60="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            '<li ><a href="#" style="color: black;" data-action60="Preview" data-id="' + rowId + '">Preview</a></li>' +
                            "<a href='#' style='color: black;' data-action60='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }


                },

            ],
        });
       
        
        var $selectedDate = {
            startDate: null,
            endDate: null,
        };


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
                getquotations();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getquotations();
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
                getquotations();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getquotations();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditQuotationDetailModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Client/ClientQuotation/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditClientQuotationModal',
        });
        var _createOrEditQuotationDetailModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditQuotationDetailModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/ClientsQuotation/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditClientQuotationModal',
        });
        var _createOrEditModalEmail = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/ClientEmailCompose',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Client/ApplicationClient/_CreateOrEditModal.js',
            modalClass: 'ClientEmailCompose',
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

 

        function getquotations() {
            dataTable.ajax.reload();
        }

        function deleteQuotationDetails(clientQuotationHead) {
             
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientQuotationHeadsService
                        .delete({
                            id: clientQuotationHead.id,
                        })
                        .done(function () {
                            getquotations(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        }

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

        $('#CreateNewQuotation').click(function () {
            // _createOrEditModal.open();
        });

        $('#CreateNewQuotation').click(function () {
             
            var hiddenfield = $("#ID").val();

    
            var baseUrl = "/AppAreaName/Clients/CreateOrEditClientQuotationModal/";
            var url = baseUrl + "?clientId=" + hiddenfield;

            // Redirect to the constructed URL
            window.location.href = url;
            // _createOrEditModal.open();


        });

        $('#addquotationBtn').click(function () {
             
            var modal = $(this)
          //  closestTr = $(this).closest('tr')
            var workflow = 0;
            var product = 0;
            var branch = 0;
            var partner = 0;
            modal.find('.modal-body #workflowId').val(workflow)
            modal.find('.modal-body #productId').val(product)
            modal.find('.modal-body #branchId').val(branch)
            modal.find('.modal-body #partnerId').val(partner)
            $('#workflowId').val(workflow);
            $('#productId').val(product);
            $('#branchId').val(branch);
            $('#partnerId').val(partner);
            $('#QuotationDetailModal').modal('show');
            
           // _createOrEditQuotationDetailModal.open();


        });
        //$('#closedquotationBtn').click(function () {
        //     
        //    $('#QuotationDetailModal').modal('hide');

        //    // _createOrEditQuotationDetailModal.open();


        //});
        $('#ExportToExcelButton').click(function () {
            _subjectsService
                .getPartnerTypesToExcel({
                    filter: $('#SubjectsTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                    subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditClientQuotationModalSaved', function () {
            getquotations();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getquotations();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getquotations();
            }
        });

        $('.reload-on-change').change(function (e) {
            getquotations();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getquotations();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getquotations();
        });
        $("#ClientsQuotationDetailtable").on('change', '.fee', function () {
             ;
            var $CalAmount = $(this).closest('tr');
            var fee = parseFloat($CalAmount.find(".fee").val());
            var discount = +($CalAmount.find(".discount").val());
            if (isNaN(fee)) {
                fee = 0;
            }
            if (isNaN(discount)) {
                discount = 0
            }
            if (discount > fee) {
                discount = fee
                $CalAmount.find(".discount").val(parseFloat(fee).toFixed(2));
            }
            var NetFee = parseFloat(fee - discount).toFixed(2);
            $CalAmount.find(".NetFee").val(parseFloat(NetFee).toFixed(2));
            var $CalAmount = $(this).closest('tr');
            var Rate = parseFloat($CalAmount.find(".Rate").val());
            if (isNaN(Rate)) {
                Rate = 0;
            }
            var Amount = (NetFee * Rate).toFixed(2);
            
            $CalAmount.find(".total").val(parseFloat(Amount).toFixed(2));
            CalculateAmt();
            //$CalAmount.find(".total").text(netamount);
        });
        $("#ClientsQuotationDetailtable").on('change', '.Rate', function () {
             ;
            var $CalAmount = $(this).closest('tr');
            var fee = parseFloat($CalAmount.find(".fee").val());
            var discount = +($CalAmount.find(".discount").val());
            if (isNaN(fee)) {
                fee = 0;
            }
            if (isNaN(discount)) {
                discount = 0
            }
            if (discount > fee) {
                discount = fee
                $CalAmount.find(".discount").val(parseFloat(fee).toFixed(2));
            }
            var NetFee = parseFloat(fee - discount).toFixed(2);
            $CalAmount.find(".NetFee").val(parseFloat(NetFee).toFixed(2));
            var $CalAmount = $(this).closest('tr');
            var Rate = parseFloat($CalAmount.find(".Rate").val());
            if (isNaN(Rate)) {
                Rate = 0;
            }
            var Amount = (NetFee * Rate).toFixed(2);

            $CalAmount.find(".total").val(parseFloat(Amount).toFixed(2));
            //$CalAmount.find(".total").text(netamount);
            CalculateAmt();
        });
        $("#ClientsQuotationDetailtable").on('change', '.discount', function () {
             ;
            var $CalAmount = $(this).closest('tr');
            var fee = parseFloat($CalAmount.find(".fee").val());
            var discount = +($CalAmount.find(".discount").val());
            if (isNaN(fee)) {
                fee = 0;
            }
            if (isNaN(discount)) {
                discount = 0
            }
            if (discount > fee) {
                discount = fee
                $CalAmount.find(".discount").val(parseFloat(fee).toFixed(2));
            }
            var NetFee = parseFloat(fee - discount).toFixed(2);
            $CalAmount.find(".NetFee").val(parseFloat(NetFee).toFixed(2));
            var $CalAmount = $(this).closest('tr');
            var Rate = parseFloat($CalAmount.find(".Rate").val());
            if (isNaN(Rate)) {
                Rate = 0;
            }
            var Amount = (NetFee * Rate).toFixed(2);

            $CalAmount.find(".total").val(parseFloat(Amount).toFixed(2));
            CalculateAmt();
            //$CalAmount.find(".total").text(netamount);
        });
        $('#ClientsQuotationDetailtable').on('change', '.total', function () {
            CalculateAmt();
        });
        function CalculateAmt() {
             
            var total = 0;

            var totalAmount = 0;
            var taxRowCounter = $('.total').length;
            
            $("#ProductCount").val(taxRowCounter);
            $('.total').each(function () {
                 
                // Assuming there is an input field with class 'total' in each row
                var amount = parseFloat($(this).val()) || 0;
                totalAmount += amount;
            });
            
         

            // Set the calculated total to the #Total input
            //$("#TotalAmount").val(totalAmount.toFixed(2).toLocaleString());
            //console.log('Total calculated:', total.toFixed(2));
            var localizedTotalAmount = parseFloat(totalAmount).toLocaleString(undefined, { minimumFractionDigits: 2 });
            $("#TotalAmount").val(localizedTotalAmount);
        }

        //function CalculateAmt() {
        //     ;

        //    var Workflowsetuptable = $('#ClientsQuotationDetailtable');
        //    var data = Workflowsetuptable.rows().data().toArray();
        //    var total = 0;

        //    for (var i = 0; i < data.length; i++) {
        //         ;

        //        // Assuming the field you want to sum is in the 'total' column
        //        var totalValue = parseFloat(data[i].total) || 0;

        //        // Add the current row's total to the overall total
        //        total += totalValue;
        //    }

        //    // Set the calculated total to the #Total input
        //    $("#Total").val(total.toFixed(2));
        //}
        //$(document).on('click', '.ellipsis60', function (e) {
        //    e.preventDefault();
        //     
        //    var options = $(this).closest('.context-menu').find('.Appointmentoptions');
        //    var allOptions = $('.Appointmentoptions');  // Select all options

        //    // Close all other open options
        //    allOptions.not(options).hide();

        //    // Toggle the visibility of the options
        //    options.toggle();
        //});

        //// Close the context menu when clicking outside of it
        //$(document).on('click', function (event) {
        //    if (!$(event.target).closest('.context-menu').length) {
        //        $('.Appointmentoptions').hide();
        //    }
        //});
        //$(document).on('click', 'a[data-action60]', function (e) {
        //    e.preventDefault();
        //     
        //    var rowId = $(this).data('id');
        //    var action = $(this).data('action60');
        //     
        //    // Handle the selected action based on the rowId..
        //    if (action === 'edit') {

        //        var hiddenfield = $("#ID").val();


        //        var baseUrl = "/AppAreaName/Clients/CreateOrEditClientQuotationModal/";
        //        var url = baseUrl + "?clientId=" + hiddenfield + "&id=" + rowId;

        //        // Redirect to the constructed URL
        //        window.location.href = url;
               
        //    }
        //    if (action === 'Preview') {

        //        var hiddenfield = $("#ID").val();


        //        var baseUrl = "/AppAreaName/Clients/ClientsQuotationPreview/";
        //        var url = baseUrl + "?clientId=" + hiddenfield + "&id=" + rowId;

        //        // Redirect to the constructed URL....
        //        window.location.href = url;
               
        //    }
        //    else if (action === 'delete') {
        //        // console.log(rowId);
        //        deleteQuotationDetails(rowId);
        //    }
        //});
        $(document).on('click', '.ellipsis60', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });

        // Handle menu item clicks
        $(document).on('click', 'a[data-action60]', function (e) {
            e.preventDefault();
             
            var rowId = $(this).data('id');
            var action = $(this).data('action60');
             
            // Handle the selected action based on the rowId..
            if (action === 'edit') {

                var hiddenfield = $("#ID").val();


                var baseUrl = "/AppAreaName/Clients/CreateOrEditClientQuotationModal/";
                var url = baseUrl + "?clientId=" + hiddenfield + "&id=" + rowId;

                // Redirect to the constructed URL
                window.location.href = url;

            }
            if (action === 'Preview') {

                var hiddenfield = $("#ID").val();


                var baseUrl = "/AppAreaName/Clients/ClientsQuotationPreview/";
                var url = baseUrl + "?clientId=" + hiddenfield + "&id=" + rowId;

                // Redirect to the constructed URL....
                window.location.href = url;

            }
            else if (action === 'delete') {
                // console.log(rowId);
                deleteQuotationDetails(rowId);
            }
        });
    });
})();
