(function () {
	$("#kt_app_sidebar_toggle").trigger("click");

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

		var _createOrEditModalEmail = new app.ModalManager({
			viewUrl: abp.appPath + 'AppAreaName/Clients/ClientEmailCompose',
			modalClass: 'ClientEmailCompose'
		});

		var _viewClientModal = new app.ModalManager({
			viewUrl: abp.appPath + 'AppAreaName/Clients/ViewclientModal',
			modalClass: 'ViewClientModal'
		});

        abp.event.on('app.createOrEditBranchModalSaved', function () {
            getLeadSource();
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
					width: 200,
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
						
						// Generate the URLs using JavaScript variables.........
						let clientDetailUrl = `/AppAreaName/partners/DetailsForm?id=${row.partner.id}`;
						let clientEmailComposeUrl = `/AppAreaName/Client/ClientEmailCompose?id=${row.partner.id}`;
						let profilePicture = row.imageBytes
						return `
    <div class="d-flex align-items-center">
         ${profilePicture
								? `<a href="${profilePicture}" target="_blank"><img class="rounded-circle border border-dark text-white me-2 h-40px w-40px" src="data:image/png;base64,${profilePicture}" alt="${fullName}" title="${fullName}"></a>`
								: `<span class="rounded-circle bg-dark text-white text-center border border-dark fs-3 pt-2 me-2 h-40px w-40px" title="${fullName}"><b>${initials}</b></span>`}
        <div class="d-flex flex-column">
            <a href="${clientDetailUrl}" class="fs-6 text-uppercase text-bold" title="${fullName}">
                ${fullName}
            </a> 
             <a href="#" class="EmailForm" data-id="${row.partner.id}">${row.partner.email}</a>
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
					data: null,
					orderable: false,
					autoWidth: false,
					defaultContent: '',
					render: function (data, type, row) {
						let city = row.partner.city;
						let countryName = row.countryName;

						let fullName = `${city} <br> ${countryName}`;

						return ` ${fullName}`;
					},
					name: 'concatenatedData',
				},
				
				{
					targets: 6,
					data: 'partner.productCount',
					name: 'productCount',
				},
				{
					targets: 7,
					data: 'partner.enrolledCount',
					name: 'enrolledCount',
				},
				{
					targets: 8,
					data: 'partner.progressCount',
					name: 'progressCount',
				},
				{
					targets: 9,
					data: 'partner.marketingEmail',
					name: 'MarketingEmail',
				},
				{
					targets: 10,
					width: 30,
					data: null,
					orderable: false,
					searchable: false,
					render: function (data, type, full, meta) {
						console.log(data);
						var rowId = data.partner.id; 
						var rowData = data.partner;
						var RowDatajsonString = JSON.stringify(rowData);
						var contextMenu = '<div class="context-menu" style="position:relative;">' +
							'<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
							'<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
							'<ul style="list-style: none; padding: 0;color:black">' +
							'<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
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


		$(document).on('click', '.EmailForm', function (e) {
			e.preventDefault();
			debugger
			var rowId = $(this).data('id');
			var action = $(this).data('action');
			_createOrEditModalEmail.open(rowId);

		});
		function getPartners() {
			dataTable.ajax.reload();
		}

		function deletePartner(partner) {
			abp.message.confirm(
				'',
				app.localize('AreYouSure'),
				function (isConfirmed) {
					if (isConfirmed) {
						_partnersService.delete({
							id: partner.id
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
				window.location = "/AppAreaName/Partners/DetailsForm/" + rowId;     
			} else if (action === 'edit') {
				window.location = "/AppAreaName/Partners/CreateOrEdit/" + rowId;     
			} else if (action === 'delete') {
				deletePartner(rowId);
			}
		});




	});
})();
