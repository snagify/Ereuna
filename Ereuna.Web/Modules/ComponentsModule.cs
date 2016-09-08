using Autofac;
using Ereuna.Web.Common;
using Ereuna.Web.Common.Session;

namespace Ereuna.Web.Modules
{
    public class ComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StandardSessionProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FacebookApi>().AsImplementedInterfaces().SingleInstance();
        }
    }
}