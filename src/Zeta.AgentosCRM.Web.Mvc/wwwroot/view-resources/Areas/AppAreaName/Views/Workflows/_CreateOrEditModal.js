
(function ($) {
    app.modals.CreateOrEditWorkflowModal = function () {
        var _workflowsService = abp.services.app.workflows;

        var _modalManager;
        var _$workflowInformationForm = null;
        //var _$WorkflowInformationSetupsForm = null; 



        var _$WorkflowSetupInformationSetupsForm = "";

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });
            //debugger
            _$workflowInformationForm = _modalManager.getModal().find('form[name=WorkflowInformationsForm]');
            _$workflowInformationForm.validate();
            // WorkflowInformationsetupsForm Form Data 
            _$WorkflowSetupInformationSetupsForm = _modalManager.getModal().find('form[name=WorkflowSetupInformationSetupsForm]');
            _$WorkflowSetupInformationSetupsForm.validate();


        };

        var SrlNo = document.getElementById("Srno");
        var Abbrivation = document.getElementById("abbrivation");
        var Name = document.getElementById("name");

        var Workflowsetuptable = $('#Workflowsetuptable').DataTable();
        $(document).find('.add-button').on('click', (e) => {
            debugger
            if (!_$WorkflowSetupInformationSetupsForm.valid()) {
                return;
            }
            // Srl = $("#Srno").val();
            //Abbr = $("#abbrivation").val();
            //nam = $("#name").val();

            //if (Srl == "") {
            //    return;
            //}
            //if (Abbr == "") {
            //    return;
            //} 
            //if (nam.length < 5 || nam == "") {
            //    return;
            //}
            Workflowsetuptable.row
                .add([
                    SrlNo.value,
                    Abbrivation.value,
                    Name.value,
                    '<button type="button" class="btn btn-sm delete" data-workflow-id="0"><i class="fa fa-trash" style="font-size: 30px"></i></button>'
                ])
                .draw(false);
            WorkflowsetupFeildClear();
        });

        $(document).on('click', '.delete', function () {
            //var Workflowsetuptablerow = $(this).closest("tr");
            //Workflowsetuptablerow.find(".data-workflow-id").text();
            //$(this).closest('tr').delete();
            //Workflowsetuptable.row(this).delete();
            Workflowsetuptable
                .row($(this).parents('tr'))
                .remove()
                .draw();
        });
        function WorkflowsetupFeildClear() {
            SrlNo.value = '';
            Abbrivation.value = '';
            Name.value = '';
        }

        this.save = function () {
            debugger
            if (!_$workflowInformationForm.valid()) {
                return;
            }
            
            var data = Workflowsetuptable
                .rows().data().toArray();
            var dataRows = "";
            for (var i = 0; i < data.length; i++) {
                debugger
                var SrlnoGrid = data[i][0];
                var AbbrivationGrid = data[i][1];
                var NameGrid = data[i][2];

                dataRows += '{"SrlNo":"' + SrlnoGrid + '","Abbrivation":"' + AbbrivationGrid + '","Name":"' + NameGrid + '"},';
            }
            dataRows = dataRows.substring(0, dataRows.length - 1);
            debugger
            var Steps = "[" + dataRows + "]";
            Steps = JSON.parse(Steps);
            var workflow = _$workflowInformationForm.serializeFormToObject();
            workflow.Steps = Steps;
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
