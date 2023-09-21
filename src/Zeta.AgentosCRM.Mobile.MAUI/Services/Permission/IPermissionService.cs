namespace Zeta.AgentosCRM.Services.Permission
{
    public interface IPermissionService
    {
        bool HasPermission(string key);
    }
}