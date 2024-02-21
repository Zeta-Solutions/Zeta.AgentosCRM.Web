(function () {
    $(function () {
         
        var _$PartnerContactTable = $('#ContactsTable');
        var _partnerContactsService = abp.services.app.partnerContacts;

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
                getPartnerContact();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getPartnerContact();
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
                getPartnerContact();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getPartnerContact();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.LeadSources.Create'),
        //    edit: abp.auth.hasPermission('Pages.LeadSources.Edit'),
        //    delete: abp.auth.hasPermission('Pages.LeadSources.Delete'),
        //};

        var _createOrEditContactsModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditContactslModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Contacts/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditContactsModal',
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

        var dataTable = _$PartnerContactTable.DataTable({
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
                ajaxFunction: _partnerContactsService.getAll,
                inputFilter: function () {
                    return {
                 
                        partnerIdFilter: ContactPartnerValue,
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
                //{
                //  width: 120,
                //  targets: 1,
                //  data: null,
                //  orderable: false,
                //  autoWidth: false,
                //  defaultContent: '',
                //  rowAction: {
                //    cssClass: 'btn btn-brand dropdown-toggle',
                //    text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                //    items: [
                //      {
                //        text: app.localize('View'),
                //        action: function (data) {
                //            _viewSubjectModal.open({ id: data.record.subject.id });
                //        },
                //      },
                //      {
                //        text: app.localize('Edit'),
                //        visible: function () {
                //          return _permissions.edit;
                //        },
                //        action: function (data) {
                //            _createOrEditModal.open({ id: data.record.subject.id,clientId: clientId});
                //        },
                //      },
                //      {
                //        text: app.localize('Delete'),
                //        visible: function () {
                //          return _permissions.delete;
                //        },
                //        action: function (data) {
                //            deletePartnerType(data.record.subject);
                //        },
                //      },
                //    ],
                //  },
                //},
                {
                    targets: 1,
                    data: 'partnerContact.name',
                    name: 'Name',
                },
                {
                    targets: 2,
                    data: 'partnerContact.email',
                    name: 'Email',
                },
                {
                    targets: 3,
                    data: 'partnerContact.department',
                    name: 'Department',
                },
                {
                    targets: 4,
                    data: 'partnerContact.position',
                    name: 'Position',
                },
                
                {
                    targets: 5,
                    data: 'branchName',
                    name: 'branchFk.name',
                },
                {
                    targets: 6,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) { 
                        var rowId = data.partnerContact.id;
                        var rowData = data.partnerContact;
                        var RowDatajsonString = JSON.stringify(rowData);

                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                           /* '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +*/
                            '<a href="#" style="color: black;" data-action1="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action1='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }

                },
            ],
        });

        function getPartnerContact() {
            dataTable.ajax.reload();
        }
        // Add a click event handler for the ellipsis icons


        // Close the context menu when clicking outside of it
        $(document).on('click', function (event) {
            if (!$(event.target).closest('.context-menu').length) {
                $('.options').hide();
            }
        });

        // Handle menu item clicks
        $(document).on('click', 'a[data-action1]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action1');
             
            // Handle the selected action based on the rowId
            if (action === 'view') {
                _viewFeeTypeModal.open({ id: rowId });
            } else if (action === 'edit') {
                _createOrEditContactsModal.open({ id: rowId });
            } else if (action === 'delete') {
           
                deletePartnerContact(rowId);
            }
        });
        function deletePartnerContact(PartnerContact) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _partnerContactsService
                        .delete({
                            id: PartnerContact.id,
                        })
                        .done(function () {
                            getPartnerContact(true);
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

        $('#CreateNewContactsButton').click(function () {
            _createOrEditContactsModal.open();
            //alert("clicked");
           // window.location.href = abp.appPath + 'AppAreaName/Partners/AddProductsDetails';
        });

        $('#ExportToExcelButton').click(function () {
            _partnerContactsService
                .getMasterCategoriesToExcel({
                    filter: $('#LeadSourcesTableFilter').val(),
                    abbrivationFilter: $('#AbbrivationFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditContactModalSaved', function () {
            getPartnerContact();
        });

        $('#GetLeadSourcesButton').click(function (e) {
            e.preventDefault();
            getPartnerContact();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getPartnerContact();
            }
        });

        $('.reload-on-change').change(function (e) {
            getPartnerContact();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getPartnerContact();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getPartnerContact();
        });

    });
})();
