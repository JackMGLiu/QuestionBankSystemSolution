using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.Mvc;
using Project.QuestionBank.Core.AutoMapper;
using Project.QuestionBank.Infrastructure;

namespace Project.QuestionBank.Application
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //移除所有视图引擎
            ViewEngines.Engines.Clear();
            //添加Razor视图引擎
            ViewEngines.Engines.Add(new RazorViewEngine());

            //DI
            AutofacRegister();

            //AutoMapper
            AutoMapperRegister();
        }

        private void AutofacRegister()
        {
            var builder = new ContainerBuilder();

            //注册MvcApplication程序集中所有的控制器
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //注册仓储层服务
            //builder.RegisterType<PostRepository>().As<IPostRepository>();
            //注册基于接口约束的实体
            //var assembly = AppDomain.CurrentDomain.GetAssemblies();

            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .Where(
                    assembly =>
                        assembly.GetTypes()
                            .FirstOrDefault(type => type.GetInterfaces().Contains(typeof(IDependency))) !=
                        null
                );

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //注册过滤器
            builder.RegisterFilterProvider();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            var container = builder.Build();

            //设置依赖注入解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        /// <summary>
        /// AutoMapper的配置初始化
        /// </summary>
        private void AutoMapperRegister()
        {
            new AutoMapperStartup().Execute();
        }
    }
}
