(function ($) {
    //...
    app.modals.CreateOrEditEnglishTestScoreModal = function () {
        debugger
        var hiddenfield = $("#ID").val();
        $("#ClientId").val(hiddenfield);
        $('input[name*="clientId"]').val(hiddenfield)
        getRecordsById(hiddenfield);
        debugger
        function getRecordsById(hiddenfield) {
            debugger
            $.ajax({
                url: abp.appPath + 'api/services/app/EnglisTestScores/GetAll',
                data: {
                    ClientIdFilter: hiddenfield,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                    debugger
                    console.log('Response from server:', data);
                    globalenglishData = data;
                    if (data || data.length > 1) {
                        fetchrecord(data);
                    }

                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function fetchrecord(data) {
            debugger;
            var modal = $(this);
            var reversedItems = data.result.items.slice().reverse();

            for (var i = 0; i < data.result.items.length; i++) {
                var Name = "name" + (i + 1);
                var idListening = "listenting" + (i + 1);
                var idReading = "Reading" + (i + 1);
                var idWriting = "Writing" + (i + 1);
                var idSpeaking = "Speaking" + (i + 1);
                var idTotalScore = "TotalScore" + (i + 1);
                var currentData = reversedItems[i].englisTestScore;


                $("." + Name).text(currentData.name);
                $("." + idListening).val(currentData.listenting);
                $("." + idReading).val(currentData.reading);
                $("." + idWriting).val(currentData.writing);
                $("." + idSpeaking).val(currentData.speaking);
                $("." + idTotalScore).val(currentData.totalScore);


            }

            _createOrEditenglishscoreModal.open();
        }


        var _englisTestScoresService = abp.services.app.englisTestScores;

        var _modalManager;
        var _$englisTestScoresInformationsForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$englisTestScoresInformationsForm = _modalManager.getModal().find('form[name=EnglishTestScoreInformationsForm]');
            _$englisTestScoresInformationsForm.validate();
        };

        this.save = function () {
            if (!_$englisTestScoresInformationsForm.valid()) {
                return;
            }
            debugger
            var items = [];

            // Assuming each entry has a unique identifier, you may need to modify this part accordingly
            for (var i = 1; i <= 3; i++) {
                var Name = "name" + i;
                var idListening = "listenting" + i;
                var idReading = "Reading" + i;
                var idWriting = "Writing" + i;
                var idSpeaking = "Speaking" + i;
                var idTotalScore = "TotalScore" + i;
                var hiddenfield = $("#ClientId").val();
                var NameValue = $("." + Name).text(); // or .val() depending on the actual element type
                debugger
                var subject = {
                    Name: NameValue,
                    listenting: parseFloat($("." + idListening).val()),
                    Reading: parseFloat($("." + idReading).val()),
                    Writing: parseFloat($("." + idWriting).val()),
                    Speaking: parseFloat($("." + idSpeaking).val()),
                    TotalScore: parseFloat($("." + idTotalScore).val()),
                    clientId: hiddenfield,
                };
                items.push(subject);
            }
            debugger
            row1 = $('.card-text.col-md-2 input[name="Id0"]').val();
            row2 = $('.card-text.col-md-2 input[name="Id1"]').val();
            row3 = $('.card-text.col-md-2 input[name="Id2"]').val();
            $.each(items, function (index, data) {
                var Steps = JSON.stringify(data);
                debugger
                _englisTestScoresService
                    .delete({
                        id: row1,
                    })
                _englisTestScoresService.delete({
                    id: row2,
                })
                _englisTestScoresService.delete({
                    id: row3,
                })
                //.delete(row2) 
                //.delete(row3) 
                Steps = JSON.parse(Steps);
                _modalManager.setBusy(true);
                _englisTestScoresService
                    .createOrEdit(Steps) // Pass an object with a 'subjects' property
                    .done(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        _modalManager.close();
                        abp.event.trigger('app.createOrEditEducationModalSaved');
                    })
                    .always(function () {
                        _modalManager.setBusy(false);
                    });
            });
        };

        function getTestName(order) {
            if (order === 1) {
                return "TOEFl";
            } else if (order === 2) {
                return "IELTS";
            } else if (order === 3) {
                return "PTE";
            }
        }
    };
})(jQuery);
