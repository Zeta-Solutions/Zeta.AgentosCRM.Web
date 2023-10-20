(function () {
    $(function () {
        var _$SubjectTable = $('#Clientssstable');
    var _subjectsService = abp.services.app.subjects;

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
      create: abp.auth.hasPermission('Pages.Subjects.Create'),
        edit: abp.auth.hasPermission('Pages.Subjects.Edit'),
        delete: abp.auth.hasPermission('Pages.Subjects.Delete'),
    };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Client/CreateOrEditModal',
            //scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Agents/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditModal',
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
        var dataTable = _$SubjectTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _subjectsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SubjectsTableFilter').val(),
                        abbrivationFilter: $('#AbbrivationFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
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
                                },
                            },
                            {
                                text: app.localize('Email'),
                                visible: function () {
                                    return _permissions.edit;
                                },
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
                                    deletePartnerType(data.record.partnerType);
                                },
                            },
                        ],
                    },
                },
                {
                    targets: 2,
                    data: 'subject.abbrivation',
                    name: 'abbrivation',
                },
                {
                    targets: 3,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 4,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 5,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 6,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 7,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 8,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 9,
                    data: 'subject.name',
                    name: 'name',
                },
                {
                    targets: 10,
                    data: 'subject.name',
                    name: 'name',
                },
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

    $('#CreateNewButton').click(function () {
      _createOrEditModal.open();
    });

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

    abp.event.on('app.createOrEditPartnerTypeModalSaved', function () {
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
