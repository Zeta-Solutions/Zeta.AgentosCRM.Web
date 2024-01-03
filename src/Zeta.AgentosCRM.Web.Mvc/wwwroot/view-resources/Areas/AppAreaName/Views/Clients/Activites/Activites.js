(function () {
    $(function () {
        var hiddenfield = $('input[name="Clientid"]').val();
        var dynamicValue = hiddenfield;
        var NameFilter = 'Zeta.AgentosCRM.CRMClient.Client';
        getActivityreload(dynamicValue, NameFilter);
        var globalData; // Declare the data variable in a broader scope
        function createActivityCard(item) {
            debugger;
            var clienActivitytname = $("#clientAppID").val();
             
            function renderTime(changeTime) {
                return changeTime ? moment(changeTime).format(' LT') : "";
            }
            var formattedTime = renderTime(item.changeTime);
            function renderDateTime(changeTime) {
                return changeTime ? moment(changeTime).format('MMM DD YYYY') : "";
            }
            var formattedDate = renderDateTime(item.changeTime);
            var imageUrl = $.ajax({
                url: abp.appPath + 'api/services/app/ClientProfile/GetProfilePictureByPictireId',
                data: {
                    fileTokkenId: item.profilePictureId,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    debugger
                    console.log('Response from server:', data);
                    $('.userimage').attr('src', "data:image/png;base64," + data.result.profilePicture);
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });

            // Call renderDateTime to get the formatted date
          
            const currentDate = new Date(item.changeTime);
            const today = new Date();

            // Calculate the time difference in milliseconds
            const timeDifference = today - currentDate;

            // Convert the time difference to days, hours, and minutes
            const days = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
            const hours = Math.floor((timeDifference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const minutes = Math.floor((timeDifference % (1000 * 60 * 60)) / (1000 * 60));

            // Format the result
            const result = `${days} days, ${hours} hours, ${minutes} minutes`;
            var cardHtml = `
        <div class="container">
            <ul class="timeline">
                <li>
                    <div class="timeline-time">
                        <span class="date">${formattedDate}<br/>${formattedTime}</span>
                    </div>
                    <div class="timeline-icon">
                        <a href="javascript:;">&nbsp;</a>
                    </div>
                    <div class="timeline-body">
                        <div class="timeline-header">
                            <img class="userimage"src="" alt/>
                            <span class="username"><a href="javascript:;">${item.userName}</a> <small></small></span>
                        </div>
                        <div class="timeline-content">
                            <p>
                               ${item.userName}  ${item.changeTypeName} Client  <strong>${clienActivitytname}</strong>    &nbsp; &nbsp;   <span class="text-muted">${result} ago</span> 
                            </p>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    `;

            // Return the HTML string
            return cardHtml;
        }
      






        function getActivityreload(dynamicValue,NameFilter) {
            debugger


           $.ajax({
               url: abp.appPath + 'api/services/app/AuditLog/GetEntityCustomizeChanges',
                data: {
                    EntityId: dynamicValue,
                    EntityTypeFullName: NameFilter
                },
                method: 'GET',
                dataType: 'json',
            })
               .done(function (data) {
                   debugger
                    // console.log('Response from server:', data);
                    globalData = data; // Assign data to the global variable....
                    processActivityData(); // Call processData function after data is available..
                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function processActivityData() {
            debugger
            var cardContainer = $('#cardContainerActivity');

            // Check if globalData.result.items is an array before attempting to iterate
            if (Array.isArray(globalData.result.items)) {
                // Iterate through items and create timeline cards
                for (var i = 0; i < globalData.result.items.length; i++) {
                    var item = globalData.result.items[i];
                    var card = createActivityCard(item);

                    cardContainer.append(card);
                }
            } else {
                console.error('globalData.result.items is not an array:', globalData.result.items);
            }
        }
            function clearMainDiv() {
                // Assuming main div has an id 'mainDiv', replace it with your actual id if needed
                $('.maindivcard').remove();

                    }
                });
})(jQuery);