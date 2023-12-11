
(function () {
    $(function () {
        const urlParams = new URLSearchParams(window.location.search);
        const DocumentCheckListIdValue = urlParams.get('id');
        const WorkFlowidIdValue = urlParams.get('WorkFlowid');
        $("#DocumentCheckListID").val(DocumentCheckListIdValue);
        $("#WorkFlowDocumentID").val(WorkFlowidIdValue);
        var DocumentCheckListID = $("#DocumentCheckListID").val();
        var workflowId = $("#WorkFlowDocumentID").val();
        debugger
        var _$FeeTypeTable = $('#LeadFormTable');
        var _workflowDocumentsService = abp.services.app.workflowDocuments;
        var _workflowStepDocumentCheckListsService = abp.services.app.workflowStepDocumentCheckLists;
        var _workflowStepsService = abp.services.app.workflowSteps;
        if (DocumentCheckListID > 0) {
            $.ajax({
                url: abp.appPath + 'api/services/app/WorkflowDocuments/GetAll',
                data: {
                    ID: DocumentCheckListID,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    var TotalRecord = data.result.items;
                    workflowId = TotalRecord[0].workflowDocument.workflowId;
                    //$("#WorkFlowDocumentID").val(TotalRecord[0].workflowDocument.workflowId)
                    $("#WorkFlowDocumentName").val(TotalRecord[0].workflowDocument.name)

                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        if (workflowId > 0) {
            // Set to store processed WorkflowStepIDs
            var processedWorkflowStepIDs = new Set();

            $.ajax({
                url: abp.appPath + 'api/services/app/WorkflowSteps/GetAll',
                data: {
                    WorkflowIdFilter: workflowId,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    var totalRecord = data.result.items;
                 
                    $.each(totalRecord, function (index, item) {
                        var WorkflowStepID = item.workflowStep.id;
                        var WorkflowStepName = item.workflowStep.name;

                        // Check if WorkflowStepID is already processed
                        if (!processedWorkflowStepIDs.has(WorkflowStepID)) {
                            processedWorkflowStepIDs.add(WorkflowStepID);

                            //var workFlowStep = ` 
                            //    <div class="RunTimeDivCreate"></div> `;
                            //$(".WorkFlowStepDetail").append(workFlowStep);
                            
                            if (WorkflowStepID > 0) {
                                WorkflowStepId = WorkflowStepID;

                                $.ajax({
                                    url: abp.appPath + 'api/services/app/WorkflowStepDocumentCheckLists/GetAll',
                                    data: {
                                        WorkflowIdFilter: WorkflowStepId,
                                    },
                                    method: 'GET',
                                    dataType: 'json',
                                })
                                    .done(function (data) {
                                        var CheckListRecord = data.result.items;
                                       debugger
                                        var ListOfCheckList = "";
                                        var DocumentWorkflowStepID = 0;
                                        
                                        var NewCheckListRecord = `
                            <div class="col-12 timeline-item">  
                                    <span class="WorkFlowStepId" hidden  style="">${WorkflowStepID}</span>
                                    <span><b>${WorkflowStepName}</b></span><br> `;
                                        ListOfCheckList += NewCheckListRecord;
                                        $.each(CheckListRecord, function (index, item) {
                                            DocumentWorkflowStepID = item.workflowStepDocumentCheckList.workflowStepId; 

                                            var CheckPartner = item.workflowStepDocumentCheckList.isForAllPartners; debugger
                                            if (WorkflowStepID == DocumentWorkflowStepID) {
                                                var NewCheckListRecord = ` 
                                        <span class="DocumentCheckList" style=""><input type="text" hidden id="WorkFlowRowId" value="`+ item.workflowStepDocumentCheckList.id+`"/></span>
                                        <span class="" id="workFlowStepId" hidden style="">${item.workflowStepDocumentCheckList.workflowStepId}</span>
                                        <span class="" hidden style="">${item.workflowStepDocumentCheckList.documentTypeId}</span>
                                        <span>${item.workflowStepDocumentCheckList.description}</span> 
                                         <a href="#" style="float: right;margin-right: 5px;" data-action="Delete" data-id="`+ item.workflowStepDocumentCheckList.id +`"><i class="fa fa-trash" style="font-size: 15px;"></i></a>
                                         <a href="#" style="float: right;margin-right: 5px;" data-action="edit" data-id="`+ item.workflowStepDocumentCheckList.id + `"><i class="fa fa-edit" style="font-size: 15px;"></i></a>`
                                                if (CheckPartner == false) {
                                                    NewCheckListRecord += `<span style="float: right;margin-right: 5px;">All Partner</span>`
                                                } else {
                                                    NewCheckListRecord += `<span style="float: right;margin-right: 5px;">Selected Partner</span>`
                                                } NewCheckListRecord+=`<br> `;
                                                ListOfCheckList += NewCheckListRecord;
                                            }
                                        });
                                        var NewCheckListRecord = ` 
                                        <span class="AddNewCheckList"><a href="#">Add New CheckList</a></span>
                                        <hr> </div>`;
                                        ListOfCheckList += NewCheckListRecord;
                                        debugger
                                        $(".WorkFlowStepDetail").append(ListOfCheckList);
                                    });
                            }
                        }
                    });
                });
        }

        //if (workflowId > 0) {
        //    $.ajax({
        //        url: abp.appPath + 'api/services/app/WorkflowSteps/GetAll',
        //        data: {
        //            WorkflowIdFilter: workflowId,
        //        },
        //        method: 'GET',
        //        dataType: 'json',
        //    })
        //        .done(function (data) {
        //            var TotalRecord = data.result.items;

        //            $.each(TotalRecord, function (index, item) {
        //                //    var workFlowStep = `
        //                //<div class="col-12 timeline-item  timeline-itemRowDelete ">
        //                //    <div class="timeline-item col-11">
        //                //        <div class="timeline-circle"></div>
        //                //        <div class="timeline-content">
        //                //            <span class="WorkFlowStepId" style="">${item.workflowStep.id}</span>
        //                //            <span><b>${item.workflowStep.name}</b></span>
        //                //            <div class="RunTimeDivCreate"></div>
        //                //        </div>
        //                //    </div>
        //                //</div>`;
        //                //    $(".WorkFlowStepDetail").append(workFlowStep);
        //                var workFlowStep = `
        //                        <div class="col-12 timeline-item  timeline-itemRowDelete ">
        //                            <div class="timeline-item col-11">
        //                                <div class="timeline-circle"></div>
        //                                <div class="timeline-content"> 
        //                                    <div class="RunTimeDivCreate"></div>
        //                                </div>
        //                            </div>
        //                        </div>`;
        //                $(".WorkFlowStepDetail").append(workFlowStep);
        //                var WorkflowStepID = item.workflowStep.id;
        //                var WorkflowStepName = item.workflowStep.name;
        //                if (item.workflowStep.id > 0) {
        //                    WorkflowStepId = item.workflowStep.id;
        //                    $.ajax({
        //                        url: abp.appPath + 'api/services/app/WorkflowStepDocumentCheckLists/GetAll',
        //                        data: {
        //                            WorkflowIdFilter: WorkflowStepId,
        //                        },
        //                        method: 'GET',
        //                        dataType: 'json',
        //                    })
        //                        .done(function (data) {
        //                            var CheckListRecord = data.result.items
        //                            var ListOfCheckList = "";
        //                            var DocumentWorkflowStepID = 0;
        //                            var NewCheckListRecord = `
        //                        <div class="col-12 ">
        //                            <div class="col-11">  
        //                                        <span class="WorkFlowStepId" style="">${WorkflowStepID}</span>
        //                                     <span><b>${WorkflowStepName}</b></span>  
        //                            </div>
        //                        </div>`;
        //                            ListOfCheckList += NewCheckListRecord
        //                            $.each(CheckListRecord, function (index, item) {
        //                                debugger
        //                                DocumentWorkflowStepID = item.workflowStepDocumentCheckList.workflowStepId;
        //                                var NewCheckListRecord = `
        //                        <div class="col-12 ">
        //                            <div class="col-11"> 
        //                                     <span class="" hidden style="">${item.workflowStepDocumentCheckList.id}</span>
        //                                    <span class="" style="">${item.workflowStepDocumentCheckList.workflowStepId}</span>
        //                                    <span>${item.workflowStepDocumentCheckList.description}</span><br>  
        //                            </div>
        //                        </div>`;
        //                                ListOfCheckList += (NewCheckListRecord);
        //                                debugger
        //                            });
        //                            if (WorkflowStepID == DocumentWorkflowStepID) {
        //                                debugger
        //                                var NewCheckListRecord = `
        //                        <div class="col-12 ">
        //                            <div class="col-11">   
        //                        <span class="AddNewCheckList"><a href="#">Add New CheckList</a></span>
        //                                    <hr> 
        //                            </div>
        //                        </div>`;
        //                                ListOfCheckList += NewCheckListRecord
        //                                //$(".WorkFlowStepDetail").append(NewCheckListRecord);
        //                                $(".RunTimeDivCreate").append(ListOfCheckList);

        //                            }

        //                            console.log(data);
        //                        });
        //                }
        //            });
        //        })
        //}

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
                getDocumentCheckList();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getDocumentCheckList();
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
                getDocumentCheckList();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getDocumentCheckList();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
        //    edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
        //    delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DocumentCheckList/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditDocumentCheckListHeadModal',
        });
        var _createOrEditAddNewCheckListModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/CreateOrEditCheckListModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DocumentCheckList/AddDocumentCheckList/_CreateOrEditCheckListModal.js',
            modalClass: 'CreateOrEditDocumentCheckListHeadModal',
        });
        //var _createOrEditAddNewCheckListModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/AddCheckList',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/DocumentCheckList/AddDocumentCheckList/AddNewCheckList.js',
        //    modalClass: 'CreateOrEditDocumentCheckListHeadModal',
        //});
        //var _viewFeeTypeModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/DocumentCheckList/ViewLeadFormModal',
        //    modalClass: 'ViewLeadFormModal',
        //});

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
        //var dataTable = _$FeeTypeTable.DataTable({

        //    paging: true,
        //    serverSide: true,
        //    processing: true,
        //    listAction: {
        //        ajaxFunction: _workflowDocumentsService.getAll,
        //        inputFilter: function () {
        //            return {
        //                filter: $('#MasterCategoriesTableFilter').val(),
        //                abbrivationFilter: $('#AbbrivationFilterId').val(),
        //                nameFilter: $('#NameFilterId').val(),
        //            };
        //        },
        //    },
        //    columnDefs: [
        //        {
        //            className: 'control responsive',
        //            orderable: false,
        //            render: function () {
        //                return '';
        //            },
        //            targets: 0,
        //        },
        //        {
        //            targets: 1,
        //            data: 'workflowDocument.name',
        //            name: 'name',
        //        },
        //        {
        //            targets: 2,
        //            width: 30,
        //            data: null,
        //            orderable: false,
        //            searchable: false,
        //            render: function (data, type, full, meta) {
        //                var rowId = data.workflowDocument.id;
        //                var rowData = data.workflowDocument;
        //                var RowDatajsonString = JSON.stringify(rowData);

        //                var contextMenu = '<div class="context-menu" style="position:relative;">' +
        //                    '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="flaticon-more"></span></a></div>' +
        //                    '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
        //                    '<ul style="list-style: none; padding: 0;color:black">' +
        //                    '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
        //                    '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
        //                    "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
        //                    '</ul>' +
        //                    '</div>' +
        //                    '</div>';

        //                return contextMenu;
        //            }
        //        },
        //    ],
        //});
        //function getDocumentCheckList() {
        //    dataTable.ajax.reload();

        //}


        $('#CreateNewDocumentCheckList').click(function () {
            debugger
            _createOrEditModal.open();
        });
        $(document).on('click', 'a[data-action]', function () {
            debugger

            var rowId = $(this).data('id');
            var action = $(this).data('action');
            if (action === 'edit') {
                debugger
                _createOrEditAddNewCheckListModal.open({ id: rowId });
            }
            else if (action === 'Delete') {
                debugger
                console.log(rowId);
                deleteWorkFlowDocumentCheckList(rowId);
            }
        });
        
        $(document).on('click', '.AddNewCheckList', function () {
            debugger
            var $timelineItem = $(this).closest('.timeline-item');

            var workFlowStepId = $timelineItem.find('.WorkFlowStepId').text();

            $("#workFlowStepId").val(workFlowStepId);

            _createOrEditAddNewCheckListModal.open();


        });
        function deleteWorkFlowDocumentCheckList(rowId) {
            debugger
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _workflowStepDocumentCheckListsService
                        .delete({
                            id: rowId,
                        })
                        .done(function () {
                            debugger 

                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                            location.reload();
                        });
                }
            });
        }
        abp.event.on('app.createOrEditFeeTypeModalSaved', function () {
            debugger
            location.reload();
        });

        $('#GetMasterCategoriesButton').click(function (e) {
            e.preventDefault();
            getDocumentCheckList();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getDocumentCheckList();
            }
        });

        $('.reload-on-change').change(function (e) {
            getDocumentCheckList();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getDocumentCheckList();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getDocumentCheckList();
        });
    });
})();
