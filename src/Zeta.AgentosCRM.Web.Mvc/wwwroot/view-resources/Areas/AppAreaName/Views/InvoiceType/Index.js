(function () { 
    $(function () {
        var _$DocumentTypeTable = $('#DocumentTypeTable');
        var _documentTypesService = abp.services.app.invoiceTypes;
         
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
                getDocumentType();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getDocumentType();
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
                getDocumentType();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getDocumentType();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
        //    edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
        //    delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/InvoiceType/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/InvoiceType/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditDocumentTypeModal',
        });

        var _viewFeeTypeModal = new app.ModalManager({
            
            viewUrl: abp.appPath + 'AppAreaName/InvoiceType/ViewDocumentTypeModal',
            modalClass: 'ViewDocumentTypeModal',
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
        var dataTable = _$DocumentTypeTable.DataTable({

            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _documentTypesService.getAll,
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
                    data: 'invoiceType.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'invoiceType.name',
                    name: 'name',
                },
                {
                    targets: 3,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.invoiceType.id;
                        var rowData = data.invoiceType;
                        var RowDatajsonString = JSON.stringify(rowData);

                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
                            '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }
                },
            ],
        });
        function getDocumentType() {
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

            var rowId = $(this).data('id');
            var action = $(this).data('action');
             
            // Handle the selected action based on the rowId
            if (action === 'view') {
                 
                _viewFeeTypeModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteFeeType(rowId);
            }
        });

        function deleteFeeType(feeType) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _documentTypesService
                        .delete({
                            id: feeType.id,
                        })
                        .done(function () {
                            getDocumentType(true);
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

        $('#CreateNewInvoiceType').click(function () {
             
            _createOrEditModal.open();
        });

        abp.event.on('app.createOrEditFeeTypeModalSaved', function () {
             
            getDocumentType();
        });

        $('#GetMasterCategoriesButton').click(function (e) {
            e.preventDefault();
            getDocumentType();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getDocumentType();
            }
        });

        $('.reload-on-change').change(function (e) {
            getDocumentType();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getDocumentType();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getDocumentType();
        });
    });
})();
