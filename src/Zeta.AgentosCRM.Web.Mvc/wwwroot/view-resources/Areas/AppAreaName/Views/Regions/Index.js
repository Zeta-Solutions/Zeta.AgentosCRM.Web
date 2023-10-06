(function () {
  $(function () {
      var _$RegionsTable = $('#RegionsTable');
      var _regionsService = abp.services.app.regions;

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
          getRegions();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
          getRegions();
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
          getRegions();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
          getRegions();
      });

    var _permissions = {
        create: abp.auth.hasPermission('Pages.Regions.Create'),
        edit: abp.auth.hasPermission('Pages.Regions.Edit'),
        delete: abp.auth.hasPermission('Pages.Regions.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/Regions/CreateOrEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Regions/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditRegionModal',
    });

      var _viewRegionModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/Regions/ViewRegionsModal',
        modalClass: 'ViewRegionsModal',
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
      var dataTable = _$RegionsTable.DataTable({
          paging: true,
          serverSide: true,
          processing: true,
          listAction: {
              ajaxFunction: _regionsService.getAll,
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
                                  _viewRegionModal.open({ id: data.record.region.id });
                              },
                          },
                          {
                              text: app.localize('Edit'),
                              visible: function () {
                                return _permissions.edit;
                              },
                              action: function (data) {
                                  _createOrEditModal.open({ id: data.record.region.id });
                              },
                          },
                          {
                              text: app.localize('Delete'),
                              visible: function () {
                                return _permissions.delete;
                              },
                              action: function (data) {
                                  deleteRegion(data.record.region);
                              },
                          },
                      ],
                  },
              },
              {
                  targets: 2,
                  data: 'region.abbrivation',
                  name: 'abbrivation',
              },
              {
                  targets: 3,
                  data: 'region.name',
                  name: 'name',
              },
          ],
      });
    function getRegions() {
      dataTable.ajax.reload();
    }

      function deleteRegion(regions) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
            _regionsService
            .delete({
                id: regions.id,
            })
            .done(function () {
                getRegions(true);
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

      $('#CreateNewFeeButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
        _regionsService
        .getMasterCategoriesToExcel({
          filter: $('#MasterCategoriesTableFilter').val(),
          abbrivationFilter: $('#AbbrivationFilterId').val(),
          nameFilter: $('#NameFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

      abp.event.on('app.createOrEditRegionModalSaved', function () {
        getRegions();
    });

    $('#GetMasterCategoriesButton').click(function (e) {
      e.preventDefault();
        getRegions();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
          getRegions();
      }
    });

    $('.reload-on-change').change(function (e) {
        getRegions();
    });

    $('.reload-on-keyup').keyup(function (e) {
        getRegions();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
        getRegions();
    });
  });
})();
