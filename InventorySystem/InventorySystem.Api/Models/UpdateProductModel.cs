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
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public UpdateProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        internal void Update(int id)
        {
            //var product = new Product {
            //    Id = Id.HasValue? Id.Value : 0,
            //    ProductName = ProductName,
            //    Price = Price.HasValue? Price.Value :0,
            //    Date = Date.HasValue? Date.Value :DateTime.MinValue,
            //    CategoryId = CategoryId.HasValue? CategoryId.Value:0
            //};
            var product = _mapper.Map<Product>(this);
            _productService.UpdateProduct(product);
        }
    }
}
