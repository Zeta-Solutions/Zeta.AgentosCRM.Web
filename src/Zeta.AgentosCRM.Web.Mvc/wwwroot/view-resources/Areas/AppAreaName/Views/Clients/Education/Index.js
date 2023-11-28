(function () {
    $(function () {
        var _$EducationEnglishTestScoreTable = $('#EnglishTestScoretable');
        var _$EducationOtherTestScoreTable = $('#OtherTestScoretable');
        var _clientEducationsService = abp.services.app.clientEducations;

        var hiddenfield = $("#clientId").val();
        var dynamicValue = hiddenfield;
        //CArd start
        getnotesreload(dynamicValue);
        var globalData; // Declare the data variable in a broader scope
         



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
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEnglishScoreModal',
        });
        var _createOrEditOtherScoreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditOtherScoreModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Education/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditOtherScoreModal',
        });
        var _createOrEditModalEmail = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/ClientEmailCompose',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Client/ApplicationClient/_CreateOrEditModal.js',
            modalClass: 'ClientEmailCompose',
        });
        var _viewSubjectModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ApplicationClient/ViewApplicationModal',
            modalClass: 'ViewApplicationModal',
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
        //DataTable For English Score
        var dataTable = _$EducationEnglishTestScoreTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientEducationsService.getAll,
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
                //{
                //    targets: 1, // The column index (zero-based) where you want to add the "View" button
                //    data: 'subject.abbrivation',
                //    name: 'abbrivation',
                //    render: function (data, type, row) {
                //        return '<a href="' + abp.appPath + 'AppAreaName/Client/ClientDetail/' + row.subject.id + '" class="btn btn-primary">View</a>';
                //    }
                //},
                {
                    targets: 1,
                    data: 'subject.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 3,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 4,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 5,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 6,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 7,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
            ],
        });

        //DataTable For Other Score
        var dataTable = _$EducationOtherTestScoreTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientEducationsService.getAll,
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
                    data: 'subject.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 3,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 4,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 5,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 6,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
                {
                    targets: 7,
                    data: 'subjectAreaName',
                    name: 'subjectAreaFk.name',
                },
            ],
        });
        function getSubjects() {
            dataTable.ajax.reload();
        }

        function deletePartnerType(subject) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _subjectsService
                        .delete({
                            id: subject.id,
                        })
                        .done(function () {
                            getSubjects(true);
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

        $('#AddEducationBackgroundButton').click(function () {
            _createOrEditModal.open();
        });
        $('#AddEnglishTestScoreButton').click(function () {
            _createOrEditEnglishScoreModal.open();
        });
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
