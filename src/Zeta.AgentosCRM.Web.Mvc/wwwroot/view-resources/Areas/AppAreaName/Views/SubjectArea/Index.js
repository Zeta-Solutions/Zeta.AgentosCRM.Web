(function () {
    $(function () {
        var _$SubjectAreaTable = $('#SubjectAreatable');
        var _subjectAreasService = abp.services.app.subjectAreas;

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
                getSubjectAreas();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getSubjectAreas();
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
                getSubjectAreas();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getSubjectAreas();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.SubjectAreas.Create'),
            edit: abp.auth.hasPermission('Pages.SubjectAreas.Edit'),
            delete: abp.auth.hasPermission('Pages.SubjectAreas.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/SubjectArea/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/SubjectArea/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditSubjectAreaModal',
        });
      
        var _viewDegeeLevelModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/SubjectArea/ViewSubjectAreaModal',
            modalClass: 'ViewSubjectAreaModal',
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

        var dataTable = _$SubjectAreaTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _subjectAreasService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SubjectAreasTableFilter').val(),
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
                                    _viewDegeeLevelModal.open({ id: data.record.subjectArea.id });
                                },
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.subjectArea.id });
                                },
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteMasterCategory(data.record.subjectArea);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'subjectArea.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'subjectArea.name',
                    name: 'name',
                },
            ],
        });

        function getSubjectAreas() {
            dataTable.ajax.reload();
        }

        function deleteMasterCategory(subjectArea) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _subjectAreasService
                        .delete({
                            id: subjectArea.id,
                        })
                        .done(function () {
                            getSubjectAreas(true);
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
            _subjectAreasService
                .getSubjectAreasToExcel({
                    filter: $('#SubjectAreasTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditSubjectAreasModalSaved', function () {
            
            getSubjectAreas();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getSubjectAreas();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getSubjectAreas();
            }
        });

        $('.reload-on-change').change(function (e) {
            getSubjectAreas();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getSubjectAreas();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getSubjectAreas();
        });
    });
})();
