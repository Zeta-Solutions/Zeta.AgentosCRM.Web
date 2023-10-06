(function () {
  $(function () {
      var _$ProductTypeTable = $('#ProductTypeTable');
      var _productTypesService = abp.services.app.productTypes;

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
          getProductTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
          getProductTypes();
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
          getProductTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
          getProductTypes();
      });

    var _permissions = {
        create: abp.auth.hasPermission('Pages.ProductTypes.Create'),
        edit: abp.auth.hasPermission('Pages.ProductTypes.Edit'),
        delete: abp.auth.hasPermission('Pages.ProductTypes.Delete'),
    };
     
      var _createOrEditModal = new app.ModalManager({
          viewUrl: abp.appPath + 'AppAreaName/ProductType/CreateOrEditModal',
          scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ProductType/_CreateOrEditModal.js',
          modalClass: 'CreateOrEditProductTypeModal',
      });
    var _viewProductTypeModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/ProductType/ViewProductTypeModal',
        modalClass: 'ViewProductTypeModal',
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
      var dataTable = _$ProductTypeTable.DataTable({
          paging: true,
          serverSide: true,
          processing: true,
          listAction: {
              ajaxFunction: _productTypesService.getAll,
              inputFilter: function () {
                  return {
                      filter: $('#MasterCategoriesTableFilter').val(),
                      abbrivationFilter: $('#AbbrivationFilterId').val(),
                      nameFilter: $('#NameFilterId').val(),
                      masterCategoryNameFilter: $('#MasterCategoryNameFilterId').val(),

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
                                  _viewProductTypeModal.open({ id: data.record.productType.id });
                              },
                          },
                          {
                              text: app.localize('Edit'),
                              visible: function () {
                                return _permissions.edit;
                              },
                              action: function (data) {
                                  _createOrEditModal.open({ id: data.record.productType.id });
                              },
                          },
                          {
                              text: app.localize('Delete'),
                              visible: function () {
                                return _permissions.delete;
                              },
                              action: function (data) {
                                  deleteProductType(data.record.productType);
                              },
                          },
                      ],
                  },
              },
              {
                  targets: 2,
                  data: 'productType.abbrivation',
                  name: 'abbrivation',
              },
              {
                  targets: 3,
                  data: 'productType.name',
                  name: 'name',
              },

              {
                  targets: 4,
                  data: 'masterCategoryName',
                  name: 'masterCategoryFk.name',
              },
          ],
      });
    function getProductTypes() {
      dataTable.ajax.reload();
    }

      function deleteProductType(productType) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _productTypesService
            .delete({
                id: productType.id,
            })
            .done(function () {
                getProductTypes(true);
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

      $('#CreateNewProductTypeButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _productTypesService
        .getMasterCategoriesToExcel({
          filter: $('#MasterCategoriesTableFilter').val(),
          abbrivationFilter: $('#AbbrivationFilterId').val(),
            nameFilter: $('#NameFilterId').val(),
            masterCategoryNameFilter: $('#MasterCategoryNameFilterId').val(),

        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

      abp.event.on('app.createOrEditProductTypeModalSaved', function () {
        getProductTypes();
    });

    $('#GetMasterCategoriesButton').click(function (e) {
      e.preventDefault();
        getProductTypes();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
          getProductTypes();
      }
    });

    $('.reload-on-change').change(function (e) {
        getProductTypes();
    });

    $('.reload-on-keyup').keyup(function (e) {
        getProductTypes();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
        getProductTypes();
    });
  });
})();
