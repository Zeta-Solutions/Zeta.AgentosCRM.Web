(function ($) {
  app.modals.CreateOrEditUserModal = function () {
    var _userService = abp.services.app.user;

    var _modalManager;
    var _$userInformationForm = null;
    var _passwordComplexityHelper = new app.PasswordComplexityHelper();
      var _organizationTree;
      //var userId = 2;

      //// Make an AJAX request to the server to get the image URL
      //$.ajax({
      //    url: abp.appPath + 'api/services/app/Profile/GetProfilePictureByUser',
      //    type: 'GET',
      //    data: { userId: userId },
      //    success: function (data) {
      //        debugger
      //        // Assuming data.profilePicture contains the base64-encoded image string
      //        var base64Image = data.result.profilePicture;

      //        // Set the image source directly
      //        $('#profileImage').attr('src', 'Profile/GetProfilePictureByUser?userId=2&amp;profilePictureId=fb30221e-2981-745e-8875-3a0e4fe0ca77');
      //    },
      //    error: function (error) {
      //        // Handle the error if needed
      //        console.error('Error fetching image URL:', error);
      //    }
      //}); 
      
      var userId = $('input[name="Id"]').val();
      var profilePictureId = $('input[name="profilePictureId"]').val();

      // Construct the URL
      var imageUrl = '/Profile/GetProfilePictureByUser?userId=' + userId + '&profilePictureId=' + profilePictureId;

      // Set the image source dynamically
      $('#profileImage').attr('src', imageUrl);

    var changeProfilePictureModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/Profile/ChangePictureModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Profile/_ChangePictureModal.js',
      modalClass: 'ChangeProfilePictureModal',
    });

    function _findAssignedRoleNames() {
      var assignedRoleNames = [];

      _modalManager
        .getModal()
        .find('.user-role-checkbox-list input[type=checkbox]')
        .each(function () {
          if ($(this).is(':checked') && !$(this).is(':disabled')) {
            assignedRoleNames.push($(this).attr('name'));
          }
        });

      return assignedRoleNames;
    }

    this.init = function (modalManager) {
      _modalManager = modalManager;

      _organizationTree = new OrganizationTree();
      _organizationTree.init(_modalManager.getModal().find('.organization-tree'), {
        cascadeSelectEnabled: false,
      });

      _$userInformationForm = _modalManager.getModal().find('form[name=UserInformationsForm]');
      _$userInformationForm.validate();

      var passwordInputs = _modalManager.getModal().find('input[name=Password],input[name=PasswordRepeat]');
      var passwordInputGroups = passwordInputs.closest('.user-password');

      _passwordComplexityHelper.setPasswordComplexityRules(passwordInputs, window.passwordComplexitySetting);

      $('#EditUser_SetRandomPassword').change(function () {
        if ($(this).is(':checked')) {
          passwordInputGroups.slideUp('fast');
          if (!_modalManager.getArgs().id) {
            passwordInputs.removeAttr('required');
          }
        } else {
          passwordInputGroups.slideDown('fast');
          if (!_modalManager.getArgs().id) {
            passwordInputs.attr('required', 'required');
          }
        }
      });

      _modalManager
        .getModal()
        .find('.user-role-checkbox-list input[type=checkbox]')
        .change(function () {
          $('#assigned-role-count').text(_findAssignedRoleNames().length);
        });

      _modalManager.getModal().find('[data-bs-toggle=tooltip]').tooltip();

      _modalManager
        .getModal()
        .find('#changeProfilePicture')
        .click(function () {
          changeProfilePictureModal.open({userId: _modalManager.getModal().find('input[name=Id]').val()});
        });

      changeProfilePictureModal.onClose(function () {
          _modalManager.getModal().find('.user-edit-dialog-profile-image').attr('src', abp.appPath + "Profile/GetProfilePictureByUser?userId="+ _modalManager.getModal().find('input[name=Id]').val())        
      });
    };

    this.save = function () {
      if (!_$userInformationForm.valid()) {
        return;
      }

      var assignedRoleNames = _findAssignedRoleNames();
      var user = _$userInformationForm.serializeFormToObject();

      if (user.SetRandomPassword) {
        user.Password = null;
      }

      _modalManager.setBusy(true);
      _userService
        .createOrUpdateUser({
          user: user,
          assignedRoleNames: assignedRoleNames,
          sendActivationEmail: user.SendActivationEmail,
          SetRandomPassword: user.SetRandomPassword,
          organizationUnits: _organizationTree.getSelectedOrganizationIds(),
        })
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditUserModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
