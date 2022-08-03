using Autofac;
using InventorySystem.Training.BusinessObjects;
using InventorySystem.Training.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Api.Models
{
    public class GetProductByIdModel
    {
        private IProductService _poductService;
       
        public Product Product { get; set; }
        public GetProductByIdModel()
        {
            _poductService = Startup.AutofacContainer.Resolve<IProductService>();
            
        }
        public GetProductByIdModel(IProductService poductService )
        {
            _poductService = poductService;
           
        }
        public void GetProductById(int id)
        {
            Product = _poductService.GetProductById(id);
        }
    }
}
