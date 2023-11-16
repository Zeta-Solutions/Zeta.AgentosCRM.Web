(function () {
    $(function () {
        var _$ProductTypeTable = $('#ProductTypeTable');
        var _productTypesService = abp.services.app.productTypes;

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };
        debugger
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
                getProductTypes();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getProductTypes();
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
                getProductTypes();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getProductTypes();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ProductTypes.Create'),
            edit: abp.auth.hasPermission('Pages.ProductTypes.Edit'),
            delete: abp.auth.hasPermission('Pages.ProductTypes.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ProductType/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ProductType/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditProductTypeModal',
        });
        var _viewProductTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ProductType/ViewProductTypeModal',
            modalClass: 'ViewProductTypeModal',
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
        var dataTable = _$ProductTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _productTypesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#MasterCategoriesTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        masterCategoryNameFilter: $('#MasterCategoryNameFilterId').val(),

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
                    targets: 1,
                    data: 'productType.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 2,
                    data: 'productType.name',
                    name: 'name',
                },

                {
                    targets: 3,
                    data: 'masterCategoryName',
                    name: 'masterCategoryFk.name',
                },
                {
                    width: 30,
                    targets: 4,
                    data: null,
                    orderable: false,
                    searchable: false,

                    //render: function (data, type, full, meta) {
                    //    //return '<td> <div class="context-menu">' +
                    //    //    '< div class="dots d-flex flex-column " > <span style="font-weight: bold; font-size: 1.2em;">...</span></div >' +
                    //    //    '<div class="options left">' +
                    //    //    '<ul>' +
                    //    //    '<li>Edit</li>' +
                    //    //    '<li>Delete</li>' +
                    //    //    '<!-- Add other options as needed -->' +
                    //    //    '</ul>' +
                    //    //    '</div>' +
                    //    //    '</div >' +
                    //    //    '</td >';
                    //    return '<a href="#" class="ellipsis" data-id="' + data.productType.id + '"><span class="fa fa-ellipsis-v"></span></a>';
                    //}
                    render: function (data, type, full, meta) {

                        var rowId = data.productType.id;
                        var rowData = data.productType;
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


                }
            ],
        });
        function getProductTypes() {
            dataTable.ajax.reload();
        }
         
        $(document).on('click', '.ellipsis', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();



            //// Toggle the visibility of the options when the ellipsis is clicked
            //var options = $(this).next('.options');
            //options.toggle();
        });
         
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });
         
        $(document).on('click', 'a[data-action]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action');
             
            if (action === 'view') {
                _viewProductTypeModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                deleteProductType(rowId);
            }
        });



        //// Add a click event handler for the ellipsis anchors
        //$(document).on('click', 'a.ellipsis', function (e) {
        //    e.preventDefault(); // Prevent the anchor from navigating

        //    // Get the row ID from the data attribute
        //    //var dataid = $(this).dataset.id;
        //    var rowId = $(this).data('id');

        //    debugger
        //    // Create a context menu with edit and delete options
        //    // Create a context menu with edit and delete options
        //    var contextMenu = $('<ul class="context-menu" style="list-style: none;">' +
        //        '<li><a href="#" data-action="view">View</a></li>' +
        //        '<li><a href="#" data-action="edit">Edit</a></li>' +
        //        '<li><a href="#" data-action="delete">Delete</a></li>' +
        //        '</ul>');


        //    // Position the context menu near the click event
        //    contextMenu.css({
        //        position: 'absolute',
        //        left: e.pageX + 'px',
        //        top: e.pageY + 'px'
        //    });

        //    // Append the context menu to the document
        //    $('body').append(contextMenu);

        //    // Handle menu item clicks
        //    contextMenu.on('click', 'a[data-action]', function () {
        //        debugger
        //        var action = $(this).data('action');
        //        contextMenu.remove(); // Remove the context menu
        //        handleContextMenuAction(rowId, action); // Handle the selected action
        //    });

        //    // Close the context menu when clicking outside of it
        //    $(document).on('click', function (event) {
        //        if (!$(event.target).closest('.context-menu').length) {
        //            contextMenu.remove();
        //        }
        //    });
        //});

        //function handleContextMenuAction(rowId, action) {
        //    debugger
        //    // Implement your logic for the selected action based on the rowId
        //    // For example, open a modal for editing or perform a delete operation
        //    if (action =="view") {

        //        _viewProductTypeModal.open({ id: rowId });
        //    }
        //    if (action == "edit") {
        //        _createOrEditModal.open({ id: rowId });

        //    }
        //    if (action =="delete") {

        //        deleteProductType(rowId);
        //    }
        //    console.log('Action: ' + action + ' for row ID: ' + rowId);
        //}



        function deleteProductType(productType) {
            debugger
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _productTypesService
                        .delete({
                            id: productType.id,
                        })
                        .done(function () {
                            getProductTypes(true);
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

        $('#CreateNewProductTypeButton').click(function () {
            debugger
            _createOrEditModal.open();
        });

        //$('#ExportToExcelButton').click(function () {
        //    _productTypesService
        //        .getMasterCategoriesToExcel({
        //            filter: $('#MasterCategoriesTableFilter').val(),
        //            abbrivationFilter: $('#AbbrivationFilterId').val(),
        //            nameFilter: $('#NameFilterId').val(),
        //            masterCategoryNameFilter: $('#MasterCategoryNameFilterId').val(),

        //        })
        //        .done(function (result) {
        //            app.downloadTempFile(result);
        //        });
        //});

        abp.event.on('app.createOrEditProductTypeModalSaved', function () {

            getProductTypes();
        });

        $('#GetPartnerTypesButton').click(function (e) {
            e.preventDefault();
            getProductTypes();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getProductTypes();
            }
        });

        $('.reload-on-change').change(function (e) {
            getProductTypes();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getProductTypes();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getProductTypes();
        });
    });
})();
