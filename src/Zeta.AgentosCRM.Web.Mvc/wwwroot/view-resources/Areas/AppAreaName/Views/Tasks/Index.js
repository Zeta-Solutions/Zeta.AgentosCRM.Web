(function () {
    $(function () {
        //var hiddenfield = $("#ClientId").val();
        //var dynamicValue = hiddenfield;

        getLtasksreload();
        var globalData; // Declare the data variable in a broader scope

        function createCardTask(item) {
            var cRMTasks = item.crmTask;
            var cardId = 'card_' + cRMTasks.id;
            // Create a single row for all cards
            var mainDiv = $('<div>').addClass('maincard maindivcard')
                .css({
                    'margin-left': '0.2px',
                    'margin-bottom': '5px' // Add margin between cards
                });

            // Create a column for each card
            //var colDiv = $('<div>').addClass('col-md-12');
            var cardDiv = $('<div>').addClass('card').css({
                'padding': '5px '
            });
            var cardBodyDiv = $('<div>').addClass('card-body').css({
                'padding': '5px '
            });

            // Set max-height and overflow for the card body
            //cardBodyDiv.css({
            //    'max-height': '100px', // Adjust the height as needed
            //    'overflow-y': 'auto'  // Add vertical scroll if content exceeds max height
            //});
            $('.circle-svg').css({
                'display': 'inline-block',
                'vertical-align': 'middle', // Align vertically with the text
                'margin-left': '10px'  // Adjust margin as needed
            });

            // Create a row for the card title and dots
            //var titleRowDiv = $('<div>').addClass('row');
            var titleColDiv = $('<div>').addClass('col-md-12'); // Adjust the column size as needed
            var cardTitle = $('<p>').addClass('card-title');

            //var formattedDate = cRMTasks.dueDate.toLocaleDateString('en-US', {
            //year: 'numeric',
            //    month: '2-digit',
            //        day: '2-digit',
            //                });
            var infoColDiv = $('<div>').addClass('row'); // Adjust the column size as needed
            var infoParagraph1 = $('<p>').addClass('card-text col-md-1');
            var infoParagraph2 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph3 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph4 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph5 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph6 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph7 = $('<p>').addClass('card-text col-md-1');
            function render(endTime) {
                if (endTime) {
                    return moment(endTime).format('L LT');
                }
                return "";
            }
            infoParagraph1.html('<input type="checkbox" id="reminderCheckbox" />');
            infoParagraph2.html('<strong>Reminder:</strong>' + '&nbsp;&nbsp;' + cRMTasks.title);
            infoParagraph3.html('<span class="replacename label badge badge-primary">' + item.userName + '</span>');
            infoParagraph4.html('<span class="replacedate label badge badge-danger">' + render(cRMTasks.dueDate) + '</span>');
            infoParagraph5.html(item.taskPriorityName);
            infoParagraph6.html('<span style="color: red; font-weight: bold;" class="replace"> Todo</span>');
            infoParagraph7.html('<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                '<div class="ellipsisT"><a href="#" data-id="' + cRMTasks.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                '<a href="#" style="color: black;" data-action15="edit" data-id="' + cRMTasks.id + '"><li>Edit</li></a>' +
                "<a href='#' style='color: black;' data-action15='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
                '</ul>' +
                '</div>' +
                '</div>');
            // infoParagraph.html(branch.name + '<br>' + item.countryName + '<br><hr>' + branch.email);
            infoColDiv.append(infoParagraph1, infoParagraph2, infoParagraph3, infoParagraph5, infoParagraph4, infoParagraph6, infoParagraph7);
            cardBodyDiv.append(infoColDiv);

            cardDiv.append(cardBodyDiv);
            //colDiv.append(cardDiv);
            mainDiv.addClass(cardId);
            // Append the card container to the mainDiv
            mainDiv.append(cardDiv);

            return mainDiv; // Return the created card
        }


        $(document).on('change', '#reminderCheckbox', function () {
            var card = $(this).closest('.maincard'); // Assuming the unique class is 'maincard'
            var replaceElement = card.find('.replace');
            var replaceElementdate = card.find('.replacedate');

            // Check if the checkbox is checked
            if ($(this).prop('checked')) {
                replaceElement.text('Completed').css({
                    'color': 'green',
                    'font-weight': 'bold'
                });
                replaceElementdate.removeClass('badge-danger').addClass('badge-success').css('color', 'white');
            } else {
                replaceElement.text('Todo').css({
                    'color': 'red',
                    'font-weight': 'bold'
                });
                replaceElementdate.removeClass('badge-success').addClass('badge-danger').css('color', 'white');
            }
            // Additional logic or actions can be added here

            // For testing, you can log the current state to the console
            // console.log(replaceElement.text());
        });







        function getLtasksreload() {
            debugger


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/cRMTasks/GetAll',
                //data: {
                //    PartnerIdFilter: dynamicValue,
                //},
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    // console.log('Response from server:', data);
                    globalData = data; // Assign data to the global variable
                    processData(data); // Call processData function after data is available
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function processData(data) {
            debugger
            var cardContainer = $('#cardTaskContainer'); // or replace '#container' with your actual container selector

            // Check if globalData.result.items is an array before attempting to iterate
            if (Array.isArray(globalData.result.items)) {
                // Iterate through items and create cards
                for (var i = 0; i < globalData.result.items.length; i += 3) {
                    var rowDiv = $('<div>').addClass('row mt-3');

                    for (var j = 0; j < 3 && (i + j) < globalData.result.items.length; j++) {
                        var item = globalData.result.items[i + j];
                        var card = createCardTask(item);

                        var colDiv = $('<div>').addClass('col-md-12'); // Set the column size to 4 for three columns in a row
                        colDiv.append(card);
                        rowDiv.append(colDiv);
                        rowDiv.append('<hr/>');
                    }
                   
                    cardContainer.append(rowDiv);

                }
            } else {
                console.error('globalData.result.items is not an array:', globalData.result.items);
            }
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
        var _cRMTasksService = abp.services.app.cRMTasks;

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };
        //_cRMTasksService.getAll().done(function (data) {
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
                gettasks();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                gettasks();
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
                gettasks();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                gettasks();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Tasks/CreateOrEditTasksModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Tasks/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditTaskModal',
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
                ajaxFunction: _cRMTasksService.getAll,
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

        function gettasks() {
            //branchesAjax.reload();
            location.reload();
        }

        function deletetasks(crmTask) {
            debugger

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _cRMTasksService
                        .delete({
                            id: crmTask.crmTask.id,
                        })
                        .done(function () {
                            gettasks(true);
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

        $('#CreateNewTaskButton').click(function () {
            debugger
            _createOrEditModal.open();

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
            _cRMTasksService
                .getMasterCategoriesToExcel({
                    filter: $('#LeadSourcesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditTaskModalSaved', function () {
            gettasks();
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            gettasks();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                gettasks();
            }
        });

        $('.reload-on-change').change(function (e) {
            gettasks();
        });

        $('.reload-on-keyup').keyup(function (e) {
            gettasks();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            gettasks();
        });
        //Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsisT', function (e) {
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
        $(document).on('click', 'a[data-action15]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action15');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'view') {
                //_viewMasterCategoryModal.open({ id: rowId });
                window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;
            } else if (action === 'edit') {
                //window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {

                deletetasks(rowId);
            }
        });
    });
})(jQuery);
