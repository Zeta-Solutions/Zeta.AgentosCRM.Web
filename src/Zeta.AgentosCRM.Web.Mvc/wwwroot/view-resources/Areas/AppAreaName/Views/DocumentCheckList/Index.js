(function () {
    $(function () {
        var _$FeeTypeTable = $('#LeadFormTable');
        var _workflowDocumentsService = abp.services.app.workflowDocuments;
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
                getDocumentCheckList();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getDocumentCheckList();
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
                getDocumentCheckList();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getDocumentCheckList();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
        //    edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
        //    delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DocumentCheckList/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditDocumentCheckListHeadModal',
        });
        //var _createOrEditAddNewCheckListModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/AddCheckList',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DocumentCheckList/AddDocumentCheckList/AddNewCheckList.js',
        //    modalClass: 'AddCheckList',
        //});
        //var _viewFeeTypeModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/ViewLeadFormModal',
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
        var dataTable = _$FeeTypeTable.DataTable({

            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _workflowDocumentsService.getAll,
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
                    targets: 1,
                    data: 'workflowDocument.name',
                    name: 'name',
                },
                {
                    targets: 2,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.workflowDocument.id;
                        var GetWorkFlowId = data.workflowDocument.workflowId;
                        var rowData = data.workflowDocument;
                        var RowDatajsonString = JSON.stringify(rowData);
                        debugger
                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><input type="hidden" id="WorkFlowRowId" value="' + GetWorkFlowId + '"/><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }
                },
            ],
        });
        function getDocumentCheckList() {
            dataTable.ajax.reload();

        }


        $(document).on('click', '.ellipsis', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');
            allOptions.not(options).hide();

            options.toggle();
        });

        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });

        // Handle menu item clicks
        $(document).on('click', 'a[data-action]', function (e) {
            e.preventDefault();
             
            var options = $(this).closest('.context-menu')
            var GetWorkFlowId = options.find('#WorkFlowRowId').val();

            var rowId = $(this).data('id');
            var action = $(this).data('action');
             
            debugger
          // Handle the selected action based on the rowId
          //if (action === 'view') {
          //    debugger
          //    _viewFeeTypeModal.open({ id: rowId });
            // } else
            if (action === 'edit') { 
                debugger
                //    viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditClientTags',
                window.location = "/AppAreaName/DocumentCheckList/EditDocumentCheckList/" + "?id=" + rowId + "&WorkFlowid=" + GetWorkFlowId; 
                //_createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteFeeType(rowId);
            }
        });

        function deleteFeeType(feeType) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _workflowDocumentsService
                        .delete({
                            id: feeType.id,
                        })
                        .done(function () {
                            getDocumentCheckList(true);
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

        $('#CreateNewDocumentCheckList').click(function () {
            debugger
            _createOrEditModal.open();
        });
        //$('#NewCheckList').click(function () {
        //    debugger
        //    _createOrEditAddNewCheckListModal.open();
        //    $("#DDlPartners").hide();

        //});
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


        //$(document).on('click', '#ProductID', function () {
        //    $("#RadioProducts").show();
        //});
        //$(document).on('click', '#AllProduct', function () {
        //    $("#DDlProduct").hide();
        //});

        //$(document).on('click', '#selectedProduct', function () {
        //    $("#DDlProduct").show()
        //});
        //$(document).on('click', '#AllPartner', function () {
        //    $("#DDlPartners").hide();
        //    debugger
        //});

        //$(document).on('click', '#selectedPartner', function () {
        //    $("#DDlPartners").show();
        //    debugger
        //});

        abp.event.on('app.createOrEditFeeTypeModalSaved', function () {
            debugger
            getDocumentCheckList();
        });

        $('#GetMasterCategoriesButton').click(function (e) {
            e.preventDefault();
            getDocumentCheckList();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getDocumentCheckList();
            }
        });

        $('.reload-on-change').change(function (e) {
            getDocumentCheckList();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getDocumentCheckList();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getDocumentCheckList();
        });
    });
})();
