using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using InventorySystem.Training.BusinessObjects;
using InventorySystem.Training.Services;

namespace InventorySystem.Areas.Admin.Models
{
    public class AddProductModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public AddProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public AddProductModel(IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        internal void AddProduct()
        {
            var product = _mapper.Map<Product>(this);
            _productService.AddProduct(product);
        }
    }
}
