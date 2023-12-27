(function () {
    
    $(function () {
        debugger
        var _$EducationEnglishTestScoreTable = $('#EnglishTestScoretable');
        var _englisTestScoresService = abp.services.app.englisTestScores;

        var _$EducationOtherTestScoreTable = $('#OtherTestScoretable');
        var _otherTestScoresService = abp.services.app.otherTestScores;
        var _clientEducationsService = abp.services.app.clientEducations;

        var hiddenfield = $('input[name="Clientid"]').val();
        var dynamicValue = hiddenfield;
        //CArd start
        var receivedId = 0;
        geteducationsreload(dynamicValue);
        getproductEnglishreload(dynamicValue);
        getproductOtherscoresreload(dynamicValue);
        function getproductOtherscoresreload(dynamicValue) {


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/OtherTestScores/GetAll',
                data: {
                    ClientIdFilter: dynamicValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    console.log('Response from server:', data);
                    globalotherData = data; // Assign data to the global variable
                    processotherscoreData(data); // Call processData function after data is available
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function processotherscoreData(data) {
            debugger
            var cardContainer = $('#cardProductotherContainer'); // or replace '#container' with your actual container selector
            debugger
            // Check if globalData.result.items is defined and is an array with elements
            if (Array.isArray(globalotherData.result.items) && globalotherData.result.items.length > 0) {
                debugger
                // Call createEnglishCardTask once with the entire array
                var cards = createProductothercard(globalotherData.result.items);

                // Create a row and column for each card
                var rowDiv = $('<div>').addClass('row mt-3');
                var colDiv = $('<div>').addClass('col-md-12');

                // Append all cards to the column
                colDiv.append(cards);
                // Append the column to the row
                rowDiv.append(colDiv);
                // Append the row to the cardContainer
                cardContainer.append(rowDiv);
            } else {
                debugger
                var defaultotherItem = {}; // You can provide some default values or an empty object
                var defaultCard = createProductothercard(defaultotherItem);

                var defaultColDiv = $('<div>').addClass('col-md-12');
                defaultColDiv.append(defaultCard);

                var defaultRowDiv = $('<div>').addClass('row mt-3');
                defaultRowDiv.append(defaultColDiv);

                cardContainer.append(defaultRowDiv);
            }
        }
        function createProductothercard(item) {
            debugger;

            var otherTestScores = item || [{ otherTestScore: { id: 0, name: '' } }];

            // Now you have an array with a default object if item is falsy or undefined


            var mainDiv = $('<div>').addClass('maincard maindivcard').css({
                'margin-left': '0.2px',
                'margin-bottom': '20px' // Add margin between cards
            });

            // Create card components
            var cardDiv = $('<div>').addClass('card').css({
                'padding': '5px'
            });
            $('.circle-svg').css({
                'display': 'inline-block',
                'vertical-align': 'middle', // Align vertically with the text
                'margin-left': '10px'  // Adjust margin as needed
            });
            var cardBodyDiv = $('<div>').addClass('card-body').css({
                'padding': '5px'
            });

            var cardTitle = $('<h5>').addClass('card-title');

            cardTitle.html("Other Test Score" +
                '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                '<div class="ellipsis71"><a href="#" data-id="' + (otherTestScores.length > 0 ? otherTestScores[0].otherTestScore.id : 1) + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                '<a href="#" style="color: black;" data-action71="edit" data-id="' + (otherTestScores.length > 0 ? otherTestScores[0].otherTestScore.id : 1) + '"><li>Edit</li></a>' +
                '</ul>' +
                '</div>' +
                '</div><hr/>');


            var infoColheadingDiv = $('<div>').addClass('row');
            var infoColDiv = $('<div>').addClass('row');

            // Create headings
            var headings = ['&nbsp;&nbsp;', '<strong>SAT I:</strong>', '<strong>SAT II:</strong>', '<strong>GRE:</strong>', '<strong>GMAT:</strong>'];
            var dynamicHeadings = [];
            for (var i = 0; i < otherTestScores.length; i++) {
                var testName = otherTestScores[i].otherTestScore.name;
                var heading = '<strong>' + testName + ':</strong>';
                dynamicHeadings.push(heading);
            }
            var allHeadings = headings.slice(0, 1).concat(dynamicHeadings, headings.slice(dynamicHeadings.length + 1));
            for (var j = 0; j < allHeadings.length; j++) {
                $('<p>').addClass('card-text col-md-2').html(allHeadings[j]).appendTo(infoColheadingDiv);
            }
            cardBodyDiv.append(cardTitle);
            // Append headings to card body
            cardBodyDiv.append(infoColheadingDiv);
            debugger
            var infoParagraphs = [];
            var paragraph1 = $('<p>').addClass('card-text col-md-2').html('<strong>Over All Score:</strong>');
            infoColDiv.append(paragraph1);
            // Create info paragraphs
            for (var i = 0; i < otherTestScores.length; i++) {
                var currentProduct = otherTestScores[i];

                var paragraph = $('<p>').addClass('card-text col-md-2').html(
                    '<svg height="70" width="70">' +
                    '<circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" />' +
                    '<text x="30" y="35" text-anchor="middle" fill="white">' +
                    (currentProduct.otherTestScore.totalScore ? currentProduct.otherTestScore.totalScore : '-') +
                    '</text>' +
                    '</svg>'
                ).append(
                    $('<input>').attr('type', 'hidden').attr('name', 'Idother' + [i]).val(currentProduct.otherTestScore.id)
                );
                infoParagraphs.push(paragraph);
                // Append info paragraphs to card body

            }
            for (var k = 0; k < infoParagraphs.length; k++) {
                infoColDiv.append(infoParagraphs[k]);
            }
            // Append components to card body
            cardBodyDiv.append(infoColDiv);

            // Append card components to the mainDiv

            cardDiv.append(cardBodyDiv);
            mainDiv.append(cardDiv);

            return mainDiv; // Return the created card
        }
        function createEnglishCardTask(item) {
            debugger;

            var englisTestScores = item || [{ englisTestScore: { id: 0 } }];

            // Now you have an array with a default object if item is falsy or undefined..


            var mainDiv = $('<div>')
                .addClass('maincard maindivcard').css({
                'margin-left': '0px',
                'margin-bottom': '20px' // Add margin between cards
            });

            // Create card components
            var cardDiv = $('<div>').addClass('card').css({
                'padding': '5px'
            });
            $('.circle-svg').css({
                'display': 'inline-block',
                'vertical-align': 'middle', // Align vertically with the text
                'margin-left': '10px'  // Adjust margin as needed
            });
            var cardBodyDiv = $('<div>').addClass('card-body').css({
                'padding': '5px'
            });

            var cardTitle = $('<h5>').addClass('card-title');

            cardTitle.html("English Test Score" +
                '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                '<div class="ellipsis51"><a href="#" data-id="' + (englisTestScores.length > 0 ? englisTestScores[0].englisTestScore.id : 1) + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                '<a href="#" style="color: black;" data-action51="edit" data-id="' + (englisTestScores.length > 0 ? englisTestScores[0].englisTestScore.id : 1) + '"><li>Edit</li></a>' +
                '</ul>' +
                '</div>' +
                '</div><hr/>');


            var infoColheadingDiv = $('<div>').addClass('row');
            var infoColDiv = $('<div>').addClass('row');

            // Create headings
            var headings = ['&nbsp;&nbsp;', '<strong>Listening:</strong>', '<strong>Reading:</strong>', '<strong>Writing:</strong>', '<strong>Speaking:</strong>', '<strong>OverAllScore:</strong>'];

            for (var j = 0; j < headings.length; j++) {
                $('<p>').addClass('card-text col-md-2').html(headings[j]).appendTo(infoColheadingDiv);
            }
            cardBodyDiv.append(cardTitle);
            // Append headings to card body
            cardBodyDiv.append(infoColheadingDiv);
            debugger
            // Create info paragraphs
            for (var i = 0; i < englisTestScores.length; i++) {
                var currentProduct = englisTestScores[i];

                var infoParagraphs = [
                    $('<p>').addClass('card-text col-md-2').html('<strong>' + currentProduct.englisTestScore.name + ':' + '</strong>' + '&nbsp;&nbsp;' +
                        '<input type="hidden" name="Id' + [i] + '" value="' + currentProduct.englisTestScore.id + '"/>')
                        .appendTo(infoColheadingDiv),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.englisTestScore.listenting ? currentProduct.englisTestScore.listenting : '-')),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.englisTestScore.reading ? currentProduct.englisTestScore.reading : '-')),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.englisTestScore.writing ? currentProduct.englisTestScore.writing : '-')),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.englisTestScore.speaking ? currentProduct.englisTestScore.speaking : '-')),
                    $('<p>').addClass('card-text col-md-2').html(
                        '<svg height="70" width="70">' +
                        '<circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" />' +
                        '<text x="30" y="35" text-anchor="middle" fill="white">' +
                        (currentProduct.englisTestScore.totalScore ? currentProduct.englisTestScore.totalScore : '-') +
                        '</text>' +
                        '</svg>'
                    )
                ];

                // Append info paragraphs to card body
                for (var k = 0; k < infoParagraphs.length; k++) {
                    infoColDiv.append(infoParagraphs[k]);
                }
            }

            // Append components to card body
            cardBodyDiv.append(infoColDiv);

            // Append card components to the mainDiv

            cardDiv.append(cardBodyDiv);
            mainDiv.append(cardDiv);

            return mainDiv; // Return the created card
        }
        function getproductEnglishreload(dynamicValue) {


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/EnglisTestScores/GetAll',
                data: {
                    ClientIdFilter: dynamicValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    console.log('Response from server:', data);
                    globalenglishData = data; // Assign data to the global variable
                    procesenglishData(data); // Call processData function after data is available
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function procesenglishData(data) {
            debugger
            var cardContainer = $('#cardProductEnglishContainer'); // or replace '#container' with your actual container selector
            debugger
            // Check if globalData.result.items is defined and is an array with elements
            if (Array.isArray(globalenglishData.result.items) && globalenglishData.result.items.length > 0) {
                // Call createEnglishCardTask once with the entire array
                var cards = createEnglishCardTask(globalenglishData.result.items);

                // Create a row and column for each card
                var rowDiv = $('<div>').addClass('row mt-3');
                var colDiv = $('<div>').addClass('col-md-12');

                // Append all cards to the column
                colDiv.append(cards);
                // Append the column to the row
                rowDiv.append(colDiv);
                // Append the row to the cardContainer
                cardContainer.append(rowDiv);
            } else {
                debugger
                var defaultItem = {}; // You can provide some default values or an empty object
                var defaultCard = createEnglishCardTask(defaultItem);

                var defaultColDiv = $('<div>').addClass('col-md-12');
                defaultColDiv.append(defaultCard);

                var defaultRowDiv = $('<div>').addClass('row mt-3');
                defaultRowDiv.append(defaultColDiv);

                cardContainer.append(defaultRowDiv);
            }
        }
        //var globalData; // Declare the data variable in a broader scope

        ////For Id

    

        //


        function geteducationsreload(dynamicValue) {
            debugger


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/ClientEducations/GetAll',
                data: {
                    ClientIdFilter: dynamicValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    
                    var Card = data.result.items;
                    $.each(Card, function (index, item) {
                        debugger

                        const CourseStartDate = new Date(item.clientEducation.courseStartDate);
                        const CourseendDate = new Date(item.clientEducation.courseEndDate);

                        // Convert timestamp to a readable date format
                        const formattedStartDate = CourseStartDate.toLocaleDateString(); // Adjust this according to the format you desire
                        const formattedEndDate = CourseendDate.toLocaleDateString(); 
                        
                        // Convert the date string to a Date object
                        const Startdate = new Date(formattedStartDate);
                        const Enddate = new Date(formattedEndDate);

                        // Array of month names in English
                        const monthsInEnglish = [
                            'January', 'February', 'March', 'April', 'May', 'June',
                            'July', 'August', 'September', 'October', 'November', 'December'
                        ];

                        // Extract month in English
                        const Startmonth = monthsInEnglish[Startdate.getMonth()];
                        const Endmonth = monthsInEnglish[Enddate.getMonth()];

                        // Extract year
                        const Startyear = Startdate.getFullYear();
                        const Endyear = Enddate.getFullYear();

                        // The formatted date in English (Month and Year)
                        const formattedStartDateEnglish = `${Startmonth} ${Startyear}`;
                        const formattedEndDateEnglish = `${Endmonth} ${Endyear}`;

                        // Now you can use formattedDateEnglish where needed
                        //console.log('Formatted Date:', formattedStartDateEnglish); // Example output: "December 2009"
                        //console.log('Formatted Date:', formattedEndDateEnglish); // Example output: "December 2009"


// Adjust this according to the format you desire..
                       
                        var rowId = item.clientEducation.id;
                        var CardDiv = '<div class="row maincard"><div class="col-lg-6"><span><span style="font-weight:bold;">' + item.clientEducation.degreeTitle + '</span><br>' + item.clientEducation.institution + '<span></div>'
                            + '<div class="col-lg-5">'
                            + '<span>'
                            + '<span style="background-color:lightgray;border-radius: 5px; padding: 0 5px;""> ' + formattedStartDateEnglish + ' - ' + formattedEndDateEnglish + ' </span>' +
                            ' <br> <span style="color:blue;font-weight:bold;"><span>Score: </span> ' + item.clientEducation.acadmicScore
                            + (item.clientEducation.isGpa == true ? ' GPA' : ' Percentage') 
                            + '</span>'
                            + '<br><span>' + item.clientEducation.degreeTitle + ' >> ' + item.subjectAreaName + ' >>' + item.subjectName + '</span>'
                            + '</span></div>' +
                            '<div class="col-lg-1">' +
                            '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" Education-data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            '<a href="#" style="color: black;" Education-data-action="delete" data-id="' + rowId  + '"><li>Delete</li></a>' +
                            '</ul>' +
                            '</div>' +
                            '</div>' +'</div></div><br><br>   <hr/>';
                        $("#cardContainerEducation").append(CardDiv);
                      
                    });
                     
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }

        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis', function (e) {
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
        $(document).on('click', 'a[Education-data-action]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).attr('Education-data-action');
            debugger
            // Handle the selected action based on the rowId
             if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                
                deleteeducation(rowId);
            }
        });

        function deleteeducation(education) {
            debugger
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientEducationsService
                        .delete({
                            id: education,
                        })
                        .done(function () {
                            //getSubjectAreas(true);
                            debugger
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                            location.reload();
                        });
                }
            });
        }  

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
                getSubjects();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getSubjects();
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
                getSubjects();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getSubjects();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditClientEducationModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditEducationModal.js',
            modalClass: 'CreateOrEditEducationModal',
        });
        var _createOrEditenglishscoreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditEnglishScoreModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditEnglishModal.js',
            modalClass: 'CreateOrEditEnglishTestScoreModal',
        });
        var _createOrEditOtherscoreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditOtherScoreModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditOtherscoreModal',
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

        //testing


    //    var dataTable = _$EducationEnglishTestScoreTable.DataTable({
    //        paging: true,
    //        serverSide: true,
    //        processing: true,
    //        listAction: {
    //            ajaxFunction: _englisTestScoresService.getAll,
    //            inputFilter: function () {
    //                return {
    //                    filter: $('#PartnersTableFilter').val(),
    //                    abbrivationFilter: $('#AbbrivationFilterId').val(),
    //                    nameFilter: $('#NameFilterId').val(),
    //                };
    //            },
    //        },
    //        columnDefs: [
    //            {
    //                className: 'control responsive',
    //                orderable: false,
    //                render: function () {
    //                    return '';
    //                },
    //                targets: 0,
    //            },
    //            {
    //                targets: 1,
    //                render: function (data, type, row, meta) {
    //                    if (meta.row === 0) {
    //                        return '<p style="font-weight:bold;font-size:12px">toefl</p>';
    //                    } else if (meta.row === 1) {
    //                        return '<p style="font-weight:bold;font-size:12px">ielts</p>';
    //                    } else if (meta.row === 2) {
    //                        return '<p style="font-weight:bold;font-size:12px">pte</p>';
    //                    }
    //                }
    //            },
    //            {
    //                targets: 2,
    //                data: 'englisTestScore.listenting',
    //                name: 'listenting',
    //            },
    //            {
    //                targets: 3,
    //                data: 'englisTestScore.reading',
    //                name: 'reading',
    //            },
    //            {
    //                targets: 4,
    //                data: 'englisTestScore.writing',
    //                name: 'writing',
    //            },
    //            {
    //                targets: 5,
    //                data: 'englisTestScore.speaking',
    //                name: 'speaking',
    //            },

    //            {
    //                width: 100,
    //                targets: 6,
    //                data: null,
    //                orderable: false,
    //                autowidth: false,
    //                defaultcontent: '',
    //                // assuming 'row' contains the client data with properties 'firstname', 'lastname', and 'email'
    //                render: function (data, type, row) {

    //                    console.log(data.englisTestScore.totalScore);
    //                    return `
    //    <div class="d-flex align-items-center">
    //        <svg height="60" width="60"> <!-- increase height and width of svg -->
    //            <circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" /> <!-- increase the value of 'r' -->
    //            <text x="30" y="35" text-anchor="middle" fill="white">${data.englisTestScore.totalScore}</text>
    //        </svg>
    //    </div>
    //`;
    //                },


    //                name: 'totalscore',

    //            },

    //        ],
    //    });


    //    //
      
       
    //    var dataTable = _$EducationOtherTestScoreTable.DataTable({
    //        paging: true,
    //        serverSide: true,
    //        processing: true,
    //        listAction: {
    //            ajaxFunction: _otherTestScoresService.getAll,
    //            inputFilter: function () {
    //                return {
    //                    filter: $('#SubjectsTableFilter').val(),
    //                    abbrivationFilter: $('#AbbrivationFilterId').val(),
    //                    nameFilter: $('#NameFilterId').val(),
    //                    subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
    //                };
    //            },
    //        },
    //        columnDefs: [
    //            {
    //                className: ' responsive',
    //                orderable: false,
    //                render: function () {
    //                    return '';
    //                },
    //                targets: 0,
    //            },
            
    //            {
    //                targets: 1,
    //                render: function (data, type, row, meta) {
    //                    if (meta.row === 0) {
    //                        return '<p style="font-weight:bold;font-size:12px">OverAllScore</p>';
    //                    }
    //                }
    //            },
    //            {
    //                targets: 2,
    //                data: 'otherTestScore.listenting',
    //                name: 'listenting',
    //            },
    //            {
    //                targets: 3,
    //                data: 'otherTestScore.reading',
    //                name: 'reading',
    //            },
    //            {
    //                targets: 4,
    //                data: 'otherTestScore.writing',
    //                name: 'writing',
    //            },
    //            {
    //                targets: 5,
    //                data: 'otherTestScore.speaking',
    //                name: 'speaking',
    //            },

    //            {
    //                width: 100,
    //                targets: 6,
    //                data: null,
    //                orderable: false,
    //                autowidth: false,
    //                defaultcontent: '',
    //                // assuming 'row' contains the client data with properties 'firstname', 'lastname', and 'email'
    //                render: function (data, type, row) {

    //                    console.log(data.englisTestScore.totalScore);
    //                    return `
    //    <div class="d-flex align-items-center">
    //        <svg height="60" width="60"> <!-- increase height and width of svg -->
    //            <circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" /> <!-- increase the value of 'r' -->
    //            <text x="30" y="35" text-anchor="middle" fill="white">${data.englisTestScore.totalScore}</text>
    //        </svg>
    //    </div>
    //`;
    //                },


    //                name: 'totalscore',

    //            },
    //        ],
    //    });
        function getSubjects() {
            dataTable.ajax.reload();
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

        $('#AddEducationBackgroundButton').click(function () {
            _createOrEditModal.open();
        });
        //$('#AddEnglishTestScoreButton').click(function () {
        //    debugger;

        //    // Assuming receivedId is a globally declared variable
        //    if (receivedId && parseInt(receivedId) > 0) {
        //        _createOrEditEnglishScoreModal.open({ id: receivedId });

        //    } else {
        //        // Prompt the user to provide an ID or handle it according to your application logic.
        //        _createOrEditEnglishScoreModal.open();

        //    }
        //    getagreementsreload(dynamicValue);
        //});


        //$('#AddEnglishTestScoreButton').click(function () {
        //   debugger
        //    _createOrEditEnglishScoreModal.open([id]);
        //});
        $('#AddOtherTestScoreButton').click(function () {
            _createOrEditOtherScoreModal.open();
        });

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

        abp.event.on('app.createOrEditEducationModalSaved', function () {
           // getSubjects();
            location.reload();
        });
      


        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getSubjects();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getSubjects();
            }
        });

        $('.reload-on-change').change(function (e) {
            getSubjects();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getSubjects();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getSubjects();
        });
        $(document).on('click', '.ellipsis51', function (e) {
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
        $(document).on('click', 'a[data-action51]', function (e) {
            e.preventDefault();
            debugger
            var rowId = $(this).data('id');
            var action = $(this).data('action51');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'view') {
                //_viewMasterCategoryModal.open({ id: rowId });
                window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;
            } else if (action === 'edit') {
                if (rowId == 0) {
                    _createOrEditenglishscoreModal.open()
                }
                else {
                    _createOrEditenglishscoreModal.open()

                    /*  getRecordsById();*/
                }
            } else if (action === 'delete') {

                deleteotherinfo(rowId);
            }
        });
        $(document).on('click', '.ellipsis71', function (e) {
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
        $(document).on('click', 'a[data-action71]', function (e) {
            e.preventDefault();
            debugger
            var rowId = $(this).data('id');
            var action = $(this).data('action71');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'view') {
                //_viewMasterCategoryModal.open({ id: rowId });
                window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;
            } else if (action === 'edit') {
                if (rowId == 0) {
                    _createOrEditOtherscoreModal.open()
                }
                else {
                    _createOrEditOtherscoreModal.open()

                    /*  getRecordsById();*/
                }
            } else if (action === 'delete') {

                deleteotherinfo(rowId);
            }
        });
    });
})(jQuery);
