/*********************************************************************************
    The MIT License (MIT)

    Copyright (c) 2016 bernhard.richter@gmail.com

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
******************************************************************************
    LightInject.Microsoft.AspNetCore.Hosting version 1.0.0
    http://www.lightinject.net/
    http://twitter.com/bernhardrichter
******************************************************************************/
using LightInject.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LightInject.Microsoft.AspNetCore.Hosting
{
    /// <summary>
    /// Extends the <see cref="IWebHostBuilder"/> to enable LightInject to be used as the service container.
    /// </summary>
    public static class LightInjectWebHostBuilderExtensions
    {
        /// <summary>
        /// Configures the <paramref name="hostBuilder"/> to use LightInject as the service container.
        /// </summary>
        /// <param name="hostBuilder">The target <see cref="IWebHostBuilder"/>.</param>
        /// <returns></returns>
        public static IWebHostBuilder UseLightInject(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.UseLightInject(ContainerOptions.Default);
        }

        /// <summary>
        /// Configures the <paramref name="hostBuilder"/> to use LightInject as the service container.
        /// </summary>
        /// <param name="hostBuilder">The target <see cref="IWebHostBuilder"/>.</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseLightInject(this IWebHostBuilder hostBuilder, ContainerOptions options)
        {
            return hostBuilder.ConfigureServices(services => services.AddSingleton<IServiceProviderFactory<IServiceContainer>>(sp => new LightInjectServiceProviderFactory(options)));
        }
    }

    /// <summary>
    /// Extends the <see cref="ContainerOptions"/> class.
    /// </summary>
    public static class ContainerOptionsExtensions
    {
        /// <summary>
        /// Sets up the <see cref="ContainerOptions"/> to be compliant with the conventions used in Microsoft.Extensions.DependencyInjection.
        /// </summary>
        /// <param name="options">The target <see cref="ContainerOptions"/>.</param>
        /// <returns><see cref="ContainerOptions"/>.</returns>
        public static ContainerOptions WithAspNetCoreSettings(this ContainerOptions options)
        {
            options.EnableVariance = false;
            return options;
        }
    }
}
