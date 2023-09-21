(function () {
  $(function () {
    var _$workflowStepsTable = $('#MasterDetailChild_Workflow_WorkflowStepsTable');
    var _workflowStepsService = abp.services.app.workflowSteps;

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
        getWorkflowSteps();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getWorkflowSteps();
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
        getWorkflowSteps();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getWorkflowSteps();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.WorkflowSteps.Create'),
      edit: abp.auth.hasPermission('Pages.WorkflowSteps.Edit'),
      delete: abp.auth.hasPermission('Pages.WorkflowSteps.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MasterDetailChild_Workflow_WorkflowSteps/CreateOrEditModal',
      scriptUrl:
        abp.appPath +
        'view-resources/Areas/AppAreaName/Views/MasterDetailChild_Workflow_WorkflowSteps/_CreateOrEditModal.js',
      modalClass: 'MasterDetailChild_Workflow_CreateOrEditWorkflowStepModal',
    });

    var _viewWorkflowStepModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MasterDetailChild_Workflow_WorkflowSteps/ViewworkflowStepModal',
      modalClass: 'MasterDetailChild_Workflow_ViewWorkflowStepModal',
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

    var dataTable = _$workflowStepsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _workflowStepsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#MasterDetailChild_Workflow_WorkflowStepsTableFilter').val(),
            minSrlNoFilter: $('#MasterDetailChild_Workflow_MinSrlNoFilterId').val(),
            maxSrlNoFilter: $('#MasterDetailChild_Workflow_MaxSrlNoFilterId').val(),
            abbrivationFilter: $('#MasterDetailChild_Workflow_AbbrivationFilterId').val(),
            nameFilter: $('#MasterDetailChild_Workflow_NameFilterId').val(),
            workflowIdFilter: $('#MasterDetailChild_Workflow_WorkflowStepsId').val(),
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
                  _viewWorkflowStepModal.open({ id: data.record.workflowStep.id });
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.workflowStep.id });
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteWorkflowStep(data.record.workflowStep);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'workflowStep.srlNo',
          name: 'srlNo',
        },
        {
          targets: 3,
          data: 'workflowStep.abbrivation',
          name: 'abbrivation',
        },
        {
          targets: 4,
          data: 'workflowStep.name',
          name: 'name',
        },
      ],
    });

    function getWorkflowSteps() {
      dataTable.ajax.reload();
    }

    function deleteWorkflowStep(workflowStep) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _workflowStepsService
            .delete({
              id: workflowStep.id,
            })
            .done(function () {
              getWorkflowSteps(true);
              abp.notify.success(app.localize('SuccessfullyDeleted'));
            });
        }
      });
    }

    $('#MasterDetailChild_Workflow_ShowAdvancedFiltersSpan').click(function () {
      $('#MasterDetailChild_Workflow_ShowAdvancedFiltersSpan').hide();
      $('#MasterDetailChild_Workflow_HideAdvancedFiltersSpan').show();
      $('#MasterDetailChild_Workflow_AdvacedAuditFiltersArea').slideDown();
    });

    $('#MasterDetailChild_Workflow_HideAdvancedFiltersSpan').click(function () {
      $('#MasterDetailChild_Workflow_HideAdvancedFiltersSpan').hide();
      $('#MasterDetailChild_Workflow_ShowAdvancedFiltersSpan').show();
      $('#MasterDetailChild_Workflow_AdvacedAuditFiltersArea').slideUp();
    });

    $('#CreateNewWorkflowStepButton').click(function () {
      _createOrEditModal.open();
    });

    abp.event.on('app.createOrEditWorkflowStepModalSaved', function () {
      getWorkflowSteps();
    });

    $('#GetWorkflowStepsButton').click(function (e) {
      e.preventDefault();
      getWorkflowSteps();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getWorkflowSteps();
      }
    });

    $('.reload-on-change').change(function (e) {
      getWorkflowSteps();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getWorkflowSteps();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getWorkflowSteps();
    });
  });
})();
