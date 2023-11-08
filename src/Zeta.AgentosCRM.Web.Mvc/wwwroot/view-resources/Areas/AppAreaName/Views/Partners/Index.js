(function () {
	$(function () {

		var _$partnersTable = $('#PartnersTable');
		var _partnersService = abp.services.app.partners;
		var _entityTypeFullName = 'Zeta.AgentosCRM.CRMPartner.Partner';

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
				getPartners();
			})
			.on('cancel.daterangepicker', function (ev, picker) {
				$(this).val("");
				$selectedDate.startDate = null;
				getPartners();
			});

		$('.endDate').daterangepicker({
			autoUpdateInput: false,
			singleDatePicker: true,
			locale: abp.localization.currentLanguage.name,
			format: 'L',
		})
			.on("apply.daterangepicker", (ev, picker) => {
				$selectedDate.endDate = picker.startDate;
				getPartners();
			})
			.on('cancel.daterangepicker', function (ev, picker) {
				$(this).val("");
				$selectedDate.endDate = null;
				getPartners();
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

		var dataTable = _$partnersTable.DataTable({
			paging: true,
			serverSide: true,
			processing: true,
			listAction: {
				ajaxFunction: _partnersService.getAll,
				inputFilter: function () {
					debugger;
					return {
						filter: $('#PartnersTableFilter').val(),
						/*		abbrivationFilter: $('#AbbrivationFilterId').val(),*/
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
				//{
				//	targets: 1,
				//	data: 'partner.abbrivation',
				//	name: 'abbrivation',
				//},
				{
					targets: 1,
					data: null,
					orderable: false,
					autoWidth: false,
					defaultContent: '',
					render: function (data, type, full, meta) {
						console.log(data);
						var rowId = data.partner.id;
						var contaxtMenu = '<div class="context-menu" style="position: absolute;">' +
							'<div class="ellipsis"><input type="checkbox" ></div>' +
							'</div>';


						return contaxtMenu;
					}
				},
				//{
				//	targets: 1,
				//	data: 'partner.partnerName',
				//	name: 'PartnerName',
				//},
				//{

				//	targets: 2,
				//	data: 'workflowName',
				//	name: 'Workflow',
				//},
				{
					width: 100,
					targets: 2,
					data: null,
					orderable: false,
					autoWidth: false,
					defaultContent: '',
					// Assuming 'row' contains the client data with properties 'firstName', 'lastName', and 'email'
					render: function (data, type, row) {
						let firstNameInitial = row.partner.partnerName.charAt(0).toUpperCase();
						//let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
						let initials = `${firstNameInitial}`;
						let fullName = `${row.partner.partnerName}`;
						
						// Generate the URLs using JavaScript variables
						let clientDetailUrl = `/AppAreaName/partners/PartnersDetails?id=${row.partner.id}`;
						let clientEmailComposeUrl = `/AppAreaName/Client/ClientEmailCompose?id=${row.partner.id}`;

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
                <span class="text-truncate" id="CreateNewClientEmailButton">${row.partner.email}</span>
            </a>
        </div>
    </div>
`;
					},

					name: 'concatenatedData',
				},
				{

					targets: 3,
					data: 'workflowName',
					name: 'Workflow',
				},
				{
					targets: 4,
					data: 'partnerTypeName',
					name: 'partnerType',
				},
				{
					targets: 5,
					data: 'partner.city',
					name: 'city',
				},
				
				{
					targets: 6,
					data: 'partner.marketingEmail',
					name: 'MarketingEmail',
				},
				{
					targets: 7,
					width: 30,
					data: null,
					orderable: false,
					searchable: false,
					render: function (data, type, full, meta) {
						console.log(data);
						var rowId = data.partner.id;
						var contextMenu = '<div class="context-menu" style="position:relative;">' +
							'<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
							'<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
							'<ul style="list-style: none; padding: 0;color:black">' +
							'<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
							'<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
							'<a href="#" style="color: black;" data-action="delete" data-id="' + rowId + '"><li>Delete</li></a>' +
							'</ul>' +
							'</div>' +
							'</div>';

						return contextMenu;
					}


				},
			],
		});




		//var dataTable = _$partnersTable.DataTable({
		//	paging: true,
		//	serverSide: true,
		//	processing: true,
		//	listAction: {
		//		ajaxFunction: _partnersService.getAll,
		//		inputFilter: function () {
		//			return {
		//				filter: $('#ClientsTableFilter').val(),
		//				firstNameFilter: $('#FirstNameFilterId').val(),
		//				lastNameFilter: $('#LastNameFilterId').val(),
		//				emailFilter: $('#EmailFilterId').val(),
		//				phoneNoFilter: $('#PhoneNoFilterId').val(),
		//				minDateofBirthFilter: getDateFilter($('#MinDateofBirthFilterId')),
		//				maxDateofBirthFilter: getMaxDateFilter($('#MaxDateofBirthFilterId')),
		//				universityFilter: $('#UniversityFilterId').val(),
		//				minRatingFilter: $('#MinRatingFilterId').val(),
		//				maxRatingFilter: $('#MaxRatingFilterId').val(),
		//				countryDisplayPropertyFilter: $('#CountryDisplayPropertyFilterId').val(),
		//				userNameFilter: $('#UserNameFilterId').val(),
		//				binaryObjectDescriptionFilter: $('#BinaryObjectDescriptionFilterId').val(),
		//				degreeLevelNameFilter: $('#DegreeLevelNameFilterId').val(),
		//				subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
		//				leadSourceNameFilter: $('#LeadSourceNameFilterId').val(),
		//				countryName2Filter: $('#CountryName2FilterId').val(),
		//				countryName3Filter: $('#CountryName3FilterId').val()
		//			};
		//		}
		//	},
		//	columnDefs: [
		//		{
		//			className: 'control responsive',
		//			orderable: false,
		//			render: function () {
		//				return '';
		//			},
		//			targets: 0
		//		},
		//		{
		//			width: 120,
		//			targets: 1,
		//			data: null,
		//			orderable: false,
		//			autoWidth: false,
		//			defaultContent: '',
		//			rowAction: {
		//				cssClass: 'btn btn-brand dropdown-toggle',
		//				text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
		//				items: [
		//					{
		//						text: app.localize('View'),
		//						action: function (data) {
		//							window.location = "/AppAreaName/Clients/ViewClient/" + data.record.client.id;
		//						}
		//					},
		//					{
		//						text: app.localize('Edit'),
		//						visible: function () {
		//							return _permissions.edit;
		//						},
		//						action: function (data) {
		//							window.location = "/AppAreaName/Clients/CreateOrEdit/" + data.record.client.id;
		//						}
		//					},
		//					{
		//						text: app.localize('History'),
		//						iconStyle: 'fas fa-history mr-2',
		//						visible: function () {
		//							return entityHistoryIsEnabled();
		//						},
		//						action: function (data) {
		//							_entityTypeHistoryModal.open({
		//								entityTypeFullName: _entityTypeFullName,
		//								entityId: data.record.client.id
		//							});
		//						}
		//					},
		//					{
		//						text: app.localize('Delete'),
		//						visible: function () {
		//							return _permissions.delete;
		//						},
		//						action: function (data) {
		//							deleteClient(data.record.client);
		//						}
		//					}]
		//			}
		//		},
		//		{
		//			targets: 2,
		//			data: "client.firstName",
		//			name: "firstName"
		//		},
		//		{
		//			targets: 3,
		//			data: "client.lastName",
		//			name: "lastName"
		//		},
		//		{
		//			targets: 4,
		//			data: "client.email",
		//			name: "email"
		//		},
		//		{
		//			targets: 5,
		//			data: "client.phoneNo",
		//			name: "phoneNo"
		//		},
		//		{
		//			targets: 6,
		//			data: "client.dateofBirth",
		//			name: "dateofBirth",
		//			render: function (dateofBirth) {
		//				if (dateofBirth) {
		//					return moment(dateofBirth).format('L');
		//				}
		//				return "";
		//			}

		//		},
		//		{
		//			targets: 7,
		//			data: "client.contactPreferences",
		//			name: "contactPreferences",
		//			render: function (contactPreferences) {
		//				return app.localize('Enum_ContactPrefernce_' + contactPreferences);
		//			}

		//		},
		//		{
		//			targets: 8,
		//			data: "client.university",
		//			name: "university"
		//		},
		//		{
		//			targets: 9,
		//			data: "client.street",
		//			name: "street"
		//		},
		//		{
		//			targets: 10,
		//			data: "client.city",
		//			name: "city"
		//		},
		//		{
		//			targets: 11,
		//			data: "client.state",
		//			name: "state"
		//		},
		//		{
		//			targets: 12,
		//			data: "client.zipCode",
		//			name: "zipCode"
		//		},
		//		{
		//			targets: 13,
		//			data: "client.preferedIntake",
		//			name: "preferedIntake",
		//			render: function (preferedIntake) {
		//				if (preferedIntake) {
		//					return moment(preferedIntake).format('L');
		//				}
		//				return "";
		//			}

		//		},
		//		{
		//			targets: 14,
		//			data: "client.passportNo",
		//			name: "passportNo"
		//		},
		//		{
		//			targets: 15,
		//			data: "client.visaType",
		//			name: "visaType"
		//		},
		//		{
		//			targets: 16,
		//			data: "client.visaExpiryDate",
		//			name: "visaExpiryDate",
		//			render: function (visaExpiryDate) {
		//				if (visaExpiryDate) {
		//					return moment(visaExpiryDate).format('L');
		//				}
		//				return "";
		//			}

		//		},
		//		{
		//			targets: 17,
		//			data: "client.rating",
		//			name: "rating"
		//		},
		//		{
		//			targets: 18,
		//			data: "client.clientPortal",
		//			name: "clientPortal",
		//			render: function (clientPortal) {
		//				if (clientPortal) {
		//					return '<div class="text-center"><i class="fa fa-check text-success" title="True"></i></div>';
		//				}
		//				return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
		//			}

		//		},
		//		{
		//			targets: 19,
		//			data: "countryDisplayProperty",
		//			name: "countryCodeFk.displayProperty"
		//		},
		//		{
		//			targets: 20,
		//			data: "userName",
		//			name: "assigneeFk.name"
		//		},
		//		{
		//			targets: 21,
		//			data: "binaryObjectDescription",
		//			name: "profilePictureFk.description"
		//		},
		//		{
		//			targets: 22,
		//			data: "degreeLevelName",
		//			name: "highestQualificationFk.name"
		//		},
		//		{
		//			targets: 23,
		//			data: "subjectAreaName",
		//			name: "studyAreaFk.name"
		//		},
		//		{
		//			targets: 24,
		//			data: "leadSourceName",
		//			name: "leadSourceFk.name"
		//		},
		//		{
		//			targets: 25,
		//			data: "countryName2",
		//			name: "countryFk.name"
		//		},
		//		{
		//			targets: 26,
		//			data: "countryName3",
		//			name: "passportCountryFk.name"
		//		}
		//	]
		//});

		function getPartners() {
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
							getPartners(true);
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
					filter: $('#ClientsTableFilter').val(),
					firstNameFilter: $('#FirstNameFilterId').val(),
					lastNameFilter: $('#LastNameFilterId').val(),
					emailFilter: $('#EmailFilterId').val(),
					phoneNoFilter: $('#PhoneNoFilterId').val(),
					minDateofBirthFilter: getDateFilter($('#MinDateofBirthFilterId')),
					maxDateofBirthFilter: getMaxDateFilter($('#MaxDateofBirthFilterId')),
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

		abp.event.on('app.createOrEditPartnerModalSaved', function () {
			getPartners();
		});

		$('#GetPartnersButton').click(function (e) {
			e.preventDefault();
			getPartners();
		});

		$(document).keypress(function (e) {
			if (e.which === 13) {
				getPartners();
			}
		});

		$('.reload-on-change').change(function (e) {
			getPartners();
		});

		$('.reload-on-keyup').keyup(function (e) {
			getPartners();
		});

		$('#btn-reset-filters').click(function (e) {
			$('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
			getPartners();
		});
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
				//_viewMasterCategoryModal.open({ id: rowId });
				window.location = "/AppAreaName/Partners/ViewPartner/" + rowId;     
			} else if (action === 'edit') {
				window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;     
			} else if (action === 'delete') {
				deleteMasterCategory(rowId);
			}
		});




	});
})();
