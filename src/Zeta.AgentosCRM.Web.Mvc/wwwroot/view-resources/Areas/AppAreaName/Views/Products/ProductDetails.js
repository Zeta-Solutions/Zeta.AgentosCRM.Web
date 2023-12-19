﻿(function () {

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
                 if (data.result.profilePicture == null || data.result.profilePicture == '') {
                     var fullname = $("#ProductName").val();
                     let firstNameInitial = fullname.charAt(0).toUpperCase();
                     let initials = `${firstNameInitial}`;
                     $('#initals').text(initials);
                     $('#initalsdiv').css({
                         'height': '150px',
                         'width': '150px'
                     });
                 }
                 else {
                     $('#profileImage').attr('src', "data:image/png;base64," + data.result.profilePicture);

                 }
             })
            .fail(function (error) {
                console.error('Error fetching data:', error);
            });
    });
})();