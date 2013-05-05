namespace More.Tests.ServiceModel
{
    using System.Reflection;
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using More.ServiceModel;
    using FluentAssertions;

    [TestClass]
    public class ServiceModelTests
    {
        [TestMethod]
        public void ChannelFactoryEx()
        {
            ChannelFactory<IFakeService> service =
                ChannelFactoryEx<IFakeService>.CreateChannelFromAssemblyConfig(
                    Assembly.GetExecutingAssembly());

            service.Should().NotBeNull();
            service.Endpoint.Address.Uri.AbsoluteUri.Should().Be("net.tcp://localhost:8141/FakeService");
           service.Endpoint.Binding.Name.Should().Be("NetTcpBinding");
           service.Endpoint.Contract.Name.Should().Be("IFakeService");
        }

        [TestMethod]
        public void ChannelFactoryExNamedEndpoint()
        {
            ChannelFactory<IFakeService> service =
                ChannelFactoryEx<IFakeService>.CreateChannelFromAssemblyConfig(
                    Assembly.GetExecutingAssembly(),
                    "FakeServiceEndpointName");

            service.Should().NotBeNull();
            service.Endpoint.Address.Uri.AbsoluteUri.Should().Be("net.tcp://localhost:8141/FakeService");
            service.Endpoint.Binding.Name.Should().Be("NetTcpBinding");
            service.Endpoint.Contract.Name.Should().Be("IFakeService");
        }

        [TestMethod]
        public void ChannelFactoryExNamedEnpointAndCustomAddress()
        {
            ChannelFactory<IFakeService> service =
                ChannelFactoryEx<IFakeService>.CreateChannelFromAssemblyConfig(
                    Assembly.GetExecutingAssembly(),
                    "FakeServiceEndpointName",
                    new EndpointAddress("net.tcp://remote:8000/FakeService2"));

            service.Should().NotBeNull();
            service.Endpoint.Address.Uri.AbsoluteUri.Should().Be("net.tcp://remote:8000/FakeService2");
            service.Endpoint.Binding.Name.Should().Be("NetTcpBinding");
            service.Endpoint.Contract.Name.Should().Be("IFakeService");
        }
    }

    [ServiceContract]
    public interface IFakeService
    {
    }
}
