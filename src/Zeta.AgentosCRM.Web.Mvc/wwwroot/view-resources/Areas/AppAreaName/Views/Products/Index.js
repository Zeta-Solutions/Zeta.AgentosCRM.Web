(function () {
    $(function () {
        var _$ProductTable = $('#ProductsTable');
        var _productsService = abp.services.app.products;

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
                getProducts();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getProducts();
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
                getProducts();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getProducts();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/AddProducts',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Products/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditProductsModal',
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
        var hiddenfield = $("#PartnerId").val();
        var ContactPartnerValue = hiddenfield;
        var dataTable = _$ProductTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            //ajax: {
            //    url: abp.appPath + 'api/services/app/PartnerContacts/GetAll',
            //    data: {
            //        PartnerIdFilter: ContactPartnerValue,
            //    },
            //    method: 'GET',
            //    dataType: 'json',
            //},
            listAction: {
                ajaxFunction: _productsService.getAll,
                inputFilter: function () {
                    return {

                        partnerIdFilter: ContactPartnerValue,
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
                width: 100,
                targets: 1,
                data: null,
                orderable: false,
                autoWidth: false,
                defaultContent: '',
                // Assuming 'row' contains the client data with properties 'firstName', 'lastName', and 'email'
                render: function (data, type, row) {
                    //let firstNameInitial = row.product.name.charAt(0).toUpperCase();
                    ////let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                    //let initials = `${firstNameInitial}`;
                    let fullName = `${row.product.name}`;

                    // Generate the URLs using JavaScript variables
                    let clientDetailUrl = `/AppAreaName/Products/ProductsDetail?id=${row.product.id}`;

                    return `
    <div class="d-flex align-items-center">
        
        <div class="d-flex flex-column">
            <a href="${clientDetailUrl}" class="text-truncate" title="${fullName}">
                ${fullName}
            </a>
        </div>
    </div>
`;
                },

                name: 'concatenatedData',
				},
                //{
                //    targets: 1,
                //    data: 'product.name',
                //    name: 'Name',
                //},
                {
                    targets: 2,
                    data: 'partnerPartnerName',
                    name: 'PartnerPartnerName',
                },
                {
                    targets: 3,
                    data: 'partnerTypeName',
                    name: 'PartnerTypeName',
                },
                {
                    targets: 4,
                    data: 'branchName',
                    name: 'BranchName',
                },

                {
                    targets: 5,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.product.id;
                        var rowData = data.product;
                        var RowDatajsonString = JSON.stringify(rowData);

                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis20"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            /* '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +*/
                            '<a href="#" style="color: black;" data-action20="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action20='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }

                },
            ],
        });

        function getProducts() {
            dataTable.ajax.reload();
        }

        function deleteProducts(product) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _productsService
                        .delete({
                            id: product.id,
                        })
                        .done(function () {
                            getProducts(true);
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
        $('#CreateNewProductsButton').click(function () {
            // Get the value from the hidden field
            var hiddenfield = $("#PartnerId").val();

            // Construct the URL with the data as a query parameter
            var url = "/AppAreaName/Partners/AddProducts/?partnerId=" + encodeURIComponent(hiddenfield);

            // Redirect to the constructed URL
            window.location.href = url;
        });

        $('#ExportToExcelButton').click(function () {
            _productsService
                .getMasterCategoriesToExcel({
                    filter: $('#LeadSourcesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditProductModalSaved', function () {
            getProducts();
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            getProducts();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getProducts();
            }
        });

        $('.reload-on-change').change(function (e) {
            getProducts();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getProducts();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getProducts();
        });
        $(document).on('click', '.ellipsis20', function (e) {
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
        $(document).on('click', 'a[data-action20]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action20');
            debugger
            // Handle the selected action based on the rowId
            if (action === 'view') {
                _viewFeeTypeModal.open({ id: rowId });
            } else if (action === 'edit') {
                window.location = "/AppAreaName/Products/AddProducts/" + rowId;
            } else if (action === 'delete') {

                deleteProducts(rowId);
            }
        });
    });
})();
