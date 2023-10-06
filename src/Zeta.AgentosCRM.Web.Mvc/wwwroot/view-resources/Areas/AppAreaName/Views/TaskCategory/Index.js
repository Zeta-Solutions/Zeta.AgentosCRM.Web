(function () {
    $(function () {
        var _$CategoriesTable = $('#Categorytable');
        var _taskCategoriesService = abp.services.app.taskCategories;

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
                getCategories();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getCategories();
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
                getCategories();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getCategories();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.TaskCategories.Create'),
            edit: abp.auth.hasPermission('Pages.TaskCategories.Edit'),
            delete: abp.auth.hasPermission('Pages.TaskCategories.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/TaskCategory/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/TaskCategory/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditTaskCategoryModal',
        });
      
        var _viewCategoryModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/TaskCategory/ViewCategoryModal',
            modalClass: 'ViewCategoryModal',
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

        var dataTable = _$CategoriesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _taskCategoriesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#TaskCategoriesTableFilter').val(),
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
                                    _viewCategoryModal.open({ id: data.record.taskCategory.id });
                                },
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.taskCategory.id });
                                },
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteMasterCategory(data.record.taskCategory);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'taskCategory.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'taskCategory.name',
                    name: 'name',
                },
            ],
        });

        function getCategories() {
            dataTable.ajax.reload();
        }

        function deleteMasterCategory(taskCategory) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _taskCategoriesService
                        .delete({
                            id: taskCategory.id,
                        })
                        .done(function () {
                            getCategories(true);
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

        $('#CreateNewCategoryButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _taskCategoriesService
                .getMasterCategoriesToExcel({
                    filter: $('#TaskCategoriesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditTaskCategoryModalSaved', function () {
            getCategories();
        });

        $('#GetCategoriesButton').click(function (e) {
            e.preventDefault();
            getCategories();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getMasterCategories();
            }
        });

        $('.reload-on-change').change(function (e) {
            getCategories();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getCategories();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getCategories();
        });
    });
})();
