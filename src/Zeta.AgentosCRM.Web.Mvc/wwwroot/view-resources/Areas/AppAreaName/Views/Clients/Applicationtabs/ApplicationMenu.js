(function () {
    $(function () {
        var stagesdata;
        var InputValue = 0;
        if (InputValue == 1) {
           
        }
        else if (InputValue == 2) {
           
        }
        else if (InputValue == 3) {
           
        }
        else if (InputValue == 0) {

            $('#cardContainerapplicationnotes').empty();
            var hiddenfield = $('input[name="Clientid"]').val();
            var Applicationdynamic = $("#ApplicationId").val();
            //catch application stages current id 
             $.ajax({
                 url: abp.appPath + 'api/services/app/ApplicationStages/GetAll',
                data: {
                    ApplicationIdFilter: Applicationdynamic,
                    IsCurrentIdFilter:true
                },
                method: 'GET',
                dataType: 'json',
            })
                 .done(function (data) {
                     debugger
                    stagesdata=data
                    // console.log('Response from server:', data);
                     // Call processData function after data is 
                     var Applicationstage = stagesdata.result.items[0].applicationStage.id;
                     var dynamicValue = hiddenfield;
                     getApplicationtasksreload(dynamicValue, Applicationdynamic, Applicationstage);
                     function getApplicationtasksreload(dynamicValue, Applicationdynamic, Applicationstage) {
                         debugger


                         var branchesAjax = $.ajax({
                             url: abp.appPath + 'api/services/app/cRMTasks/GetAll',
                             data: {
                                 ClientIdFilter: dynamicValue,
                                 ApplicationIdFilter: Applicationdynamic,
                                 ApplicationstageIdFilter: Applicationstage,
                             },
                             method: 'GET',
                             dataType: 'json',
                         })
                             .done(function (data) {
                                 // console.log('Response from server:', data);
                                 globalData = data; // Assign data to the global variable
                                 processApplicationtaskData(data); // Call processData function after data is available
                             })
                             .fail(function (error) {
                                 console.error('Error fetching data:', error);
                             });
                     }
                 })
            //var srno = $('.nav-link.default.active .num').text().trim();
           
            function processApplicationtaskData(data) {
                debugger
                var cardContainer = $('#cardContainerapplicationTasks'); // or replace '#container' with your actual container selector

                // Check if globalData.result.items is an array before attempting to iterate
                if (Array.isArray(globalData.result.items)) {
                    // Iterate through items and create cards
                    for (var i = 0; i < globalData.result.items.length; i += 3) {
                        var rowDiv = $('<div>').addClass('row mt-3');

                        for (var j = 0; j < 3 && (i + j) < globalData.result.items.length; j++) {
                            var item = globalData.result.items[i + j];
                            var card = createCardApplicationTask(item);

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
            function createCardApplicationTask(item) {
                var cRMTasks = item.crmTask;
                var cardId = 'card_' + cRMTasks.id;
                var mainDiv = $('<div>').addClass('maincard maindivcard')
                    .css({
                        'margin-left': '0.2px',
                        'margin-bottom': '20px'
                    });

                var cardDiv = $('<div>').addClass('card').css({
                    'padding': '5px '
                });
                var cardBodyDiv = $('<div>').addClass('card-body').css({
                    'padding': '5px '
                });

                $('.circle-svg').css({
                    'display': 'inline-block',
                    'vertical-align': 'middle',
                    'margin-left': '10px'
                });

                var titleColDiv = $('<div>').addClass('col-md-12');
                var cardTitle = $('<p>').addClass('card-title');
                // Create a column for the card information
                var infoColDiv = $('<div>').addClass('row');
                var infoParagraph1 = $('<p>').addClass('card-text col-md-1');
                var infoParagraph2 = $('<p>').addClass('card-text col-md-2');
                var infoParagraph3 = $('<p>').addClass('card-text col-md-2');
                var infoParagraph4 = $('<p>').addClass('card-text col-md-2');
                var infoParagraph5 = $('<p>').addClass('card-text col-md-2');
                var infoParagraph6 = $('<p>').addClass('card-text col-md-2');
                var infoParagraph7 = $('<p>').addClass('card-text col-md-1');
                function renderDateTime(dueDate, dueTime) {
                    let formattedDate = dueDate ? moment(dueDate).format('L') : "";
                    let formattedTime = dueTime ? moment(dueTime).format('LT') : "";
                    return formattedDate + " " + formattedTime;
                }

                infoParagraph1.html('<input type="checkbox" id="reminderCheckboxApplication" ' + (cRMTasks.isCompleted == true ? 'checked' : '') + '/>' + '<input type="hidden" id="categoriesId" class="categoriesId" value="' + cRMTasks.taskCategoryId + '"/>');
                infoParagraph2.html('<strong>Reminder:</strong>&nbsp;&nbsp;<span class="tasktittle">' + cRMTasks.title + '</span><input type="hidden" id="taskid" class="taskid" value="' + cRMTasks.id + '"/>');
                infoParagraph3.html('<span class="replacename label badge badge-primary">' + item.userName + '</span>' + '<input type="hidden" id="assigneId" class="assigneId" value="' + cRMTasks.assigneeId + '"/>' + '<input type="hidden" id="taskdescription" class="taskdescription" value="' + cRMTasks.description + '"/>');
                if (cRMTasks.isCompleted == true) {
                    infoParagraph4.html('<span class="replacedate label badge badge-success">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>' + '<input type="hidden" id="dueDates" class="dueDates" value="' + cRMTasks.dueDate + '"/>' + '<input type="hidden" id="dueTimes" class="dueTimes" value="' + cRMTasks.dueTime + '"/>');
                } else {
                    infoParagraph4.html('<span class="replacedate label badge badge-danger">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>' + '<input type="hidden" id="dueDates" class="dueDates" value="' + cRMTasks.dueDate + '"/>' + '<input type="hidden" id="dueTimes" class="dueTimes" value="' + cRMTasks.dueTime + '"/>');
                }
                infoParagraph5.html(item.taskPriorityName + '<input type="hidden" id="taskPrioritysId" class="taskPrioritysId" value="' + cRMTasks.taskPriorityId + '"/>');
                if (cRMTasks.isCompleted) {
                    infoParagraph6.html('<span style="color: green; font-weight: bold;" class="replace">Completed</span>');
                } else {
                    infoParagraph6.html('<span style="color: red; font-weight: bold;" class="replace">Todo</span>');
                }
                infoParagraph7.html('<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                    '<div class="ellipsis151"><a href="#" data-id="' + cRMTasks.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                    '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                    '<ul style="list-style: none; padding: 0;color:black">' +
                    '<a href="#" style="color: black;" data-action151="edit" data-id="' + cRMTasks.id + '"><li>Edit</li></a>' +
                    "<a href='#' style='color: black;' data-action151='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
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

                return mainDiv; // Return the created card....
            }
        }

        $(document).ready(function () {
            $('ul.activeTab li').on('click', function () {
                $('ul.activeTab li').removeClass('active');


                $(this).addClass('active');


                $(this).addClass('custom-bg-color').css({ 'border-radius': '10em' })

                $('ul.activeTab li:not(.active)').removeClass("custom-bg-color").css('border-radius', '');
                debugger
                var InputValue = $(this).find('input').val();
               
                if (InputValue == 1) {
                    
                    $('#cardContainerapplicationTasks').empty();
                    var hiddenfield = $('input[name="Clientid"]').val();
                    var Applicationdynamic = $("#ApplicationId").val();
                    var srno = $('.nav-link.default.active .num').text().trim();
                    var Applicationstage = $("#StageID-" + srno).val();
                    var dynamicValue = hiddenfield;
                    getapplicationsnotesreload(dynamicValue, Applicationdynamic, Applicationstage);
                    function getapplicationsnotesreload(dynamicValue, Applicationdynamic, Applicationstage) {
                        debugger


                        var branchesAjax = $.ajax({
                            url: abp.appPath + 'api/services/app/notes/GetAll',
                            data: {
                                ClientIdFilter: dynamicValue,
                                ApplicationIdFilter: Applicationdynamic,
                                ApplicationstageIdFilter: Applicationstage,
                            },
                            method: 'GET',
                            dataType: 'json',
                        })
                            .done(function (data) {
                                // console.log('Response from server:', data);
                                globalData = data; // Assign data to the global variable
                                processData(); // Call processData function after data is available
                            })
                            .fail(function (error) {
                                console.error('Error fetching data:', error);
                            });
                    }
                    function processData() {
                        var cardContainer = $('#cardContainerapplicationnotes'); // or replace '#container' with your actual container selector

                        // Check if globalData.result.items is an array before attempting to iterate
                        if (Array.isArray(globalData.result.items)) {
                            // Iterate through items and create cards
                            for (var i = 0; i < globalData.result.items.length; i += 3) {
                                var rowDiv = $('<div>').addClass('row mt-3');

                                for (var j = 0; j < 3 && (i + j) < globalData.result.items.length; j++) {
                                    var item = globalData.result.items[i + j];
                                    var card = createCard(item);

                                    var colDiv = $('<div>').addClass('col-md-4'); // Set the column size to 4 for three columns in a row
                                    colDiv.append(card);
                                    rowDiv.append(colDiv);
                                }

                                cardContainer.append(rowDiv);
                            }
                        } else {
                            console.error('globalData.result.items is not an array:', globalData.result.items);
                        }
                    }
                    function createCard(item) {
                        debugger
                        var note = item.note;

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
                        var cardTitle = $('<p>').addClass('card-title');
                        function renderDateTime(creationTime) {
                            let formattedDate = creationTime ? moment(creationTime).format('L') : "";

                            return formattedDate;
                        }
                        const currentDate = new Date(note.creationTime);
                        const today = new Date();

                        // Calculate the time difference in milliseconds
                        const timeDifference = today - currentDate;

                        // Convert the time difference to days, hours, and minutes
                        const days = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
                        const hours = Math.floor((timeDifference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                        const minutes = Math.floor((timeDifference % (1000 * 60 * 60)) / (1000 * 60));

                        // Format the result
                        const result = `${days} days, ${hours} hours, ${minutes} minutes`;

                        cardTitle.html('<strong>' + note.title + '</strong>' +
                            '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                            '<div class="ellipsis1234"><a href="#" data-id="' + note.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            /*   '<a href="#" style="color: black;" data-action="view" data-id="' + branch.id + '"><li>View</li></a>' +*/
                            '<a href="#" style="color: black;" data-action1234="edit" data-id="' + note.id + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action1234='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>');

                        // Append title and dots to the title column......
                        titleColDiv.append(cardTitle);
                        titleRowDiv.append(titleColDiv);

                        // Create a column for the card information
                        var infoColDiv = $('<div>').addClass('col-md-6'); // Adjust the column size as needed
                        var infoParagraph1 = $('<p>').addClass('card-text');
                        var infoParagraph2 = $('<p>').addClass('card-text');
                        infoParagraph1.html(note.description);
                        infoParagraph2.html('<hr/>' +
                            'Added by: ' +
                            '<strong>' + item.userName + '</strong>' +
                            '<br>' +
                            '<span class="text-muted">' + renderDateTime(note.creationTime) + '</span>' +
                            '<span class="text-muted pull-right">' + result + '</span>');

                        cardBodyDiv.append(titleRowDiv, infoParagraph1, infoParagraph2);
                        cardDiv.append(cardBodyDiv);
                        colDiv.append(cardDiv);
                        cardContainer.append(colDiv);

                        // Append the card container to the mainDiv
                        mainDiv.append(cardContainer);

                        return mainDiv; // Return the created card..
                    }
            }
                    else if(InputValue==2){
                    $('#cardContainerapplicationnotes').empty();
                    $('#cardContainerapplicationTasks').empty();
                    }
                    else if(InputValue==3){
                    $('#cardContainerapplicationnotes').empty();
                    $('#cardContainerapplicationTasks').empty();
                    }
                else if (InputValue == 0) {
                    $('#cardContainerapplicationnotes').empty();
                    var hiddenfield = $('input[name="Clientid"]').val();
                    var Applicationdynamic = $("#ApplicationId").val();
                    var srno = $('.nav-link.default.active .num').text().trim();
                    var Applicationstage = $("#StageID-" + srno).val();
                    var dynamicValue = hiddenfield;
                    getApplicationtasksreload(dynamicValue, Applicationdynamic, Applicationstage);
                    function getApplicationtasksreload(dynamicValue, Applicationdynamic, Applicationstage) {
                        debugger


                        var branchesAjax = $.ajax({
                            url: abp.appPath + 'api/services/app/cRMTasks/GetAll',
                            data: {
                                ClientIdFilter: dynamicValue,
                                ApplicationIdFilter: Applicationdynamic,
                                ApplicationstageIdFilter: Applicationstage,
                            },
                            method: 'GET',
                            dataType: 'json',
                        })
                            .done(function (data) {
                                // console.log('Response from server:', data);
                                globalData = data; // Assign data to the global variable
                                processApplicationtaskData(data); // Call processData function after data is available
                            })
                            .fail(function (error) {
                                console.error('Error fetching data:', error);
                            });
                    }
                    function processApplicationtaskData(data) {
                        debugger
                        var cardContainer = $('#cardContainerapplicationTasks'); // or replace '#container' with your actual container selector

                        // Check if globalData.result.items is an array before attempting to iterate
                        if (Array.isArray(globalData.result.items)) {
                            // Iterate through items and create cards
                            for (var i = 0; i < globalData.result.items.length; i += 3) {
                                var rowDiv = $('<div>').addClass('row mt-3');

                                for (var j = 0; j < 3 && (i + j) < globalData.result.items.length; j++) {
                                    var item = globalData.result.items[i + j];
                                    var card = createCardApplicationTask(item);

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
                    function createCardApplicationTask(item) {
                        var cRMTasks = item.crmTask;
                        var cardId = 'card_' + cRMTasks.id;
                        var mainDiv = $('<div>').addClass('maincard maindivcard')
                            .css({
                                'margin-left': '0.2px',
                                'margin-bottom': '20px'
                            });

                        var cardDiv = $('<div>').addClass('card').css({
                            'padding': '5px '
                        });
                        var cardBodyDiv = $('<div>').addClass('card-body').css({
                            'padding': '5px '
                        });

                        $('.circle-svg').css({
                            'display': 'inline-block',
                            'vertical-align': 'middle',
                            'margin-left': '10px'
                        });

                        var titleColDiv = $('<div>').addClass('col-md-12');
                        var cardTitle = $('<p>').addClass('card-title');
                        // Create a column for the card information
                        var infoColDiv = $('<div>').addClass('row');
                        var infoParagraph1 = $('<p>').addClass('card-text col-md-1');
                        var infoParagraph2 = $('<p>').addClass('card-text col-md-2');
                        var infoParagraph3 = $('<p>').addClass('card-text col-md-2');
                        var infoParagraph4 = $('<p>').addClass('card-text col-md-2');
                        var infoParagraph5 = $('<p>').addClass('card-text col-md-2');
                        var infoParagraph6 = $('<p>').addClass('card-text col-md-2');
                        var infoParagraph7 = $('<p>').addClass('card-text col-md-1');
                        function renderDateTime(dueDate, dueTime) {
                            let formattedDate = dueDate ? moment(dueDate).format('L') : "";
                            let formattedTime = dueTime ? moment(dueTime).format('LT') : "";
                            return formattedDate + " " + formattedTime;
                        }

                        infoParagraph1.html('<input type="checkbox" id="reminderCheckboxApplication" ' + (cRMTasks.isCompleted == true ? 'checked' : '') + '/>' + '<input type="hidden" id="categoriesId" class="categoriesId" value="' + cRMTasks.taskCategoryId + '"/>');
                        infoParagraph2.html('<strong>Reminder:</strong>&nbsp;&nbsp;<span class="tasktittle">' + cRMTasks.title + '</span><input type="hidden" id="taskid" class="taskid" value="' + cRMTasks.id + '"/>');
                        infoParagraph3.html('<span class="replacename label badge badge-primary">' + item.userName + '</span>' + '<input type="hidden" id="assigneId" class="assigneId" value="' + cRMTasks.assigneeId + '"/>' + '<input type="hidden" id="taskdescription" class="taskdescription" value="' + cRMTasks.description + '"/>');
                        if (cRMTasks.isCompleted == true) {
                            infoParagraph4.html('<span class="replacedate label badge badge-success">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>' + '<input type="hidden" id="dueDates" class="dueDates" value="' + cRMTasks.dueDate + '"/>' + '<input type="hidden" id="dueTimes" class="dueTimes" value="' + cRMTasks.dueTime + '"/>');
                        } else {
                            infoParagraph4.html('<span class="replacedate label badge badge-danger">' + renderDateTime(cRMTasks.dueDate, cRMTasks.dueTime) + '</span>' + '<input type="hidden" id="dueDates" class="dueDates" value="' + cRMTasks.dueDate + '"/>' + '<input type="hidden" id="dueTimes" class="dueTimes" value="' + cRMTasks.dueTime + '"/>');
                        }
                        infoParagraph5.html(item.taskPriorityName + '<input type="hidden" id="taskPrioritysId" class="taskPrioritysId" value="' + cRMTasks.taskPriorityId + '"/>');
                        if (cRMTasks.isCompleted) {
                            infoParagraph6.html('<span style="color: green; font-weight: bold;" class="replace">Completed</span>');
                        } else {
                            infoParagraph6.html('<span style="color: red; font-weight: bold;" class="replace">Todo</span>');
                        }
                        infoParagraph7.html('<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                            '<div class="ellipsis151"><a href="#" data-id="' + cRMTasks.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action151="edit" data-id="' + cRMTasks.id + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action151='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
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

                        return mainDiv; // Return the created card....
                    }
                    }
            });
        }); 
        //$(document).off("click", ".BtnNewNotes").on("click", ".BtnNewNotes", function () {
        //    debugger
        //    _createOrEditNotesApplicationModal.open();
        //});

        //var _createOrEditNotesApplicationModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditNotesModal',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Applicationtabs/Notes/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditNotesAndTermsModal',
        //});
    });
})(jQuery);
