﻿using System.Threading.Tasks;
using Abp.Dependency;

namespace Zeta.AgentosCRM.UiCustomization
{
    public interface IUiThemeCustomizerFactory : ISingletonDependency
    {
        Task<IUiCustomizer> GetCurrentUiCustomizer();

        IUiCustomizer GetUiCustomizer(string theme);
    }
}