(function () {
    $(function () {
            $("#kt_app_sidebar_toggle").trigger("click");

        //alert("23123");
        var _$Clienttable = $('#Clienttable');
         ;
        var _clientService = abp.services.app.clients;
        var _$clientInformationForm = $('form[name=ClientInformationsForm]');
       // _$clientInformationForm.validate();

        function save(successCallback) {
            //if (!_$clientInformationForm.valid()) {
            //    return;
            //}
            //if ($('#Client_CountryCodeId').prop('required') && $('#Client_CountryCodeId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}
            //if ($('#Client_AssigneeId').prop('required') && $('#Client_AssigneeId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
            //    return;
            //}
            //if ($('#Client_ProfilePictureId').prop('required') && $('#Client_ProfilePictureId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('BinaryObject')));
            //    return;
            //}
            //if ($('#Client_HighestQualificationId').prop('required') && $('#Client_HighestQualificationId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('DegreeLevel')));
            //    return;
            //}
            //if ($('#Client_StudyAreaId').prop('required') && $('#Client_StudyAreaId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('SubjectArea')));
            //    return;
            //}
            //if ($('#Client_LeadSourceId').prop('required') && $('#Client_LeadSourceId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('LeadSource')));
            //    return;
            //}
            //if ($('#Client_CountryId').prop('required') && $('#Client_CountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}
            //if ($('#Client_PassportCountryId').prop('required') && $('#Client_PassportCountryId').val() == '') {
            //    abp.message.error(app.localize('{0}IsRequired', app.localize('Country')));
            //    return;
            //}



            var client = _$clientInformationForm.serializeFormToObject();



            abp.ui.setBusy();
            _clientService.createOrEdit(
                client
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditClientModalSaved');

                if (typeof (successCallback) === 'function') {
                    successCallback();
                }
            }).always(function () {
                abp.ui.clearBusy();
            });
        };

        function clearForm() {
            _$clientInformationForm[0].reset();
        }
        $('#saveBtn').click(function () {
            //alert("ok");
            save(function () {
                 
                alert("123");
                window.location = "/AppAreaName/Client";
            });
        });

        $('#saveAndNewBtn').click(function () {
            save(function () {
                if (!$('input[name=id]').val()) {//if it is create page
                    clearForm();
                }
            });
        }); 
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
          getSubjects();
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
          getSubjects();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
          getSubjects();
      });

    var _permissions = {
        create: abp.auth.hasPermission('Pages.Clients.Create'),
        edit: abp.auth.hasPermission('Pages.Clients.Edit'),
        delete: abp.auth.hasPermission('Pages.Clients.Delete'),
    };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/CreateOrEditModal',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Agents/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal',
        });
        var _createOrEditProfile = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/ChangePictureModal',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Agents/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditUserModal',
        });
        var changeProfilePictureModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Profile/_ChangePictureModal.js',
            modalClass: 'CreateOrEditUserModal',
        });
        var _createOrEditModalEmail = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/ClientEmailCompose',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Agents/_CreateOrEditModal.js',
            modalClass: 'ClientEmailCompose',
        });
    var _viewSubjectModal = new app.ModalManager({
        viewUrl: abp.appPath + 'AppAreaName/ApplicationClient/ViewApplicationModal',
        modalClass: 'ViewApplicationModal',
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
  
        var dataTable = _$Clienttable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SubjectsTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val()
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
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('View'),
                                action: function (data) {
                                    window.location.href = abp.appPath + 'AppAreaName/Client/ClientDetail';
                                    //_viewServiceCategoryModal.open({ id: data.record.serviceCategory.id });
                                },
                            },
                            {
                                text: app.localize('Email'),
                                //visible: function () {
                                //    return _permissions.edit;
                                //},
                                action: function (data) {
                                    _createOrEditModalEmail.open();
                                },
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deletePartnerType(data.record.clients);
                                },
                            },
                        ],
                    },
                },
                //{
                //    targets: 2,
                //    data: 'clients.AddedFrom',
                //    name: 'AddedFrom',
                //},
                //{
                //    targets: 3,
                //    data: 'clients.Rating',
                //    name: 'Rating',
                //},
                //{
                //    targets: 4,
                //    data: 'Client.InternalId',
                //    name: 'InternalId',
                //},
                //{
                //    targets: 5,
                //    data: 'Client.AssigneeId',
                //    name: 'AssigneeId',
                //},
                //{
                //    targets: 6,
                //    data: 'Client.PhoneNo',
                //    name: 'PhoneNo',
                //},
                //{
                //    targets: 7,
                //    data: 'Client.ClientPortal',
                //    name: 'ClientPortal',
                //},
                //{
                //    targets: 8,
                //    data: 'Client.PassportNo',
                //    name: 'PassportNo',
                //},
                {
                    targets: 2,
                    data: 'client.FirstName',
                    name: 'FirstName',
                },
                //{
                //    targets: 10,
                //    data: 'Client.PreferedIntake',
                //    name: 'PreferedIntake',
                //}, 
              
            ],
        });


    function getSubjects() {
      dataTable.ajax.reload();
    }
    
    function deletePartnerType(subject) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
            _subjectsService
            .delete({
                id: subject.id,
            })
            .done(function () {
                getSubjects(true);
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

        $('#CreateNewClientButton').click(function () {
      _createOrEditModal.open();
        });
        $('#CreateNewClientEmailButton').click(function () {
            _createOrEditModalEmail.open();
        });
        $('#changeProfilePicture').click(function () {
            changeProfilePictureModal.open();
        });
       
        //$('#ClientBackListButton').click(function () {
        //    window.location.href = abp.appPath + 'AppAreaName/Partners/AddPartnersDetails';
        //});

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

    abp.event.on('app.createOrEditClientModalSaved', function () {
        getSubjects();
    });

      $('#GetSubjectAreaButton').click(function (e) {
      e.preventDefault();
        getSubjects();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
          getSubjects();
      }
    });

    $('.reload-on-change').change(function (e) {
        getSubjects();
    });

    $('.reload-on-keyup').keyup(function (e) {
        getSubjects();
    });
    
    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
        getSubjects();
    });
       
  });
})();
