(function () {
  $(function () {
    var _$workflowsTable = $('#WorkflowsTable');
    var _workflowsService = abp.services.app.workflows;

    var $selectedDate = {
      startDate: null,
      endDate: null,
    };

    $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
      $(this).val(picker.startDate.format('MM/DD/YYYY'));
    });

    $('.startDate')
      .daterangepicker({
        autoUpdateInput: false,
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      })
      .on('apply.daterangepicker', (ev, picker) => {
        $selectedDate.startDate = picker.startDate;
        getWorkflows();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getWorkflows();
      });

    $('.endDate')
      .daterangepicker({
        autoUpdateInput: false,
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      })
      .on('apply.daterangepicker', (ev, picker) => {
        $selectedDate.endDate = picker.startDate;
        getWorkflows();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getWorkflows();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.Workflows.Create'),
      edit: abp.auth.hasPermission('Pages.Workflows.Edit'),
      delete: abp.auth.hasPermission('Pages.Workflows.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/Workflows/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Workflows/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditWorkflowModal',
    });

    var _viewWorkflowModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/Workflows/ViewworkflowModal',
      modalClass: 'ViewWorkflowModal',
    });

    var getDateFilter = function (element) {
      if ($selectedDate.startDate == null) {
        return null;
      }
      return $selectedDate.startDate.format('YYYY-MM-DDT00:00:00Z');
    };

    var getMaxDateFilter = function (element) {
      if ($selectedDate.endDate == null) {
        return null;
      }
      return $selectedDate.endDate.format('YYYY-MM-DDT23:59:59Z');
    };

    var dataTable = _$workflowsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _workflowsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#WorkflowsTableFilter').val(),
            nameFilter: $('#NameFilterId').val(),
          };
        },
      },
      columnDefs: [
        {
          className: 'control responsive',
          orderable: false,
          render: function () {
            return '';
          },
          targets: 0,
        },
        {
          width: 120,
          targets: 1,
          data: null,
          orderable: false,
          autoWidth: false,
          defaultContent: '',
          rowAction: {
            cssClass: 'btn btn-brand dropdown-toggle',
            text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
            items: [
              {
                text: app.localize('View'),
                action: function (data) {
                  _viewWorkflowModal.open({ id: data.record.workflow.id });
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.workflow.id });
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteWorkflow(data.record.workflow);
                },
              },
            ],
          },
        },
        {
          className: 'details-control',
          targets: 2,
          orderable: false,
          autoWidth: false,
          visible: abp.auth.hasPermission('Pages.WorkflowSteps'),
          render: function () {
            return `<button class="btn btn-primary btn-xs Edit_WorkflowStep_WorkflowId">${app.localize(
              'EditWorkflowStep'
            )}</button>`;
          },
        },
        {
          targets: 3,
          data: 'workflow.name',
          name: 'name',
        },
      ],
    });

    function getWorkflows() {
      dataTable.ajax.reload();
    }

    function deleteWorkflow(workflow) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _workflowsService
            .delete({
              id: workflow.id,
            })
            .done(function () {
              getWorkflows(true);
              abp.notify.success(app.localize('SuccessfullyDeleted'));
            });
        }
      });
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
      $('#ShowAdvancedFiltersSpan').hide();
      $('#HideAdvancedFiltersSpan').show();
      $('#AdvacedAuditFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
      $('#HideAdvancedFiltersSpan').hide();
      $('#ShowAdvancedFiltersSpan').show();
      $('#AdvacedAuditFiltersArea').slideUp();
    });

    $('#CreateNewWorkflowButton').click(function () {
      _createOrEditModal.open();
    });

    abp.event.on('app.createOrEditWorkflowModalSaved', function () {
      getWorkflows();
    });

    $('#GetWorkflowsButton').click(function (e) {
      e.preventDefault();
      getWorkflows();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getWorkflows();
      }
    });

    $('.reload-on-change').change(function (e) {
      getWorkflows();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getWorkflows();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getWorkflows();
    });

    var currentOpenedDetailRow;
    function openDetailRow(e, url) {
      var tr = $(e).closest('tr');
      var row = dataTable.row(tr);

      if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
        currentOpenedDetailRow = null;
      } else {
        if (currentOpenedDetailRow) currentOpenedDetailRow.child.hide();

        $.get(url).then((data) => {
          row.child(data).show();
          tr.addClass('shown');
          currentOpenedDetailRow = row;
        });
      }
    }

    _$workflowsTable.on('click', '.Edit_WorkflowStep_WorkflowId', function () {
      var tr = $(this).closest('tr');
      var row = dataTable.row(tr);
      openDetailRow(this, '/AppAreaName/MasterDetailChild_Workflow_WorkflowSteps?WorkflowId=' + row.data().workflow.id);
    });
  });
})();
