(function ($) {
	
	//window.onload = codeAddress;
	//$(document).on('loaded', '#ApplicationDetailTab',
	$('#tabs').on('click', 'a', function (e) {
		e.preventDefault();

		var tabId = $(this)
		var tabHref= tabId.attr('href')
		if (tabHref == "#ApplicationDetailTabDiv") {
            debugger
            setTimeout(function () {
                // Show the modal (adjust as needed) 
                LoadSteps();
            }, 500);
		}
		
	});

    function LoadSteps() {
        // SmartWizard initialize

        var _workflowStepsService = abp.services.app.workflowSteps;
        var workflowsId = $("#ApplicationWorkflowId").val()

        $.ajax({
            url: abp.appPath + 'api/services/app/WorkflowSteps/GetAll',
            data: {
                WorkflowIdFilter: workflowsId,
            },
            method: 'GET',
            dataType: 'json',
        })
            .done(function (data) {
                //Row.remove();
                var TotalRecord = data.result.items;

                $.each(TotalRecord, function (index, item) {
                    var step = `<li class="nav-item">
                        <a class="nav-link" href="#step-${index + 1}">
                            <div class="num">${index + 1}</div>
                            ${item.workflowStep.name}
                        </a>
                    </li>`
                    var stepForm = `<div id="step-${index + 1}" class="tab-pane" role="tabpanel" aria-labelledby="step-${index + 1}">
                        Step content
                            <input type="text" id="stepId-${index + 1}" value="${item.workflowStep.id}"></input>
                    </div>`

                    $("#progressHead").append(step);
                    $("#contentId").append(stepForm);
                    if (!TotalRecord[index + 1]) {
                        console.log('End')
                    }
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
                    }, 
                    warningSteps: [TotalRecord.length - 1],// Highlight step with warnings
                    keyboard: {
                        keyNavigation: false, // Enable/Disable keyboard navigation(left and right keys are used if enabled)
                        keyLeft: [37], // Left key code
                        keyRight: [39] // Right key code
                    } 
                })
                    .on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
                        debugger
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
                    })

            })
            .fail(function (error) {
                //
                console.error('Error fetching data:', error);
            });


    };
    $(document).on('click', '#AppNextBtn', function () {
        var stepId = $(this)
         
        $('#smartwizard').smartWizard("next");

    });
    $(document).on('click', '#AppPreviousBtn', function () {
        var stepId = $(this)
         
        $('#smartwizard').smartWizard("prev");
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