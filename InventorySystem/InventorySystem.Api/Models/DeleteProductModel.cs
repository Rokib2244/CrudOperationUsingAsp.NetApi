using Autofac;
using InventorySystem.Training.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Api.Models
{
    public class DeleteProductModel
    {
        private readonly IProductService _productService;
      
        public DeleteProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
          
        }
        internal void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
