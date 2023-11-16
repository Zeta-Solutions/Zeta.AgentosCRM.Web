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
                //                    _viewCountryModal.open({ id: data.record.country.id });
                //                },
                //            },
                //            {
                //                text: app.localize('Edit'),
                //                visible: function () {
                //                  return _permissions.edit;
                //                },
                //                action: function (data) {
                //                    _createOrEditModal.open({ id: data.record.country.id });
                //                },
                //            },
                //            {
                //                text: app.localize('Delete'),
                //                visible: function () {
                //                  return _permissions.delete;
                //                },
                //                action: function (data) {
                //                    deleteCountries(data.record.country);
                //                },
                //            },
                //        ],
                //    },
                //},
                {
                    targets: 1,
                    data: 'country.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'country.name',
                    name: 'name',
                },
                {
                    targets: 3,
                    data: 'country.code',
                    name: 'code',
                },
                {
                    targets: 4,
                    data: 'country.icon',
                    name: 'icon',
                },
                {
                    targets: 5,
                    data: 'regionName',
                    name: 'RegionFk.name',
                },
                {
                    targets: 6,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.country.id;
                        var rowData = data.country;
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

        function getCountries() {
            dataTable.ajax.reload();
        }


        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis', function (e) {
            e.preventDefault();
            debugger
            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });

        // Close the context menu when clicking outside of it
        $(document).on('click', function (event) {
            debugger
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
                _viewCountryModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteCountries(rowId);
            }
        });

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

        //$('#ExportToExcelButton').click(function () {
        //    _masterCategoriesService
        //        .getMasterCategoriesToExcel({
        //            filter: $('#CountryTableFilter').val(),
        //            abbrivationFilter: $('#AbbrivationFilterId').val(),
        //            nameFilter: $('#NameFilterId').val(),
        //            codeFilter: $('#CodeFilterId').val(),
        //            iconFilter: $('#IconFilterId').val(),
        //            regionNameFilter: $('#RegionNameFilterId').val(),
        //        })
        //        .done(function (result) {
        //            app.downloadTempFile(result);
        //        });
        //});

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
