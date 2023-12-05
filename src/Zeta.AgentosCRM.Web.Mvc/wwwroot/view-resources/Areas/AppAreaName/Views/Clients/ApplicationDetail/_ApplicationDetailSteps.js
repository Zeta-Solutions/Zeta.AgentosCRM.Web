(function ($) {
	 
	$(document).on('click', '#ApplicationDetailTab', function (e) {
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

			$.each(TotalRecord, function (index, item)
			{
				var step =`<li class="nav-item">
                    <a class="nav-link" href="#step-${index+1}">
                        <div class="num">${index + 1}</div>
                        ${item.workflowStep.name}
                    </a>
                </li>`
				var stepForm = `<div id="step-${index + 1}" class="tab-pane" role="tabpanel" aria-labelledby="step-${index + 1}">
                    Step content
                </div>`
				 
				$("#progressHead").append(step);
				$("#contentId").append(stepForm);
				if (!TotalRecord[index + 1]) {
					console.log('End')
				}
			})

			debugger
			//abp.notify.success(app.localize('SuccessfullyLoaded'));
			$('#smartwizard').smartWizard({ 
				//selected: 0, // Initial selected step, 0 = first step
				theme: 'square', // theme for the wizard, related css need to include for other than default theme
				toolbar: {
					position: 'top', // none|top|bottom|both 
					showFinishButton: true, // show/hide a Previous button
					extraHtml: `<button class="btn btn-success btn-sm" onclick="onFinish()">Finish</button>`
				},
				anchor: {
					enableNavigation: false, // Enable/Disable anchor navigation  
				},
				style: { // CSS Class settings 
					btnCss: 'sw-btn btn-sm',
					btnNextCss: 'sw-btn-next btn-sm',
					btnPrevCss: 'sw-btn-prev btn-sm',
				},
				warningSteps: [TotalRecord.length - 1],// Highlight step with warnings 
			});

		})
		.fail(function (error) {
			//
			console.error('Error fetching data:', error);
		});
	

	});
})(jQuery)