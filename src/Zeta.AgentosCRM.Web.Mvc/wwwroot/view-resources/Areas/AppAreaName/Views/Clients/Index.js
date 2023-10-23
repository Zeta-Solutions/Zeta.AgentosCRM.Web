(function () {
    $(function () {

        var _$clientsTable = $('#ClientsTable');
        var _clientsService = abp.services.app.clients;
		var _entityTypeFullName = 'Zeta.AgentosCRM.CRMClient.Client';
        
       var $selectedDate = {
            startDate: null,
            endDate: null,
        }

        $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('.startDate').daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L',
        })
        .on("apply.daterangepicker", (ev, picker) => {
            $selectedDate.startDate = picker.startDate;
            getClients();
        })
        .on('cancel.daterangepicker', function (ev, picker) {
            $(this).val("");
            $selectedDate.startDate = null;
            getClients();
        });

        $('.endDate').daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L',
        })
        .on("apply.daterangepicker", (ev, picker) => {
            $selectedDate.endDate = picker.startDate;
            getClients();
        })
        .on('cancel.daterangepicker', function (ev, picker) {
            $(this).val("");
            $selectedDate.endDate = null;
            getClients();
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Clients.Create'),
            edit: abp.auth.hasPermission('Pages.Clients.Edit'),
            'delete': abp.auth.hasPermission('Pages.Clients.Delete')
        };

               

		 var _viewClientModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/ViewclientModal',
            modalClass: 'ViewClientModal'
        });

		        var _entityTypeHistoryModal = app.modals.EntityTypeHistoryModal.create();
		        function entityHistoryIsEnabled() {
            return abp.auth.hasPermission('Pages.Administration.AuditLogs') &&
                abp.custom.EntityHistory &&
                abp.custom.EntityHistory.IsEnabled &&
                _.filter(abp.custom.EntityHistory.EnabledEntities, entityType => entityType === _entityTypeFullName).length === 1;
        }

        var getDateFilter = function (element) {
            if ($selectedDate.startDate == null) {
                return null;
            }
            return $selectedDate.startDate.format("YYYY-MM-DDT00:00:00Z"); 
        }
        
        var getMaxDateFilter = function (element) {
            if ($selectedDate.endDate == null) {
                return null;
            }
            return $selectedDate.endDate.format("YYYY-MM-DDT23:59:59Z"); 
        }

        var dataTable = _$clientsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ClientsTableFilter').val(),
					firstNameFilter: $('#FirstNameFilterId').val(),
					lastNameFilter: $('#LastNameFilterId').val(),
					emailFilter: $('#EmailFilterId').val(),
					phoneNoFilter: $('#PhoneNoFilterId').val(),
					minDateofBirthFilter:  getDateFilter($('#MinDateofBirthFilterId')),
					maxDateofBirthFilter:  getMaxDateFilter($('#MaxDateofBirthFilterId')),
					universityFilter: $('#UniversityFilterId').val(),
					minRatingFilter: $('#MinRatingFilterId').val(),
					maxRatingFilter: $('#MaxRatingFilterId').val(),
					countryDisplayPropertyFilter: $('#CountryDisplayPropertyFilterId').val(),
					userNameFilter: $('#UserNameFilterId').val(),
					binaryObjectDescriptionFilter: $('#BinaryObjectDescriptionFilterId').val(),
					degreeLevelNameFilter: $('#DegreeLevelNameFilterId').val(),
					subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
					leadSourceNameFilter: $('#LeadSourceNameFilterId').val(),
					countryName2Filter: $('#CountryName2FilterId').val(),
					countryName3Filter: $('#CountryName3FilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
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
                                    window.location="/AppAreaName/Clients/ViewClient/" + data.record.client.id;
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            window.location="/AppAreaName/Clients/CreateOrEdit/" + data.record.client.id;                                
                            }
                        },
                        {
                            text: app.localize('History'),
                            iconStyle: 'fas fa-history mr-2',
                            visible: function () {
                                return entityHistoryIsEnabled();
                            },
                            action: function (data) {
                                _entityTypeHistoryModal.open({
                                    entityTypeFullName: _entityTypeFullName,
                                    entityId: data.record.client.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteClient(data.record.client);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "client.firstName",
						 name: "firstName"   
					},
					{
						targets: 3,
						 data: "client.lastName",
						 name: "lastName"   
					},
					{
						targets: 4,
						 data: "client.email",
						 name: "email"   
					},
					{
						targets: 5,
						 data: "client.phoneNo",
						 name: "phoneNo"   
					},
					{
						targets: 6,
						 data: "client.dateofBirth",
						 name: "dateofBirth" ,
					render: function (dateofBirth) {
						if (dateofBirth) {
							return moment(dateofBirth).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 7,
						 data: "client.contactPreferences",
						 name: "contactPreferences"   ,
						render: function (contactPreferences) {
							return app.localize('Enum_ContactPrefernce_' + contactPreferences);
						}
			
					},
					{
						targets: 8,
						 data: "client.university",
						 name: "university"   
					},
					{
						targets: 9,
						 data: "client.street",
						 name: "street"   
					},
					{
						targets: 10,
						 data: "client.city",
						 name: "city"   
					},
					{
						targets: 11,
						 data: "client.state",
						 name: "state"   
					},
					{
						targets: 12,
						 data: "client.zipCode",
						 name: "zipCode"   
					},
					{
						targets: 13,
						 data: "client.preferedIntake",
						 name: "preferedIntake" ,
					render: function (preferedIntake) {
						if (preferedIntake) {
							return moment(preferedIntake).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 14,
						 data: "client.passportNo",
						 name: "passportNo"   
					},
					{
						targets: 15,
						 data: "client.visaType",
						 name: "visaType"   
					},
					{
						targets: 16,
						 data: "client.visaExpiryDate",
						 name: "visaExpiryDate" ,
					render: function (visaExpiryDate) {
						if (visaExpiryDate) {
							return moment(visaExpiryDate).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 17,
						 data: "client.rating",
						 name: "rating"   
					},
					{
						targets: 18,
						 data: "client.clientPortal",
						 name: "clientPortal"  ,
						render: function (clientPortal) {
							if (clientPortal) {
								return '<div class="text-center"><i class="fa fa-check text-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 19,
						 data: "countryDisplayProperty" ,
						 name: "countryCodeFk.displayProperty" 
					},
					{
						targets: 20,
						 data: "userName" ,
						 name: "assigneeFk.name" 
					},
					{
						targets: 21,
						 data: "binaryObjectDescription" ,
						 name: "profilePictureFk.description" 
					},
					{
						targets: 22,
						 data: "degreeLevelName" ,
						 name: "highestQualificationFk.name" 
					},
					{
						targets: 23,
						 data: "subjectAreaName" ,
						 name: "studyAreaFk.name" 
					},
					{
						targets: 24,
						 data: "leadSourceName" ,
						 name: "leadSourceFk.name" 
					},
					{
						targets: 25,
						 data: "countryName2" ,
						 name: "countryFk.name" 
					},
					{
						targets: 26,
						 data: "countryName3" ,
						 name: "passportCountryFk.name" 
					}
            ]
        });

        function getClients() {
            dataTable.ajax.reload();
        }

        function deleteClient(client) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _clientsService.delete({
                            id: client.id
                        }).done(function () {
                            getClients(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
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

                

		$('#ExportToExcelButton').click(function () {
            _clientsService
                .getClientsToExcel({
				filter : $('#ClientsTableFilter').val(),
					firstNameFilter: $('#FirstNameFilterId').val(),
					lastNameFilter: $('#LastNameFilterId').val(),
					emailFilter: $('#EmailFilterId').val(),
					phoneNoFilter: $('#PhoneNoFilterId').val(),
					minDateofBirthFilter:  getDateFilter($('#MinDateofBirthFilterId')),
					maxDateofBirthFilter:  getMaxDateFilter($('#MaxDateofBirthFilterId')),
					universityFilter: $('#UniversityFilterId').val(),
					minRatingFilter: $('#MinRatingFilterId').val(),
					maxRatingFilter: $('#MaxRatingFilterId').val(),
					countryDisplayPropertyFilter: $('#CountryDisplayPropertyFilterId').val(),
					userNameFilter: $('#UserNameFilterId').val(),
					binaryObjectDescriptionFilter: $('#BinaryObjectDescriptionFilterId').val(),
					degreeLevelNameFilter: $('#DegreeLevelNameFilterId').val(),
					subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
					leadSourceNameFilter: $('#LeadSourceNameFilterId').val(),
					countryName2Filter: $('#CountryName2FilterId').val(),
					countryName3Filter: $('#CountryName3FilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditClientModalSaved', function () {
            getClients();
        });

		$('#GetClientsButton').click(function (e) {
            e.preventDefault();
            getClients();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getClients();
		  }
		});

        $('.reload-on-change').change(function(e) {
			getClients();
		});

        $('.reload-on-keyup').keyup(function(e) {
			getClients();
		});

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getClients();
        });
		
		
		

    });
})();
