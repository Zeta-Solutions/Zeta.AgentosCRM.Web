(function () {
    $(function () {
        var $Prioritytable = $('#Prioritytable');
        var _taskPrioritiesService = abp.services.app.taskPriorities;

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
                getPriorites();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getPriorites();
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
                getPriorites();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getPriorites();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.TaskPriorities.Create'),
            edit: abp.auth.hasPermission('Pages.TaskPriorities.Edit'),
            delete: abp.auth.hasPermission('Pages.TaskPriorities.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Taskpriority/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/priority/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditTaskPriorityModal',
        });

        var _viewPriorityModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Taskpriority/ViewPriorityModal',
            modalClass: 'ViewpriorityModal',
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

        var dataTable = $Prioritytable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _taskPrioritiesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#TaskPrioritesTableFilter').val(),
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
                                    _viewPriorityModal.open({ id: data.record.taskPriority.id });
                                },
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.taskPriority.id });
                                },
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteTaskPriority(data.record.taskPriority);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'taskPriority.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'taskPriority.name',
                    name: 'name',
                },
            ],
        });

        function getPriorites() {
            dataTable.ajax.reload();
        }

        function deleteTaskPriority(taskPriority) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _taskPrioritiesService
                        .delete({
                            id: taskPriority.id,
                        })
                        .done(function () {
                            getPriorites(true);
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

        $('#CreateNewButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _taskPrioritiesService
                .getTaskPrioritiesToExcel({
                    filter: $('#TaskPrioritesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditMasterCategoryModalSaved', function () {
            getPriorites();
        });

        $('#GetTaskPrioritiesButton').click(function (e) {
            e.preventDefault();
            getPriorites();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getPriorites();
            }
        });

        $('.reload-on-change').change(function (e) {
            getPriorites();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getPriorites();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getPriorites();
        });
    });
})();
