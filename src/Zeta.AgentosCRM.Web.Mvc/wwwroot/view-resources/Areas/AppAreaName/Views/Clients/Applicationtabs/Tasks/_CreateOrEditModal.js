﻿(function ($) {
    app.modals.CreateOrEditTaskModal = function () {
        $('#taskCategoryId').select2({

            width: '100%',
            dropdownParent: $('#taskCategoryId').parent(),
            // Adjust the width as needed
        });
        $('#assigneeId').select2({
            width: '100%',
            dropdownParent: $('#assigneeId').parent(),
            // Adjust the width as needed
        });
        $('#taskPriorityId').select2({
            width: '100%',
            dropdownParent: $('#taskPriorityId').parent(),
            // Adjust the width as needed
        });
        $('#followerId').select2({
            multiple: true,
            width: '100%',
            placeholder: 'Select Follower',
            // Adjust the width as needed
        });
        function getCurrentTime() {
            const now = new Date();
            const hours = now.getHours().toString().padStart(2, '0');
            const minutes = now.getMinutes().toString().padStart(2, '0');
            return `${hours}:${minutes}`;
        }

        // Set the current time in the StartTime field
        $(document).ready(function () {
            if ($('input[name="id"]').val() < 1 || $('input[name="id"]').val() == undefined) {
                const startTimeField = $("#DueTime");
                if (startTimeField.length) {
                    startTimeField.val(getCurrentTime());
                }
            }
        });
        $.ajax({
            url: abp.appPath + 'api/services/app/TaskFollowers/GetAllUserForTableDropdown',
            method: 'GET',
            dataType: 'json',
            //data: {
            //    PartnerIdFilter: dynamicValue,
            //},
            success: function (data) {

                // Populate the dropdown with the fetched data
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
        function populateDropdown(data) {
            debugger
            var dropdown = $('#followerId');

            dropdown.empty();

            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }
        var idValue = 0;
        var idElements = document.getElementsByName("id");

        if (idElements.length > 0) {
            // Check if at least one element with the name "id" is found
            var idElement = idElements[0];

            if (idElement.value !== undefined) {
                // Check if the value property is defined
                idValue = idElement.value;
            } else {
                console.error("Element with name 'id' does not have a value attribute.");
            }
        } else {
            console.error("Element with name 'id' not found.");
        }
        if (idValue > 0) {


            $.ajax({
                url: abp.appPath + 'api/services/app/CRMTasks/GetCRMTaskForEdit?id=' + idValue,
                method: 'GET',
                dataType: 'json',
                success: function (data) {
                    debugger
                    // Populate the dropdown with the fetched data
                    updateProductDropdown(data);
                },
                error: function (error) {
                    console.error('Error fetching data:', error);
                }
            });
        }
        function updateProductDropdown(data) {
            debugger;
            var ms_val = 0;

            // Assuming data.result.promotionproduct is an array of objects with OwnerID property..
            $.each(data.result.taskFollower, function (index, obj) {
                ms_val += "," + obj.userId;

            });

            //var ms_array = ms_val.length > 0 ? ms_val.substring(1).split(',') : [];
            var ms_array = ms_val.split(',');
            var $productId = $("#followerId");


            $productId.val(ms_array).trigger('change');

        }

        var _cRMTasksService = abp.services.app.cRMTasks;

        var _modalManager;
        var _$tasksInformationForm = null;



        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });
            var hiddenfield = $('input[name="Clientid"]').val();

            $("#clientId").val(hiddenfield);
            _$tasksInformationForm = _modalManager.getModal().find('form[name=TaskApplicationInformationsForm]');
            _$tasksInformationForm.validate();
        };


        $(document).on('select2:open', function () {
            var $searchField = $('.select2-search__field');
            $searchField.on('keydown', function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        });
        $(document).off("click", "#saveTaskBtn").on("click", "#saveTaskBtn", function (e) {
            debugger

            var hiddenfield = $('input[name="Clientid"]').val();
            var hiddenapplicationfield = $("#ApplicationId").val()
            var srno = $('.nav-link.default.active .num').text().trim();
            var workflowStepId = $("#StageID-" + srno).val();

            $("#clientId").val(hiddenfield);
            $("#applicationId").val(hiddenapplicationfield);
            $("#ApplicationStageId").val(workflowStepId);


            var datarows = [];
            var datarowsList = $("#followerId :selected").map(function (i, el) {
                debugger
                return $(el).val();
            }).get();
            $.each(datarowsList, function (index, value) {
                var datarowsItem = {
                    UserId: datarowsList[index]
                }
                datarows.push(datarowsItem);
            });
            var Steps = JSON.stringify(datarows);

            Steps = JSON.parse(Steps);

            var Subject = _$tasksInformationForm.serializeFormToObject();
            Subject.Steps = Steps;
            _modalManager.setBusy(true);
            _cRMTasksService
                .createOrEdit(Subject)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    //abp.event.trigger('app.createOrEditTaskModalSaved');
                    location.reload();
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });

        });
        $(document).off("click", "#closeTaskBtn").on("click", "#closeTaskBtn", function (e) {
            _modalManager.close();
        });
    };
})(jQuery);