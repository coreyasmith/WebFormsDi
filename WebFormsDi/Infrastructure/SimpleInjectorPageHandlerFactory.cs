using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using SimpleInjector;

namespace WebFormsDi.Infrastructure
{
    public class SimpleInjectorPageHandlerFactory : PageHandlerFactory
    {
        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
        {
            var handler = base.GetHandler(context, requestType, virtualPath, path);
            if (handler == null) return null;

            InitializeInstance(handler);
            HookChildControlInitialization(handler);
            return handler;
        }

        private static void InitializeInstance(object instance)
        {
            var pageType = instance.GetType().BaseType;
            var ctor = GetInjectableConstructor(pageType);
            if (ctor == null) return;

            try
            {
                var args = GetMethodArguments(ctor);
                ctor.Invoke(instance, args);
            }
            catch (Exception ex)
            {
                var msg = $"The type {pageType} " + $"could not be initialized. {ex.Message}";
                throw new InvalidOperationException(msg, ex);
            }
        }

        private static object[] GetMethodArguments(MethodBase method)
        {
            return (
                from parameter in method.GetParameters()
                let parameterType = parameter.ParameterType
                select GetInstance(parameterType)
            ).ToArray();
        }

        private static object GetInstance(Type type)
        {
            return HttpContext.Current.GetContainer().GetInstance(type);
        }

        private static ConstructorInfo GetInjectableConstructor(Type type)
        {
            var overloadedPublicConstructors = (
                from ctor in type.GetConstructors()
                where ctor.GetParameters().Length > 0
                select ctor
            ).ToArray();

            if (overloadedPublicConstructors.Length == 0)
            {
                return null;
            }

            if (overloadedPublicConstructors.Length == 1)
            {
                return overloadedPublicConstructors[0];
            }

            throw new ActivationException($"The type {type} has multiple public overloaded constructors and can't be initialized.");
        }

        private static void HookChildControlInitialization(object handler)
        {
            if (handler is Page page)
            {
                // Child controls are not created at this point.
                // They will be when PreInit fires.
                page.PreInit += (s, e) =>
                {
                    InitializeChildControls(page);
                };
            }
        }

        private static void InitializeChildControls(Control control)
        {
            var childControls = GetChildControls(control);
            foreach (var childControl in childControls)
            {
                InitializeInstance(childControl);
                InitializeChildControls(childControl);
            }
        }

        private static IEnumerable<Control> GetChildControls(Control control)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            return (
                from field in control.GetType().GetFields(flags)
                let type = field.FieldType
                where typeof(UserControl).IsAssignableFrom(type)
                let userControl = field.GetValue(control) as Control
                where userControl != null
                select userControl
            ).ToArray();
        }
    }
}
