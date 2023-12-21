(function () {
    $(function () {
        var _$workflowsTable = $('#WorkflowsTable');
        var _workflowsService = abp.services.app.workflows;

        var _workflowStepsService = abp.services.app.workflowSteps;
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
                getWorkflows();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getWorkflows();
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
                getWorkflows();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getWorkflows();
            });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Workflows.Create'),
            edit: abp.auth.hasPermission('Pages.Workflows.Edit'),
            delete: abp.auth.hasPermission('Pages.Workflows.Delete'),
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Workflows/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Workflows/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditWorkflowModal',
        });

        var _viewWorkflowModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Workflows/ViewworkflowModal',
            modalClass: 'ViewWorkflowModal',
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

        var dataTable = _$workflowsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _workflowsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#WorkflowsTableFilter').val(),
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
                    className: 'details-control',
                    targets: 1,
                    orderable: false,
                    autoWidth: false,
                    visible: abp.auth.hasPermission('Pages.WorkflowSteps'),
                    render: function () {
                        return `<button class="btn btn-primary btn-xs Edit_WorkflowStep_WorkflowId">${app.localize(
                            'EditWorkflowStep'
                        )}</button>`;
                    },
                    visible: false,
                },
                {
                    targets: 2,
                    data: 'workflow.name',
                    name: 'name',
                },
                {
                    targets: 3,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.workflow.id;
                        var rowData = data.workflow;
                        var RowDatajsonString = JSON.stringify(rowData);
                        console.log(RowDatajsonString);
                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
                            '<a href="#" style="color: black;" data-action="edit" class="Edit_WorkflowStep_WorkflowIdTest" data-id="' + rowId + '"><li>Edit</li></a>' +
                            "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }

                },
            ],
        });

        function getWorkflows() {
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
   
            // Handle the selected action based on the rowId
            if (action === 'view') {
                _viewWorkflowModal.open({ id: rowId });
            } else if (action === 'edit') {
             
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'delete') {
                console.log(rowId);
                deleteWorkflow(rowId);
            }
        });

        function deleteWorkflow(workflow) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _workflowsService
                        .delete({
                            id: workflow.id,
                        })
                        .done(function () {
                            getWorkflows(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            });
        }

         

        //// Attach a click event handler to the button with the specified class
        //$(document).on('click', '.Edit_WorkflowStep_WorkflowIdTest', function (event) {
        //    event.preventDefault();  
        //    debugger
        //    // Extract the data-id attribute from the clicked button
        //    var workflowId = $(this).data('id');

        //    // Make an AJAX call here
        //    $.ajax({
        //        url: '/AppAreaName/MasterDetailChild_Workflow_WorkflowSteps',
        //        method: 'GET',
        //        data: { workflowId: workflowId },
        //        success: function (response) {

        //        },
        //        error: function (error) { 

        //        }
        //    });
        //});

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

        $('#CreateNewWorkflowButton').click(function () {
            _createOrEditModal.open();
        });

        abp.event.on('app.createOrEditWorkflowModalSaved', function () {
            getWorkflows();
        });

        $('#GetWorkflowsButton').click(function (e) {
            e.preventDefault();
            getWorkflows();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getWorkflows();
            }
        });

        $('.reload-on-change').change(function (e) {
            getWorkflows();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getWorkflows();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getWorkflows();
        });

        var currentOpenedDetailRow;
        function openDetailRow(e, url) {
            var tr = $(e).closest('tr');
            var row = dataTable.row(tr);

            if (row.child.isShown()) {
                row.child.hide();
                tr.removeClass('shown');
                currentOpenedDetailRow = null;
            } else {
                if (currentOpenedDetailRow) currentOpenedDetailRow.child.hide();

                $.get(url).then((data) => {
                    debugger
                    row.child(data).show();
                    tr.addClass('shown');
                    currentOpenedDetailRow = row;
                });
            }
        }


        _$workflowsTable.on('click', '.Edit_WorkflowStep_WorkflowId', function () {
            var tr = $(this).closest('tr');
            var row = dataTable.row(tr);
            openDetailRow(this, '/AppAreaName/MasterDetailChild_Workflow_WorkflowSteps?WorkflowId=' + row.data().workflow.id);
        });
    });
})();
