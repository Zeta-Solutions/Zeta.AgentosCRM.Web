(function () {
    $(function () {
     
        var hiddenfield = $("#ProductId").val();
        var dynamicValue = hiddenfield;

        getproductrequirmentssreload(dynamicValue);
        getproductEnglishreload(dynamicValue);
        getproductOtherscoresreload(dynamicValue);
        var globalData; // Declare the data variable in a broader scope

        function createCardTask(item) {
            debugger
            var productAcadamicRequirement = item.productAcadamicRequirement || { id: 0 };
            //var cardId = 'card_' + productOtherInformation.id;
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
            var titleRowDiv = $('<div>').addClass('row');
            var titleColDiv = $('<div>').addClass('col-md-12'); // Adjust the column size as needed

            //var cardTitle = $('<h5>').addClass('card-title col-md-12');

        
            var cardTitle = $('<h5>').addClass('card-title');

            cardTitle.html("Academic Requirements" +
                '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                '<div class="ellipsis5"><a href="#" data-id="' + productAcadamicRequirement.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                /*   '<a href="#" style="color: black;" data-action="view" data-id="' + branch.id + '"><li>View</li></a>' +*/
                '<a href="#" style="color: black;" data-action5="edit" data-id="' + productAcadamicRequirement.id + '"><li>Edit</li></a>' +
                "<a href='#' style='color: black;' data-action5='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
                '</ul>' +
                '</div>' +
                '</div><hr/>');
            // Append both parts to cardTitle
            var infoColDiv = $('<div>').addClass('row');
            if (productAcadamicRequirement.id!=0 && Object.keys(productAcadamicRequirement).length > 0) {
                var infoParagraph1 = $('<p>').addClass('card-text col-md-4');
                var infoParagraph2 = $('<p>').addClass('card-text col-md-4');
                var infoParagraph3 = $('<p>').addClass('card-text col-md-3');

              
                infoParagraph2.html('<strong>AcadamicScore:</strong>' + '&nbsp;&nbsp;' + productAcadamicRequirement.acadamicScore);
                infoParagraph1.html('<strong>DegreeLevel:</strong>' + '&nbsp;&nbsp;' + item.degreeLevelName);

                titleColDiv.append(cardTitle);
                titleRowDiv.append(titleColDiv);
                infoColDiv.append(infoParagraph1, infoParagraph2);
            }
            else if (productAcadamicRequirement !== undefined) {
                debugger
                var infoParagraph1 = $('<p>').addClass('card-text col-md-4');

                infoParagraph1.html('<strong>No Data:</strong>');
                titleColDiv.append(cardTitle);
                titleRowDiv.append(titleColDiv);
                infoColDiv.append(infoParagraph1);
            }

            cardBodyDiv.append(titleRowDiv, infoColDiv);

            cardDiv.append(cardBodyDiv);
            //colDiv.append(cardDiv);
            //mainDiv.addClass(cardId);
            // Append the card container to the mainDiv
            mainDiv.append(cardDiv);

            return mainDiv; // Return the created card
        }
        function createEnglishCardTask(item) {
            debugger;
        
        var productEnglishRequirements = item || [{ productEnglishRequirement: { id: 0 } }];

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

            cardTitle.html("English Test Score" +
                '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
                '<div class="ellipsis51"><a href="#" data-id="' + (productEnglishRequirements.length > 0 ? productEnglishRequirements[0].productEnglishRequirement.id : 1) + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                '<a href="#" style="color: black;" data-action51="edit" data-id="' + (productEnglishRequirements.length > 0 ? productEnglishRequirements[0].productEnglishRequirement.id : 1) + '"><li>Edit</li></a>' +             
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
            for (var i = 0; i < productEnglishRequirements.length; i++) {
                var currentProduct = productEnglishRequirements[i];

                var infoParagraphs = [
                    $('<p>').addClass('card-text col-md-2').html('<strong>' + currentProduct.productEnglishRequirement.name + ':' + '</strong>' + '&nbsp;&nbsp;' +
                        '<input type="hidden" name="Id' + [i] + '" value="' + currentProduct.productEnglishRequirement.id + '"/>')
                        .appendTo(infoColheadingDiv),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.productEnglishRequirement.listening ? currentProduct.productEnglishRequirement.listening : '-')),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.productEnglishRequirement.reading ? currentProduct.productEnglishRequirement.reading : '-')),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.productEnglishRequirement.writing ? currentProduct.productEnglishRequirement.writing : '-')),
                    $('<p>').addClass('card-text col-md-2').html((currentProduct.productEnglishRequirement.speaking ? currentProduct.productEnglishRequirement.speaking : '-')),
                    $('<p>').addClass('card-text col-md-2').html(
                        '<svg height="70" width="70">' +
                        '<circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" />' +
                        '<text x="30" y="35" text-anchor="middle" fill="white">' +
                        (currentProduct.productEnglishRequirement.totalScore ? currentProduct.productEnglishRequirement.totalScore : '-') +
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


        //function createEnglishCardTask(item) {
        //    debugger;
        //    var productEnglishRequirements = item; /*item[0].productEnglishRequirement || [];*/

        //    var mainDiv = $('<div>').addClass('maincard maindivcard').css({
        //        'margin-left': '0.2px',
        //        'margin-bottom': '20px' // Add margin between cards
        //    });

           

        //        var cardDiv = $('<div>').addClass('card').css({
        //            'padding': '5px'
        //        });

        //        var cardBodyDiv = $('<div>').addClass('card-body').css({
        //            'padding': '5px'
        //        });

        //        var titleRowDiv = $('<div>').addClass('row');
        //        var titleColDiv = $('<div>').addClass('col-md-12');

        //        var cardTitle = $('<h5>').addClass('card-title');

        //        cardTitle.html("English Test Score" +
        //            '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
        //            '<div class="ellipsis51"><a href="#" data-id="' + currentProduct.id + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
        //            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
        //            '<ul style="list-style: none; padding: 0;color:black">' +
        //            '<a href="#" style="color: black;" data-action51="edit" data-id="' + currentProduct.id + '"><li>Edit</li></a>' +
        //            "<a href='#' style='color: black;' data-action51='delete' data-id='" + JSON.stringify(item) + "'><li>Delete</li></a>" +
        //            '</ul>' +
        //            '</div>' +
        //            '</div><hr/>');

        //        var infoColheadingDiv = $('<div>').addClass('row');
        //        var infoColDiv = $('<div>').addClass('row');
        //    for (var i = 0; i < productEnglishRequirements.length; i++) {
        //        var currentProduct = productEnglishRequirements[i];

        //        // Create card components
        //        var cardTitle = $('<h5>').addClass('card-title');

        //        cardTitle.html("English Test Score" +
        //            '<div class="context-menu" style="position:relative; display: inline-block; float: right;">' +
        //// ... (rest of your cardTitle HTML)

        //         var headinginfo1 = $('<p>').addClass('card-text col-md-2');
        //        var headinginfo2 = $('<p>').addClass('card-text col-md-2');
        //        var headinginfo3 = $('<p>').addClass('card-text col-md-2');
        //        var headinginfo4 = $('<p>').addClass('card-text col-md-2');
        //        var headinginfo5 = $('<p>').addClass('card-text col-md-2');
        //        var headinginfo6 = $('<p>').addClass('card-text col-md-2');

        //        headinginfo1.html('&nbsp;&nbsp;');
        //        headinginfo2.html('<strong>Listening:</strong>');
        //        headinginfo3.html('<strong>Reading:</strong>');
        //        headinginfo4.html('<strong>Writing:</strong>');
        //        headinginfo5.html('<strong>Speaking:</strong>');
        //        headinginfo6.html('<strong>OverAllScore:</strong>');

        //        var infoParagraph1 = $('<p>').html('<strong>' + currentProduct.productEnglishRequirement.name + ':' + '</strong>' + '&nbsp;&nbsp;');
        //        var infoParagraph2 = $('<p>').html((currentProduct.productEnglishRequirement.listening ? currentProduct.productEnglishRequirement.listening : '-'));
        //        var infoParagraph3 = $('<p>').html((currentProduct.productEnglishRequirement.reading ? currentProduct.productEnglishRequirement.reading : '-'));
        //        var infoParagraph4 = $('<p>').html((currentProduct.productEnglishRequirement.writing ? currentProduct.productEnglishRequirement.writing : '-'));
        //        var infoParagraph5 = $('<p>').html((currentProduct.productEnglishRequirement.speaking ? currentProduct.productEnglishRequirement.speaking : '-'));
        //        var infoParagraph6 = $('<p>').html((currentProduct.productEnglishRequirement.listening ? currentProduct.productEnglishRequirement.totalScore : '-'));

        //        // Create card components container
        //        var titleRowDiv = $('<div>').addClass('row');
        //        var titleColDiv = $('<div>').addClass('col-md-12');
        //        var infoColheadingDiv = $('<div>').addClass('row');
        //        var infoColDiv = $('<div>').addClass('row');

        //        // Append components to containers
        //        titleColDiv.append(cardTitle);
        //        titleRowDiv.append(titleColDiv);
        //        infoColDiv.append(infoParagraph1, infoParagraph2, infoParagraph3, infoParagraph4, infoParagraph5, infoParagraph6);
        //        infoColheadingDiv.append(headinginfo1, headinginfo2, headinginfo3, headinginfo4, headinginfo5, headinginfo6);

        //        // Append card components container to the mainDiv
        //        cardBodyDiv.append(titleRowDiv, infoColheadingDiv, infoColDiv);

        //        // Create a new card container
        //        var cardDiv = $('<div>').addClass('card').css({
        //            'padding': '5px'
        //        });

        //        // Append the card container to the mainDiv
        //        cardDiv.append(cardBodyDiv);
        //        mainDiv.append(cardDiv);
        //    }
        //    return mainDiv; // Return the created card
        //}

        function createProductothercard(item) {
            debugger;

            var productOtherTestRequirements = item || [{ productOtherTestRequirement: { id: 0, name: '' } }];

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
                '<div class="ellipsis71"><a href="#" data-id="' + (productOtherTestRequirements.length > 0 ? productOtherTestRequirements[0].productOtherTestRequirement.id : 1) + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 0;border: 1px solid #ccc; border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                '<ul style="list-style: none; padding: 0;color:black">' +
                '<a href="#" style="color: black;" data-action71="edit" data-id="' + (productOtherTestRequirements.length > 0 ? productOtherTestRequirements[0].productOtherTestRequirement.id : 1) + '"><li>Edit</li></a>' +           
                '</ul>' +
                '</div>' +
                '</div><hr/>');


            var infoColheadingDiv = $('<div>').addClass('row');
            var infoColDiv = $('<div>').addClass('row');

            // Create headings
            var headings = ['&nbsp;&nbsp;', '<strong>SAT I:</strong>', '<strong>SAT II:</strong>', '<strong>GRE:</strong>', '<strong>GMAT:</strong>'];
            var dynamicHeadings = [];
            for (var i = 0; i < productOtherTestRequirements.length; i++) {
                var testName = productOtherTestRequirements[i].productOtherTestRequirement.name;
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
            for (var i = 0; i < productOtherTestRequirements.length; i++) {
                var currentProduct = productOtherTestRequirements[i];
               
                var paragraph = $('<p>').addClass('card-text col-md-2').html(
                    '<svg height="70" width="70">' +
                    '<circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" />' +
                    '<text x="30" y="35" text-anchor="middle" fill="white">' +
                    (currentProduct.productOtherTestRequirement.totalScore ? currentProduct.productOtherTestRequirement.totalScore : '-') +
                    '</text>' +
                    '</svg>'
                ).append(
                    $('<input>').attr('type', 'hidden').attr('name', 'Idother' + [i]).val(currentProduct.productOtherTestRequirement.id)
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



        function getproductOtherscoresreload(dynamicValue) {


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/ProductOtherTestRequirements/GetAll',
                data: {
                    ProductIdFilter: dynamicValue,
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


        function getproductrequirmentssreload(dynamicValue) {


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/ProductAcadamicRequirements/GetAll',
                data: {
                    ProductIdFilter: dynamicValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    console.log('Response from server:', data);
                    globalData = data; // Assign data to the global variable
                    processData(data); // Call processData function after data is available
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function getproductEnglishreload(dynamicValue) {


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/ProductEnglishRequirements/GetAll',
                data: {
                    ProductIdFilter: dynamicValue,
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
        function processData(data) {
            debugger
            var cardContainer = $('#cardProductRequirmentsContainer'); // or replace '#container' with your actual container selector

            // Check if globalData.result.items is defined and is an array with elements
            if (Array.isArray(globalData.result.items) && globalData.result.items.length > 0) {
                // Iterate through items and create cards
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
                debugger
                var defaultItem = {}; // You can provide some default values or an empty object
                var defaultCard = createCardTask(defaultItem);

                var defaultColDiv = $('<div>').addClass('col-md-12');
                defaultColDiv.append(defaultCard);

                var defaultRowDiv = $('<div>').addClass('row mt-3');
                defaultRowDiv.append(defaultColDiv);

                cardContainer.append(defaultRowDiv);
            }
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
            }else {
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
        var _productOtherInformationsService = abp.services.app.productOtherInformations;
        var _productEnglishRequirements = abp.services.app.productEnglishRequirements;

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };
        //_productOtherInformationsService.getAll().done(function (data) {
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
                getrequirement();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getrequirement();
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
                getrequirement();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getrequirement();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Products/CreateOrEditAcadamicRequirementModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Products/ProductRequirment/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditAcadamicRequirementModal',
        });
        var _createOrEditenglishscoreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Products/CreateOrEditproductenglishscoreModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Products/ProductRequirment/_CreateOrEditEnglishscoreModal.js',
            modalClass: 'CreateOrEditEnglishscoreModal',
        });
        var _createOrEditOtherscoreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Products/CreateOrEditproductotherscoreModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Products/ProductRequirment/_CreateOrEditOtherscoreModal.js',
            modalClass: 'CreateOrEditOtherscoreModal',
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
                                    debugger;
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

        function getrequirement() {
            //branchesAjax.reload();
            clearMainDiv();
            getproductrequirmentssreload(dynamicValue);
            getproductEnglishreload(dynamicValue);
        }

        function deleteotherinfo(productOtherInformation) {
            debugger

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _productOtherInformationsService
                        .delete({
                            id: productOtherInformation.productOtherInformation.id,
                        })
                        .done(function () {
                            getrequirement(true);
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

        $('#CreateNewproductAcadamicRequirementButton').click(function () {
            debugger
            if(receivedId && parseInt(receivedId) > 0) {
                _createOrEditModal.open({ id: receivedId });

            } else {
                // Prompt the user to provide an ID or handle it according to your application logic.
                _createOrEditModal.open();

            }


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

        abp.event.on('app.createOrEditAcadamicRequirementModalSaved', function () {
            //getrequirement();
            location.reload();
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            getrequirement();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getrequirement();
            }
        });

        $('.reload-on-change').change(function (e) {
            getrequirement();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getrequirement();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getrequirement();
        });
        //Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis5', function (e) {
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
        $(document).on('click', 'a[data-action5]', function (e) {
            e.preventDefault();
            debugger
            var rowId = $(this).data('id');
            var action = $(this).data('action5');
            debugger
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
                    //window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;
                    //_createOrEditenglishscoreModal.open({ id: rowId });
                    //_createOrEditenglishscoreModal.open({ id: rowId });
                    //row1 = $('.card-text.col-md-2 input[name="Id0"]').val();
                    //row2 = $('.card-text.col-md-2 input[name="Id1"]').val();
                    //row3 = $('.card-text.col-md-2 input[name="Id2"]').val();
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
                    //window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;
                    //_createOrEditenglishscoreModal.open({ id: rowId });
                    //_createOrEditenglishscoreModal.open({ id: rowId });
                    //row1 = $('.card-text.col-md-2 input[name="Id0"]').val();
                    //row2 = $('.card-text.col-md-2 input[name="Id1"]').val();
                    //row3 = $('.card-text.col-md-2 input[name="Id2"]').val();
                    _createOrEditOtherscoreModal.open()

                    /*  getRecordsById();*/
                }
            } else if (action === 'delete') {

                deleteotherinfo(rowId);
            }
        });

    });
})();
