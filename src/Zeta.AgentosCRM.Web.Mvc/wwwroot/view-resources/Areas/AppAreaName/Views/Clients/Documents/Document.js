(function () {
    //alert("clientAppointment");
    $(function () {
        var _$Documentstable = $('#Documentstable');
        var _clientDocumentsService = abp.services.app.clientAttachments;
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
                getClientDocuments();
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
                getClientDocuments();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getClientDocuments();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Subjects.Create'),
            edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
            delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateDocuments',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/Documents/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditDocumentsModal',
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
        var hiddenfield = $('input[name="Clientid"]').val();
        var dataTable = _$Documentstable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientDocumentsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#NameFilterId').val(),
                        TimeZoneFilter: $('#TimeZoneFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        clientIdFilter: hiddenfield,
                    };
                },
            },
            columnDefs: [
                {
                    className: 'responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0,
                },
                {
                    width: 200,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        

                        let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.clientAttachment.attachmentId}`;
                       
                        return `
    <div class="d-flex align-items-center">
        <div class="d-flex flex-column">
        <a href="/AppAreaName/DocumentAttachment/DownloadFile?fileId=${row.clientAttachment.attachmentId}&fileName=${row.clientAttachment.name}" target="_blank">${row.clientAttachment.name}</a>
            
        </div>
    </div>
`;
                    },



                    name: 'concatenatedData',

                },

                //{
                //    targets: 1,
                //    data: 'clientAttachment.name',
                //    name: 'name',
                //    render: function (data, type, row) {
                //        // Assuming 'clientAttachment.attachmentId' is a property of the row data
                //        return `<input type="hidden" class="attachmentId" value="${row.clientAttachment.attachmentId}" /> ${data}`;
                //    }
                //},
                {
                    targets: 2,
                    data: 'userName',
                    name: 'UserName',
                },
                {
                    targets: 3,
                    data: 'clientAttachment.creationTime',
                    name: 'CreationTime',
                    render: function (creationTime) {
                        if (creationTime) {
                            return moment(creationTime).format('L');
                        }
                        return "";
                    }
                },
                {
                    width: 30,
                    targets: 4,
                    data: null,
                    orderable: false,
                    searchable: false,


                    render: function (data, type, full, meta) {
                        //console.log(data);
                        var rowId = data.clientAttachment.id;
                        //console.log(rowId);
                        var rowData = data.clientAttachment;
                        var RowDatajsonString = JSON.stringify(rowData);
                        //console.log(RowDatajsonString);

                        var contextMenu = '<div class="context-menu" style="position:relative;">'+
                            '<div class="ellipsis201"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            /* '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +*/
                            /*'<a href="#" style="color: black;" data-action20="edit" data-id="' + rowId + '"><li>Edit</li></a>' +*/
                            "<a href='#' style='color: black;' data-action201='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }


                },

            ],
        });
        $(document).on('click', '.ellipsis201', function (e) {
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
        $(document).on('click', 'a[data-action201]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action201');
            debugger
            // Handle the selected action based on the rowId
            // Handle the selected action based on the rowId
            if (action === 'edit') {
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                // console.log(rowId);
                deleteDocuments(rowId);
            }
        });
        function getClientDocuments() {
            dataTable.ajax.reload();
        }

        function deleteDocuments(clientAttachment) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _clientDocumentsService
                        .removeAttachmentIdFile({
                            id: clientAttachment.id,
                        })
                        .done(function () {
                            getClientDocuments(true);
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

        $('#CreateNewDocumentsButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _clientDocumentsService
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

        abp.event.on('app.createOrEditClientDocumentsModalSaved', function () {
            getClientDocuments();
        });

        $('#GetSubjectAreaButton').click(function (e) {
            e.preventDefault();
            getClientDocuments();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getClientDocuments();
            }
        });

        $('.reload-on-change').change(function (e) {
            getClientDocuments();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getClientDocuments();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getClientDocuments();
        });

    });
})(jQuery);
