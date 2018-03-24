using System;
using System.Web;
using SimpleInjector;

namespace WebFormsDi.Infrastructure
{
    public static class HttpContextExtensions
    {
        public const string ContainerKey = "__Container";

        public static void SetContainer(this HttpContext httpContext, Container container)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            httpContext.Cache[ContainerKey] = container ?? throw new ArgumentNullException(nameof(container));
        }

        public static Container GetContainer(this HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            return httpContext.Cache[ContainerKey] as Container;
        }
    }
}
