using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.MultiTenancy;

namespace Zeta.AgentosCRM.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}