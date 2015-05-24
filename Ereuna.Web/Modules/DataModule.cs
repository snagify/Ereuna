using Autofac;
using Ereuna.Web.Data;

namespace Ereuna.Web.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EreunaContext>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}