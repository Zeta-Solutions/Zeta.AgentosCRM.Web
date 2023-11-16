﻿(function () {
    $(function () {
        var hiddenfield = $("#PartnerId").val();
        var dynamicValue = hiddenfield;
        var receivedId = 0;

        var globalData; // Declare the data variable in a broader scope
        getagreementsreload(dynamicValue)
        function createCard(item) {
            var branch = item.branch;

            // Create a single row for all cards
            var mainDiv = $('<div>').addClass('maincard maindivcard').css({
                'margin-left': '0.2px',
            });
            var cardContainer = $('<div>').addClass('row'); // New container for cards in a row

            // Create a column for each card
            var colDiv = $('<div>').addClass('col-md-12');
            var cardDiv = $('<div>').addClass('card');
            var cardBodyDiv = $('<div>').addClass('card-body');

            // Create a row for the card title and dots
            var titleRowDiv = $('<div>').addClass('row');
            var titleColDiv = $('<div>').addClass('col-md-12'); // Adjust the column size as needed
            var cardTitle = $('<h5>').addClass('card-title');

            // Include context menu HTML within the title
            //var rowId = data.partner.id;
            //var rowData = data.partner;
            //var RowDatajsonString = JSON.stringify(rowData);
            cardTitle.html('Head Office' +
                '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                '<div class="ellipsis"><a href="#" data-id="' + branch.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                /*   '<a href="#" style="color: black;" data-action="view" data-id="' + branch.id + '"><li>View</li></a>' +*/
                '<a href="#" style="color: black;" data-action="edit" data-id="' + branch.id + '"><li>Edit</li></a>' +
                "<a href='#' style='color: black;' data-action='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
                '</ul>' +
                '</div>' +
                '</div>');

            // Append title and dots to the title column
            titleColDiv.append(cardTitle);
            titleRowDiv.append(titleColDiv);

            // Create a column for the card information
            var infoColDiv = $('<div>').addClass('col-md-6'); // Adjust the column size as needed
            var infoParagraph = $('<p>').addClass('card-text');
            infoParagraph.html(branch.name + '<br>' + item.countryName + '<br><hr>' + branch.email);

            cardBodyDiv.append(titleRowDiv, infoParagraph);
            cardDiv.append(cardBodyDiv);
            colDiv.append(cardDiv);
            cardContainer.append(colDiv);

            // Append the card container to the mainDiv
            mainDiv.append(cardContainer);

            return mainDiv; // Return the created card
        }






        function getagreementsreload(dynamicValue) {
            debugger


            var PartnerContractsAjax = $.ajax({
                url: abp.appPath + 'api/services/app/PartnerContracts/GetAll',
                data: {
                    PartnerIdFilter: dynamicValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    receivedId = data.result.items[0].partnerContract.id; 

                    globalData = data; 
                    processData(data); 

                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function processData(data) {
            debugger
            var dateValue = data.result.items[0].partnerContract.contractExpiryDate;
            var AgentName = data.result.items[0].agentName;
            var RegionName = data.result.items[0].regionName;
            var Commessionper = data.result.items[0].partnerContract.commissionPer;

            // Select the <p> element with the class "text-muted date"
            var paragraphElement = document.querySelector('.date');

            var paragraphElement1 = document.querySelector('.region');
            var paragraphElement2 = document.querySelector('.Commission');
            var paragraphElement3 = document.querySelector('.Agents');

            // Update the text content of the <p> element with the 'date' value
            //paragraphElement.textContent = dateValue;
            paragraphElement1.textContent = RegionName;
            paragraphElement2.textContent = Commessionper;
            paragraphElement3.textContent = AgentName;

            var formattedDate = moment(dateValue).format('L');
            paragraphElement.textContent = formattedDate;

               
            

        }
        function clearMainDiv() {
            // Assuming main div has an id 'mainDiv', replace it with your actual id if needed
            $('.maindivcard').remove();

        }





        // Button click event handler
        $('#showCardsButton').click(function () {
            debugger
            var cardContainer = $('#cardContainer');
            cardContainer.empty(); // Clear existing cards
            _createOrEditModal.open();
            //data.forEach(function (item) {
            //    var card = createCard(item);
            //    cardContainer.append(card);
            //});
        });


        var _$LeadSourceTable = $('#BranchTable');
        var _partnerContractsService = abp.services.app.partnerContracts;

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };
        //_branchesService.getAll().done(function (data) {
        //    debugger;
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
                getcontracts();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getcontracts();
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
                getcontracts();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getcontracts();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditAgreementsModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Agreements/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditAgreementsModal',
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
                ajaxFunction: _partnerContractsService.getAll,
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
                                    debugger;
                                    alert("sabar kro abhi bnaya nhi !")
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

        function getcontracts() {
            //branchesAjax.reload();
            clearMainDiv();
            getagreementsreload(dynamicValue);
        }

        function deletebranches(branch) {
            debugger

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _partnerContractsService
                        .delete({
                            id: branch.branch.id,
                        })
                        .done(function () {
                            getcontracts(true);
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

        $('#CreateNewContractButton').click(function () {
            debugger;

            // Assuming receivedId is a globally declared variable
            if (receivedId && parseInt(receivedId) > 0) {
                _createOrEditModal.open({ id: receivedId });
               
            } else {
                // Prompt the user to provide an ID or handle it according to your application logic.
                _createOrEditModal.open();
              
            }
             getagreementsreload(dynamicValue);
        });

        //$('#showCardsButton').click(function () {
        //    debugger
        //    _createOrEditModal.open();

        //   // window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        //});

        $('#BranchesButton').click(function () {
            debugger
            var cardContainer = $('#cardContainer');
            cardContainer.empty(); // Clear existing cards

            //data.forEach(function (item) {
            //    var card = createCard(item);
            //    cardContainer.append(card);
            //});

            // window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        });

        $('#ExportToExcelButton').click(function () {
            _partnerContractsService
                .getMasterCategoriesToExcel({
                    filter: $('#LeadSourcesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditPartnerContractsModalSaved', function () {
            getagreementsreload(dynamicValue);
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            getcontracts();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getcontracts();
            }
        });

        $('.reload-on-change').change(function (e) {
            getcontracts();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getcontracts();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getcontracts();
        });
        // Add a click event handler for the ellipsis icons
        //$(document).on('click', '.ellipsis', function (e) {
        //    e.preventDefault();

        //    var options = $(this).closest('.context-menu').find('.options');
        //    var allOptions = $('.options');  // Select all options

        //    // Close all other open options
        //    allOptions.not(options).hide();

        //    // Toggle the visibility of the options
        //    options.toggle();
        //});

        //// Close the context menu when clicking outside of it
        //$(document).on('click', function (event) {
        //    if (!$(event.target).closest('.context-menu').length) {
        //        $('.options').hide();
        //    }
        //});

        //// Handle menu item clicks
        //$(document).on('click', 'a[data-action]', function (e) {
        //    e.preventDefault();

        //    var rowId = $(this).data('id');
        //    var action = $(this).data('action');
        //    debugger
        //    // Handle the selected action based on the rowId
        //    if (action === 'view') {
        //        //_viewMasterCategoryModal.open({ id: rowId });
        //        window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;
        //    } else if (action === 'edit') {
        //        //window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;
        //        _createOrEditModal.open({ id: rowId });
        //    } else if (action === 'delete') {

        //        deletebranches(rowId);
        //    }
        //});
    });
})();