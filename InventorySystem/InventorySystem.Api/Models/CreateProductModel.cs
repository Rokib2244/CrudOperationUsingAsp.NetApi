using Autofac;
using AutoMapper;
using InventorySystem.Training.BusinessObjects;
using InventorySystem.Training.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Api.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public CreateProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateProductModel(IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        internal void CreateProduct()
        {
            var product = _mapper.Map<Product>(this);
            _productService.CreateProduct(product);
        }
    }
}
