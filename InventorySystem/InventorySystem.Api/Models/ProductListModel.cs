using Autofac;
using InventorySystem.Training.BusinessObjects;
using InventorySystem.Training.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Api.Models
{
    public class ProductListModel
    {
        private IProductService _poductService;
        //private IHttpContextAccessor _httpContextAccessor;
        public IList<Product> Products { get; set; }
        public ProductListModel()
        {
            _poductService = Startup.AutofacContainer.Resolve<IProductService>();
            //_httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public ProductListModel(IProductService poductService /*IHttpContextAccessor httpContextAccessor*/)
        {
            _poductService = poductService;
            //_httpContextAccessor = httpContextAccessor;
        }
        public void GetAllProudcts()
        {
            Products = _poductService.GetAllProudcts();
        }
    }
}
