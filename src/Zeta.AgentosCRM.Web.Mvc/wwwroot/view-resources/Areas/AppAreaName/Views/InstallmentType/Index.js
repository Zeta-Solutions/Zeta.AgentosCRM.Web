(function () {
  $(function () {
      var _$InstallmentTypeTable = $('#InstallmentTypeTable');
      var _installmentTypesService = abp.services.app.installmentTypes;

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
          getInstallmentTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
          getInstallmentTypes();
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
          getInstallmentTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
          getInstallmentTypes();
      });

    var _permissions = {
        create: abp.auth.hasPermission('Pages.InstallmentTypes.Create'),
        edit: abp.auth.hasPermission('Pages.InstallmentTypes.Edit'),
        delete: abp.auth.hasPermission('Pages.InstallmentTypes.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/InstallmentType/CreateOrEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/InstallmentType/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditInstallmentTypeModal',
    });

    var _viewInstallmentTypeModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/InstallmentType/ViewInstallmentTypeModal',
        modalClass: 'ViewInstallmentTypeModal',
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
      var dataTable = _$InstallmentTypeTable.DataTable({
          paging: true,
          serverSide: true,
          processing: true,
          listAction: {
              ajaxFunction: _installmentTypesService.getAll,
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
                                  //_viewSourceModal.open();
                                  _viewInstallmentTypeModal.open({ id: data.record.installmentType.id });
                              },
                          },
                          {
                              text: app.localize('Edit'),
                              visible: function () {
                                return _permissions.edit;
                              },
                              action: function (data) {
                                  _createOrEditModal.open({ id: data.record.installmentType.id });
                              },
                          },
                          {
                              text: app.localize('Delete'),
                              //visible: function () {
                              //  return _permissions.delete;
                              //},
                              action: function (data) {
                                  deleteInstallmentType(data.record.installmentType);
                              },
                          },
                      ],
                  },
              },
              {
                  targets: 2,
                  data: 'installmentType.abbrivation',
                  name: 'abbrivation',
              },
              {
                  targets: 3,
                  data: 'installmentType.name',
                  name: 'name',
              },
          ],
      });
    function getInstallmentTypes() {
      dataTable.ajax.reload();
    }

      function deleteInstallmentType(installmentType) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
            _installmentTypesService
            .delete({
                id: installmentType.id,
            })
            .done(function () {
                getInstallmentTypes(true);
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

      $('#CreateNewButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
        _installmentTypesService
        .getMasterCategoriesToExcel({
          filter: $('#MasterCategoriesTableFilter').val(),
          abbrivationFilter: $('#AbbrivationFilterId').val(),
          nameFilter: $('#NameFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

      abp.event.on('app.createOrEditInstallmentTypeModalSaved', function () {
          getInstallmentTypes();
    });

    $('#GetMasterCategoriesButton').click(function (e) {
      e.preventDefault();
        getInstallmentTypes();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
          getInstallmentTypes();
      }
    });

    $('.reload-on-change').change(function (e) {
        getInstallmentTypes();
    });

    $('.reload-on-keyup').keyup(function (e) {
        getInstallmentTypes();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
        getInstallmentTypes();
    });
  });
})();
