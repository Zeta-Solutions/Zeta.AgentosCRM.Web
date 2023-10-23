(function ($) {
    app.modals.BinaryObjectLookupTableModal = function () {

        var _modalManager;

        var _clientsService = abp.services.app.clients;
        var _$binaryObjectTable = $('#BinaryObjectTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };

        var dataTable = _$binaryObjectTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _clientsService.getAllBinaryObjectForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#BinaryObjectTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" + app.localize('Select') + "' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#BinaryObjectTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getBinaryObject() {
            dataTable.ajax.reload();
        }

        $('#GetBinaryObjectButton').click(function (e) {
            e.preventDefault();
            getBinaryObject();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $('#BinaryObjectTableFilter').keypress(function (e) {
            if (e.which === 13 && e.target.tagName.toLocaleLowerCase() != 'textarea') {
                getBinaryObject();
            }
        });

    };
})(jQuery);