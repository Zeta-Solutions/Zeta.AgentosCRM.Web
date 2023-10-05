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

            //var Workflowsetuptable = _modalManager.getModal().find('table[name=Workflowsetuptable]');
            //_$WorkflowSetupInformationSetupsForm.find('name=Workflowsetuptable')


            

        };

        var SrlNo = document.getElementById("abbrivation");
        var Abbrivation = document.getElementById("abbrivation");
        var Name = document.getElementById("name");

        var Workflowsetuptable = $('#Workflowsetuptable').DataTable({

        });
        $(document).find('.add-button').on('click', (e) => {
            debugger
            Workflowsetuptable.row
                .add([
                    '1',
                    Abbrivation.value,
                    Name.value,
                    '<button type="button" class="btn btn-sm bg-danger edit delete" data-workflow-id="1"></button>' 
                ])
                .draw(false);
                /*
            var Abbrivation = document.getElementById("abbrivation");
            var Name = document.getElementById("name");
            var rowIndex = Workflowsetuptable.rows.length;

            var dataRow = document.createElement("tr");

            var dataColumn = document.createElement("td");
            dataColumn.innerText = "1";
            //dataColumn.innerText = rowIndex;
            //dataColumn.style.display="none";
            dataRow.appendChild(dataColumn);

            //dataColumn = document.createElement("td");
            //dataColumn.innerText = "1";
            //dataRow.appendChild(dataColumn);

            dataColumn = document.createElement("td");
            dataColumn.innerText = Abbrivation.value;
            dataRow.appendChild(dataColumn);

            dataColumn = document.createElement("td");
            dataColumn.innerText = Name.value;
            dataRow.appendChild(dataColumn);

            dataColumn = document.createElement("td");
            dataColumn.innerHTML = '<button type="button" class="btn btn-sm bg-danger data-workflow-id="1" edit delete"></button>';
            dataRow.appendChild(dataColumn);

            debugger
            Workflowsetuptable.rows.add(dataRow);
            */
        });

        $(document).on('click', '.delete', function () {
            var workflowtableId = $(this).attr("data-workflow-id");

        });
        function WorkflowsetupRowDelete(workflowtableId) {
            for (var i = 0; i < Workflowsetuptable.rows.length; i++) {
                if (Workflowsetuptable.rows[i].cells[0].innerText == workflowtableId) {
                    Workflowsetuptable.rows[0].remove();
                }

            }
        }
        //var Workflowsetuptable = _$WorkflowSetupInformationSetupsForm.find('#Workflowsetuptable')

        this.save = function () {
            debugger
            if (!_$workflowInformationForm.valid()) {
                return;
            }
            if (!_$WorkflowSetupInformationSetupsForm.valid()) {
                return;
            }

            var workflow = _$workflowInformationForm.serializeFormToObject();
            var workflowSetup = _$WorkflowSetupInformationSetupsForm.serializeFormToObject();
            workflow.workflowSetup = workflowSetup;
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
