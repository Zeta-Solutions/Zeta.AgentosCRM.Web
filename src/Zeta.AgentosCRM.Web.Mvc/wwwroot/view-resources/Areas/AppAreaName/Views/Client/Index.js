(function () {
    $("#kt_app_sidebar_toggle").trigger("click");
    $(function () {
        var _$Clienttable = $('#ClientsTable');
         ;
        var _clientService = abp.services.app.clients;
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
          getClientss();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
          getClientss();
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
          getClientss();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
          getClientss();
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
        let concatenatedData = [];
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
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.client.id;
                        var contaxtMenu = '<div class="context-menu" style="position: absolute;">' +
                            '<div class="ellipsis"><input type="checkbox" ></div>' +
                            '</div>';


                        return contaxtMenu;
                    }
                }, 
                {
                    width: 100,
                    targets: 2,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    // Assuming 'row' contains the client data with properties 'firstName', 'lastName', and 'email'
                    render: function (data, type, row) {
                        let firstNameInitial = row.client.firstName.charAt(0).toUpperCase();
                        let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                        let initials = `${firstNameInitial}${lastNameInitial}`;
                        let fullName = `${row.client.firstName} ${row.client.lastName}`;

                        // Generate the URLs using JavaScript variables
                        let clientDetailUrl = `/AppAreaName/Client/ClientDetail?id=${row.client.id}`;
                        let clientEmailComposeUrl = `/AppAreaName/Client/ClientEmailCompose?id=${row.client.id}`;

                        return `
        <div class="d-flex align-items-center">
            <span class="rounded-circle bg-primary text-white p-2 me-2" title="${fullName}">
                <b>${initials}</b>
            </span>
            <div class="d-flex flex-column">
                <a href="${clientDetailUrl}" class="text-truncate" title="${fullName}">
                    ${fullName}
                </a>
                <a href="${clientEmailComposeUrl}" class="text-decoration-none">
                    <span class="text-truncate" id="CreateNewClientEmailButton">${row.client.email}</span>
                </a>
            </div>
        </div>
    `;
                    },

                    name: 'concatenatedData',
                    
                }, 
                
                {
                    targets: 3,
                    data: 'client.rating',
                    name: 'rating',
                },
                {
                    targets: 4,
                    data: 'client.id',
                    name: 'id',
                },
                {
                    targets: 5,
                    data: 'userName',
                    name: 'assigneeName',
                },
                {
                    targets: 6,
                    data: "client.phoneNo",
                    name: "phoneNo" 
                },
                {
                    targets: 7,
                    data: 'client.clientPortal',
                    name: 'clientPortal',
                },
                {
                    targets: 8,
                    data: 'client.passportNo',
                    name: 'passportNo',
                },
                {
                    targets: 9,
                    data: 'client.city',
                    name: 'city',
                },
                {
                    targets: 10,
                    data: 'client.state',
                    name: 'state',
                },  
                {
                    targets: 11,
                    data: 'client.state',
                    name: 'state',
                },  
                {
                    width: 30,
                    targets: 12,
                    data: null,
                    orderable: false,
                    searchable: false,

                  
                    render: function (data, type, full, meta) {

                        var rowId = data.client.id;
                        var contaxtMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 4px 4px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<li  style="color:black"><a href="#" style="color: black;" data-action="view" data-id="' + rowId + '">View</a></li>' +
                            '<li ><a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '">Edit</a></li>' +
                            '<li ><a href="#" style="color: black;" data-action="delete" data-id="' + rowId + '">Delete</a></li>' +
                            '</ul>' +
                            '</div>' +
                            '</div>';


                        return contaxtMenu;
                    }


                }
            ],
        });

        // Add an event listener to handle the click event for the full name link
        $(document).on('click', '.text-truncate', function (e) {
            e.preventDefault(); // Prevent the default behavior of the link
            window.location.href = $(this).attr('href'); // Redirect to the specified URL
        });
        $(document).on('click', '#CreateNewClientEmailButton', function (e) {
            e.preventDefault(); // Prevent the default behavior of the link
            _createOrEditModalEmail.open();
           // window.location.href = $(this).attr('href'); // Redirect to the specified URL
        });
    function getClientss() {
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



            //// Toggle the visibility of the options when the ellipsis is clicked
            //var options = $(this).next('.options');
            //options.toggle();
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
                /*_viewProductTypeModal.open({ id: rowId });*/
                _createOrEditModal.open({ id: rowId });
            } else if (action === 'edit') {
                //_createOrEditModal.open({ id: rowId });
                _createOrEditModal.open({ id: rowId });

            } else if (action === 'delete') {
                _createOrEditModal.open({rowId });

                //deleteProductType(rowId);
            }
        });



    function deletePartnerType(subject) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
            _subjectsService
            .delete({
                id: subject.id,
            })
            .done(function () {
                getClientss(true);
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
           // _createOrEditModal.open();
            window.location.href = abp.appPath + 'AppAreaName/Client/ClientCreateDetail';

        });
        $('#CreateNewClientEmailButton').click(function () {
            _createOrEditModalEmail.open();
        });
        $('#changeProfilePicture').click(function () {
            changeProfilePictureModal.open();
        });
        //$(".menu-link").click(function () {
        //    alert("ok");
        //    $("#kt_body").toggleClass("app-sidebar-closed");
        //});

     
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
        getClientss();
    });

      $('#getClientsAreaButton').click(function (e) {
      e.preventDefault();
        getClientss();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
          getClientss();
      }
    });

    $('.reload-on-change').change(function (e) {
        getClientss();
    });

    $('.reload-on-keyup').keyup(function (e) {
        getClientss();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
        getClientss();
    });
  });
})();
