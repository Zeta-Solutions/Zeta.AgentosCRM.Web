(function () {
   
    $(function () {
        var _$countryTable = $('#CountryTable');
        var _countriesService = abp.services.app.countries;

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
                getCountries();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getCountries();
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
                getCountries();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getCountries();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Countries.Create'),
            edit: abp.auth.hasPermission('Pages.Countries.Edit'),
            delete: abp.auth.hasPermission('Pages.Countries.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Country/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Country/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditCountriesModal',
        });

        var _viewCountryModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Country/ViewCountryModal',
            modalClass: 'ViewCountryModal',
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

        var dataTable = _$countryTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _countriesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#CountryTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        codeFilter: $('#CodeFilterId').val(),
                        iconFilter: $('#IconFilterId').val(),
                        regionNameFilter: $('#RegionNameFilterId').val(),
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
                                    _viewCountryModal.open({ id: data.record.country.id });
                                },
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                  return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.country.id });
                                },
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                  return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteCountries(data.record.country);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'country.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'country.name',
                    name: 'name',
                },
                {
                    targets: 4,
                    data: 'country.code',
                    name: 'code',
                },
                {
                    targets: 5,
                    data: 'country.icon',
                    name: 'icon',
                },
                {
                    targets: 6,
                    data: 'regionName',
                    name: 'RegionFk.name',
                },
            ],
        });

        function getCountries() {
            dataTable.ajax.reload();
        }

        function deleteCountries(country) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _countriesService
                        .delete({
                            id: country.id,
                        })
                        .done(function () {
                            getCountries(true);
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

        $('#CreateNewCountryButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _masterCategoriesService
                .getMasterCategoriesToExcel({
                    filter: $('#CountryTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                    codeFilter: $('#CodeFilterId').val(),
                    iconFilter: $('#IconFilterId').val(),
                    regionNameFilter: $('#RegionNameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditCountryModalSaved', function () {
            getCountries();
        });

        $('#GetCountryButton').click(function (e) {
            e.preventDefault();
            getCountries();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getCountries();
            }
        });

        $('.reload-on-change').change(function (e) {
            getCountries();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getCountries();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getCountries();
        });
    });
})();
