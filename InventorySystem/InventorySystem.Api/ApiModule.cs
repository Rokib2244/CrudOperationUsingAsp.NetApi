using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Api
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {



            //builder.RegisterType<CourseModel>().AsSelf();

            //builder.RegisterType<DataTablesAjaxRequestModel>().As<IDataTablesAjaxRequestModel>()
            //    .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
