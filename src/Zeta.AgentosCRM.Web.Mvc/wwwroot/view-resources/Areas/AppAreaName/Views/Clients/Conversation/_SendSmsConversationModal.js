(function ($) {
    app.modals.SendSmsModal = function () {
         ;
        


        
        var _clientsService = abp.services.app.clients;
        //var hiddenfield = $("#clientId").val();
        var hiddenfield = $("#clientAppID").val();
        //$("#clientId").val(hiddenfield);
        $("#Applointment_ClietName").val(hiddenfield);
        //$("#applointment_Invitees").select2()...;
        var hiddenfield = $("#ID").val();
        $("#ClientId").val(hiddenfield);


        $('input[name="clientId"]').val(hiddenfield)
        //$('input[name*="AddedById"]').val(1);
        var _modalManager;
        var _$clientAppointmentsInformationForm = null;


        //....
        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$clientAppointmentsInformationForm = _modalManager.getModal().find('form[name=AppointmentInformationsForm]');
            _$clientAppointmentsInformationForm.validate();
        };




        //$(document).on('blur', '#applointment_Invitees', function ()
        //{
        //    var applointment_InviteesID=$("#applointment_Invitees").val();
        //    console.log(applointment_InviteesID);
        //    $("#AddedById").val(applointment_InviteesID);
        //});
        $(document).off("click", "#saveSmsConversationBtn").on("click", "#saveSmsConversationBtn", function (e) {
             
            var inputGetData = {
                phoneNumber: $("input[name='number']").val(),
                message: $("#message").val()
            };
            var Getdata = JSON.stringify(inputGetData);
            Getdata = JSON.parse(Getdata);
            _clientsService
                .sendConversationSms(Getdata)
                .done(function (data) {
                    // 
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    
                })
        });
        $(document).off("click", "#closeSmsConversationBtn").on("click", "#closeSmsConversationBtn", function (e) {
            _modalManager.close();
        });
    };
})(jQuery);
