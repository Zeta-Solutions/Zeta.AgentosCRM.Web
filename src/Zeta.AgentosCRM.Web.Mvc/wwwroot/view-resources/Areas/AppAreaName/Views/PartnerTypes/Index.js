(function () {
    $(function () {
        var _$partnerTypesTable = $('#PartnerTypesTable');
        var _partnerTypesService = abp.services.app.partnerTypes;

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };
        //...
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
        debugger
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
                    targets: 1,
                    data: 'partnerType.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'partnerType.name',
                    name: 'name',
                },
                {
                    targets: 3,
                    data: 'masterCategoryName',
                    name: 'masterCategoryFk.name',
                },
                {
                    targets: 4,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {

                        var rowId = data.partnerType.id;
                        var rowData = data.partnerType;
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

        function getPartnerTypes() {
            debugger
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
         
        $(document).on('click', 'a[data-action]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action');
            debugger 
            if (action === 'view') {
                _viewPartnerTypeModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deletePartnerType(rowId);
            }
        });

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
            debugger
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
