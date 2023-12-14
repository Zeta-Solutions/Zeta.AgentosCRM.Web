using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Layout;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Startup;
using Zeta.AgentosCRM.Web.Views;
using Hangfire.Dashboard;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using AngleSharp.Dom;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameSetupForms
{
    public class AppAreaNameSetupFormsViewComponent : AgentosCRMViewComponent
    {

        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;

        public AppAreaNameSetupFormsViewComponent(IUserNavigationManager userNavigationManager, IAbpSession abpSession)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
        }

        //public Task<IViewComponentResult> InvokeAsync(string cssClass, string iconClass = "fa-sharp fa-solid fa-gear fs-2")
        //{
        //    var model = new SetupFormListViewModel
        //    { 
        //        CssClass = cssClass,
        //        IconClass = iconClass 
        //    };

        //    return Task.FromResult<IViewComponentResult>(View(model));
        //}
        public async Task<IViewComponentResult> InvokeAsync(
        
            bool isLeftMenuUsed,
            bool iconMenu = false,
            string currentPageName = null,
            string height = "auto",
            string sideMenuClass = "menu menu-column menu-rounded menu-sub-indention px-3",
            string topMenuClass = "fa-sharp fa-solid fa-gear fs-2")
        {
            var model = new MenuViewModel
            {
                Menu = await _userNavigationManager.GetMenuAsync(AppAreaNameNavigationProvider.MenuName, _abpSession.ToUserIdentifier()),
                Height = height,
                CurrentPageName = currentPageName,
                IconMenu = iconMenu,
                SideMenuClass = sideMenuClass,
                TopMenuClass = topMenuClass
            }; 

            return View(model);
        }
          

    }
}
