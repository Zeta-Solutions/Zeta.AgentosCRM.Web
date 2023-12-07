
(function ($) {
    app.modals.CreateOrEditWorkflowModal = function () {
        var _workflowsService = abp.services.app.workflows;
        if ($("#allOffice").attr("checked")) {
            debugger
            
            document.getElementById("showhide").style.display = 'none';
        }
        else {
            debugger
            document.getElementById("showhide").style.display = 'block';
        }
        $('#WorkFlowOfficeId').select2({
            multiple: true,
            width: '650px',
            // Adjust the width as needed
        });

        $.ajax({
            url: abp.appPath + 'api/services/app/Agents/GetAllOrganizationUnitForTableDropdown',
            method: 'GET',
            dataType: 'json',
 
            success: function (data) {
                debugger
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });

        var dataRows = [];  
        function populateDropdown(data) {
            var dropdown = $('#WorkFlowOfficeId');



            dropdown.empty();

            $.each(data.result, function (index, item) {
                if (item && item.id !== null && item.id !== undefined && item.displayName !== null && item.displayName !== undefined) {
                    dropdown.append($('<option></option>').attr('value', item.id).attr('data-id', item.id).text(item.displayName));
                } else {
                    console.warn('Invalid item:', item);
                }
            });
        }


        debugger
        var Workflowsetuptable = $('#Workflowsetuptable').DataTable();
        var _workflowStepsService = abp.services.app.workflowSteps;

        $("#sortable").sortable({
            axis: "y",
            containment: "parent",
            cursor: "move",
            zIndex: 1000,

            update: function (event, ui) {
                updateIndices();
            }
        });

        // Initialize the sortable
        $("#sortable").disableSelection();

        // Function to update indices based on the current order
        function updateIndices() {
            $("#sortable .timeline-item").each(function (index) {
                // Update the index for each item
                $(this).find(".SrlNo-input").text(index);
            });
        }

        // Initially update indices
        updateIndices();
        var _modalManager;
        var _$workflowInformationForm = null;
        var workflowId = $("#WorkflowId").val();
        debugger
        //Fill Drop Down

        if (workflowId > 0) {


            $.ajax({
                url: abp.appPath + 'api/services/app/Workflows/GetWorkflowForEdit?id=' + workflowId,
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
            
            // Assuming data.result.promotionproduct is an array of objects with OwnerID property
            $.each(data.result.workflowOffice, function (index, obj) {
                ms_val += "," + obj.organizationUnitId;

            });
             
            //var ms_array = ms_val.length > 0 ? ms_val.substring(1).split(',') : [];
            var ms_array = ms_val.split(',');
            var $productId = $("#WorkFlowOfficeId");
            $productId.val(ms_array).trigger('change');
        }
        if (workflowId > 0) {
            $.ajax({
                url: abp.appPath + 'api/services/app/WorkflowSteps/GetAll',
                data: {
                    WorkflowIdFilter: workflowId,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {

                    var TotalRecord = data.result.items;

                    $.each(TotalRecord, function (index, item) {
                        debugger
                        var newTimelineItem = `
    <div class="col-12 timeline-item  timeline-itemRowDelete ">
        <div class="timeline-item col-11">
            <div class="timeline-circle"></div>
            <div class="timeline-content">
                <span style="display :none;" class="SrlNo-input" placeholder="Your stage Srno here">${item.workflowStep.srlNo}</span>
                <input type="text" hidden class="abbreviation-input" placeholder="Your stage Abbrivation here" value="${item.workflowStep.abbrivation}">
                <input type="text" class="name-input" placeholder="Your stage name here" value="${item.workflowStep.name}">
                <input type="text" hidden value="`+ item.workflowStep.isPartnerClientIdRequired + `" class="HashTag-inputValue">`

                        if (item.workflowStep.isPartnerClientIdRequired == true) {
                            newTimelineItem += `<span class="hashtag" ><i class="fa fa-hashtag" style="font-size: 20px; color:Blue;" title="Remove Partner Client ID"></i></span>`
                        } else {
                            newTimelineItem += `<span class="hashtag" ><i class="fa fa-hashtag" style="font-size: 20px" title="Required Partner Client ID"></i></span>`
                        }
                        newTimelineItem += `<input type="text" hidden value="` + item.workflowStep.isStartEndDateRequired + `" class="calendar-inputValue">`

                        if (item.workflowStep.isStartEndDateRequired == true) {
                            newTimelineItem += `<span class="calendar" ><i class="fa fa-calendar" style="font-size: 20px; color:Blue;" title="Remove Start and End Date"></i></span>`
                        } else {
                            newTimelineItem += `<span class="calendar"><i class="fa fa-calendar" style="font-size: 20px" title="Add Start and End Date"></i></span>`
                        }
                        newTimelineItem += `
                
                <input type="text" hidden value="`+ item.workflowStep.isNoteRequired + `" class="file-inputValue">`

                        if (item.workflowStep.isNoteRequired == true) {
                            newTimelineItem += `<span class="file" ><i class="fa fa-file-text-o" style="font-size: 20px; color:Blue;" title="Remove Note"></i></span>`
                        } else {
                            newTimelineItem += `
                <span class="file"><i class="fa fa-file-text-o" style="font-size: 20px "title="Add Note"></i></span>`
                        }
                        newTimelineItem += `
                <input type="text" hidden value="`+ item.workflowStep.isApplicationIntakeRequired + `" class="Application-inputValue">`

                        if (item.workflowStep.isApplicationIntakeRequired == true) {
                            newTimelineItem += `<span class="Application"><i class="fa fa-calendar-check-o" style="font-size: 20px; color:Blue;" title="Remove Application intake field"></i></span>`
                        } else {
                            newTimelineItem += ` 
                <span class="Application"><i class="fa fa-calendar-check-o" style="font-size: 20px" title="Add Application intake field"></i></span>`
                        }
                        newTimelineItem += `
                <span class="delete"><i class="fa fa-trash-o" style="font-size: 20px"></i></span>

                 <span class="form-check form-switch activeValue">
                    <input class="form-check-input switchValue" type="checkbox" value="${item.workflowStep.isActive}" role="switch" id="flexSwitchCheckDefault" ${item.workflowStep.isActive ==true ? 'checked' : ''}>
                </span>
                <input type="text" hidden class="WorkfrlowStepId-input" placeholder="Your stage workflowStepId here" value="${item.workflowStep.id}">
                <input type="text" hidden class="WorkflowId-input" placeholder="Your stage workflowId here" value="${workflowId}">
                <input type="text" hidden value="`+ item.workflowStep.isWinStage + `" class="Stage-inputValue"> 
            </div>
        </div>
        <div class="col-1">`
                        debugger
                        if (item.workflowStep.isWinStage == true) {
                            newTimelineItem += `<span  class="btn btn-sm Stage"  data-workflow-id="0"><i class="fa fa-trophy" style="font-size: 20px; color:Blue;" title="Remove to win Satge"></i></span>`
                        } else {
                            newTimelineItem += ` 
                <span  class="btn btn-sm Stage" data-workflow-id="0"><i class="fa fa-trophy" style="font-size: 20px" title="Set to win Stage"></i></span>`
                        }
                        newTimelineItem += `
            
        </div>
    </div>`;


                        $("#sortable").append(newTimelineItem);

                        // Refresh the sortable
                        $("#sortable").sortable("refresh");

                    });
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        
        var _$WorkflowSetupInformationSetupsForm = "";

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });
            _$workflowInformationForm = _modalManager.getModal().find('form[name=WorkflowInformationsForm]');
            _$workflowInformationForm.validate();
            _$WorkflowSetupInformationSetupsForm = _modalManager.getModal().find('form[name=WorkflowSetupInformationSetupsForm]');
            _$WorkflowSetupInformationSetupsForm.validate();


        };


        var SrlNo = document.getElementById("Srno");
        var Abbrivation = document.getElementById("abbrivation");
        var Name = document.getElementById("name");
        $(document).on('click', '.add-button-WorkFlow', function () {
            debugger;
            //var newIndex = 0;
            var newIndex = $("#sortable .timeline-item").length + 1;
            //var newIndex = $("#sortable").length + 1;
            var newTimelineItem = `
    <div class="col-12 timeline-item timeline-itemRowDelete">
        <div class="timeline-item col-11">
            <div class="timeline-circle"></div>
            <div class="timeline-content">

                <span class="SrlNo-input" style="display :none;" id="SrnoID">${newIndex}</span> 
                <input type="text" hidden placeholder="Your stage Abbreviation here" class="abbreviation-input">
                 <input type="text" placeholder="Your stage name here" class="name-input">
                 <input type="text" hidden value="false" class="HashTag-inputValue">
                <span class="hashtag" ><i class="fa fa-hashtag" style="font-size: 20px" title="Required Partner Client ID"></i></span>
                <input type="text" hidden value="false" class="calendar-inputValue">
                <span class="calendar"><i class="fa fa-calendar" style="font-size: 20px" title="Add Start and End Date"></i></span>
                <input type="text" hidden value="false" class="file-inputValue">
                <span class="file"><i class="fa fa-file-text-o" style="font-size: 20px "title="Add Note"></i></span>
                <input type="text" hidden value="false" class="Application-inputValue">
                <span class="Application"><i class="fa fa-calendar-check-o" style="font-size: 20px" title="Add Application intake field"></i></span>
                <span class="delete"><i class="fa fa-trash-o" style="font-size: 20px"></i></span>
                <span class="form-check form-switch activeValue"><input class="form-check-input switchValue" type="checkbox" value="false" role="switch" id="flexSwitchCheckDefault"></span>
                <input type="text" hidden placeholder="Your stage work flow step id here" value="0" class="WorkfrlowStepId-input">`
            if (workflowId > 0) {
                newTimelineItem += `<input type="text" hidden placeholder="Your stage work flow step id here" value='` + workflowId + `'class="WorkflowId-input">
`
            } else {
                newTimelineItem += `<input type="text" hidden placeholder="Your stage work flow step id here" value="0" class="WorkflowId-input">`
            }
            newTimelineItem += `<input type="text" hidden value="false" class="Stage-inputValue"> 
            </div>
        </div>
        <div class="col-1">
           <span  class="btn btn-sm Stage" value="false" data-workflow-id="0"><i class="fa fa-trophy" style="font-size: 20px" title="Set to win Stage"></i></span>
        </div>
    </div>`;

            $("#sortable").append(newTimelineItem);

        });

        $(document).on('blur', '.name-input', function () {
            var inputValue = $(this).val();

            var firstThreeLetters = inputValue.substring(0, 3);

            $(this).siblings('.abbreviation-input').val(firstThreeLetters);

        });

        $(document).on('click', '.delete', function () {

            var Row = $(this).closest('.timeline-itemRowDelete');
            var workflowstepId = Row.find('.WorkfrlowStepId-input').val();

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _workflowStepsService
                        .delete({
                            id: workflowstepId,
                        })
                        .done(function () {

                            Row.remove();

                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        });
        $(document).on('click', '.close-button,.btn-close', function () {

            location.reload();
        });


         
        $('#selectedOffice').change(function () {

            document.getElementById("showhide").style.display = 'block';

        });
        $('#allOffice').change(function () {
            $("#WorkFlowOfficeId").val(null).trigger('change');
            document.getElementById("showhide").style.display = 'none';

        }); 

        $(document).on('click', '.hashtag', function () {
            debugger;

            var hashtagID11 = $(this).closest('.timeline-item').find('.HashTag-inputValue').val();
            console.log(hashtagID);
            if (hashtagID11 == "true") {
                debugger
                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.HashTag-inputValue').val('false');
                $(this).html('<i class="fa fa-hashtag" style="font-size: 20px;  title="Required Partner Client ID"></i>');

            } else {
                debugger

                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.HashTag-inputValue').val('true');
                $(this).html('<i class="fa fa-hashtag" style="font-size: 20px; color:Blue;" title="Remove Partner Client ID"></i>');
            }
            
        });


        $(document).on('click', '.calendar', function () {
            

            var hashtagID11 = $(this).closest('.timeline-item').find('.calendar-inputValue').val();

            if (hashtagID11 == "true") {
                debugger
                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.calendar-inputValue').val('false');
                $(this).html('<i class="fa fa-calendar" style="font-size: 20px" title="Add Start and End Date"></i>');

            } else {
                debugger

                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.calendar-inputValue').val('true');
                $(this).html('<i class="fa fa-calendar" style="font-size: 20px; color:Blue;" title="Remove Start and End Date"></i>');
            }
            
        });

        $(document).on('click', '.file', function () {
            /*  debugger;*/

            var hashtagID11 = $(this).closest('.timeline-item').find('.file-inputValue').val();

            if (hashtagID11 == "true") {
                debugger
                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.file-inputValue').val('false');
                $(this).html('<i class="fa fa-file-text-o" style="font-size: 20px "title="Add Note"></i>');

            } else {
                debugger

                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.file-inputValue').val('true');
                $(this).html('<i class="fa fa-file-text-o" style="font-size: 20px; color:Blue;" title="Remove Note"></i>');
            }
            
        });

        $(document).on('click', '.Application', function () {
            /*debugger;*/

            var hashtagID11 = $(this).closest('.timeline-item').find('.Application-inputValue').val();

            if (hashtagID11 == "true") {
                debugger
                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.Application-inputValue').val('false');
                $(this).html('<i class="fa fa-calendar-check-o" style="font-size: 20px" title="Add Application intake field"></i>');

            } else {
                debugger

                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.Application-inputValue').val('true');
                $(this).html('<i class="fa fa-calendar-check-o" style="font-size: 20px; color:Blue;" title="Remove Application intake field"></i>');
            }
             
        });

        $(document).on('click', '.Stage', function () {

            var hashtagID11 = $(this).closest('.timeline-item').find('.Stage-inputValue').val();

            if (hashtagID11 == "true") {
                debugger
                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.Stage-inputValue').val('false');
                $(this).html('<i class="fa fa-trophy" style="font-size: 20px" title="Set to win Stage"></i>');

            } else {
                debugger

                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.Stage-inputValue').val('true');
                $(this).html('<i class="fa fa-trophy" style="font-size: 20px; color:Blue;" title="Remove to win Satge"></i>');
            }
             
        });

        $(document).on('click', '.activeValue', function () {
            

            var hashtagID11 = $(this).closest('.timeline-item').find('.switchValue').val();

            if (hashtagID11 == "true") {
                debugger
                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.switchValue').val('false');
              
            } else {
                debugger

                var hashtagID = $(this).closest('.timeline-item');
                hashtagID.find('.switchValue').val('true');
             }
              
        });

         
        function WorkflowsetupFeildClear() {
            SrlNo.value = '';
            Abbrivation.value = '';
            Name.value = '';
        }

        this.save = function () {
            
            if (!_$workflowInformationForm.valid()) {
                return;
            }
             
            var dataRows = [];

            $(".timeline-item").each(function () {
                var SrlNo = $(this).find('.SrlNo-input').html();
                var abbrivation = $(this).find('.abbreviation-input').val();
                var name = $(this).find('.name-input').val();
                var WorkfrlowStepId = $(this).find('.WorkfrlowStepId-input').val();
                var WorkflowId = $(this).find('.WorkflowId-input').val();
                var HashTagValue = $(this).find('.HashTag-inputValue').val();
                var calendarinputValue = $(this).find('.calendar-inputValue').val();
                var fileinputValue = $(this).find('.file-inputValue').val();
                var ApplicationinputValue = $(this).find('.Application-inputValue').val();
                var switchValue = $(this).find('.switchValue').val();
                var Stage = $(this).find('.Stage-inputValue').val();
                
                 
                dataRows.push({
                    srlNo: SrlNo,
                    abbrivation: abbrivation,
                    name: name,
                    id: WorkfrlowStepId,
                    workflowId: WorkflowId,
                    IsPartnerClientIdRequired: HashTagValue,
                    IsStartEndDateRequired: calendarinputValue,
                    IsNoteRequired: fileinputValue,
                    IsApplicationIntakeRequired: ApplicationinputValue,
                    IsActive: switchValue,
                    IsWinStage: Stage,
                });
            });

            // Remove duplicate records based on the SrlNo property
            var uniqueDataRows = dataRows.reduce(function (acc, current) {
                var duplicate = acc.find(function (item) {
                    return item.srlNo === current.srlNo;
                });

                if (!duplicate) {
                    return acc.concat([current]);
                } else {
                    return acc;
                }
            }, []);

            var Steps = JSON.stringify(uniqueDataRows);
            console.log(Steps);
            Steps = JSON.parse(Steps);


            var officerows = [];
            var datarowsList = $("#WorkFlowOfficeId :selected").map(function (i, el) {
                
                return $(el).val();
            }).get();
            debugger
            var test = $("input[name='IsForAllOffices']:checked").val();
            if (test == "false") {
                if (datarowsList == '') {
                    debugger

                    abp.notify.warn(app.localize('Please Select Office'));
                    return true
                }  
            }
            console.log(datarowsList);
            $.each(datarowsList, function (index, value) {
                var datarowsItem = {
                    OrganizationUnitId: datarowsList[index]
                }
                officerows.push(datarowsItem);
            });
            var officeSteps = JSON.stringify(officerows);
            debugger
            officeSteps = JSON.parse(officeSteps);

            var workflow = _$workflowInformationForm.serializeFormToObject();
            
            workflow.steps = Steps;
            workflow.officeSteps = officeSteps;
            debugger
            _modalManager.setBusy(true);
            _workflowsService
                .createOrEdit(workflow)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();

                    location.reload();
                    abp.event.trigger('app.createOrEditWorkflowModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
