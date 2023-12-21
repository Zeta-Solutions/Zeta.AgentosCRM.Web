(function ($) {
    var _ApplicationStagesService = abp.services.app.applicationStages;
    var stepsArr=[];
    //window.onload = codeAddress;
	//$(document).on('loaded', '#ApplicationDetailTab',
	$('#tabs').on('click', 'a', function (e) {
        e.preventDefault();
        $("#ApplicationDetailTabDiv").empty();
		var tabId = $(this)
		var tabHref= tabId.attr('href')
		if (tabHref == "#ApplicationDetailTabDiv") {
            //debugger

            $("#ApplicationDetailTabDiv").empty();
            setTimeout(function () {
                $('#smartwizard').smartWizard("reset"); 
                // Show the modal (adjust as needed) 
                LoadSteps();
            }, 500);
		}
		
	});

    function LoadSteps() {
        // SmartWizard initialize

        var _workflowStepsService = abp.services.app.workflowSteps;
        var workflowsId = $("#ApplicationWorkflowId").val()
        var applicationId = $("#ApplicationId").val();
        var currentstage = -1;
        $.ajax({
            url: abp.appPath + 'api/services/app/WorkflowSteps/GetAll',
            data: {
                WorkflowIdFilter: workflowsId,
            },
            method: 'GET',
            dataType: 'json', 
        })
            .done(function (data) {
                //Row.remove()..;
                var TotalRecord = data.result.items;
                //debugger
                stepsArr = [];
                $.each(TotalRecord, function (index, item) {
                    var step = `<li class="nav-item">
                        <a class="nav-link" href="#step-${index + 1}">
                            <div class="num">${index + 1}</div>
                            ${item.workflowStep.name}
                        </a>
                    </li>`
                    var stepForm = `<div id="step-${index + 1}" class="tab-pane" role="tabpanel" aria-labelledby="step-${index + 1}">
                       
                            <input type="hidden" id="stepId-${index + 1}" value="${item.workflowStep.id}"></input>
                    <input type="hidden" id="StageID-${index + 1}" value="0" ></input> </div>`
                    $("#progressHead").append(step);
                    $("#contentId").append(stepForm);
                    if (!TotalRecord[index + 1]) {
                        console.log('End')
                    }
                    var inputGetData = {
                        applicationIdFilter: applicationId,
                        workflowStepIdFilter: item.workflowStep.id 
                    };
                    stepsArr.push(inputGetData);
                    //var Getdata = JSON.stringify(inputGetData);
                    //Getdata = JSON.parse(Getdata);
                    //_ApplicationStagesService
                    //    .getAll(Getdata)
                    //    .done(function (data) {
                    //        //debugger
                    //        if (data && data.items && data.items.length > 0) {
                    //            //stepForm += `<input type="hidden" id="StageID-${index + 1}" value="${data.items[0].applicationStage.id}"></input> </div>`;
                    //            $("#StageID-" + index + 1).val(data.items[0].applicationStage.id);
                    //            if (data.items[0].applicationStage.isCurrent == true) {
                    //                currentstage = index;
                    //                $('#smartwizard').smartWizard("goToStep", currentstage, true);
                    //            }
                    //        }
                    //    })
                        //.fail(function (error) {
                        //    console.error("Error fetching data:", error);
                        //    // Handle the error appropriately
                        //});
                  
                })

                //abp.notify.success(app.localize('SuccessfullyLoaded'));
                $('#smartwizard').smartWizard({
                    selected: -1, // Initial selected step, 0 = first step
                    theme: 'round', // theme for the wizard, related css need to include for other than default theme 
                    enableUrlHash: false, // Enable selection of the step based on url hash 
                    toolbar: {
                        position: 'top', // none|top|bottom|both
                        showNextButton: false, // show/hide a Next button
                        showPreviousButton: false, // show/hide a Previous button
                       // extraHtml: `<button class="btn btn-success btn-sm" type="button" id="finish-btn">Submit</button>
               // <button class="btn btn-danger btn-sm" id="discontinue-btn" type="button" onclick="onCancel()">Discontinue</button>`
                    },
                    anchor: {
                        enableNavigation: true, // Enable/Disable anchor navigation 
                        enableNavigationAlways: false, // Activates all anchors clickable always
                        enableDoneState: true, // Add done state on visited steps
                        markPreviousStepsAsDone: true, // When a step selected by url hash, all previous steps are marked done
                        unDoneOnBackNavigation: true, // While navigate back, done state will be cleared
                        enableDoneStateNavigation: true // Enable/Disable the done state navigation
                    } 
                    //warningSteps: [TotalRecord.length - 1],// Highlight step with warnings
                    //keyboard: {
                    //    keyNavigation: false, // Enable/Disable keyboard navigation(left and right keys are used if enabled)
                    //    keyLeft: [37], // Left key code
                    //    keyRight: [39] // Right key code
                    //} 
                })
                    .on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
                        //debugger
                        if (stepPosition === 'first') {
                            $("#AppPreviousBtn").addClass('disabled');
                            $("#AppSubmitBtn").hide();
                        } else if (stepPosition === 'last') {
                            $("#AppNextBtn").hide();
                            $("#AppSubmitBtn").show();
                        } else {
                            $("#AppSubmitBtn").hide();
                            $("#AppNextBtn").show();
                            $("#AppPreviousBtn").removeClass('disabled');
                        }
                        
                    })
                    .on("loaded", function (e) {
                        $("#AppPreviousBtn").addClass('disabled');
                        $("#finish-btn").hide();
                        $('#AppSubmitBtn').hide(); 

                        getStageData(stepsArr)
                    })

            })
            .fail(function (error) {
                //
                console.error('Error fetching data:', error);
            });


    };
    function getStageData(inputGetData) {
        debugger
        $.each(inputGetData, function (index, item) { 
            var Getdata = JSON.stringify(item);
            Getdata = JSON.parse(Getdata);
            _ApplicationStagesService
                .getAll(Getdata)
                .done(function (data) {
                    //debugger
                    if (data && data.items && data.items.length > 0) {
                        //stepForm += `<input type="hidden" id="StageID-${index + 1}" value="${data.items[0].applicationStage.id}"></input> </div>`;
                        $("#StageID-" + parseInt(index + 1)).val(data.items[0].applicationStage.id);

                        if (data.items[0].applicationStage.isCurrent == true) {
                            currentstage = index;
                            debugger
                            $('#smartwizard').smartWizard("goToStep", index, true);
                        }
                    }
                })
                .fail(function (error) {
                    console.error("Error fetching data:", error);
                    // Handle the error appropriately
                });

        })
        
    }
    $(document).on('click', '#AppNextBtn', function () {
        //debugger
        var stepId = $(this)
        $('#smartwizard').smartWizard("next");
        //debugger
        var ApplicationIdFilter = parseInt($("#ApplicationId").val());
        var applicationId = $("#ApplicationId").val();
        var IsCurrentIdFilter = true;
        var srno = $('.nav-link.default.active .num').text().trim();
        //debugger
        if (srno > 1) {
            var inputGetData = {
                applicationIdFilter: ApplicationIdFilter,
                isCurrentIdFilter: IsCurrentIdFilter
            };
            var Getdata = JSON.stringify(inputGetData);
            Getdata = JSON.parse(Getdata);
            _ApplicationStagesService
                .getAll(Getdata)
                .done(function (data) {
                    //debugger
                   
                    var inputData = {
                        name: data.items[0].applicationStage.name,
                        workflowStepId: data.items[0].applicationStage.workflowStepId,
                        applicationId: data.items[0].applicationStage.applicationId,
                        isCurrent: false,
                        isCompleted: true,
                        isActive: true,
                        id: data.items[0].applicationStage.id
                    };
                    var Steps = JSON.stringify(inputData);
                    Steps = JSON.parse(Steps);
                    _ApplicationStagesService
                        .createOrEdit(Steps)
                })
                //...done(function (data) {
                //    //debugger

                //    })
        }
        var applicationId = $("#ApplicationId").val();
        var srno = $('.nav-link.default.active .num').text().trim();
        var workflowStepId = $("#stepId-" + srno).val();
        var name = document.querySelector('.nav-link.active').innerText;
        var textNodes = $('.nav-link.default.active').contents().filter(function () {
            return this.nodeType === 3; // Filter out text nodes..
        });

        var name = textNodes.map(function () {
            return $(this).text().trim();
        }).get().join(' ');
        var isCurrent = true;
        var isCompleted = false;
        var isActive = true;

        var inputData = {
            name: name,
            workflowStepId: workflowStepId,
            applicationId: applicationId,
            isCurrent: isCurrent,
            isCompleted: isCompleted,
            isActive: isActive

        };
        var Steps = JSON.stringify(inputData);
        Steps = JSON.parse(Steps);
        _ApplicationStagesService
            .createOrEdit(Steps)
            .done(function () {
                //debugger
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditApplicationStagesSaved');
            })
            .always(function () {
                _modalManager.setBusy(false);
            });

    });
    $(document).on('click', '#AppPreviousBtn', function () {
        //debugger
        var stepId = $(this)
        var srno = $('.nav-link.default.active .num').text().trim();
        var stageid = $("#StageID-" + srno).val();
        _ApplicationStagesService
            .delete({
                id: stageid,
            })
        $('#smartwizard').smartWizard("prev");


        var applicationId = $("#ApplicationId").val();
        var srno = $('.nav-link.default.active .num').text().trim();
        var workflowStepId = $("#stepId-" + srno).val();
        var stageid = $("#StageID-" + srno).val();
        var name = document.querySelector('.nav-link.active').innerText;
        var textNodes = $('.nav-link.default.active').contents().filter(function () {
            return this.nodeType === 3; // Filter out text nodes..
        });

        var name = textNodes.map(function () {
            return $(this).text().trim();
        }).get().join(' ');
        var isCurrent = true;
        var isCompleted = false;
        var isActive = true;

        var inputData = {
            name: name,
            workflowStepId: workflowStepId,
            applicationId: applicationId,
            isCurrent: isCurrent,
            isCompleted: isCompleted,
            isActive: isActive,
            id: stageid
        };
        var Steps = JSON.stringify(inputData);
        Steps = JSON.parse(Steps);
        _ApplicationStagesService
            .createOrEdit(Steps)
            .done(function () {
                //debugger
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditApplicationStagesSaved');
            })
    });
    $(document).on('click', '#AppSubmitBtn', function () {
        var stepId = $(this)
        $("#AppSubmitBtn").addClass("validate");
        $('#smartwizard').smartWizard("disable");

    });
    $(document).on('click', '#AppDiscontinueBtn', function () {
        var stepId = $(this)
         alert("Discontinue")
        //$('#smartwizard').smartWizard("disable"); 

    });

     
})(jQuery)