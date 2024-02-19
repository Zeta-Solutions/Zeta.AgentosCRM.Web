(function () {
  $(function () {
      var _$FeeTypeTable = $('#LeadFormTable');
    //var _masterCategoriesService = abp.services.app.masterCategories;
      //var _feeTypesService = abp.services.app.feeTypes;

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
          getLeadForm();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
          getLeadForm();
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
          getLeadForm();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
          getLeadForm();
      });

    //var _permissions = {
    //    create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
    //    edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
    //    delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
    //};

    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/Leadform/CreateOrEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Leadform/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditModal',
    });

    //var _viewFeeTypeModal = new app.ModalManager({
    //    viewUrl: abp.appPath + 'AppAreaName/Leadform/ViewLeadFormModal',
    //    modalClass: 'ViewLeadFormModal',
    //});

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
      //var dataTable = _$FeeTypeTable.DataTable({
          
      //    paging: true,
      //    serverSide: true,
      //    processing: true,
      //    listAction: {
      //        ajaxFunction: _feeTypesService.getAll,
      //        inputFilter: function () {
      //            return {
      //                filter: $('#MasterCategoriesTableFilter').val(),
      //                abbrivationFilter: $('#AbbrivationFilterId').val(),
      //                nameFilter: $('#NameFilterId').val(),
      //            };
      //        },
      //    },
      //    columnDefs: [
      //        {
      //            className: 'control responsive',
      //            orderable: false,
      //            render: function () {
      //                return '';
      //            },
      //            targets: 0,
      //        }, 
      //        {
      //            targets: 1,
      //            data: 'feeType.abbrivation',
      //            name: 'abbrivation',
      //        },
      //        {
      //            targets: 2,
      //            data: 'feeType.name',
      //            name: 'name',
      //        },
      //        {
      //            targets: 3,
      //            width: 30,
      //            data: null,
      //            orderable: false,
      //            searchable: false,
      //            render: function (data, type, full, meta) {
      //                console.log(data);
      //                var rowId = data.feeType.id;
      //                var rowData = data.feeType;
      //                var RowDatajsonString = JSON.stringify(rowData);

      //                var contextMenu = '<div class="context-menu" style="position:relative;">' +
      //                    '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
      //                    '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
      //                    '<ul style="list-style: none; padding: 0;color:black">' +
      //                    '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
      //                    '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
      //                    "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
      //                    '</ul>' +
      //                    '</div>' +
      //                    '</div>';

      //                return contextMenu;
      //            }
      //        },
      //    ],
      //});
      function getLeadForm() {
      dataTable.ajax.reload();
    }



      //// Add a click event handler for the ellipsis icons
      //$(document).on('click', '.ellipsis', function (e) {
      //    e.preventDefault();

      //    var options = $(this).closest('.context-menu').find('.options');
      //    var allOptions = $('.options');  // Select all options

      //    // Close all other open options
      //    allOptions.not(options).hide();

      //    // Toggle the visibility of the options
      //    options.toggle();
      //});

      //// Close the context menu when clicking outside of it
      //$(document).on('click', function (event) {
      //    if (!$(event.target).closest('.context-menu').length) {
      //        $('.options').hide();
      //    }
      //});

      //// Handle menu item clicks
      //$(document).on('click', 'a[data-action]', function (e) {
      //    e.preventDefault();

      //    var rowId = $(this).data('id');
      //    var action = $(this).data('action');
      //     
      //    // Handle the selected action based on the rowId
      //    if (action === 'view') {
      //        _viewFeeTypeModal.open({ id: rowId });
      //    } else if (action === 'edit') {
      //        _createOrEditModal.open({ id: rowId });
      //    } else if (action === 'delete') {
      //        console.log(rowId);
      //        deleteFeeType(rowId);
      //    }
      //});

      function deleteFeeType(feeType) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
            _feeTypesService
                .delete({
                    id: feeType.id,
            })
            .done(function () {
                getLeadForm(true);
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

      $('#CreateNewLeadform').click(function () {
           
      _createOrEditModal.open();
    });

    //$('#ExportToExcelButton').click(function () {
    //    _feeTypesService
    //    .getMasterCategoriesToExcel({
    //      filter: $('#MasterCategoriesTableFilter').val(),
    //      abbrivationFilter: $('#AbbrivationFilterId').val(),
    //      nameFilter: $('#NameFilterId').val(),
    //    })
    //    .done(function (result) {
    //      app.downloadTempFile(result);
    //    });
    //});

      abp.event.on('app.createOrEditFeeTypeModalSaved', function () {
           
          getLeadForm();
    });

    $('#GetMasterCategoriesButton').click(function (e) {
      e.preventDefault();
        getLeadForm();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
          getLeadForm();
      }
    });

    $('.reload-on-change').change(function (e) {
        getLeadForm();
    });

    $('.reload-on-keyup').keyup(function (e) {
        getLeadForm();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
        getLeadForm();
    });
  });
})();
