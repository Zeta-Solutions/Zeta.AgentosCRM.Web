(function () {
    $(function () {
        var _emailTemplatesService = abp.services.app.emailTemplates;

        var $selectedDate = {
            startDate: null,
            endDate: null,
        };

        getAllEmailData();
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
                getAllEmailData();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.startDate = null;
                getAllEmailData();
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
                getAllEmailData();
            })
            .on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                $selectedDate.endDate = null;
                getAllEmailData();
            });

        //var _permissions = {
        //    create: abp.auth.hasPermission('Pages.FeeTypes.Create'),
        //    edit: abp.auth.hasPermission('Pages.FeeTypes.Edit'),
        //    delete: abp.auth.hasPermission('Pages.FeeTypes.Delete'),
        //};

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EmailTemplate/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EmailTemplate/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEmailTemplateModal',
        });

        //var _viewFeeTypeModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/EmailTemplate/ViewLeadFormModal',
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

        function getAllEmailData() {
            dataTable.ajax.reload();
        }

        function getAllEmailData() {
            $.ajax({
                url: abp.appPath + 'api/services/app/EmailTemplates/GetAll',
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                     ;
                    var AllData = data.result.items;

                    $(".EmailtemplateRecord").empty();
                    $.each(AllData, function (index, item) { 
                        var AllEmailTemplate = `<div class="col-lg-3 Emailtemplate">  
                             <div class="Card-head" style="font-weight:bold;"><input id="EmailTemplateID" type="hidden" value="`+ item.emailTemplate.id + `"/>` + item.emailTemplate.title +
                            `<span class="Edit-icon" style="float: right; cursor: pointer;"><i class="fa fa-edit" style="font-size: 10px;"></i></span>
                             <span class="delete-icon" style="float: right; cursor: pointer; margin-right: 5px;"><i class="fa fa-trash" style="font-size: 10px;"></i></span>
                                </div><hr>
                                <div class="Card-Body"> 
                                    <div><span>`+ item.emailTemplate.emailSubject + `</span></div> <hr>
                                    <div><span>`+ item.emailTemplate.emailBody + `</span></div> <hr>
                                    <div><span>Attachment</span></div> 
                                </div> 
                            </div>`;
                             
                        $(".EmailtemplateRecord").append(AllEmailTemplate);

                    });
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        $(document).on("click", ".delete-icon", function () {
            
            var emailTemplateID = $(this).closest('.Emailtemplate'); 
           var EmailID= emailTemplateID.find("#EmailTemplateID").val();
              
            deleteEmailTemplate(EmailID);
        });
        $(document).on("click", ".Edit-icon", function () {
            
            var emailTemplateID = $(this).closest('.Emailtemplate'); 
           var EmailID= emailTemplateID.find("#EmailTemplateID").val();
            _createOrEditModal.open({ id: EmailID });
               
        });
       
        function deleteEmailTemplate(EmailID) {
            abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
                if (isConfirmed) {
                    _emailTemplatesService
                        .delete({
                            id: EmailID,
                        })
                        .done(function () {
                             
                            getAllEmailData(true);
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

        $('#CreateNewEmailTemplate').click(function () {
             
            _createOrEditModal.open();
        }); 
        abp.event.on('app.createOrEditEmailTemplateModalSaved', function () {
             
            getAllEmailData();
        });

        $('#GetMasterCategoriesButton').click(function (e) {
            e.preventDefault();
            getAllEmailData();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getAllEmailData();
            }
        });

        $('.reload-on-change').change(function (e) {
            getAllEmailData();
        });

        $('.reload-on-keyup').keyup(function (e) {
            getAllEmailData();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getAllEmailData();
        });
    });
})();
