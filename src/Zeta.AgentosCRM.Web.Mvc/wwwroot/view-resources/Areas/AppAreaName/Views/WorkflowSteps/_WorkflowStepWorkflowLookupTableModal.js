(function ($) {
  app.modals.WorkflowLookupTableModal = function () {
    var _modalManager;

    var _workflowStepsService = abp.services.app.workflowSteps;
    var _$workflowTable = $('#WorkflowTable');

    this.init = function (modalManager) {
      _modalManager = modalManager;
    };

    var dataTable = _$workflowTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _workflowStepsService.getAllWorkflowForLookupTable,
        inputFilter: function () {
          return {
            filter: $('#WorkflowTableFilter').val(),
          };
        },
      },
      columnDefs: [
        {
          targets: 0,
          data: null,
          orderable: false,
          autoWidth: false,
          defaultContent:
            "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" +
            app.localize('Select') +
            "' /></div>",
        },
        {
          autoWidth: false,
          orderable: false,
          targets: 1,
          data: 'displayName',
        },
      ],
    });

    $('#WorkflowTable tbody').on('click', '[id*=selectbtn]', function () {
      var data = dataTable.row($(this).parents('tr')).data();
      _modalManager.setResult(data);
      _modalManager.close();
    });

    function getWorkflow() {
      dataTable.ajax.reload();
    }

    $('#GetWorkflowButton').click(function (e) {
      e.preventDefault();
      getWorkflow();
    });

    $('#SelectButton').click(function (e) {
      e.preventDefault();
    });

    $('#WorkflowTableFilter').keypress(function (e) {
      if (e.which === 13 && e.target.tagName.toLocaleLowerCase() != 'textarea') {
        getWorkflow();
      }
    });
  };
})(jQuery);
