﻿(function () {
  $(function () {
    var _$partnerTypesTable = $('#PartnerTypesTable');
    var _partnerTypesService = abp.services.app.partnerTypes;

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
        getPartnerTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getPartnerTypes();
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
        getPartnerTypes();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getPartnerTypes();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.PartnerTypes.Create'),
      edit: abp.auth.hasPermission('Pages.PartnerTypes.Edit'),
      delete: abp.auth.hasPermission('Pages.PartnerTypes.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/PartnerTypes/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/PartnerTypes/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditPartnerTypeModal',
    });

    var _viewPartnerTypeModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/PartnerTypes/ViewpartnerTypeModal',
      modalClass: 'ViewPartnerTypeModal',
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

    var dataTable = _$partnerTypesTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _partnerTypesService.getAll,
        inputFilter: function () {
          return {
            filter: $('#PartnerTypesTableFilter').val(),
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
                  _viewPartnerTypeModal.open({ id: data.record.partnerType.id });
                },
              },
              {
                text: app.localize('Edit'),
                //visible: function () {
                //  return _permissions.edit;
                //},
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.partnerType.id });
                },
              },
              {
                text: app.localize('Delete'),
                //visible: function () {
                //  return _permissions.delete;
                //},
                action: function (data) {
                  deletePartnerType(data.record.partnerType);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'partnerType.abbrivation',
          name: 'abbrivation',
        },
        {
          targets: 3,
          data: 'partnerType.name',
          name: 'name',
        },
        {
          targets: 4,
          data: 'masterCategoryName',
          name: 'masterCategoryFk.name',
        },
      ],
    });

    function getPartnerTypes() {
      dataTable.ajax.reload();
    }

    function deletePartnerType(partnerType) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _partnerTypesService
            .delete({
              id: partnerType.id,
            })
            .done(function () {
              getPartnerTypes(true);
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

    $('#CreateNewPartnerTypeButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _partnerTypesService
        .getPartnerTypesToExcel({
          filter: $('#PartnerTypesTableFilter').val(),
          abbrivationFilter: $('#AbbrivationFilterId').val(),
          nameFilter: $('#NameFilterId').val(),
          masterCategoryNameFilter: $('#MasterCategoryNameFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditPartnerTypeModalSaved', function () {
      getPartnerTypes();
    });

    $('#GetPartnerTypesButton').click(function (e) {
      e.preventDefault();
      getPartnerTypes();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getPartnerTypes();
      }
    });

    $('.reload-on-change').change(function (e) {
      getPartnerTypes();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getPartnerTypes();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getPartnerTypes();
    });
  });
})();
