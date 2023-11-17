
(function ($) {
    app.modals.CreateOrEditWorkflowModal = function () {
        var _workflowsService = abp.services.app.workflows;


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
                    debugger;


                    var TotalRecord = data.result.items;

                    $.each(TotalRecord, function (index, item) {
                        debugger;
                          
                        var newTimelineItem = `
    <div class="col-12 timeline-item  timeline-itemRowDelete ">
        <div class="timeline-item col-11">
            <div class="timeline-circle"></div>
            <div class="timeline-content">
                <span style="display :none;" class="SrlNo-input" placeholder="Your stage Srno here">${item.workflowStep.srlNo}</span>
                <input type="text" hidden class="abbreviation-input" placeholder="Your stage Abbrivation here" value="${item.workflowStep.abbrivation}">
                <input type="text" class="name-input" placeholder="Your stage name here" value="${item.workflowStep.name}">
                <input type="text" hidden value="0" class="HashTag-inputValue">
                <span class="hashtag" ><i class="fa fa-hashtag" style="font-size: 20px" title="Required Partner Client ID"></i></span>
                <input type="text" hidden value="0" class="calendar-inputValue">
                <span class="calendar"><i class="fa fa-calendar" style="font-size: 20px" title="Add Start and End Date"></i></span>
                <input type="text" hidden value="0" class="file-inputValue">
                <span class="file"><i class="fa fa-file-text-o" style="font-size: 20px "title="Add Note"></i></span>
                <input type="text" hidden value="0" class="Application-inputValue">
                <span class="Application"><i class="fa fa-calendar-check-o" style="font-size: 20px" title="Add Application intake field"></i></span>
                <span class="delete"><i class="fa fa-trash-o" style="font-size: 20px"></i></span>
                <span class="form-check form-switch activeValue"><input class="form-check-input switchValue" type="checkbox" value="0" role="switch" id="flexSwitchCheckDefault"></span>
                <input type="text" hidden class="WorkfrlowStepId-input" placeholder="Your stage workflowStepId here" value="${item.workflowStep.id}">
                <input type="text" hidden class="WorkflowId-input" placeholder="Your stage workflowId here" value="${workflowId}">
            </div>
        </div>
        <div class="col-1">
            <button type="button" class="btn btn-sm Stage" value="0" data-workflow-id="0"><i class="fa fa-trophy" style="font-size: 20px" title="Set to win Stage"></i></button>
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
                 <input type="text" hidden value="0" class="HashTag-inputValue">
                <span class="hashtag" ><i class="fa fa-hashtag" style="font-size: 20px" title="Required Partner Client ID"></i></span>
                <input type="text" hidden value="0" class="calendar-inputValue">
                <span class="calendar"><i class="fa fa-calendar" style="font-size: 20px" title="Add Start and End Date"></i></span>
                <input type="text" hidden value="0" class="file-inputValue">
                <span class="file"><i class="fa fa-file-text-o" style="font-size: 20px "title="Add Note"></i></span>
                <input type="text" hidden value="0" class="Application-inputValue">
                <span class="Application"><i class="fa fa-calendar-check-o" style="font-size: 20px" title="Add Application intake field"></i></span>
                <span class="delete"><i class="fa fa-trash-o" style="font-size: 20px"></i></span>
                <span class="form-check form-switch activeValue"><input class="form-check-input switchValue" type="checkbox" value="0" role="switch" id="flexSwitchCheckDefault"></span>
                <input type="text" hidden placeholder="Your stage work flow step id here" value="0" class="WorkfrlowStepId-input">
                <input type="text" hidden placeholder="Your stage work flow step id here" value='`+ workflowId + `'class="WorkflowId-input">
            </div>
        </div>
        <div class="col-1">
           <button type="button" class="btn btn-sm Stage" value="0" data-workflow-id="0"><i class="fa fa-trophy" style="font-size: 20px" title="Set to win Stage"></i></button>
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
            debugger
            var Row = $(this).closest('.timeline-itemRowDelete');
            var workflowstepId = Row.find('.WorkfrlowStepId-input').val();

            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _workflowStepsService
                        .delete({
                            id: workflowstepId,
                        })
                        .done(function () {
                            debugger
                            Row.remove();

                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        });
        $(document).on('click', '.close-button,.btn-close', function () {
            /* debugger */
            location.reload();
        });



        $(document).on('click', '#allOffice', function () {
            $("#showhide").hide();
        });

        $(document).on('click', '#selectedOffice', function () {
            $("#showhide").show()
        });

        $(document).on('click', '.hashtag', function () {
            debugger;

            $(this).toggleClass('clicked');

            if ($(this).hasClass('clicked')) {

                //$(this).text('1');
                $('.HashTag-inputValue').val('1');

                $(this).html('<i class="fa fa-hashtag" style="font-size: 20px; color:Blue;" title="Remove Partner Client ID"></i>');
            } else {
                //$(this).text('0');
                $('.HashTag-inputValue').val('0');


                $(this).html('<i class="fa fa-hashtag" style="font-size: 20px" title="Required Partner Client ID"></i>');
            }
        });


        $(document).on('click', '.calendar', function () {
            debugger;

            // Toggle the class 'clicked' to change color
            $(this).toggleClass('clicked');

            // Update the value (you can customize this part)
            if ($(this).hasClass('clicked')) {
                // Value when clicked

                //$(this).text('1');

                $('.calendar-inputValue').val('1');

                $(this).html('<i class="fa fa-calendar" style="font-size: 20px; color:Blue;" title="Remove Start and End Date"></i>');
            } else {
                //// Default value
                //$(this).text('0');

                $('.calendar-inputValue').val('0');

                $(this).html('<i class="fa fa-calendar" style="font-size: 20px" title="Add Start and End Date"></i>');
            }
        });

        $(document).on('click', '.file', function () {
            debugger;

            // Toggle the class 'clicked' to change color
            $(this).toggleClass('clicked');

            // Update the value (you can customize this part)
            if ($(this).hasClass('clicked')) {

                $('.file-inputValue').val('1');
                $(this).html('<i class="fa fa-file-text-o" style="font-size: 20px; color:Blue;" title="Remove Note"></i>');
            } else {

                $('.file-inputValue').val('0');

                $(this).html('<i class="fa fa-file-text-o" style="font-size: 20px "title="Add Note"></i>');
            }
        });

        $(document).on('click', '.Application', function () {
            debugger;

            // Toggle the class 'clicked' to change color
            $(this).toggleClass('clicked');

            // Update the value (you can customize this part)
            if ($(this).hasClass('clicked')) {

                $('.Application-inputValue').val('1');

                $(this).html('<i class="fa fa-calendar-check-o" style="font-size: 20px; color:Blue;" title="Remove Application intake field"></i>');
            } else {

                $('.Application-inputValue').val('0');

                $(this).html('<i class="fa fa-calendar-check-o" style="font-size: 20px" title="Add Application intake field"></i>');
            }
        });

        $(document).on('click', '.Stage', function () {
            debugger;

            // Toggle the class 'clicked' to change color
            $(this).toggleClass('clicked');

            // Update the value (you can customize this part)
            if ($(this).hasClass('clicked')) {

                $('.Stage-inputValue').val('1');

                $(this).html('<i class="fa fa-trophy" style="font-size: 20px; color:Blue;" title="Remove to win Satge"></i>');
            } else {

                $('.Stage-inputValue').val('1');

                $(this).html('<i class="fa fa-trophy" style="font-size: 20px" title="Set to win Stage"></i>');
            }
        });

        $(document).on('click', '.activeValue', function () {
            debugger;

            // Toggle the class 'clicked' to change color
            $(this).toggleClass('clicked');

            // Update the value (you can customize this part)
            if ($(this).hasClass('clicked')) {

                $('.switchValue').val('1');

            } else {

                $('.switchValue').val('0');

            }
        });



        //$(document).find('.add-button').on('click', (e) => {

        //    //if (!_$WorkflowSetupInformationSetupsForm.valid()) {
        //    //    return;
        //    //}
        //    //debugger
        //    //// Check if the table has existing rows
        //    //if (Workflowsetuptable.rows().count() > 0) {
        //    //    debugger
        //    //    // If there are existing rows, append the new row
        //    //    Workflowsetuptable.row.add([ 
        //    //        SrlNo.value,
        //    //        Abbrivation.value,
        //    //        Name.value,
        //    //        '<button type="button" class="btn btn-sm delete" data-workflow-id="0"><i class="fa fa-trash" style="font-size: 30px"></i></button>'
        //    //    ]).draw(false);
        //    //} else {
        //    //    debugger
        //    //    // If the table is empty, add the new row
        //    //    Workflowsetuptable.clear().draw();
        //    //    Workflowsetuptable.row.add([
        //    //        SrlNo.value,
        //    //        Abbrivation.value,
        //    //        Name.value,
        //    //        '<button type="button" class="btn btn-sm delete" data-workflow-id="0"><i class="fa fa-trash" style="font-size: 30px"></i></button>'
        //    //    ]).draw();
        //    //}

        //    //WorkflowsetupFeildClear();
        //});

        //var Workflowsetuptable = $('#Workflowsetuptable').DataTable();
        //$(document).find('.add-button').on('click', (e) => {

        //    if (!_$WorkflowSetupInformationSetupsForm.valid()) {
        //        return;
        //    }
        //    // Srl = $("#Srno").val();
        //    //Abbr = $("#abbrivation").val();
        //    //nam = $("#name").val();

        //    //if (Srl == "") {
        //    //    return;
        //    //}
        //    //if (Abbr == "") {
        //    //    return;
        //    //} 
        //    //if (nam.length < 5 || nam == "") {
        //    //    return;
        //    //}
        //    Workflowsetuptable.row
        //        .add([
        //            SrlNo.value,
        //            Abbrivation.value,
        //            Name.value,
        //            '<button type="button" class="btn btn-sm delete" data-workflow-id="0"><i class="fa fa-trash" style="font-size: 30px"></i></button>'
        //        ])
        //        .draw(false);
        //    WorkflowsetupFeildClear();
        //});

        //$(document).on('click', '.delete', function () {
        //    debugger
        //    //var Workflowsetuptablerow = $(this).closest("tr");
        //    //Workflowsetuptablerow.find(".data-workflow-id").text();
        //    //$(this).closest('tr').delete();
        //    //Workflowsetuptable.row(this).delete();
        //    Workflowsetuptable
        //        .row($(this).parents('tr'))
        //        .remove()
        //        .draw();
        //});


        function WorkflowsetupFeildClear() {
            SrlNo.value = '';
            Abbrivation.value = '';
            Name.value = '';
        }

        this.save = function () {

            if (!_$workflowInformationForm.valid()) {
                return;
            }

            //var data = Workflowsetuptable
            //    .rows().data().toArray();
            //var dataRows = "";
            //for (var i = 0; i < data.length; i++) {
            //    debugger
            //    var SrlnoGrid = data[i][0];
            //    var AbbrivationGrid = data[i][1];
            //    var NameGrid = data[i][2];

            //    dataRows += '{"SrlNo":"' + SrlnoGrid + '","Abbrivation":"' + AbbrivationGrid + '","Name":"' + NameGrid + '"},';
            //}
            //dataRows = dataRows.substring(0, dataRows.length - 1);
            var dataRows = [];

            $(".timeline-item").each(function () {
                var SrlNo = $(this).find('.SrlNo-input').html();
                var abbrivation = $(this).find('.abbreviation-input').val();
                var name = $(this).find('.name-input').val();
                var WorkfrlowStepId = $(this).find('.WorkfrlowStepId-input').val();
                var WorkflowId = $(this).find('.WorkflowId-input').val();

                dataRows.push({
                    srlNo: SrlNo,
                    abbrivation: abbrivation,
                    name: name,
                    id: WorkfrlowStepId,
                    workflowId: WorkflowId
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


            var workflow = _$workflowInformationForm.serializeFormToObject();
            workflow.steps = Steps;
            _modalManager.setBusy(true);
            _workflowsService
                .createOrEdit(workflow)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditWorkflowModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };
    };
})(jQuery);
