(function () {
    $(function () {
        var _$ServiceCategoryTable = $('#ServiceCategoryTable');
        var _serviceCategoriesService = abp.services.app.serviceCategories;

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
                getServiceCategory();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getServiceCategory();
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
                getServiceCategory();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getServiceCategory();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ServiceCategories.Create'),
            edit: abp.auth.hasPermission('Pages.ServiceCategories.Edit'),
            delete: abp.auth.hasPermission('Pages.ServiceCategories.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ServiceCategory/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ServiceCategory/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditServiceCategoryModal',
        });

        var _viewServiceCategoryModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ServiceCategory/ViewServiceCategoryModal',
            modalClass: 'ViewServiceCategoryModal',
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

        var dataTable = _$ServiceCategoryTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _serviceCategoriesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#ServiceCategoryTableFilter').val(),
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
                                    _viewServiceCategoryModal.open({ id: data.record.serviceCategory.id });
                                },
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                  return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.serviceCategory.id });
                                },
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                  return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteServiceCategory(data.record.serviceCategory);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'serviceCategory.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'serviceCategory.name',
                    name: 'name',
                },
            ],
        });

        function getServiceCategory() {
            dataTable.ajax.reload();
        }

        function deleteServiceCategory(serviceCategory) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _serviceCategoriesService
                        .delete({
                            id: serviceCategory.id,
                        })
                        .done(function () {
                            getServiceCategory(true);
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

        $('#CreateNewServiceCategoryButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _serviceCategoriesService
                .getServiceCategoriesToExcel({
                    filter: $('#ServiceCategoryTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditServiceCategoryModalSaved', function () {
            getServiceCategory();
        });

        $('#GetServiceCategoryButton').click(function (e) {
            e.preventDefault();
            getServiceCategory();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getServiceCategory();
            }
        });

        $('.reload-on-change').change(function (e) {
            getServiceCategory();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getServiceCategory();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getServiceCategory();
        });
    });
})();
