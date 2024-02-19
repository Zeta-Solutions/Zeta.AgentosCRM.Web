﻿(function ($) {
    app.modals.CreateOrEditDocumentCheckListHeadModal = function () {
       
        var _workflowsService = abp.services.app.workflows;
        var _workflowDocumentsService = abp.services.app.workflowDocuments;
        var _workflowStepsService = abp.services.app.workflowSteps;
        
        $('#WorkflowId').select2({
            width: '100%',
            placeholder: 'Select Workflow',
            allowClear: true,
            minimumResultsForSearch: Infinity,
        });
    var _modalManager;
        var _$feeTypesInformationForm = null;
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
                     
                    var TotalRecord = data.result.items;
                     

                        $.each(TotalRecord, function (index, item) {
                             
                            var workFlowStep = `
                                <div class="col-12 timeline-item  timeline-itemRowDelete ">
                                    <div class="timeline-item col-11">
                                        <div class="timeline-circle"></div>
                                        <div class="timeline-content">
                                            <span style="display :none;">${item.workflowStep.id}</span>
                                            <a href=""><span></span></a>  
                                            <span>${item.workflowStep.name}</span>  
                                        </div>
                                    </div> 
                                </div>`;
     
                            $(".WorkFlowStepDetail").append(workFlowStep);
                              
                        }); 
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        //_workflowStepsService.getAll(WorkflowId)
        //    .done(function (data) {
        //         
        //        console.log(data);
                 
        //    })
    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

        _$feeTypesInformationForm = _modalManager.getModal().find('form[name=FeeTypeInformationsForm]');
        _$feeTypesInformationForm.validate();
    };
        $("#WorkflowId").on("change", function () {
             
            var selectedWorkflowName = $(this).find("option:selected").text();
            $("#WorkFlow_Name").val(selectedWorkflowName); 
        });
    this.save = function () {
        if (!_$feeTypesInformationForm.valid()) {
        return;
      }

        var feeType = _$feeTypesInformationForm.serializeFormToObject();
         
      _modalManager.setBusy(true);
        _workflowDocumentsService
            .createOrEdit(feeType)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditFeeTypeModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
