(function () {

    $(function () {
        var productId = $('input[name="ProductId"]').val();
        var imageUrl = $.ajax({
            url: abp.appPath + 'api/services/app/ProductProfile/GetProfilePictureByProduct',
            data: {
                productId: productId,
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