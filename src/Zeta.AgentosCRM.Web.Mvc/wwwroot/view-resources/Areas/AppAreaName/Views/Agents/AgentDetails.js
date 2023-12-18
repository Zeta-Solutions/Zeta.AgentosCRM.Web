(function () {

    $(function () {
        var agentId = $('input[name="AgentId"]').val();
        var imageUrl = $.ajax({
            url: abp.appPath + 'api/services/app/AgentProfile/GetProfilePictureByAgent',
            data: {
                agentId: agentId,
            },
            method: 'GET',
            dataType: 'json',
        })
            .done(function (data) {
                debugger
                console.log('Response from server:', data);
                if (data.result.profilePicture == null || data.result.profilePicture == '') {
                    var fullname = $("#AgentName").val();
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