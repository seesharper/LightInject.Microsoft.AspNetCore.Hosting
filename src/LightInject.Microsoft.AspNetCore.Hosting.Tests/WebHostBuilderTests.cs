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
           .ConfigureTestServices(sr => sr.AddTransient<IFoo, FooMock>())
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
