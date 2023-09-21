using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using Zeta.AgentosCRM.Queries.Container;
using System;

namespace Zeta.AgentosCRM.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}