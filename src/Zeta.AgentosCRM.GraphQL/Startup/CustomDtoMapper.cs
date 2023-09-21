using AutoMapper;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Startup
{
    public static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDto>()
                .ForMember(dto => dto.Roles, options => options.Ignore())
                .ForMember(dto => dto.OrganizationUnits, options => options.Ignore());
        }
    }
}