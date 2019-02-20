using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Exam.Back.Mvc.App_Start
{
    public class AutofacConfig
    {
        public static void Start()
        {
            //注入
            //ContainerBuilder builder = new ContainerBuilder();
            //HttpConfiguration config = GlobalConfiguration.Configuration;

            //// Register API controllers using assembly scanning.
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            ////已过时
            ////builder.RegisterType<StudentDal>().As<IStudentDal>().InstancePerApiRequest();
            ////builder.RegisterType<StudentBll>().As<IStudentBll>().InstancePerApiRequest();

            ////操作块
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("UserRespository")).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("StageRespository")).AsImplementedInterfaces();

            //var container = builder.Build();
            //// Set the WebApi dependency resolver.
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);



            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();
            //告诉Autofac框架，将来要创建的控制器类存放在哪个程序集 (AutoFacMvcDemo)
            Assembly controllerAss = Assembly.Load("Exam.Back.Mvc");
            builder.RegisterControllers(controllerAss);

            //如果有Dal层的话，注册Dal层的组件
            //告诉autofac框架注册数据仓储层所在程序集中的所有类的对象实例
            //Assembly dalAss = Assembly.Load("Exam.Back.Respository");
            //创建respAss中的所有类的instance以此类的实现接口存储
            //builder.RegisterTypes(dalAss.GetTypes()).AsImplementedInterfaces();

            //告诉autofac框架注册业务逻辑层所在程序集中的所有类的对象实例
            Assembly serviceAss = Assembly.Load("Exam.Back.Respository");
            //创建serAss中的所有类的instance以此类的实现接口存储
            builder.RegisterTypes(serviceAss.GetTypes()).AsImplementedInterfaces();

            //创建一个Autofac的容器
            var container = builder.Build();
            //将MVC的控制器对象实例 交由autofac来创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}