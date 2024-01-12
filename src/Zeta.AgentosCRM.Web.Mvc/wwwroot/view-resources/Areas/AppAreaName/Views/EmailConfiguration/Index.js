(function () {
    $(function () {
        var _$emailConfigurationTable = $('#EmailConfigurationTable');
        //var _masterCategoriesService = abp.services.app.masterCategories;
        var _emailConfigurationsService = abp.services.app.emailConfigurations;

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
                getEmailConfigurations();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getEmailConfigurations();
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
                getEmailConfigurations();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getEmailConfigurations();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
            edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
            delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EmailConfigurations/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EmailConfiguration/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEmailConfigurationModal',
        });

        var _viewFeeTypeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/FeeType/ViewFeeTypeModal',
            modalClass: 'ViewFeeTypeModal',
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
        var dataTable = _$emailConfigurationTable.DataTable({

            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _emailConfigurationsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#MasterCategoriesTableFilter').val(),
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
                    targets: 1,
                    data: 'emailConfiguration.name',
                    name: 'name',
                },
                {
                    targets: 2,
                    data: 'emailConfiguration.senderEmail',
                    name: 'SenderEmail',
                },
                {
                    targets: 3,
                    data: 'emailConfiguration.senderPassword',
                    name: 'SenderPassword',
                },
                {
                    targets: 4,
                    data: 'emailConfiguration.smtpServer',
                    name: 'SmtpServer',
                },
                {
                    targets: 5,
                    data: 'emailConfiguration.smtpPort',
                    name: 'smtpPort',
                },
                {
                    targets: 6,
                    data: 'emailConfiguration.protocol',
                    name: 'Protocol',
                },
                {
                    targets: 7,
                    data: 'emailConfiguration.isEnableSsl',
                    name: 'isEnableSsl',
                },
                {
                    targets: 8,
                    data: 'emailConfiguration.isActive',
                    name: 'IsActive',
                },
                {
                    targets: 9,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.emailConfiguration.id;
                        var rowData = data.emailConfiguration;
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
        function getEmailConfigurations() {
            dataTable.ajax.reload();
        }



        // Add a click event handler for the ellipsis icons
        $(document).on('click', '.ellipsis', function (e) {
            e.preventDefault();

            var options = $(this).closest('.context-menu').find('.options');
            var allOptions = $('.options');  // Select all options

            // Close all other open options
            allOptions.not(options).hide();

            // Toggle the visibility of the options
            options.toggle();
        });

        // Close the context menu when clicking outside of it
        $(document).on('click', function (event) {
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
               // _viewFeeTypeModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteEmailConfigurations(rowId);
            }
        });

        function deleteEmailConfigurations(emailConfiguration) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _emailConfigurationsService
                        .delete({
                            id: emailConfiguration.id,
                        })
                        .done(function () {
                            getEmailConfigurations(true);
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

        $('#CreateNewEmailConfiguration').click(function () {
            _createOrEditModal.open();
        });


        abp.event.on('app.createOrEditEmailConfigurationModalSaved', function () {
            debugger
            getEmailConfigurations();
        });

        $('#GetFeetypesButton').click(function (e) {
            e.preventDefault();
            getEmailConfigurations();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEmailConfigurations();
            }
        });

        $('.reload-on-change').change(function (e) {
            getEmailConfigurations();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getEmailConfigurations();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getEmailConfigurations();
        });
    });
})();
