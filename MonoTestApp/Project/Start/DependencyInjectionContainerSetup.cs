using Autofac.Integration.Mvc;
using Autofac;
using System.Web.Mvc;
using X.PagedList;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MonoTestApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MonoTestApp.Project.Start
{
    public class DependencyInjectionContainerSetup
    {
        public static ContainerBuilder containerBuilder { get; set; }
        public static IContainer Container { get; set; }
        static DependencyInjectionContainerSetup()
        {
            //Application start for AutoFac
            containerBuilder = new ContainerBuilder();

            //containerBuilder.RegisterType<PagedList<>>().As<IPagedList>();
            containerBuilder.RegisterAutoMapper(typeof(Program).Assembly);

            //containerBuilder.RegisterControllers(typeof(VehicleMakesController).Assembly);
            //containerBuilder.RegisterControllers(typeof(VehicleModelsController).Assembly);
            //containerBuilder.RegisterType<VehicleMakesController>().InstancePerRequest();
            //containerBuilder.RegisterType<VehicleModelsController>().InstancePerRequest();
            Container = containerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
