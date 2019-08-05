using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace LightInject.Microsoft.AspNetCore.Hosting.Tests
{
    public class WebHostBuilderTests
    {
        [Fact]
        public void ShouldResolveService()
        {
            IWebHostBuilder builder = new WebHostBuilder()
           .UseLightInject()
           .UseStartup<TestStartup>();

            using (var webHost = builder.Build())
            {
                var foo = webHost.Services.GetRequiredService<IFoo>();
                Assert.IsType<Foo>(foo);
            }
        }

        [Fact]
        public void ShouldResolveMockedService()
        {
            IWebHostBuilder builder = new WebHostBuilder()
           .UseLightInject()
           .ConfigureTestContainer<IServiceContainer>(c => c.RegisterTransient<IFoo, FooMock>())
           .UseStartup<TestStartup>();

            using (var webHost = builder.Build())
            {
                var foo = webHost.Services.GetRequiredService<IFoo>();
                Assert.IsType<FooMock>(foo);
            }
        }

        [Fact]
        public void ShouldUseExistingContainer()
        {

            var container = new ServiceContainer(ContainerOptions.Default.WithAspNetCoreSettings());

            IWebHostBuilder builder = new WebHostBuilder()
           .UseLightInject(container)
           .UseStartup<TestStartup>();

            using (var webHost = builder.Build())
            {
                var foo = webHost.Services.GetRequiredService<IFoo>();
                Assert.IsType<Foo>(foo);
            }
        }

        [Fact]
        public void ShouldUseConfigureAction()
        {
            IWebHostBuilder builder = new WebHostBuilder()
           .UseLightInject(o => { })
           .ConfigureTestContainer<IServiceContainer>(c => c.RegisterTransient<IFoo, FooMock>())
           .UseStartup<TestStartup>();

            using (var webHost = builder.Build())
            {
                var foo = webHost.Services.GetRequiredService<IFoo>();
                Assert.IsType<FooMock>(foo);
            }
        }


        public class FooMock : IFoo
        {

        }
    }
}
