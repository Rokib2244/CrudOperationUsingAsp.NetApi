using Autofac;
using InventorySystem.Training.Contexts;
using InventorySystem.Training.Repositories;
using InventorySystem.Training.Services;
using InventorySystem.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Training
{
   public class TrainingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public TrainingModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<TrainingContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();
            builder.RegisterType<TrainingContext>().As<ITrainingContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();
            //builder.RegisterType<CourseModel>().AsSelf();
            builder.RegisterType<ProductService>().As<IProductService>()
    .InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>()
    .InstancePerLifetimeScope();
            builder.RegisterType<TrainingUnitOfWork>().As<ITrainingUnitOfWork>()
    .InstancePerLifetimeScope();

            //builder.RegisterType<DataTablesAjaxRequestModel>().As<IDataTablesAjaxRequestModel>()
            //    .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
