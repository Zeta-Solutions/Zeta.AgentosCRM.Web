(function () {
    $(function () {
        var _$LeadSourceTable = $('#ApplicationTable');
        var _leadSourcesService = abp.services.app.leadSources;

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
                getLeadSource();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getLeadSource();
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
                getLeadSource();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getLeadSource();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditAppModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Application/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditAppModal',
        });

        var _viewLeadSourceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/PartnersDetails',
            //modalClass: 'ViewPartnersDetails',
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

        var dataTable = _$LeadSourceTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _leadSourcesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#PartnersTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                    };
                },
            },
            columnDefs: [
                {
                    className: ' responsive',
                  /*  className: 'control responsive',*/
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0,
                },
                {
                    targets: 1, // The column index (zero-based) where you want to add the "View" button
                    data: 'leadSource.abbrivation',
                    name: 'abbrivation',
                    render: function (data, type, row) {
                        return '<a href="' + abp.appPath + 'AppAreaName/Partners/AddApplicationForm/' + row.leadSource.id + '" class="btn btn-primary">View</a>';
                    }
                },
                {
                    targets: 2,
                    data: 'leadSource.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'leadSource.name',
                    name: 'name',
                },
                {
                    targets: 4,
                    data: 'leadSource.name',
                    name: 'name',
                },
                {
                    targets: 5,
                    data: 'leadSource.name',
                    name: 'name',
                },
                {
                    targets: 6,
                    data: 'leadSource.name',
                    name: 'name',
                },
                {
                    targets: 7,
                    data: 'leadSource.name',
                    name: 'name',
                },
       
            ],
        });

        function getLeadSource() {
            dataTable.ajax.reload();
        }

        function deleteLeadSource(leadSource) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _leadSourcesService
                        .delete({
                            id: leadSource.id,
                        })
                        .done(function () {
                            getLeadSource(true);
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

        $('#CreateNewAppButton').click(function () {
            _createOrEditModal.open();
           // alert("clicked");
           // window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        });

        $('#ExportToExcelButton').click(function () {
            _leadSourcesService
                .getMasterCategoriesToExcel({
                    filter: $('#LeadSourcesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditLeadSourceModalSaved', function () {
            getLeadSource();
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            getLeadSource();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getLeadSource();
            }
        });

        $('.reload-on-change').change(function (e) {
            getLeadSource();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getLeadSource();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getLeadSource();
        });
    });
})();
