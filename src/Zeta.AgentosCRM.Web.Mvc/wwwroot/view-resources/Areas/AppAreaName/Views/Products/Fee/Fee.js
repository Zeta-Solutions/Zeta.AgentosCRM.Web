(function () {
    $(function () {

        var hiddenfield = $("#ProductId").val();
        var dynamicValue = hiddenfield;

       
        getproductFeesreload(dynamicValue);
        var globalData; // Declare the data variable in a broader scope
        //function createProductFeecard(item) {
          
        //    var productFeeDetail = item.productFeeDetail;
        //    // Create a single row for all cards
        //    var mainDiv = $('<div>').addClass('maincard maindivcard')
        //        .css({
        //            'margin-left': '0.2px',
        //            'margin-bottom': '20px' // Add margin between cards
        //        });

        //    // Create a column for each card
        //    //var colDiv = $('<div>').addClass('col-md-12');
        //    var cardDiv = $('<div>').addClass('card').css({
        //        'padding': '5px '
        //    });
        //    var cardBodyDiv = $('<div>').addClass('card-body').css({
        //        'padding': '5px '
        //    });
        //    /*var titleColDiv = $('<div>').addClass('col-md-12');*/ // Adjust the column size as needed

        //    var cardTitle = $('<h5>').addClass('card-title');

        //    cardTitle.html(item.productFeeName +
        //        '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
        //        '<div class="ellipsis81"><a href="#" data-id="' + productFeeDetail.productFeeId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
        //        '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
        //        '<ul style="list-style: none; padding: 0;color:black">' +
        //        '<a href="#" style="color: black;" data-action81="edit" data-id="' + productFeeDetail.productFeeId + '"><li>Edit</li></a>' +
        //        '</ul>' +
        //        '</div>' +
        //        '</div><hr/>');

        //    // Create a column for the card information
        //    var infoColDiv = $('<div>').addClass('row'); // Adjust the column size as needed
        //    var infoParagraph2 = $('<p>').addClass('card-text col-md-2');
        //    var infoParagraph3 = $('<p>').addClass('card-text col-md-5');
        //    var infoParagraph4 = $('<p>').addClass('card-text col-md-2');
        //    var infoParagraph5 = $('<p>').addClass('card-text col-md-2');
        //    infoParagraph2.html(' <span class= "text-muted" ><strong>Valid For:</strong></span><br/>' + '&nbsp;&nbsp;' + item.countryName);
        //    infoParagraph3.html(' <span class= "text-muted" ><strong>Fee Breakdown:</strong></span><br/>' + '<h5/>' + item.feeTypeName + '&nbsp;&nbsp;' + '<span class="text-muted">' + productFeeDetail.installments + '</span>' + '&nbsp;&nbsp;' + '<span class="text-muted">Installments @<span/>' + '&nbsp;&nbsp;' + productFeeDetail.totalFee + '&nbsp;&nbsp;' + '<span class="replacename label badge badge-primary">' + productFeeDetail.totalFee + '</span>');
        //    infoParagraph5.html(' <span class= "text-muted" ><strong>Installment Type:</strong></span><br/>' + '&nbsp;&nbsp;' + item.installmentTypeName);
        //    cardBodyDiv.append(cardTitle);
        //    // infoParagraph.html(branch.name + '<br>' + item.countryName + '<br><hr>' + branch.email);
        //    infoColDiv.append( infoParagraph2, infoParagraph3, infoParagraph5);
        //    cardBodyDiv.append(infoColDiv);

        //    cardDiv.append(cardBodyDiv);
        //    // Append the card container to the mainDiv
        //    mainDiv.append(cardDiv);

        //    return mainDiv; // Return the created card
        //}



        function getproductFeesreload(dynamicValue) {
       
            if (dynamicValue > 0) {
                // Set to store processed WorkflowStepIDs
                var processedWorkflowStepIDs = new Set();

                $.ajax({
                    url: abp.appPath + 'api/services/app/ProductFees/GetAll',
                    data: {
                        ProductIdFilter: dynamicValue,
                    },
                    method: 'GET',
                    dataType: 'json',
                })
                    .done(function (data) {
                        var totalRecord = data.result.items;
                       
                        $.each(totalRecord, function (index, item) {
                             
                            var WorkflowStepID = item.productFee.id;
                            var WorkflowStepName = item.productFee.name;
                            var Countryname = item.countryName;
                            var InstallmentName = item.installmentTypeName;
                            var TotaAmont = 0;
                            // Check if WorkflowStepID is already processed
                            if (!processedWorkflowStepIDs.has(WorkflowStepID)) {
                                processedWorkflowStepIDs.add(WorkflowStepID);
                                 
                                if (WorkflowStepID > 0) {
                                    WorkflowStepId = WorkflowStepID;

                                    $.ajax({
                                        url: abp.appPath + 'api/services/app/ProductFeeDetails/GetAll',
                                        data: {
                                            ProductHeadIdFilter: WorkflowStepId,
                                        },
                                        method: 'GET',
                                        dataType: 'json',
                                    })
                                        .done(function (data) {
                                            var CheckListRecord = data.result.items;
                                             
                                            var ListOfCheckList = "";
                                            var DocumentWorkflowStepID = 0;

                                            var NewCheckListRecord = `
    <div class="card mb-3 shadow">
        <div class="row card body">
            <div class="col-12 timeline-item">
            <span class="AddNewCheckList pull-right"><i class="fa fa-edit"></i></span>
                <span class="WorkFlowStepId" hidden style="">${WorkflowStepID}</span>
                <span style="font-size:30px; color:#9bb0db "><b>${WorkflowStepName}</b></span>
                <div class="row">
                <div class="col-sm-3">
                 
              
                <span class="text-muted"><strong>Valid For:</strong><br></span>
                <span><b>${Countryname}</b></span><br>
                
                <span class="text-muted"><strong>Installment Type:</strong></span> <br>
                <span><b>${InstallmentName}</b></span></div> <div class="col-sm-7">
`;

                                            $.each(CheckListRecord, function (index, item) {
                                                 
                                                DocumentWorkflowStepID = item.productFeeDetail.productFeeId;

                                                //var CheckPartner = item.workflowStepDocumentCheckList.isForAllPartners;
                                                 
                                                if (WorkflowStepID == DocumentWorkflowStepID) {
                                                    NewCheckListRecord += ` 
            <span class="DocumentCheckList" style=""><input type="text" hidden id="WorkFlowRowId" value="${item.productFeeDetail.id}"/></span>
             <span class="text-muted"><strong>Fee Breakdown:</strong></span><br>
            <span class=""><strong>${item.feeTypeName}</strong> </span>
            <span class="text-muted"> ${item.productFeeDetail.installments}&nbsp Installments </span>
            <span class="text-muted">@ &nbsp AUD ${item.productFeeDetail.totalFee} </span>&nbsp &nbsp 
            <span class="replacename label badge badge-primary">Fee: ${item.productFeeDetail.totalFee * item.productFeeDetail.installments}</span><br>
        `;
                                                    TotaAmont += item.productFeeDetail.totalFee * item.productFeeDetail.installments
                                                }
                                            });

                                            NewCheckListRecord += ` 
                              </div><div class="col-sm-2 text-muted">  <span><strong>Total Fees</strong></span><br>
                                <span class="" style="font-size:30px ;color:#9bb0db">${TotaAmont}</span>
                                
                            </div></div></div>`;
                                             
                                            $("#FeescardContainer").append(NewCheckListRecord);
                                        });
                                }
                            }
                        });
                    });
            }

        }

        $(document).on('click', '.AddNewCheckList', function () {
             
            var $timelineItem = $(this).closest('.timeline-item');

            var workFlowStepId = $timelineItem.find('.WorkFlowStepId').text();


            _createOrEditModal.open({ id: workFlowStepId });


        });
        //function getproductFeesreload(dynamicValue) {

           
        //    var branchesAjax = $.ajax({
        //        url: abp.appPath + 'api/services/app/ProductFeeDetails/GetAll',
        //        //data: {
        //        //    ProductIdFilter: dynamicValue,
        //        //},
        //        method: 'GET',
        //        dataType: 'json',
        //    })
        //        .done(function (data) {
        //             
        //            console.log('Response from server:', data);
        //            globalFeeData = data;
        //            // Assign data to the global variable
        //            procesFeeData(data); // Call processData function after data is available
        //        })
        //        .fail(function (error) {
        //            console.error('Error fetching data:', error);
        //        });
        //}
        //function procesFeeData(data) {
        //     
        //    var cardContainer = $('#FeescardContainer'); // or replace '#container' with your actual container selector

        //    // Check if globalData.result.items is an array before attempting to iterate
        //    if (Array.isArray(globalFeeData.result.items)) {
        //        // Iterate through items and create cards
        //        for (var i = 0; i < globalFeeData.result.items.length; i += 3) {
        //            var rowDiv = $('<div>').addClass('row mt-3');

        //            for (var j = 0; j < 3 && (i + j) < globalFeeData.result.items.length; j++) {
        //                var item = globalFeeData.result.items[i + j];
        //                var card = createProductFeecard(item);

        //                var colDiv = $('<div>').addClass('col-md-12'); // Set the column size to 4 for three columns in a row
        //                colDiv.append(card);
        //                rowDiv.append(colDiv);
        //            }

        //            cardContainer.append(rowDiv);
        //        }
        //    } else {
        //        console.error('globalData.result.items is not an array:', globalData.result.items);
        //    }
        //}

        function clearMainDiv() {
            // Assuming main div has an id 'mainDiv', replace it with your actual id if needed
            $('#FeescardContainer').empty();
        }





        // Button click event handler
        $('#showCardsButton').click(function () {
         
            var cardContainer = $('#cardContainer');
            cardContainer.empty(); // Clear existing cards
            _createOrEditModal.open();
            //data.forEach(function (item) {
            //    var card = createCard(item);
            //    cardContainer.append(card);
            //});
        });


        var _$LeadSourceTable = $('#BranchTable');
        var _productOtherInformationsService = abp.services.app.productFees;

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };
        //_productOtherInformationsService.getAll().done(function (data) {
        //     ;
        //    processData(data);
        //}).fail(function (error) {
        //    console.error('Error fetching data:', error);
        //});
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
                getfee();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getfee();
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
                getfee();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getfee();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Products/CreateOrEditFeeModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Products/Fee/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditFeeModal',
        });
        var _viewLeadSourceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/PartnersDetails',
            //modalClass: 'ViewPartnersDetails',
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

        var dataTable = _$LeadSourceTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _productOtherInformationsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#PartnersTableFilter').val(),
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
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('View'),
                                action: function (data) {
                             
                                    //  _viewLeadSourceModal.open("/AppAreaName/Partners/PartnersDetails");
                                    // window.location.href = abp.appPath + 'AppAreaName/Partners/ViewApplicationDetails';
                                    //_viewLeadSourceModal.window.open("/AppAreaName/Partners/PartnersDetails");
                                    //var mybesturl = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split("/")[1] + "/";
                                    ////window.location.replace("/AppAreaName/Partners/PartnersDetails");
                                    //window.location = mybesturl + 'Partners/PartnersDetails';
                                },
                            },
                            {
                                text: app.localize('Edit'),
                                //visible: function () {
                                //    return _permissions.edit;
                                //},
                                action: function (data) {
                                    _createOrEditModal.open();

                                },
                            },
                            {
                                text: app.localize('Delete'),
                                //visible: function () {
                                //    return _permissions.delete;
                                //},
                                action: function (data) {
                                    deleteLeadSource(data.record.leadSource);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'leadSource.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'leadSource.name',
                    name: 'name',
                },

            ],
        });

        function getfee() {
            clearMainDiv()
            getproductFeesreload(dynamicValue);
        }

        function deleteotherinfo(productOtherInformation) {
     

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _productOtherInformationsService
                        .delete({
                            id: productOtherInformation.productOtherInformation.id,
                        })
                        .done(function () {
                            getfee(true);
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

        $('#CreateNewFeesButton').click(function () {
          
            _createOrEditModal.open();

        });
        //$('#showCardsButton').click(function () {
        //     
        //    _createOrEditModal.open();

        //   // window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        //});



        $('#ExportToExcelButton').click(function () {
            _productOtherInformationsService
                .getMasterCategoriesToExcel({
                    filter: $('#LeadSourcesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditFeeModalSaved', function () {
            getfee();
            //location.reload();
        });



        $(document).keypress(function (e) {
            if (e.which === 13) {
                getfee();
            }
        });

        $('.reload-on-change').change(function (e) {
            getfee();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getfee();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getfee();
        });
        //Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis81', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });

        // Close the context menu when clicking outside of it
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });

        // Handle menu item clicks
        $(document).on('click', 'a[data-action81]', function (e) {
            e.preventDefault();
         
            var rowId = $(this).data('id');
            var action = $(this).data('action81');
           
            // Handle the selected action based on the rowId
            if (action === 'view') {
                //_viewMasterCategoryModal.open({ id: rowId });
                window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;
            } else if (action === 'edit') {
                if (rowId == 0) {
                    _createOrEditModal.open()
                }
                else {
                    //window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;
                    _createOrEditModal.open({ id: rowId });
                }
            } else if (action === 'delete') {

                deleteotherinfo(rowId);
            }
        });

    });
})();
