﻿using System.Threading.Tasks;
using Abp;
using Zeta.AgentosCRM.Configuration.Dto;
using Zeta.AgentosCRM.UiCustomization;
using Zeta.AgentosCRM.UiCustomization.Dto;

namespace Zeta.AgentosCRM.Test.Base.UiCustomization
{
    public class NullThemeUiCustomizer : IUiCustomizer
    {
        public Task<UiCustomizationSettingsDto> GetUiSettings()
        {
            return Task.FromResult(new UiCustomizationSettingsDto());
        }

        public Task UpdateUserUiManagementSettingsAsync(UserIdentifier user, ThemeSettingsDto settings)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateTenantUiManagementSettingsAsync(int tenantId, ThemeSettingsDto settings, UserIdentifier changerUser)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateApplicationUiManagementSettingsAsync(ThemeSettingsDto settings, UserIdentifier changerUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<ThemeSettingsDto> GetHostUiManagementSettings()
        {
            throw new System.NotImplementedException();
        }

        public Task<ThemeSettingsDto> GetTenantUiCustomizationSettings(int tenantId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateDarkModeSettingsAsync(UserIdentifier user, bool isDarkModeEnabled)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetBodyClass()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetBodyStyle()
        {
            throw new System.NotImplementedException();
        }
    }
}