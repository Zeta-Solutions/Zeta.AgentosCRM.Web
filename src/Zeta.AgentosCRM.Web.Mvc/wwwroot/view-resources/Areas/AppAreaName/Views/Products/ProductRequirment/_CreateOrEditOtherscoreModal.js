(function ($) {
    app.modals.CreateOrEditOtherscoreModal = function () {
         
        var hiddenfield = $("#ProductId").val();

        $("#productId").val(hiddenfield);
        getRecordsById(hiddenfield);
         
        function getRecordsById(hiddenfield) {
             
            $.ajax({
                url: abp.appPath + 'api/services/app/ProductOtherTestRequirements/GetAll',
                data: {
                    ProductIdFilter: hiddenfield,
                },
                method: 'GET',
                dataType: 'json',
            })
                .done(function (data) {
                     
                    console.log('Response from server:', data);
                    globalOtherData = data;
                    if (data || data.length > 1) {
                        fetchrecord(data);
                    }

                })
                .fail(function (error) {
                    console.error('Error fetching data:', error);
                });
        }
        function fetchrecord(data) {
             ;
            var modal = $(this);
            var reversedItems = data.result.items.slice().reverse();

            for (var i = 0; i < data.result.items.length; i++) {
                var Name = "name" + (i + 1);
                var idtotalscore = "Totalscore" + (i + 1);
                var currentData = reversedItems[i].productOtherTestRequirement;


                $("." + Name).text(currentData.name);
                $("." + idtotalscore).val(currentData.totalScore);


            }

            _createOrEditOtherscoreModal.open();
        }


        var _productOtherTestRequirements = abp.services.app.productOtherTestRequirements;

        var _modalManager;
        var _$othersInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L',
            });

            _$othersInformationForm = _modalManager.getModal().find('form[name=OtherscoreInformationsForm]');
            _$othersInformationForm.validate();
        };

        this.save = function () {
            if (!_$othersInformationForm.valid()) {
                return;
            }
            //var formData = _$othersInformationForm.serializeFormToObject();
            //var subject = new object();
            var items = [];

            // Assuming each entry has a unique identifier, you may need to modify this part accordingly
            for (var i = 1; i <= 4; i++) {
                var Name = "name" + i;
                var idtotalscore = "Totalscore" + i;
                var hiddenfield = $("#ProductId").val();
                var NameValue = $("." + Name).text(); // or .val() depending on the actual element type
                 
                var subject = {
                    Name: NameValue,
                    TotalScore: parseFloat($("." + idtotalscore).val()),
                    productId: hiddenfield,
                };
                items.push(subject);
            }
             
            row1 = $('.card-text.col-md-2 input[name="Idother0"]').val();
            row2 = $('.card-text.col-md-2 input[name="Idother1"]').val();
            row3 = $('.card-text.col-md-2 input[name="Idother2"]').val();
            row4 = $('.card-text.col-md-2 input[name="Idother3"]').val();
            $.each(items, function (index, data) {
                var Steps = JSON.stringify(data);
                 
                _productOtherTestRequirements
                    .delete({
                        id: row1,
                    })
                _productOtherTestRequirements.delete({
                    id: row2,
                })
                _productOtherTestRequirements.delete({
                    id: row3,
                })
                _productOtherTestRequirements.delete({
                    id: row4,
                })
                Steps = JSON.parse(Steps);
                _modalManager.setBusy(true);
                _productOtherTestRequirements
                    .createOrEdit(Steps) // Pass an object with a 'subjects' property
                    .done(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        _modalManager.close();
                        abp.event.trigger('app.createOrEditAcadamicRequirementModalSaved');
                    })
                    .always(function () {
                        _modalManager.setBusy(false);
                    });
            });
            //var Steps = JSON.stringify(items);
            // 
            //Steps = JSON.parse(Steps);
            //var Fstep = Steps[0];

            //console.log(Fstep);
            // var subjects = _$othersInformationForm.serializeFormToObject();
            //var subjects = new Object();
            //subjects.items = items;


            //_modalManager.setBusy(true);
            //_productOtherTestRequirements
            //    .createOrEdit(Fstep) // Pass an object with a 'subjects' property
            //    .done(function () {
            //        abp.notify.info(app.localize('SavedSuccessfully'));
            //        _modalManager.close();
            //        abp.event.trigger('app.createOrEditAcadamicRequirementModalSaved');
            //    })
            //    .always(function () {
            //        _modalManager.setBusy(false);
            //    });
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
