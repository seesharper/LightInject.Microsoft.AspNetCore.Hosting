using System;
using System.Collections.Generic;
using System.Text;

namespace LightInject.Microsoft.AspNetCore.Hosting.Tests
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IFoo, Foo>();
        }
    }
    public interface IFoo
    {

    }

    public class Foo : IFoo
    {

    }
}
