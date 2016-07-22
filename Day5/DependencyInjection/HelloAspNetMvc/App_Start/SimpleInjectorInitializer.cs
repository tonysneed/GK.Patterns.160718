using HelloAspNetMvc.Data.EF;
using HelloAspNetMvc.Patterns;

[assembly: WebActivator.PostApplicationStartMethod(typeof(HelloAspNetMvc.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace HelloAspNetMvc.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<ProductsDb>(Lifestyle.Scoped);
            container.Register<IProductsRepository, ProductsRepository>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, ProductsDbUnitOfWork>(Lifestyle.Scoped);
        }
    }
}