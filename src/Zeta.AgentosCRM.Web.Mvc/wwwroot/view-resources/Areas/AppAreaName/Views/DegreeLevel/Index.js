(function () {
    $(function () {
        var _$DegreeTable = $('#DegreeLeveltable');
        var _degreeLevelsService = abp.services.app.degreeLevels;

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
                getdegreeLevels();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getdegreeLevels();
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
                getdegreeLevels();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getdegreeLevels();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.DegreeLevels.Create'),
            edit: abp.auth.hasPermission('Pages.DegreeLevels.Edit'),
            delete: abp.auth.hasPermission('Pages.DegreeLevels.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DegreeLevel/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DegreeLevel/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditDegreeLevelModal',
        });
      
        var _viewDegeeLevelModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DegreeLevel/ViewDegreeLevelModal',
            modalClass: 'ViewDegreeLevelModal',
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

        var dataTable = _$DegreeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _degreeLevelsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#DegreeLevelsTableFilter').val(),
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
                                    _viewDegeeLevelModal.open({ id: data.record.degreeLevel.id });
                                },
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.degreeLevel.id });
                                },
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteMasterCategory(data.record.degreeLevel);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'degreeLevel.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'degreeLevel.name',
                    name: 'name',
                },
            ],
        });

        function getdegreeLevels() {
            dataTable.ajax.reload();
        }

        function deleteMasterCategory(degreeLevel) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _degreeLevelsService
                        .delete({
                            id: degreeLevel.id,
                        })
                        .done(function () {
                            getdegreeLevels(true);
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

        $('#CreateNewDegreeLevelButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _degreeLevelsService
                .getDegreeLevelsToExcel({
                    filter: $('#DegreeLevelsTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditMasterCategoryModalSaved', function () {
            getdegreeLevels();
        });

        $('#GetDegreeLevelButton').click(function (e) {
            e.preventDefault();
            getdegreeLevels();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getMasterCategories();
            }
        });

        $('.reload-on-change').change(function (e) {
            getdegreeLevels();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getdegreeLevels();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getdegreeLevels();
        });
    });
})();
