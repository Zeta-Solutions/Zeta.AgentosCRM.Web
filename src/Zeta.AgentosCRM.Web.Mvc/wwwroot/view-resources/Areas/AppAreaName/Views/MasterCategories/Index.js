(function () {
  $(function () {
    var _$masterCategoriesTable = $('#MasterCategoriesTable');
    var _masterCategoriesService = abp.services.app.masterCategories;

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
        getMasterCategories();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getMasterCategories();
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
        getMasterCategories();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getMasterCategories();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.MasterCategories.Create'),
      edit: abp.auth.hasPermission('Pages.MasterCategories.Edit'),
      delete: abp.auth.hasPermission('Pages.MasterCategories.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MasterCategories/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/MasterCategories/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditMasterCategoryModal',
    });

    var _viewMasterCategoryModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MasterCategories/ViewmasterCategoryModal',
      modalClass: 'ViewMasterCategoryModal',
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

    var dataTable = _$masterCategoriesTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _masterCategoriesService.getAll,
        inputFilter: function () {
          return {
            filter: $('#MasterCategoriesTableFilter').val(),
            abbrivationFilter: $('#AbbrivationFilterId').val(),
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
                  _viewMasterCategoryModal.open({ id: data.record.masterCategory.id });
                },
              },
              {
                text: app.localize('Edit'),
                //visible: function () {
                //  return _permissions.edit;
                //},
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.masterCategory.id });
                },
              },
              {
                text: app.localize('Delete'),
                //visible: function () {
                //  return _permissions.delete;
                //},
                action: function (data) {
                  deleteMasterCategory(data.record.masterCategory);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'masterCategory.abbrivation',
          name: 'abbrivation',
        },
        {
          targets: 3,
          data: 'masterCategory.name',
          name: 'name',
        },
      ],
    });

    function getMasterCategories() {
      dataTable.ajax.reload();
    }

    function deleteMasterCategory(masterCategory) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _masterCategoriesService
            .delete({
              id: masterCategory.id,
            })
            .done(function () {
              getMasterCategories(true);
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

    $('#CreateNewMasterCategoryButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _masterCategoriesService
        .getMasterCategoriesToExcel({
          filter: $('#MasterCategoriesTableFilter').val(),
          abbrivationFilter: $('#AbbrivationFilterId').val(),
          nameFilter: $('#NameFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditMasterCategoryModalSaved', function () {
      getMasterCategories();
    });

    $('#GetMasterCategoriesButton').click(function (e) {
      e.preventDefault();
      getMasterCategories();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getMasterCategories();
      }
    });

    $('.reload-on-change').change(function (e) {
      getMasterCategories();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getMasterCategories();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getMasterCategories();
    });
  });
})();
