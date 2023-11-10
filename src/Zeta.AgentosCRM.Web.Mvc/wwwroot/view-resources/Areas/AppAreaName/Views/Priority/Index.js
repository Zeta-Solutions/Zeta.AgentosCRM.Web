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
                //{
                //    width: 120,
                //    targets: 1,
                //    data: null,
                //    orderable: false,
                //    autoWidth: false,
                //    defaultContent: '',
                //    rowAction: {

                //        cssClass: 'btn btn-brand dropdown-toggle',
                //        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                //        items: [

                //            {
                //                text: app.localize('View'),
                                
                //                action: function (data) {
                //                    _viewPriorityModal.open({ id: data.record.taskPriority.id });
                //                },
                //            },
                //            {
                //                text: app.localize('Edit'),
                //                visible: function () {
                //                    return _permissions.edit;
                //                },
                //                action: function (data) {
                //                    _createOrEditModal.open({ id: data.record.taskPriority.id });
                //                },
                //            },
                //            {
                //                text: app.localize('Delete'),
                //                visible: function () {
                //                    return _permissions.delete;
                //                },
                //                action: function (data) {
                //                    deleteTaskPriority(data.record.taskPriority);
                //                },
                //            },
                //        ],
                //    },
                //},
                {
                    targets: 1,
                    data: 'taskPriority.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'taskPriority.name',
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
                        var rowId = data.taskPriority.id;
                        var rowData = data.taskPriority;
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

        function getPriorites() {
            dataTable.ajax.reload();
        }


        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });

        // Close the context menu when clicking outside of it
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
            debugger
            // Handle the selected action based on the rowId
            if (action === 'view') {
                _viewPriorityModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteTaskPriority(rowId);
            }
        });

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

        abp.event.on('app.CreateOrEditTaskPriorityModalSaved', function () {
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
