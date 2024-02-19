(function () {
    $(function () {
        var _$invoiceTable = $('#Invoicestable');
        var _invoiceheadServices = abp.services.app.invoiceHead;

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
                getclientInterstedService();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getclientInterstedService();
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
                getclientInterstedService();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getclientInterstedService();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateInvoiceTypeModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Invoice/_InvoiceTypeModal.js',
            modalClass: 'CreateOrEditInvoiceTypeModal',
        });


        var getDateFilter = function (element) {
            if ($selectedDate.startDate == null) {
                return null;
            }
            return $selectedDate.startDate.format('YYYY-MM-DDT00:00:00Z');
        };
        //..
        var getMaxDateFilter = function (element) {
            if ($selectedDate.endDate == null) {
                return null;
            }
            return $selectedDate.endDate.format('YYYY-MM-DDT23:59:59Z');
        };
        var hiddenfield = $('input[name="Clientid"]').val();
        var dataTable = _$invoiceTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _invoiceheadServices.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#InvoicesTableFilter').val(),
                        clientIdFilter: hiddenfield,
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
                    data: 'invoiceHead.invoiceNo',
                    name: 'invoiceNo',
                },
                {
                    targets: 2,

                    data: 'invoiceHead.invoiceDate',
                    name: 'invoiceDate',
                    //data: 'application.work',
                    //name: 'partnerPartnerName',
                    render: function (invoiceDate) {
                        if (invoiceDate) {
                            return moment(invoiceDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 3,
                    //data: 'application.productName',
                    //name: 'productName',

                    data: 'invoiceHead.workflow',
                    name: 'invoiceHead.workflow',
                },
                {
                    targets: 4,
                    data: 'invoiceHead.totalAmount',
                    name: 'totalAmount',
                },
                {
                    targets: 5,
                    data: 'invoiceHead.discountGivenToClient',
                    name: 'discountGivenToClient',
                    
                },
                {
                    targets: 6,
                    data: 'invoiceHead.totalIncome',
                    name: 'totalIncome',
                    
                },
                {
                    targets: 7,
                    data: 'invoiceHead.status',
                    name: 'status',

                },
                {
                    width: 30,
                    targets: 7,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                        var rowId = data.invoiceHead.id;
                        var rowData = data.invoiceHead;
                        var RowDatajsonString = JSON.stringify(rowData);
                        var contaxtMenu = '<div class="context-menu Applicationmenu" style="position:relative;">' +
                            '<div class="Serviceellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 4px 4px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action2="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action2='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contaxtMenu;
                    }


                },

            ],
        });

        function getclientInterstedService() {
            dataTable.ajax.reload();
        }
        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.Serviceellipsis', function (e) {
            e.preventDefault();
            debugger
            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });

        // Close the contextcontext menu when clicking outside of it
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });
        $(document).on('click', 'a[data-action2]', function (e) {
            e.preventDefault();
            debugger
            var rowId = $(this).data('id');
            var action = $(this).data('action2');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'edit') {
                $(".loader").show();
                _createOrEditModal.open({ id: rowId });

            } else if (action === 'delete') {
                console.log(rowId);
                deleteclientInterstedService(rowId);
            }
        });
        function deleteclientInterstedService(clientInterstedService) {
            debugger
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _invoiceheadServices
                        .delete({
                            id: clientInterstedService.id,
                        })
                        .done(function () {
                            getclientInterstedService(true);
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

        $('#General').click(function () {
            $("#invoicetype").val(0);
            _createOrEditModal.open();
        });
        $('#Commission').click(function () {
            $("#invoicetype").val(1);
            _createOrEditModal.open();
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

        abp.event.on('app.createOrEditInterstedInformationFormModalSaved', function () {
            getclientInterstedService();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getclientInterstedService();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getclientInterstedService();
            }
        });

        $('.reload-on-change').change(function (e) {
            getclientInterstedService();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getclientInterstedService();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getclientInterstedService();
        });
    });
})(jQuery);
