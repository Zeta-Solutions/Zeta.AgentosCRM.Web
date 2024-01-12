(function () {
    $(function () {



		//var sendBulkSMSButton = document.getElementById("SendBulkSMS");
		//sendBulkSMSButton.removeAttribute("hidden")

		

		var _$agentsTable = $('#AgentsTable');
		var _agentsService = abp.services.app.agents;
		var _entityTypeFullName = 'Zeta.AgentosCRM.CRMAgent.Agent';

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
				getAgents();
			})
			.on('cancel.daterangepicker', function (ev, picker) {
				$(this).val("");
				$selectedDate.startDate = null;
				getAgents();
			});

		$('.endDate').daterangepicker({
			autoUpdateInput: false,
			singleDatePicker: true,
			locale: abp.localization.currentLanguage.name,
			format: 'L',
		})
			.on("apply.daterangepicker", (ev, picker) => {
				$selectedDate.endDate = picker.startDate;
				getAgents();
			})
			.on('cancel.daterangepicker', function (ev, picker) {
				$(this).val("");
				$selectedDate.endDate = null;
				getAgents();
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

		var dataTable = _$agentsTable.DataTable({
			paging: true,
			serverSide: true,
			processing: true,
			listAction: {
				ajaxFunction: _agentsService.getAll,
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
						var rowId = data.agent.id;
						var contaxtMenu = '<div class="context-menu" style="position: absolute;">' +
							'<div><input type="checkbox" class="custom-checkbox" ></div>' +
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
						let firstNameInitial = row.agent.name.charAt(0).toUpperCase();
						//let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
						let initials = `${firstNameInitial}`;
						let fullName = `${row.agent.name}`;

						// Generate the URLs using JavaScript variables
						let clientDetailUrl = `/AppAreaName/Agents/DetailsForm?id=${row.agent.id}`;
						let clientEmailComposeUrl = `/AppAreaName/Client/ClientEmailCompose?id=${row.agent.id}`;
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
             <a href="#" class="EmailForm" data-id="${row.agent.id}">${row.agent.email}</a>
        </div>
    </div>
`;
					},

					name: 'concatenatedData',
				},
				{

					targets: 3,
					data: 'agent.isSuperAgent',
					name: 'IsSuperAgent',
					render: function (data, type, row, meta) {
						
						return data === true ? 'SuperAgent' : 'SubAgent';
					}
				},
				{

					targets: 4,
					data: 'agent.isBusiness',
					name: 'IsBusiness',
					render: function (data, type, row, meta) {

						return data === true ? 'Business' : 'Individual';
					}
				},
				{

					targets: 5,
					data: 'agent.phoneNo',
					name: 'phoneNo',
				},
				{
					targets: 6,
					data: 'agent.city',
					name: 'City',
				},
				{
					targets: 7,
					data: 'organizationUnitDisplayName',
					name: 'OrganizationUnitDisplayName',
				},
				{
					targets: 8,
					data: 'agent.clientCount',
					name: 'clientCount',
				},
				{
					targets: 9,
					data: 'agent.applicationCount',
					name: 'applicationCount',
				},
				{
					targets: 10,
					width: 30,
					data: null,
					orderable: false,
					searchable: false,
					render: function (data, type, full, meta) {
						console.log(data);
						var rowId = data.agent.id;
						var rowData = data.agent;
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

		function getAgents() {
			dataTable.ajax.reload();
		}

		function deleteAgent(partner) {
			abp.message.confirm(
				'',
				app.localize('AreYouSure'),
				function (isConfirmed) {
					if (isConfirmed) {
						_agentsService.delete({
							id: partner.id
						}).done(function () {
							getAgents(true);
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
					debugger
					app.downloadTempFile(result);
				});
		});

		abp.event.on('app.createOrEditPartnerModalSaved', function () {
			getAgents();
		});

		$('#GetPartnersButton').click(function (e) {
			e.preventDefault();
			getAgents();
		});

		$(document).keypress(function (e) {
			if (e.which === 13) {
				getAgents();
			}
		});

		$('.reload-on-change').change(function (e) {
			getAgents();
		});

		$('.reload-on-keyup').keyup(function (e) {
			getAgents();
		});

		$('#btn-reset-filters').click(function (e) {
			$('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
			getAgents();
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
				window.location = "/AppAreaName/Agents/DetailsForm/" + rowId;
			} else if (action === 'edit') {
				window.location = "/AppAreaName/Agents/CreateOrEdit/" + rowId;
			} else if (action === 'delete') {
				deleteAgent(rowId);
			}
		});


		$(document).on('click', '#SelectAllCheckBox', function () {
			chkAll();
		})
		function chkAll() {
			debugger;
			// Get the "Select All" checkbox
			var chkAll = $("#SelectAllCheckBox");

			// Get all the checkboxes except for the "Select All" checkbox
			var checkboxes = $("input.custom-checkbox").not(chkAll);

			// If the "Select All" checkbox is checked, check all the other checkboxes
			if (chkAll.prop("checked")) {
				checkboxes.prop("checked", true);
				//$(".card").hide();
				//$(".btnSmsMail").show();
				var elements = document.getElementsByClassName("btnSmsMail");

				// Loop through the collection and remove the "hidden" attribute for each element
				for (var i = 0; i < elements.length; i++) {
					elements[i].removeAttribute("hidden");
				}
			}
			// If the "Select All" checkbox is not checked, uncheck all the other checkboxes
			else {
				checkboxes.prop("checked", false);
				//$(".btnSmsMail").hide();
				var elements = document.getElementsByClassName("btnSmsMail");

				// Loop through the collection and apply the "hidden" attribute for each element
				for (var i = 0; i < elements.length; i++) {
					elements[i].setAttribute("hidden", true);
				}
			}
		}


		$('#AgentsTable').on('click', '.custom-checkbox', function () {
			// Access the checked state of the clicked checkbox
			var isChecked = $(this).prop('checked');
			if (isChecked == true) {
				//$(".btnSmsMail").show();
				var elements = document.getElementsByClassName("btnSmsMail");

				// Loop through the collection and remove the "hidden" attribute for each element
				for (var i = 0; i < elements.length; i++) {
					elements[i].removeAttribute("hidden");
				}
			}
			// If the "Select All" checkbox is not checked, uncheck all the other checkboxes
			else {
				//$(".btnSmsMail").hide();
				var elements = document.getElementsByClassName("btnSmsMail");

				// Loop through the collection and apply the "hidden" attribute for each element
				for (var i = 0; i < elements.length; i++) {
					elements[i].setAttribute("hidden", true);
				}
			}
		});


		$(document).on('click', '#Active', function () {

            var Prospect = 0;
            $("#TabValues").val(Prospect);
            //if ($.fn.DataTable.isDataTable('#ClientsTable')) {
            //    var table = $('#ClientsTable').DataTable();
            //    table.clear().destroy();
            //}
           
        });
		$(document).on('click', '#Inactive', function () {

            var clients = 1;
            $("#TabValues").val(clients) 
        });
  });
})();
