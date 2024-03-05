(function () {
    $(function () {
        var _$invoiceTable = $('#ClientInvoicestable');
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
                    render: function (data, type, row) {
                       
                        if (data) {
                            //return moment(data).format('L');
                            var formattedDate = moment(data).format('L');
                          
                            if (row.invoiceHead.invoiceType == 1) {
                                return formattedDate + ' <span style="display: inline-block; padding: 2px 5px; border-radius: 5px; background-color:#009ef7; color: white;"><b>' + "Net" + '</b></span>';
                            }
                            else if (row.invoiceHead.invoiceType == 2) {
                                return formattedDate + ' <span style="display: inline-block; padding: 2px 5px; border-radius: 5px; background-color:#009ef7; color: white;"><b>' + "Gross" + '</b></span>';
                            }
                            else {
                                return formattedDate + ' <span style="display: inline-block; padding: 2px 5px; border-radius: 5px; background-color:#009ef7; color: white;"><b>' + "General" + '</b></span>';
                            }
                           
                        }
                       
                       
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
                    data: 'invoiceHead.totalAmountInclTax',
                    name: 'TotalAmountInclTax',
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
                    render: function (data, type, row) {
                        
                        
                            if (row.invoiceHead.status == true) {
                                return '<span style="display: inline-block; padding: 2px 5px; border-radius: 5px; background-color:#009ef7; color: white;"><b>' + "Paid" + '</b></span>';
                            } else {
                                return '<span style="display: inline-block; padding: 2px 5px; border-radius: 5px; background-color:#f1416c; color: white;"><b>' + "UnPaid" + '</b></span>';
                            }
                         
                    }
                },
                {
                    width: 30,
                    targets: 8,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                        debugger
                        var rowId = full.invoiceHead.id;
                        var rowData = data.invoiceHead;
                        var ApplicationId = full.invoiceHead.applicationId;
                        var invoicetypeId = full.invoiceHead.invoiceType;
                        var RowDatajsonString = JSON.stringify(rowData);
                        
                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<input type="hidden" class="applicationid" id="applicationid" value="' + ApplicationId + '"></input>' +
                            '<input type="hidden" class="invoicetypeid" id="invoicetypeid" value="' + invoicetypeId + '"></input>' +
                            '<div class="ellipsis666"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            /* '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +*/
                            '<a href="#" style="color: black;" data-action666="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action666='delete' data-id='" + rowId + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';
                        return contextMenu;
                    }


                },

            ],
        });

        function getInvoiceHeadService() {
            dataTable.ajax.reload();
        }
        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis666', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });

        // Handle menu item clicks
        $(document).on('click', 'a[data-action666]', function (e) {
            debugger
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action666');
            var contextMenu = $(this).closest('.context-menu');
            var invoicetypeId = contextMenu.find('.invoicetypeid').val();
            var ApplicationId = contextMenu.find('.applicationid').val();
            

            // Handle the selected action based on the rowId
            // Handle the selected action based on the rowId
            if (action === 'edit') {
                var baseUrl = "/AppAreaName/Clients/CreateOrEditInvoiceHeadModal/?Id=" + rowId;
                var url = baseUrl + "&ApplicationId=" + ApplicationId + "&InvoiceType=" + invoicetypeId;
              
                // Redirect to the constructed URL
                window.location.href = url;
            } else if (action === 'delete') {
                // console.log(rowId);
                deleteInvoiceHead(rowId);
            }
        });
        function deleteInvoiceHead(invoiceHead) {
            debugger
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _invoiceheadServices
                        .delete({
                            id: invoiceHead,
                        })
                        .done(function () {
                            getInvoiceHeadService(true);
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
