﻿(function () {
    $(function () {
        var hiddenfield = $('input[name="Clientid"]').val();
        var Applicationdynamic = $("#ApplicationId").val();
        var srno = $('.nav-link.default.active .num').text().trim();
        var Applicationstage =$("#StageID-" + srno).val();
        var dynamicValue = hiddenfield;


        //getapplicationsnotesreload(dynamicValue, Applicationdynamic, Applicationstage);
        //var globalData; // Declare the data variable in a broader scope

        //function createCard(item) {
        //    debugger
        //    var note = item.note;

        //    // Create a single row for all cards
        //    var mainDiv = $('<div>').addClass('maincard maindivcard').css({
        //        'margin-left': '0.2px',
        //    });
        //    var cardContainer = $('<div>').addClass('row'); // New container for cards in a row

        //    // Create a column for each card
        //    var colDiv = $('<div>').addClass('col-md-12');
        //    var cardDiv = $('<div>').addClass('card');
        //    var cardBodyDiv = $('<div>').addClass('card-body');

        //    // Create a row for the card title and dots
        //    var titleRowDiv = $('<div>').addClass('row');
        //    var titleColDiv = $('<div>').addClass('col-md-12'); // Adjust the column size as needed
        //    var cardTitle = $('<p>').addClass('card-title');
        //    function renderDateTime(creationTime) {
        //        let formattedDate = creationTime ? moment(creationTime).format('L') : "";

        //        return formattedDate;
        //    }
        //    const currentDate = new Date(note.creationTime);
        //    const today = new Date();

        //    // Calculate the time difference in milliseconds
        //    const timeDifference = today - currentDate;

        //    // Convert the time difference to days, hours, and minutes
        //    const days = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
        //    const hours = Math.floor((timeDifference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        //    const minutes = Math.floor((timeDifference % (1000 * 60 * 60)) / (1000 * 60));

        //    // Format the result
        //    const result = `${days} days, ${hours} hours, ${minutes} minutes`;

        //    console.log(result);
        //    // Include context menu HTML within the title
        //    //var rowId = data.partner.id;
        //    //var rowData = data.partner;
        //    //var RowDatajsonString = JSON.stringify(rowData);
        //    cardTitle.html('<strong>' + note.title + '</strong>' +
        //        '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
        //        '<div class="ellipsis1234"><a href="#" data-id="' + note.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
        //        '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
        //        '<ul style="list-style: none; padding: 0;color:black">' +
        //        /*   '<a href="#" style="color: black;" data-action="view" data-id="' + branch.id + '"><li>View</li></a>' +*/
        //        '<a href="#" style="color: black;" data-action1234="edit" data-id="' + note.id + '"><li>Edit</li></a>' +
        //        "<a href='#' style='color: black;' data-action1234='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
        //        '</ul>' +
        //        '</div>' +
        //        '</div>');

        //    // Append title and dots to the title column......
        //    titleColDiv.append(cardTitle);
        //    titleRowDiv.append(titleColDiv);

        //    // Create a column for the card information
        //    var infoColDiv = $('<div>').addClass('col-md-6'); // Adjust the column size as needed
        //    var infoParagraph1 = $('<p>').addClass('card-text');
        //    var infoParagraph2 = $('<p>').addClass('card-text');
        //    infoParagraph1.html(note.description);
        //    infoParagraph2.html('<hr/>' +
        //        'Added by: ' +
        //        '<strong>' + item.userName + '</strong>' +
        //        '<br>' +
        //        '<span class="text-muted">' + renderDateTime(note.creationTime) + '</span>' +
        //        '<span class="text-muted pull-right">' + result + '</span>');

        //    cardBodyDiv.append(titleRowDiv, infoParagraph1, infoParagraph2);
        //    cardDiv.append(cardBodyDiv);
        //    colDiv.append(cardDiv);
        //    cardContainer.append(colDiv);

        //    // Append the card container to the mainDiv
        //    mainDiv.append(cardContainer);

        //    return mainDiv; // Return the created card..
        //}






        //function getapplicationsnotesreload(dynamicValue,Applicationdynamic,Applicationstage) {
        //    debugger


        ////    var branchesAjax = $.ajax({
        ////        url: abp.appPath + 'api/services/app/notes/GetAll',
        ////        data: {
        ////            ClientIdFilter: dynamicValue,
        ////            ApplicationIdFilter: Applicationdynamic,
        ////            ApplicationstageIdFilter: Applicationstage,
        ////        },
        ////        method: 'GET',
        ////        dataType: 'json',
        ////    })
        ////        .done(function (data) {
        ////            // console.log('Response from server:', data);
        ////            globalData = data; // Assign data to the global variable
        ////            processData(); // Call processData function after data is available
        ////        })
        ////        .fail(function (error) {
        ////            console.error('Error fetching data:', error);
        ////        });
        ////}
        //function processData() {
        //    var cardContainer = $('#cardContainerapplicationnotes'); // or replace '#container' with your actual container selector

        //    // Check if globalData.result.items is an array before attempting to iterate
        //    if (Array.isArray(globalData.result.items)) {
        //        // Iterate through items and create cards
        //        for (var i = 0; i < globalData.result.items.length; i += 3) {
        //            var rowDiv = $('<div>').addClass('row mt-3');

        //            for (var j = 0; j < 3 && (i + j) < globalData.result.items.length; j++) {
        //                var item = globalData.result.items[i + j];
        //                var card = createCard(item);

        //                var colDiv = $('<div>').addClass('col-md-4'); // Set the column size to 4 for three columns in a row
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


        var _$NotesTable = $('#BranchTable');
        var _notesService = abp.services.app.notes;

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
                getApplicationsNotes();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getApplicationsNotes();
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
                getApplicationsNotes();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getApplicationsNotes();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditNotesApplicationModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditApplicationNotesModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Applicationtabs/Notes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditNotesAndTermsModal',
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

        var dataTable = _$NotesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _notesService.getAll,
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

        function getApplicationsNotes() {
            //branchesAjax.reload();
            clearMainDiv();
            getapplicationsnotesreload(dynamicValue);
        }

        function deletenotes(note) {
            debugger

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _notesService
                        .delete({
                            id: note.note.id,
                        })
                        .done(function () {
                            
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                            location.reload();
                        });
                }
            });
        }



        $(document).off("click", ".BtnNewNotes").on("click", ".BtnNewNotes", function () {
            debugger
            _createOrEditNotesApplicationModal.open();
        });
        //$('#showCardsButton').click(function () {
        //    debugger
        //    _createOrEditModal.open();

        //   // window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        //});




        abp.event.on('app.createOrEditNoteModalSaved', function () {
            getApplicationsNotes();
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            getApplicationsNotes();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getApplicationsNotes();
            }
        });

        $('.reload-on-change').change(function (e) {
            getApplicationsNotes();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getApplicationsNotes();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getApplicationsNotes();
        });
        // Add a click event handler for the ellipsis icons

        $(document).off("click", ".ellipsis1234").on("click", ".ellipsis1234", function (e) {

            debugger
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
      
            $(document).off("click", "a[data-action1234]").on("click", "a[data-action1234]", function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action1234');
    
            // Handle the selected action based on the rowId
            if (action === 'view') {
                //_viewMasterCategoryModal.open({ id: rowId });
                window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;
            } else if (action === 'edit') {
                //window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;
                _createOrEditNotesApplicationModal.open({ id: rowId });
            } else if (action === 'delete') {

                deletenotes(rowId);
            }
        });
    });
})(jQuery);