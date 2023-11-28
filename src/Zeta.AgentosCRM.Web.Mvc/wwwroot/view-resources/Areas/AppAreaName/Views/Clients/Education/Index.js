(function () {
    
    $(function () {
        debugger
        var _$EducationEnglishTestScoreTable = $('#EnglishTestScoretable');
        var _englisTestScoresService = abp.services.app.englisTestScores;

        var _$EducationOtherTestScoreTable = $('#OtherTestScoretable');
        var _otherTestScoresService = abp.services.app.otherTestScores;

        var hiddenfield = $("#clientId").val();
        var dynamicValue = hiddenfield;
        //CArd start
        var receivedId = 0;
        getnotesreload(dynamicValue);
        //var globalData; // Declare the data variable in a broader scope

        ////For Id
        function getnotesreload(dynamicValue) {
            debugger


            var branchesAjax = $.ajax({
                url: abp.appPath + 'api/services/app/EnglisTestScores/GetAll',
                data: {
                    PartnerIdFilter: dynamicValue,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    debugger
                    receivedId =data.result.items[0].englisTestScore.id
                    console.log('Response from server:', data);
                    globalData = data; // Assign data to the global variable
                    processssssssssData(data); // Call processData function after data is available
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
    

        //


        function getnotesreload(dynamicValue) {
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


// Adjust this according to the format you desire
                       
                        var rowId = item.clientEducation.id;
                        var CardDiv = '<div class="row"><div class="col-lg-6"><span><span style="font-weight:bold;">' + item.clientEducation.degreeTitle + '</span><br>' + item.clientEducation.institution + '<span></div>'
                            + '<div class="col-lg-5"><span><span style="background-color:lightgray;border-radius: 5px; padding: 0 5px;""> ' + formattedStartDateEnglish + ' - ' + formattedEndDateEnglish + ' </span> <br> <span style="color:blue;font-weight:bold;"><span>Score: </span> ' + item.clientEducation.acadmicScore + ' GPA' + '</span><br><span>' + item.clientEducation.degreeTitle +' >> ' + item.subjectAreaName + ' >>' + item.subjectName+'</span>' + '</span></div>' +
                            '<div class="col-lg-1">' +
                            '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" Education-data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            '<a href="#" style="color: black;" Education-data-action="delete" data-id="' + rowId  + '"><li>Delete</li></a>' +
                            '</ul>' +
                            '</div>' +
                            '</div>' +'</div></div><br><br>';
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
                
                deleteMasterCategory(rowId);
            }
        });

        function deleteMasterCategory(rowId) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientEducationsService
                        .delete({
                            id: rowId,
                        })
                        .done(function () {
                            //getSubjectAreas(true);
                            debugger
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
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
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEducationModal',
        });
        var _createOrEditEnglishScoreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditEnglishScoreModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditEnglishModal.js',
            modalClass: 'CreateOrEditEnglishTestScoreModal',
        });
        var _createOrEditOtherScoreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditOtherScoreModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditOtherScoreModal',
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


        var dataTable = _$EducationEnglishTestScoreTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _englisTestScoresService.getAll,
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
                    targets: 1,
                    render: function (data, type, row, meta) {
                        if (meta.row === 0) {
                            return '<p style="font-weight:bold;font-size:12px">toefl</p>';
                        } else if (meta.row === 1) {
                            return '<p style="font-weight:bold;font-size:12px">ielts</p>';
                        } else if (meta.row === 2) {
                            return '<p style="font-weight:bold;font-size:12px">pte</p>';
                        }
                    }
                },
                {
                    targets: 2,
                    data: 'englisTestScore.listenting',
                    name: 'listenting',
                },
                {
                    targets: 3,
                    data: 'englisTestScore.reading',
                    name: 'reading',
                },
                {
                    targets: 4,
                    data: 'englisTestScore.writing',
                    name: 'writing',
                },
                {
                    targets: 5,
                    data: 'englisTestScore.speaking',
                    name: 'speaking',
                },

                {
                    width: 100,
                    targets: 6,
                    data: null,
                    orderable: false,
                    autowidth: false,
                    defaultcontent: '',
                    // assuming 'row' contains the client data with properties 'firstname', 'lastname', and 'email'
                    render: function (data, type, row) {

                        console.log(data.englisTestScore.totalScore);
                        return `
        <div class="d-flex align-items-center">
            <svg height="60" width="60"> <!-- increase height and width of svg -->
                <circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" /> <!-- increase the value of 'r' -->
                <text x="30" y="35" text-anchor="middle" fill="white">${data.englisTestScore.totalScore}</text>
            </svg>
        </div>
    `;
                    },


                    name: 'totalscore',

                },

            ],
        });


        //
      
       
        var dataTable = _$EducationOtherTestScoreTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _otherTestScoresService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SubjectsTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
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
                    render: function (data, type, row, meta) {
                        if (meta.row === 0) {
                            return '<p style="font-weight:bold;font-size:12px">OverAllScore</p>';
                        }
                    }
                },
                {
                    targets: 2,
                    data: 'otherTestScore.listenting',
                    name: 'listenting',
                },
                {
                    targets: 3,
                    data: 'otherTestScore.reading',
                    name: 'reading',
                },
                {
                    targets: 4,
                    data: 'otherTestScore.writing',
                    name: 'writing',
                },
                {
                    targets: 5,
                    data: 'otherTestScore.speaking',
                    name: 'speaking',
                },

                {
                    width: 100,
                    targets: 6,
                    data: null,
                    orderable: false,
                    autowidth: false,
                    defaultcontent: '',
                    // assuming 'row' contains the client data with properties 'firstname', 'lastname', and 'email'
                    render: function (data, type, row) {

                        console.log(data.englisTestScore.totalScore);
                        return `
        <div class="d-flex align-items-center">
            <svg height="60" width="60"> <!-- increase height and width of svg -->
                <circle cx="30" cy="30" r="20" stroke="#009ef7" stroke-width="2" fill="#009ef7" /> <!-- increase the value of 'r' -->
                <text x="30" y="35" text-anchor="middle" fill="white">${data.englisTestScore.totalScore}</text>
            </svg>
        </div>
    `;
                    },


                    name: 'totalscore',

                },
            ],
        });
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
        $('#AddEnglishTestScoreButton').click(function () {
            debugger;

            // Assuming receivedId is a globally declared variable
            if (receivedId && parseInt(receivedId) > 0) {
                _createOrEditEnglishScoreModal.open({ id: receivedId });

            } else {
                // Prompt the user to provide an ID or handle it according to your application logic.
                _createOrEditEnglishScoreModal.open();

            }
            getagreementsreload(dynamicValue);
        });
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

        abp.event.on('app.createOrEditPartnerTypeModalSaved', function () {
            getSubjects();
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
    });
})(jQuery);
