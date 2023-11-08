(function () {
    $(function () {

        $('input[type="radio"]').change(function () {
            if (this.value === "option1") {
                // Show fields for option1
                $("#field1").show();
                // Hide the label and field for option2
                $("#field2 label, #field2 input").hide();
                $("#field3 label, #field3 input").hide();
                $("#field1").show();
            } else if (this.value === "option2") {
                // Hide the label and field for option1
                $("#field1 label, #field1 input").hide();
                // Show the label and field for option2
                $("#field2 label, #field2 input").show();
            }
        });
      var _$FeeTypeTable = $('#FeeTypeTable');
    //var _masterCategoriesService = abp.services.app.masterCategories;
      var _feeTypesService = abp.services.app.feeTypes;

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
        format: 'L',
      })
      .on('apply.daterangepicker', (ev, picker) => {
        $selectedDate.startDate = picker.startDate;
          getFeeTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
          getFeeTypes();
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
          getFeeTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
          getFeeTypes();
      });

    var _permissions = {
        create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
        edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
        delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/Agents/CreateOrEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Agents/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditModal',
    });

    var _viewFeeTypeModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/Agents/ViewAgentMainFormModal',
        modalClass: 'ViewAgentMainFormModal',
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
      var dataTable = _$FeeTypeTable.DataTable({
          
          paging: true,
          serverSide: true,
          processing: true,
          listAction: {
              ajaxFunction: _feeTypesService.getAll,
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
                                  window.location.href = abp.appPath + 'AppAreaName/Agents/ViewAgentMainFormModal';
                                  //window.location.href = abp.appPath + 'AppAreaName/Agents/ApplicationView';
                                  //_viewFeeTypeModal.open({ id: data.record.feeType.id });
                              },
                          },

                          {
                              text: app.localize('Email'),
                              ////visible: function () {
                              ////  return _permissions.edit;
                              ////},
                              action: function (data) {
                                  _createOrEditModal.open();
                              },
                          },
                          {
                              text: app.localize('Delete'),
                              //visible: function () {
                              //  return _permissions.delete;
                              //},
                              action: function (data) {
                                  deleteFeeType(data.record.feeType);
                              },
                          },
                      ],
                  },
              },
              {
                  targets: 2,
                  data: 'feeType.abbrivation',
                  name: 'abbrivation',
              },
              {
                  targets: 3,
                  data: 'feeType.name',
                  name: 'name',
              },
              {
                  targets: 4,
                  data: 'feeType.name',
                  name: 'Structure',
              },
              {
                  targets: 5,
                  data: 'feeType.name',
                  name: 'Phone',
              },
              {
                  targets: 6,
                  data: 'feeType.name',
                  name: 'City',
              },
              {
                  targets: 7,
                  data: 'feeType.name',
                  name: 'Associated Office',
              },
              {
                  targets: 8,
                  data: 'feeType.name',
                  name: '5',
              },
              {
                  targets: 9,
                  data: 'feeType.name',
                  name: '4',
              },
          ],
      });
      function getFeeTypes() {
      dataTable.ajax.reload();
    }

      function deleteFeeType(feeType) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
            _feeTypesService
                .delete({
                    id: feeType.id,
            })
            .done(function () {
                getFeeTypes(true);
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
      //_createOrEditModal.open();
          window.location.href = abp.appPath + 'AppAreaName/Agents/AddAgentdetail';

    });

    $('#ExportToExcelButton').click(function () {
        _feeTypesService
        .getMasterCategoriesToExcel({
          filter: $('#MasterCategoriesTableFilter').val(),
          abbrivationFilter: $('#AbbrivationFilterId').val(),
          nameFilter: $('#NameFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

      abp.event.on('app.createOrEditFeeTypeModalSaved', function () {
          debugger
          getFeeTypes();
    });

    $('#GetMasterCategoriesButton').click(function (e) {
      e.preventDefault();
        getFeeTypes();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
          getFeeTypes();
      }
    });

    $('.reload-on-change').change(function (e) {
        getFeeTypes();
    });

    $('.reload-on-keyup').keyup(function (e) {
        getFeeTypes();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
        getFeeTypes();
    });
  });
})();
