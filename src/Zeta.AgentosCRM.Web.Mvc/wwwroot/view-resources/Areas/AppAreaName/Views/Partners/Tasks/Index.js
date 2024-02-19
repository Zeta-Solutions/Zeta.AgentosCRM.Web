(function () {
    $(function () {
        var hiddenfield = $("#PartnerId").val();
        var dynamicValue = hiddenfield;

        getLtasksreload(dynamicValue);
        var globalData; // Declare the data variable in a broader scope

        function createCardTask(item) {
            var cRMTasks = item.crmTask;
            var cardId = 'card_' + cRMTasks.id;
            // Create a single row for all cards
            var mainDiv = $('<div>').addClass('maincard maindivcard')
                .css({
                    'margin-left': '0.2px',
                    'margin-bottom': '20px' // Add margin between cards
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

            // Include context menu HTML within the title
            //cardTitle.html(
            //    '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
            //    '<div class="ellipsisT"><a href="#" data-id="' + cRMTasks.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
            //    '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
            //    '<ul style="list-style: none; padding: 0;color:black">' +
            //    '<a href="#" style="color: black;" data-action15="edit" data-id="' + cRMTasks.id + '"><li>Edit</li></a>' +
            //    "<a href='#' style='color: black;' data-action15='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
            //    '</ul>' +
            //    '</div>' +
            //    '</div>');

            // Append title and dots to the title column
            //titleColDiv.append(cardTitle);
            //titleRowDiv.append(titleColDiv);

            // Create a column for the card information
            var infoColDiv = $('<div>').addClass('row'); // Adjust the column size as needed
            var infoParagraph1 = $('<p>').addClass('card-text col-md-1');
            var infoParagraph2 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph3 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph4 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph5 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph6 = $('<p>').addClass('card-text col-md-2');
            var infoParagraph7 = $('<p>').addClass('card-text col-md-1');
            //infoParagraph.html('<input type="checkbox" id="reminderCheckbox" />' +'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'  +'<strong>Reminder:</strong>' + '&nbsp;&nbsp;' + cRMTasks.title + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
            //     +
            //    '<svg height="70" width="70">' +
            //    '<circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" />' +
            //    '<text x="30" y="35" text-anchor="middle" fill="white">' + item.userName + '</text>' +
            //    '</svg>' + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'
            //    + item.taskPriorityName + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + '<span class="replacedate label badge badge-success">' + cRMTasks.dueDate+'</span>'

            //    + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' +

            // );
            function renderDateTime(dueDate, dueTime) {
                let formattedDate = dueDate ? moment(dueDate).format('L') : "";
                let formattedTime = dueTime ? moment(dueTime).format('LT') : "";
                return formattedDate + " " + formattedTime;
            }
            infoParagraph1.html('<input type="checkbox" id="reminderCheckbox" ' + (cRMTasks.isCompleted == true ? 'checked' : '') + '/>');
            infoParagraph2.html('<strong> Reminder:</strong><span class="tasktittle">' + cRMTasks.title + '</span><input type="hidden" id="taskid" class="taskid" value="' + cRMTasks.id + '"/>');
            infoParagraph3.html('<span class="replacename label badge badge-primary">' + item.userName + '</span>');
            if (cRMTasks.isCompleted == true) {
                //infoParagraph4.html('<span class="replacedate label badge badge-danger">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>');

                infoParagraph4.html('<span class="replacedate label badge badge-success">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>' + '<input type="hidden" id="dueDates" class="dueDates" value="' + cRMTasks.dueDate + '"/>' + '<input type="hidden" id="dueTimes" class="dueTimes" value="' + cRMTasks.dueTime + '"/>');
            } else {
                //infoParagraph4.html('<span class="replacedate label badge badge-danger">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>');
                infoParagraph4.html('<span class="replacedate label badge badge-danger">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>' + '<input type="hidden" id="dueDates" class="dueDates" value="' + cRMTasks.dueDate + '"/>' + '<input type="hidden" id="dueTimes" class="dueTimes" value="' + cRMTasks.dueTime + '"/>');
            }
            infoParagraph5.html(item.taskPriorityName);
            if (cRMTasks.isCompleted) {
                infoParagraph6.html('<span style="color: green; font-weight: bold;" class="replace">Completed</span>');
            } else {
                infoParagraph6.html('<span style="color: red; font-weight: bold;" class="replace">Todo</span>');
            }
            //infoParagraph6.html('<span style="color: red; font-weight: bold;" class="replace"> Todo</span>');
            infoParagraph7.html('<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                '<div class="ellipsisT"><a href="#" data-id="' + cRMTasks.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                '<a href="#" style="color: black;" data-action15="edit" data-id="' + cRMTasks.id + '"><li>Edit</li></a>' +
                "<a href='#' style='color: black;' data-action15='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
                '</ul>' +
                '</div>' +
                '</div>'); 
            infoColDiv.append(infoParagraph1, infoParagraph2, infoParagraph3, infoParagraph5, infoParagraph4, infoParagraph6, infoParagraph7);
            cardBodyDiv.append(infoColDiv);

            cardDiv.append(cardBodyDiv); 
            mainDiv.addClass(cardId); 
            mainDiv.append(cardDiv);

            return mainDiv;  
        }


        $(document).on('change', '#reminderCheckbox', function () {
            var card = $(this).closest('.maincard');  
            var replaceElement = card.find('.replace');
            var replaceElementdate = card.find('.replacedate');
             
            if ($(this).prop('checked')) {
                replaceElement.text('Completed').css({
                    'color': 'green',
                    'font-weight': 'bold'
                });
                replaceElementdate.removeClass('badge-danger').addClass('badge-success').css('color', 'white');
                var taskid = card.find('.taskid').val();
                var isCompleted = true;
                var inputData = {

                    isCompleted: isCompleted,
                    taskid: taskid

                };
                var Steps = JSON.stringify(inputData);
                Steps = JSON.parse(Steps);
                _cRMTasksService
                    .updateTaskIsCompleted(Steps)

            } else {
                replaceElement.text('Todo').css({
                    'color': 'red',
                    'font-weight': 'bold'
                });
                replaceElementdate.removeClass('badge-success').addClass('badge-danger').css('color', 'white');
                var taskid = card.find('.taskid').val();
                var isCompleted = false;
                var inputData = {

                    isCompleted: isCompleted,
                    taskid: taskid

                };
                var Steps = JSON.stringify(inputData);
                Steps = JSON.parse(Steps);
                _cRMTasksService
                    .updateTaskIsCompleted(Steps)
            } 
            console.log(replaceElement.text());
        });







        function getLtasksreload(dynamicValue) {
             


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/cRMTasks/GetAll',
                data: {
                    PartnerIdFilter: dynamicValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    console.log('Response from server:', data);
                    globalData = data; 
                    processData(data); 
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function processData(data) {
             
            var cardContainer = $('#cardTaskContainer'); 
             
            if (Array.isArray(globalData.result.items)) { 
                for (var i = 0; i < globalData.result.items.length; i += 3) {
                    var rowDiv = $('<div>').addClass('row mt-3');

                    for (var j = 0; j < 3 && (i + j) < globalData.result.items.length; j++) {
                        var item = globalData.result.items[i + j];
                        var card = createCardTask(item);

                        var colDiv = $('<div>').addClass('col-md-12'); // Set the column size to 4 for three columns in a row
                        colDiv.append(card);
                        rowDiv.append(colDiv);
                    }

                    cardContainer.append(rowDiv);
                }
            } else {
                console.error('globalData.result.items is not an array:', globalData.result.items);
            }
        }
        function clearMainDiv() { 
            $('.maindivcard').remove();

        } 
        $('#showCardsButton').click(function () {
             
            var cardContainer = $('#cardContainer');
            cardContainer.empty();  
            _createOrEditModal.open(); 
        });


        var _$LeadSourceTable = $('#BranchTable');
        var _cRMTasksService = abp.services.app.cRMTasks;

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
            viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditTasksModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Tasks/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditTaskModal',
        });

        var _viewLeadSourceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/PartnersDetails', 
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
         
        function gettasks() {
            //branchesAjax.reload();
            clearMainDiv();
            getLtasksreload(dynamicValue);
        }

        function deletetasks(crmTask) {
             

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
             
            _createOrEditModal.open();

            // window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        });
        //$('#showCardsButton').click(function () {
        //     
        //    _createOrEditModal.open();

        //   // window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        //});

        $('#BranchesButton').click(function () {
             
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
})();
