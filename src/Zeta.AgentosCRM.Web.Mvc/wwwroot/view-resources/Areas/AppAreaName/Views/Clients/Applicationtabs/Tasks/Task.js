(function () {
    $(function () {
        var _cRMTasksService = abp.services.app.cRMTasks;
        var hiddenfield = $('input[name="Clientid"]').val();
        var dynamicValue = hiddenfield;

    

        $(document).off("change", "#reminderCheckboxApplication").on("change", "#reminderCheckboxApplication", function (e) {
             
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
                var id = card.find('.taskid').val();
                var taskCategoryId = card.find('.categoriesId').val();
                var assigneeId = card.find('.assigneId').val();
                var taskPriorityId = card.find('.taskPrioritysId').val();
                var dueDate = card.find('.dueDates').val();
                var dueTime = card.find('.dueTimes').val();
                var description = card.find('.taskdescription').val();
                var title = card.find('.tasktittle').text();
                var clientId = $('input[name="Clientid"]').val();
                var applicationId = $("#ApplicationId").val()
                var srno = $('.nav-link.default.active .num').text().trim();
                var applicationStageId = $("#StageID-" + srno).val();
                var isCompleted = true;
                var inputData = {
                    clientId: clientId,
                    taskCategoryId: taskCategoryId,
                    assigneeId: assigneeId,
                    taskPriorityId: taskPriorityId,
                    dueDate: dueDate,
                    dueTime: dueTime,
                    description: description,
                    title: title,
                    isCompleted: isCompleted,
                    id: id,
                    applicationId: applicationId,
                    applicationStageId: applicationStageId

                };
                var Steps = JSON.stringify(inputData);
                Steps = JSON.parse(Steps);
                _cRMTasksService
                    .createOrEdit(Steps)

            } else {
                replaceElement.text('Todo').css({
                    'color': 'red',
                    'font-weight': 'bold'
                });
                replaceElementdate.removeClass('badge-success').addClass('badge-danger').css('color', 'white');
                var id = card.find('.taskid').val();
                var taskCategoryId = card.find('.categoriesId').val();
                var assigneeId = card.find('.assigneId').val();
                var taskPriorityId = card.find('.taskPrioritysId').val();
                var dueDate = card.find('.dueDates').val();
                var dueTime = card.find('.dueTimes').val();
                var description = card.find('.taskdescription').val();
                var title = card.find('.tasktittle').text();
                var clientId = $('input[name="Clientid"]').val();
                var applicationId = $("#ApplicationId").val()
                var srno = $('.nav-link.default.active .num').text().trim();
                var applicationStageId = $("#StageID-" + srno).val();
                var isCompleted = false;
                var inputData = {
                    clientId: clientId,
                    taskCategoryId: taskCategoryId,
                    assigneeId: assigneeId,
                    taskPriorityId: taskPriorityId,
                    dueDate: dueDate,
                    dueTime: dueTime,
                    description: description,
                    title: title,
                    isCompleted: isCompleted,
                    id: id,
                    applicationId: applicationId,
                    applicationStageId: applicationStageId

                };
                var Steps = JSON.stringify(inputData);
                Steps = JSON.parse(Steps);
                _cRMTasksService
                    .createOrEdit(Steps)
            }

        });









        function clearMainDiv() {
            // Assuming main div has an id 'mainDiv', replace it with your actual id if needed...
            $('.maindivcard').remove();

        }






      
        //_cRMTasksService.getAll().done(function (data) {
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
                getapplicationtasks();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getapplicationtasks();
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
                getapplicationtasks();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getapplicationtasks();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditTaskApplicationModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditApplicationTasksModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Applicationtabs/Tasks/_CreateOrEditModal.js',
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


        function getapplicationtasks() {
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
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                            location.reload();
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

  

        $(document).off("click", ".BtnNewTask").on("click", ".BtnNewTask", function () {
             
            _createOrEditTaskApplicationModal.open();
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
            getapplicationtasks();
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            getapplicationtasks();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getapplicationtasks();
            }
        });

        $('.reload-on-change').change(function (e) {
            getapplicationtasks();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getapplicationtasks();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getapplicationtasks();
        });
        //Add a click event handler for the ellipsis icons
        $(document).off("click", ".ellipsis151").on("click", ".ellipsis151", function (e) {
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
       $(document).off("click", "a[data-action151]").on("click", "a[data-action151]", function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action151');
             
            // Handle the selected action based on the rowId
            if (action === 'view') {
                //_viewMasterCategoryModal.open({ id: rowId });
                window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;
            } else if (action === 'edit') {
                //window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;
                _createOrEditTaskApplicationModal.open({ id: rowId });
            } else if (action === 'delete') {

                deletetasks(rowId);
            }
        });
    });
})(jQuery);
