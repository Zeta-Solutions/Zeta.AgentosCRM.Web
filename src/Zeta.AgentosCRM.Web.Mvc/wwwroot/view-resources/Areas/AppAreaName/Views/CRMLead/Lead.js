(function () {
    $(function () {
        var _$LeadTable = $('#LeadTable');
        var _LeadDetailservice = abp.services.app.leadDetail;
        var _LeadHeadervice = abp.services.app.leadHead;
        //...
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
                getdegreeLevels();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getdegreeLevels();
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
                getdegreeLevels();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getdegreeLevels();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.DegreeLevels.Create'),
            edit: abp.auth.hasPermission('Pages.DegreeLevels.Edit'),
            delete: abp.auth.hasPermission('Pages.DegreeLevels.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DegreeLevel/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DegreeLevel/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditDegreeLevelModal',
        });

        var _viewDegeeLevelModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DegreeLevel/ViewDegreeLevelModal',
            modalClass: 'ViewDegreeLevelModal',
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

        var dataTable = _$LeadTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _LeadHeadervice.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#DegreeLevelsTableFilter').val(),
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
                    data: 'leadHead.formName',
                    name: 'FormName',
                },
                {
                    width: 200,
                    targets: 2,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {




                        return `
    <div class="d-flex align-items-center">
        <div class="d-flex flex-column">
        <a href="https://localhost:44302/AppAreaName/Leads/CreateLead?FormName=${row.leadHead.formName}&id=${row.leadHead.id}" target="_blank">${'https://localhost:44302/AppAreaName/Leads/CreateLead?FormName=' + row.leadHead.formName + '&id=' + row.leadHead.id}</a>
             <i class="fas fa-copy copy-url-btn Copy" title="Copy URL"></i>
        </div>
    </div>
`;
                    },



                    name: 'concatenatedData',

                },
                {
                    targets: 3,
                    data: 'leadOrganizationUnitName',
                    name: 'LeadOrganizationUnitNameFk',
                },
                {
                    targets: 4,
                    data: 'leadLeadSourceName',
                    name: 'LeadLeadSourceNameFk',
                },
                {
                    targets: 5,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.leadHead.id;
                        var rowData = data.leadHead;
                        var RowDatajsonString = JSON.stringify(rowData);

                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            //'<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
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
        $(document).on('click', '.Copy', function (e) {
            e.preventDefault();
            debugger

            var rowId = $(this).data('id');
            var url = $(this).prev('a').text();

            // Create a dummy textarea
            var dummyTextarea = document.createElement("textarea");

            // Set the value of the textarea to the URL
            dummyTextarea.value = url;

            // Append the textarea to the document
            document.body.appendChild(dummyTextarea);

            // Select the text inside the textarea
            dummyTextarea.select();
            dummyTextarea.setSelectionRange(0, 99999); // For mobile devices

            // Copy the text inside the textarea to the clipboard
            document.execCommand("copy");

            // Remove the dummy textarea from the document
            document.body.removeChild(dummyTextarea);

            // Alert the copied text
            alert("Copied the text: " + url);
        });

        function getdegreeLevels() {
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
                window.location = "/AppAreaName/Leads/DetailsForm/" + rowId;
                    
            } else if (action === 'edit') {
                window.location = "/AppAreaName/Leads/LeadAllFields/" + rowId;
                
            } else if (action === 'delete') {
                deleteCRMLead(rowId);
            }
        });

        function deleteCRMLead(degreeLevel) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _LeadDetailservice
                        .delete({
                            id: degreeLevel.id,
                        })
                        .done(function () {
                            getdegreeLevels(true);
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

        $('#CreateNewLeadButton').click(function () {
            //_createOrEditModal.open();
            window.location.href = abp.appPath + 'AppAreaName/Leads/LeadAllFields';
        });

        //$('#ExportToExcelButton').click(function () {
        //    _LeadDetailservice
        //        .getDegreeLevelsToExcel({
        //            filter: $('#DegreeLevelsTableFilter').val(),
        //            abbrivationFilter: $('#AbbrivationFilterId').val(),
        //            nameFilter: $('#NameFilterId').val(),
        //        })
        //        .done(function (result) {
        //            app.downloadTempFile(result);
        //        });
        //});

        abp.event.on('app.createOrEditDegeeLevelModalSaved', function () {
            getdegreeLevels();
        });

        $('#GetDegreeLevelButton').click(function (e) {
            e.preventDefault();
            getdegreeLevels();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getMasterCategories();
            }
        });

        $('.reload-on-change').change(function (e) {
            getdegreeLevels();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getdegreeLevels();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getdegreeLevels();
        });
    });
})();
