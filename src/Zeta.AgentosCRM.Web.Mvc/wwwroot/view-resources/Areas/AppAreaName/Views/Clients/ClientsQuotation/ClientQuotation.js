(function () {
    $(function () {
        var _$clientQuotationHeadsTable = $('#ClientsQuotationtable');
        var _clientQuotationHeadsService = abp.services.app.clientQuotationHeads;
        var _clientQuotationDetailService = abp.services.app.clientQuotationDetails;

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
                getSubjects();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getSubjects();
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
                getSubjects();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getSubjects();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/CreateOrEditClientQuotationModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Client/ClientQuotation/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditClientQuotationModal',
        });
        var _createOrEditModalEmail = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/ClientEmailCompose',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Client/ApplicationClient/_CreateOrEditModal.js',
            modalClass: 'ClientEmailCompose',
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

        var dataTable = _$clientQuotationHeadsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientQuotationDetailService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SubjectsTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
                    };
                },
            },
            columnDefs: [
                {
                    className: ' responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0,
                },
             
                {
                    targets: 1,
                    data: 'clientQuotationDetail.description',
                    name: 'description',
                },
                {
                    targets: 2,
                    data: 'productName',
                    name: 'productNameFk.name',
                },
                {
                    targets: 3,
                    data: 'clientQuotationDetail.description',
                    name: 'description',
                },
                {
                    targets: 4,
                    data: 'clientQuotationDetail.description',
                    name: 'description',
                },
                {
                    targets: 5,
                    data: 'clientQuotationDetail.description',
                    name: 'description',
                },  {
                    targets: 6,
                    data: 'clientQuotationDetail.description',
                    name: 'description',
                },

            ],
        });

        function getSubjects() {
            dataTable.ajax.reload();
        }

        function deletePartnerType(subject) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _subjectsService
                        .delete({
                            id: subject.id,
                        })
                        .done(function () {
                            getSubjects(true);
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

        $('#CreateNewQuotation').click(function () {
            // _createOrEditModal.open();
        });

        $('#CreateNewQuotation').click(function () {
            debugger
            var hiddenfield = $("#ID").val();

            // Construct the URL with the data as a query parameter
            //window.location.href = "/AppAreaName/Clients/ClientQuotationDetailIndex/" + encodeURIComponent(0);
            window.location.href = "/AppAreaName/Clients/CreateOrEditClientQuotationModal";

            // Redirect to the constructed URL
            //window.location.href = url;
            // _createOrEditModal.open();


        });
        $('#ExportToExcelButton').click(function () {
            _subjectsService
                .getPartnerTypesToExcel({
                    filter: $('#SubjectsTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                    subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditPartnerTypeModalSaved', function () {
            getSubjects();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getSubjects();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getSubjects();
            }
        });

        $('.reload-on-change').change(function (e) {
            getSubjects();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getSubjects();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getSubjects();
        });
    });
})();
