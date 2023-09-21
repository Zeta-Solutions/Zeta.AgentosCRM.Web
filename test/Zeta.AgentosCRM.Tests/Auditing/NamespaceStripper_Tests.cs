using Zeta.AgentosCRM.Auditing;
using Zeta.AgentosCRM.Test.Base;
using Shouldly;
using Xunit;

namespace Zeta.AgentosCRM.Tests.Auditing
{
    // ReSharper disable once InconsistentNaming
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("Zeta.AgentosCRM.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("Zeta.AgentosCRM.Auditing.GenericEntityService`1[[Zeta.AgentosCRM.Storage.BinaryObject, Zeta.AgentosCRM.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("Zeta.AgentosCRM.Auditing.XEntityService`1[Zeta.AgentosCRM.Auditing.AService`5[[Zeta.AgentosCRM.Storage.BinaryObject, Zeta.AgentosCRM.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[Zeta.AgentosCRM.Storage.TestObject, Zeta.AgentosCRM.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
