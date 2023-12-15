(function () {

    $(function () {
        var partnerId = $('input[name="PartnerId"]').val();
        var imageUrl = $.ajax({
            url: abp.appPath + 'api/services/app/PartnerProfile/GetProfilePictureByPartner',
            data: {
                partnerId: partnerId,
            },
            method: 'GET',
            dataType: 'json',
        })
            .done(function (data) {
                debugger
                console.log('Response from server:', data);
                $('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);
            })
            .fail(function (error) {
                console.error('Error fetching data:', error);
            });
    });
})();