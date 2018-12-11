## LightInject.Microsoft.AspNetCore.Hosting

Enables **LightInject** to be used as the service container in AspNetCore applications

## Installing

```xml
<PackageReference Include="LightInject.Microsoft.AspNetCore.Hosting" Version="<version>" />
```

## Usage

Enabling **LightInject** is as simple as calling the `UseLightInject` method that extends the [IWebHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-2.1).

The following sample shows how to do this in a simple web application.

```c#
public class Program
{
    public static void Main(string[] args)
    {           
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseLightInject()
            .UseStartup<Startup>();
}
```

Typically in **LightInject**, we register services in a composition root 

```C#
public class CompositionRoot : ICompositionRoot
{
    public void Compose(IServiceRegistry serviceRegistry)
    {
        serviceRegistry.Register<IFoo, Foo>();
    }
}
public interface IFoo {}

public class Foo : IFoo {}
```

By declaring a method named `ConfigureContainer` we can get access to the `IServiceContainer` that we in this example use to register our composition root. 



```c#
public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {          
        }
	    
    	// Use this method to add services directly to LightInject
    	// Important: This method must exist in order to replace the default provider.
        public void ConfigureContainer(IServiceContainer container)
        {
            container.RegisterFrom<CompositionRoot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {           
        }
    }
```

> Note: Services registered in the `ConfigureServices` method will also be registered with **LightInject**



## Controllers

By default, controllers are not actually created by *LightInject*. They are created by the ASP.NET infrastructure and uses LightInject to resolve its dependencies. To enable LightInject to create the controller instances, we need to add the following line.

```csharp
services.AddMvc().AddControllersAsServices();
```

## Test Services

We can register test/mock services to be used when testing our controllers.

```C#
public void ShouldResolveMockedService()
{
    var builder = new WebHostBuilder()
    .UseLightInject()
    .ConfigureTestContainer<IServiceContainer>(c => c.RegisterTransient<IFoo, FooMock>())
    .UseStartup<TestStartup>();

    using (var webHost = builder.Build())
    {
        var foo = webHost.Services.GetRequiredService<IFoo>();
        Assert.IsType<FooMock>(foo);
    }
}

public class FooMock : IFoo { }
```



