(function () {
    $("#kt_app_sidebar_toggle").trigger("click");
    $('.tag').select2();
    //$('.btnSmsMail').hide();

    $(function () {

        var _$clientsTable = $('#ClientsTable');
        //console.log(_$clientsTable);
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



        //var _createOrEditModalEmail = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/Clients/ClientEmailCompose',
        //    modalClass: 'ClientEmailCompose'
        //});
        var _createOrEditModalEmail = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/SentEmail/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/SentEmail/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditSentEmailModal',
        });
        var _createOrEditClientTagModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/CreateOrEditClientTags',
            modalClass: 'CreateOrEditClientTagsModal'
        });
        //var _createOrEditModal = new app.ModalManager({
        //    viewUrl: abp.appPath + 'AppAreaName/Partners/CreateOrEditClientTags',
        //    scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Partners/Tasks/_CreateOrEditModal.js',
        //    modalClass: 'CreateOrEditTasksModal',
        //});
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
                        countryNameFilter: $('#CountryName3FilterId').val()
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
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, full, meta) {
                        /* console.log(data);*/
                        var rowId = data.client.id;
                        console.log(rowId);
                        var contaxtMenu = '<div class="context-menu" style="position: absolute;">' +
                            '<div><input type="checkbox" class="custom-checkbox" value="' + data.client.email + '"></div>' +
                            '<input hidden class="phoneNo" value="' + data.client.phoneNo + '"></div>' +
                            '<input hidden class="clientchangeassignee" value="' + data.client.id + '"></div>' +
                            '</div>';


                        return contaxtMenu;
                    }
                },
                {
                    width: 200,
                    targets: 2,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        let firstNameInitial = row.client.firstName.charAt(0).toUpperCase();
                        let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                        let initials = `${firstNameInitial}${lastNameInitial}`;
                        let fullName = `${row.client.firstName} ${row.client.lastName}`;

                        let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.client.id}`;
                        let clientEmailComposeUrl = `/AppAreaName/Clients/ClientEmailCompose?id=${row.client.id}`;
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
             <a href="#" class="EmailForm" data-id="${row.client.id}">${row.client.email}</a>
        </div>
    </div>
`;
                    },

                    name: 'concatenatedData',

                },
                {
                    targets: 3,
                    data: 'tagName',
                    name: 'tagName',
                },
                {
                    targets: 4,
                    data: 'client.phoneNo',
                    name: 'phoneNo',
                },
                {
                    targets: 5,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        let city = row.client.city;
                        let countryName = row.countryName;

                        let fullName = `${city} <br> ${countryName}`;

                        return ` ${fullName}`;
                    },
                    name: 'concatenatedData',
                },
                {
                    targets: 6,
                    data: "userName",
                    name: "userName"
                },
                {
                    targets: 7,
                    data: 'userName',
                    name: 'userName',
                },
                {
                    targets: 8,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row) {
                        let isDiscontinue = row.client.isAnyApplicationActive;
                        let count = row.client.applicationCount;

                        if (isDiscontinue == false && count > 0) {
                            return `<span style="color: red; font-size: 14px;">&#8226;Discontinue</span>`;
                        } else if (isDiscontinue == true && count > 0) {
                            return `<span style="color:blue; font-size: 14px;">&#8226;InProgress</span>`;
                        } else {
                            return `<span style="color:red; font-size: 14px;">&#8226;No-Application</span>`;
                        }
                    },

                    name: 'concatenatedData',
                },
                {
                    targets: 9,
                    data: 'client.applicationCount',
                    name: 'applicationCount',
                },
                {
                    targets: 10,
                    data: 'client.lastModificationTime',
                    name: 'lastModificationTime',

                    render: function (lastModificationTime) {
                        if (lastModificationTime) {
                            return moment(lastModificationTime).format('L');
                        }
                        return "";
                    }
                },

                {
                    targets: 11,
                    width: 30,
                    data: null,
                    orderable: false,
                    searchable: false,
                    render: function (data, type, full, meta) {
                        console.log(data);
                        var rowId = data.client.id;
                        var rowData = data.client;
                        var RowDatajsonString = JSON.stringify(rowData);
                        var contextMenu = '<div class="context-menu" style="position:relative;">' +
                            '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                            '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                            '<ul style="list-style: none; padding: 0;color:black">' +
                            '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
                            '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                            '<a href="#" style="color: black;" data-action="archived" data-id="' + rowId + '"><li>Archived</li></a>' +
                            "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                            '</ul>' +
                            '</div>' +
                            '</div>';

                        return contextMenu;
                    }


                },
            ]
        });
        //$(".custom-checkbox").click(function () {
        //     


        //});
        function getClients() {
            dataTable.ajax.reload();
        }

        function tabgetClients() {
             
            if ($("#TabValues").val() == 1) {


                var filters = {
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
                    countryNameFilter: $('#CountryName3FilterId').val(),
                    isArchived: false,
                };
            }

            else if ($("#TabValues").val() == 2) {


                var filters = {
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
                    countryNameFilter: $('#CountryName3FilterId').val(),
                    isArchived: true,
                };
            }

            else if ($("#TabValues").val() == 0) {


                var filters = {
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
                    countryNameFilter: $('#CountryName3FilterId').val(),
                };
            }
            var filters = JSON.stringify(filters);
            filters = JSON.parse(filters)
            // dataTable.ajax.reload();
            _clientsService.getAll(filters)
                .then(function (data) {
                     
                    console.log('Data from service:', data);
                    //dataTable.ajax.reload();
                    dataTable.clear().rows.add(data.items).draw();
                })
                .catch(function (error) {
                    console.error('Error:', error);
                });

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
        $(document).on('click', '.EmailForm', function (e) {
            e.preventDefault();
            var rowId = $(this).data('id');
            var Email = $(this).text();
            $("#GetEmail").val(Email);
            _createOrEditModalEmail.open(rowId);

        });
        // Handle menu item clicks
        $(document).on('click', 'a[data-action]', function (e) {
            e.preventDefault();

            var rowId = $(this).data('id');
            var action = $(this).data('action');

            // Handle the selected action based on the rowId
            if (action === 'view') {
                window.location = "/AppAreaName/Clients/ClientProfileDetail/" + rowId;

            } else if (action === 'edit') {
                window.location = "/AppAreaName/Clients/ClientCreateDetail/" + rowId;

            } else if (action === 'delete') {
                deleteClient(rowId);
            }
            else if (action === 'archived') {
                if ($("#TabValues").val() == 2) {
                    var inputData = {
                        clientId: rowId,
                        isArchived: 0
                    };
                }
                else {

                    var inputData = {
                        clientId: rowId,
                        isArchived: 1
                    };
                }
                var Steps = JSON.stringify(inputData);
                Steps = JSON.parse(Steps);
                _clientsService
                    .updateClientIsArchived(Steps)
                    .done(function () {
                        abp.notify.info(app.localize('ArchivedSuccessfully'));
                        location.reload();
                    })
            }
        });
        // Add an event listener to handle the click event for the full name link
        $(document).on('click', '.text-truncate', function (e) {
            e.preventDefault(); // Prevent the default behavior of the link
            window.location.href = $(this).attr('href'); // Redirect to the specified URL
        });

        var selectedCheckboxes = [];
        var commaSeparatedEmails;
        var commaSeparatedPhoneNo;

        $(document).on('click', '#SelectAllCheckBox', function () {
            chkAll();
            updateSelectedCheckboxes();
            updateSelectedCheckboxesPhoneNo();
        });

        $(document).on('click', 'input.custom-checkbox', function () {
            if (!$(this).prop("checked")) {
                // If any custom checkbox is unchecked, uncheck the "Select All" checkbox
                $("#SelectAllCheckBox").prop("checked", false);
                //$(".btnSmsMail").hide();
            }
            updateSelectedCheckboxes();
            updateSelectedCheckboxesPhoneNo();
        });

        function updateSelectedCheckboxes() {
            selectedCheckboxes = $("input.custom-checkbox:checked").map(function () {
                return $(this).val();
            }).get();

            commaSeparatedEmails = selectedCheckboxes.join(',');

        }
        function updateSelectedCheckboxesPhoneNo() {

            selectedCheckboxes = $("input.custom-checkbox:checked").map(function () {
                return $(".context-menu .phoneNo").val();
            }).get();

            commaSeparatedPhoneNo = selectedCheckboxes.join(',');

            console.log(commaSeparatedPhoneNo);
        }
        function chkAll() {
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

                for (var i = 0; i < elements.length; i++) {
                    elements[i].removeAttribute("hidden");
                }
            }
            // If the "Select All" checkbox is not checked, uncheck all the other checkboxes
            else {
                checkboxes.prop("checked", false);
                //$(".btnSmsMail").hide();
                var elements = document.getElementsByClassName("btnSmsMail");

                for (var i = 0; i < elements.length; i++) {
                    elements[i].setAttribute("hidden", true);
                }
            }
        }

        $('#ClientsTable').on('click', 'input.custom-checkbox', function () {
            // Check if any of the checkboxes are checked
             
            var anyChecked = $('.custom-checkbox:checked').length > 0;

            // If any checkbox is checked, show the button
            if (anyChecked) {
                var elements = document.getElementsByClassName("btnSmsMail");

                for (var i = 0; i < elements.length; i++) {
                    elements[i].removeAttribute("hidden");
                }
            } else {

                var elements = document.getElementsByClassName("btnSmsMail");

                for (var i = 0; i < elements.length; i++) {
                    elements[i].setAttribute("hidden", true);
                }
            }
            updateSelectedCheckboxes();
            updateSelectedCheckboxesPhoneNo();
        });


        $(document).on('click', '#SendMail', function (e) {
            e.preventDefault();
            var Email = commaSeparatedEmails;
            $("#GetEmail").val(Email);
            _createOrEditModalEmail.open();

        });
        $(document).on('click', '#SendBulkSMS', function (e) {
            e.preventDefault();
             
            var PhoneNo = commaSeparatedPhoneNo;
            alert(PhoneNo);
        });
        $(document).on('click', '#Prospects', function () {


            $("#SelectAllCheckBox").prop("checked", false);
            commaSeparatedEmails = "";
            commaSeparatedPhoneNo = "";

            var Prospect = 0;
            $("#TabValues").val(Prospect);
            if ($.fn.DataTable.isDataTable('#ClientsTable')) {
                var table = $('#ClientsTable').DataTable();
                table.clear().destroy();
            }
            var dataTable = _$clientsTable.DataTable({
                paging: true,
                serverSide: true,
                processing: true,
                listAction: {
                    ajaxFunction: _clientsService.getAll,
                    //inputFilter: function () {
                    //    return {
                    //        filter: $('#ClientsTableFilter').val(),
                    //        firstNameFilter: $('#FirstNameFilterId').val(),
                    //        lastNameFilter: $('#LastNameFilterId').val(),
                    //        emailFilter: $('#EmailFilterId').val(),
                    //        phoneNoFilter: $('#PhoneNoFilterId').val(),
                    //        minDateofBirthFilter: getDateFilter($('#MinDateofBirthFilterId')),
                    //        maxDateofBirthFilter: getMaxDateFilter($('#MaxDateofBirthFilterId')),
                    //        universityFilter: $('#UniversityFilterId').val(),
                    //        minRatingFilter: $('#MinRatingFilterId').val(),
                    //        maxRatingFilter: $('#MaxRatingFilterId').val(),
                    //        countryDisplayPropertyFilter: $('#CountryDisplayPropertyFilterId').val(),
                    //        userNameFilter: $('#UserNameFilterId').val(),
                    //        binaryObjectDescriptionFilter: $('#BinaryObjectDescriptionFilterId').val(),
                    //        degreeLevelNameFilter: $('#DegreeLevelNameFilterId').val(),
                    //        subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
                    //        leadSourceNameFilter: $('#LeadSourceNameFilterId').val(),
                    //        countryNameFilter: $('#CountryName3FilterId').val(), 
                    //    };
                    //}
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
                        targets: 1,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, full, meta) {
                            /* console.log(data);*/
                            var rowId = data.client.id;
                            var contaxtMenu = '<div class="context-menu" style="position: absolute;">' +
                                '<div><input type="checkbox" class="custom-checkbox" value="' + data.client.email + '"></div>' +
                                '<input hidden class="phoneNo" value="' + data.client.phoneNo + '"></div>' +
                                '<input hidden class="clientId" value="' + data.client.id + '"></div>' +
                                '</div>';



                            return contaxtMenu;
                        }
                    },
                    {
                        width: 200,
                        targets: 2,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let firstNameInitial = row.client.firstName.charAt(0).toUpperCase();
                            let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                            let initials = `${firstNameInitial}${lastNameInitial}`;
                            let fullName = `${row.client.firstName} ${row.client.lastName}`;

                            let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.client.id}`;
                            let clientEmailComposeUrl = `/AppAreaName/Clients/ClientEmailCompose?id=${row.client.id}`;
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
                                         <a href="#" class="EmailForm" data-id="${row.client.id}">${row.client.email}</a>
                                    </div>
                                </div>
                            `;
                        },

                        name: 'concatenatedData',

                    },
                    {
                        targets: 3,
                        data: 'tagName',
                        name: 'tagName',
                    },
                    {
                        targets: 4,
                        data: 'client.phoneNo',
                        name: 'phoneNo',
                    },
                    {
                        targets: 5,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let city = row.client.city;
                            let countryName = row.countryName;

                            let fullName = `${city} <br> ${countryName}`;

                            return ` ${fullName}`;
                        },
                        name: 'concatenatedData',
                    },
                    {
                        targets: 6,
                        data: "userName",
                        name: "userName"
                    },
                    {
                        targets: 7,
                        data: 'userName',
                        name: 'userName',
                    },
                    {
                        targets: 8,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let isDiscontinue = row.client.isAnyApplicationActive;
                            let count = row.client.applicationCount;

                            if (isDiscontinue == false && count > 0) {
                                return `<span style="color: red; font-size: 14px;">&#8226;Discontinue</span>`;
                            } else if (isDiscontinue == true && count > 0) {
                                return `<span style="color:blue; font-size: 14px;">&#8226;InProgress</span>`;
                            } else {
                                return `<span style="color:red; font-size: 14px;">&#8226;No-Application</span>`;
                            }
                        },

                        name: 'concatenatedData',
                    },
                    {
                        targets: 9,
                        data: 'client.applicationCount',
                        name: 'applicationCount',
                    },
                    {
                        targets: 10,
                        data: 'client.lastModificationTime',
                        name: 'lastModificationTime',

                        render: function (lastModificationTime) {
                            if (lastModificationTime) {
                                return moment(lastModificationTime).format('L');
                            }
                            return "";
                        }
                    },
                    {
                        targets: 11,
                        width: 30,
                        data: null,
                        orderable: false,
                        searchable: false,
                        render: function (data, type, full, meta) {
                            console.log(data);
                            var rowId = data.client.id;
                            var rowData = data.client;
                            var RowDatajsonString = JSON.stringify(rowData);
                            var contextMenu = '<div class="context-menu" style="position:relative;">' +
                                '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                                '<ul style="list-style: none; padding: 0;color:black">' +
                                '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
                                '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                                '<a href="#" style="color: black;" data-action="archived" data-id="' + rowId + '"><li>Archived</li></a>' +
                                "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                                '</ul>' +
                                '</div>' +
                                '</div>';

                            return contextMenu;
                        }


                    },
                ]
            });

        });
        $(document).on('click', '#clients', function () {

            $("#SelectAllCheckBox").prop("checked", false);
            commaSeparatedEmails = "";
            commaSeparatedPhoneNo = "";

            var clients = 1;
            $("#TabValues").val(clients)
            //if ($.fn.DataTable.isDataTable('#ClientsTable')) {
            //    $('#ClientsTable').DataTable().destroy();
            //}
            if ($.fn.DataTable.isDataTable('#ClientsTable')) {
                var table = $('#ClientsTable').DataTable();
                table.clear().destroy();
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
                            countryNameFilter: $('#CountryName3FilterId').val(),
                            isArchived: false,
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
                        targets: 1,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, full, meta) {
                            var rowId = data.client.id;
                            var contaxtMenu = '<div class="context-menu" style="position: absolute;">' +
                                '<div><input type="checkbox" class="custom-checkbox" value="' + data.client.email + '"></div>' +
                                '<input hidden class="phoneNo" value="' + data.client.phoneNo + '"></div>' +
                                '<input hidden class="clientId" value="' + data.client.id + '"></div>' +
                                '</div>';



                            return contaxtMenu;
                        }
                    },
                    {
                        width: 200,
                        targets: 2,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let firstNameInitial = row.client.firstName.charAt(0).toUpperCase();
                            let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                            let initials = `${firstNameInitial}${lastNameInitial}`;
                            let fullName = `${row.client.firstName} ${row.client.lastName}`;

                            let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.client.id}`;
                            let clientEmailComposeUrl = `/AppAreaName/Clients/ClientEmailCompose?id=${row.client.id}`;
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
                                         <a href="#" class="EmailForm" data-id="${row.client.id}">${row.client.email}</a>
                                    </div>
                                </div>
                            `;
                        },

                        name: 'concatenatedData',

                    },
                    {
                        targets: 3,
                        data: 'tagName',
                        name: 'tagName',
                    },
                    {
                        targets: 4,
                        data: 'client.phoneNo',
                        name: 'phoneNo',
                    },
                    {
                        targets: 5,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let city = row.client.city;
                            let countryName = row.countryName;

                            let fullName = `${city} <br> ${countryName}`;

                            return ` ${fullName}`;
                        },
                        name: 'concatenatedData',
                    },
                    {
                        targets: 6,
                        data: "userName",
                        name: "userName"
                    },
                    {
                        targets: 7,
                        data: 'userName',
                        name: 'userName',
                    },
                    {
                        targets: 8,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let isDiscontinue = row.client.isAnyApplicationActive;
                            let count = row.client.applicationCount;

                            if (isDiscontinue == false && count > 0) {
                                return `<span style="color: red; font-size: 14px;">&#8226;Discontinue</span>`;
                            } else if (isDiscontinue == true && count > 0) {
                                return `<span style="color:blue; font-size: 14px;">&#8226;InProgress</span>`;
                            } else {
                                return `<span style="color:red; font-size: 14px;">&#8226;No-Application</span>`;
                            }
                        },

                        name: 'concatenatedData',
                    },
                    {
                        targets: 9,
                        data: 'client.applicationCount',
                        name: 'applicationCount',
                    },
                    {
                        targets: 10,
                        data: 'client.lastModificationTime',
                        name: 'lastModificationTime',

                        render: function (lastModificationTime) {
                            if (lastModificationTime) {
                                return moment(lastModificationTime).format('L');
                            }
                            return "";
                        }
                    },

                    {
                        targets: 11,
                        width: 30,
                        data: null,
                        orderable: false,
                        searchable: false,
                        render: function (data, type, full, meta) {
                            console.log(data);
                            var rowId = data.client.id;
                            var rowData = data.client;
                            var RowDatajsonString = JSON.stringify(rowData);
                            var contextMenu = '<div class="context-menu" style="position:relative;">' +
                                '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                                '<ul style="list-style: none; padding: 0;color:black">' +
                                '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
                                '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                                '<a href="#" style="color: black;" data-action="archived" data-id="' + rowId + '"><li>Archived</li></a>' +
                                "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                                '</ul>' +
                                '</div>' +
                                '</div>';

                            return contextMenu;
                        }


                    },
                ]
            });
        });
        $(document).on('click', '#Archived', function () {

            $("#SelectAllCheckBox").prop("checked", false);
            commaSeparatedEmails = "";
            commaSeparatedPhoneNo = "";

            var Archived = 2;
            $("#TabValues").val(Archived)
            if ($.fn.DataTable.isDataTable('#ClientsTable')) {
                var table = $('#ClientsTable').DataTable();
                table.clear().destroy();
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
                            countryNameFilter: $('#CountryName3FilterId').val(),
                            isArchived: true,
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
                        targets: 1,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, full, meta) {
                            var rowId = data.client.id;
                            var contaxtMenu = '<div class="context-menu" style="position: absolute;">' +
                                '<div><input type="checkbox" class="custom-checkbox" value="' + data.client.email + '"></div>' +
                                '<input hidden class="phoneNo" value="' + data.client.phoneNo + '"></div>' +
                                '<input hidden class="clientId" value="' + data.client.id + '"></div>' +
                                '</div>';



                            return contaxtMenu;
                        }
                    },
                    {
                        width: 200,
                        targets: 2,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let firstNameInitial = row.client.firstName.charAt(0).toUpperCase();
                            let lastNameInitial = row.client.lastName.charAt(0).toUpperCase();
                            let initials = `${firstNameInitial}${lastNameInitial}`;
                            let fullName = `${row.client.firstName} ${row.client.lastName}`;

                            let clientDetailUrl = `/AppAreaName/Clients/ClientProfileDetail?id=${row.client.id}`;
                            let clientEmailComposeUrl = `/AppAreaName/Clients/ClientEmailCompose?id=${row.client.id}`;
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
                                         <a href="#" class="EmailForm" data-id="${row.client.id}">${row.client.email}</a>
                                    </div>
                                </div>
                            `;
                        },

                        name: 'concatenatedData',

                    },
                    {
                        targets: 3,
                        data: 'tagName',
                        name: 'tagName',
                    },
                    {
                        targets: 4,
                        data: 'client.phoneNo',
                        name: 'phoneNo',
                    },
                    {
                        targets: 5,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let city = row.client.city;
                            let countryName = row.countryName;

                            let fullName = `${city} <br> ${countryName}`;

                            return ` ${fullName}`;
                        },
                        name: 'concatenatedData',
                    },
                    {
                        targets: 6,
                        data: "userName",
                        name: "userName"
                    },
                    {
                        targets: 7,
                        data: 'userName',
                        name: 'userName',
                    },
                    {
                        targets: 8,
                        data: null,
                        orderable: false,
                        autoWidth: false,
                        defaultContent: '',
                        render: function (data, type, row) {
                            let isDiscontinue = row.client.isAnyApplicationActive;
                            let count = row.client.applicationCount;

                            if (isDiscontinue == false && count > 0) {
                                return `<span style="color: red; font-size: 14px;">&#8226;Discontinue</span>`;
                            } else if (isDiscontinue == true && count > 0) {
                                return `<span style="color:blue; font-size: 14px;">&#8226;InProgress</span>`;
                            } else {
                                return `<span style="color:red; font-size: 14px;">&#8226;No-Application</span>`;
                            }
                        },

                        name: 'concatenatedData',
                    },
                    {
                        targets: 9,
                        data: 'client.applicationCount',
                        name: 'applicationCount',
                    },
                    {
                        targets: 10,
                        data: 'client.lastModificationTime',
                        name: 'lastModificationTime',

                        render: function (lastModificationTime) {
                            if (lastModificationTime) {
                                return moment(lastModificationTime).format('L');
                            }
                            return "";
                        }
                    },
                    {
                        targets: 11,
                        width: 30,
                        data: null,
                        orderable: false,
                        searchable: false,
                        render: function (data, type, full, meta) {
                            console.log(data);
                            var rowId = data.client.id;
                            var rowData = data.client;
                            var RowDatajsonString = JSON.stringify(rowData);
                            var contextMenu = '<div class="context-menu" style="position:relative;">' +
                                '<div class="ellipsis"><a href="#" data-id="' + rowId + '"><span class="fa fa-ellipsis-v"></span></a></div>' +
                                '<div class="options" style="display: none; color:black; left: auto; position: absolute; top: 0; right: 100%;border: 1px solid #ccc;   border-radius: 4px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1); padding:1px 0px; margin:1px 5px ">' +
                                '<ul style="list-style: none; padding: 0;color:black">' +
                                '<a href="#" style="color: black;" data-action="view" data-id="' + rowId + '"><li>View</li></a>' +
                                '<a href="#" style="color: black;" data-action="edit" data-id="' + rowId + '"><li>Edit</li></a>' +
                                '<a href="#" style="color: black;" data-action="archived" data-id="' + rowId + '"><li>Archived</li></a>' +
                                "<a href='#' style='color: black;' data-action='delete' data-id='" + RowDatajsonString + "'><li>Delete</li></a>" +
                                '</ul>' +
                                '</div>' +
                                '</div>';

                            return contextMenu;
                        }


                    },
                ]
            });
        });
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
        $('#CreateNewClientButton').click(function () {
            // _createOrEditModal.open();
            window.location.href = abp.appPath + 'AppAreaName/Clients/ClientCreateDetail';

        });

        var _createOrEditModalChangeAssignee = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/Clients/ClientChangeAssignee',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Clients/ClientChangeAssignee/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditChangeAssignee',
        });

        //$('#ChangeAssignee').click(function () {

        //    e.preventDefault(); 

        //    _createOrEditModalChangeAssignee.open();

        //});



        $(document).on('click', '#ChangeAssignee', function (e) {
            e.preventDefault();


            _createOrEditModalChangeAssignee.open();


        });
        //$(document).on('change', '#LastNameFilterId', function (e) {

        //    e.preventDefault();


        //    getClients();


        //});
        //Client Tags Modal
        //$('#CreateNewClientsTagsButton').click(function () {
        //     _createOrEditClientTagModal.open();

        //});
        //function getClientsTags() {
        //    dataTable.ajax.reload();
        //}
        //abp.event.on('app.createOrEditClientTagsModalSaved', function () {
        //    getClientsTags();
        //});
        $('#ExportToExcelButton').click(function () {
             
            //_clientsService
            //    .getClientsToExcel({
            //        filter: $('#ClientsTableFilter').val(),
            //        firstNameFilter: $('#FirstNameFilterId').val(),
            //        lastNameFilter: $('#LastNameFilterId').val(),
            //        emailFilter: $('#EmailFilterId').val(),
            //        phoneNoFilter: $('#PhoneNoFilterId').val(),
            //        minDateofBirthFilter: getDateFilter($('#MinDateofBirthFilterId')),
            //        maxDateofBirthFilter: getMaxDateFilter($('#MaxDateofBirthFilterId')),
            //        universityFilter: $('#UniversityFilterId').val(),
            //        minRatingFilter: $('#MinRatingFilterId').val(),
            //        maxRatingFilter: $('#MaxRatingFilterId').val(),
            //        countryDisplayPropertyFilter: $('#CountryDisplayPropertyFilterId').val(),
            //        userNameFilter: $('#UserNameFilterId').val(),
            //        binaryObjectDescriptionFilter: $('#BinaryObjectDescriptionFilterId').val(),
            //        degreeLevelNameFilter: $('#DegreeLevelNameFilterId').val(),
            //        subjectAreaNameFilter: $('#SubjectAreaNameFilterId').val(),
            //        leadSourceNameFilter: $('#LeadSourceNameFilterId').val(), 
            //        countryNameFilter: $('#CountryName3FilterId').val()
            //    })
            //    .done(function (result) {
            //        app.downloadTempFile(result);
            //    });





            if ($("#TabValues").val() == 1) {

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
                        countryNameFilter: $('#CountryName3FilterId').val(),
                        isArchived: false,
                    })
                    .done(function (result) {
                        app.downloadTempFile(result);
                    })
                    .fail(function (error) {
                        alert(error)
                        console.error('Error fetching data:', error);
                    });


            }

            else if ($("#TabValues").val() == 2) {

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
                        countryNameFilter: $('#CountryName3FilterId').val(),
                        isArchived: true,
                    })
                    .done(function (result) {
                        app.downloadTempFile(result);
                    })
                    .fail(function (error) {
                        alert(error)
                        console.error('Error fetching data:', error);
                    });
            }

            else if ($("#TabValues").val() == 0) {

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
                        countryNameFilter: $('#CountryName3FilterId').val()
                    })
                    .done(function (result) {
                        app.downloadTempFile(result);
                    })
                    .fail(function (error) {
                        alert(error)
                        console.error('Error fetching data:', error);
                    });
            }
        });

        abp.event.on('app.createOrEditClientModalSaved', function () {
            getClients();
        });

        $('#GetClientsButton').click(function (e) {
            e.preventDefault();
            getClients();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getClients();
            }
        });

        $('.reload-on-change').change(function (e) {
            getClients();
        });

        $('.reload-on-keyup').keyup(function (e) {
             
            //getClients();
            tabgetClients();
        });

        $('#btn-reset-filters').click(function (e) {
            $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
            getClients();
        });
    });
})();
