using AutoMapper;
using InventorySystem.Training.BusinessObjects;
using InventorySystem.Training.Contexts;
using InventorySystem.Training.Repositories;
using InventorySystem.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Training.Services
{
    public class ProductService : IProductService
    {

        
        private readonly ITrainingUnitOfWork _trainingUnitOfWork;
        
        //private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;
       
        public ProductService(ITrainingUnitOfWork trainingUnitOfWork,
           IMapper mapper)
        {
            _trainingUnitOfWork = trainingUnitOfWork;
            _mapper = mapper;
        }
        //For Web Project
        public void AddProduct(Product product)
        {
            if (product == null)
                throw new InvalidOperationException("Product Was not Found");
            if(IsProductAlreadyExisted(product.Name))
                throw new InvalidOperationException("Product Name Already Used");
            _trainingUnitOfWork.Products.Add(
                _mapper.Map<Entities.Product>(product));
            _trainingUnitOfWork.Save();
        }
        //For Api Project
        public void CreateProduct(Product product)
        {
            if (product == null)
                throw new InvalidOperationException("Product Was not Found");
            if (IsProductAlreadyExisted(product.Name))
                throw new InvalidOperationException("Product Name Already Used");
            _trainingUnitOfWork.Products.Add(
                _mapper.Map<Entities.Product>(product));
            _trainingUnitOfWork.Save();
        }

        public bool IsProductAlreadyExisted(string productName) =>

            _trainingUnitOfWork.Products.GetCount(x => x.Name == productName) > 0;
        public bool IsProductAlreadyExisted(string productName, int id) =>

            _trainingUnitOfWork.Products.GetCount(x => x.Name == productName && x.Id == id) > 0;
        public IList<Product> GetAllProudcts()
        {
            var productEntities = _trainingUnitOfWork.Products.GetAll();
            var products = new List<Product>();
            foreach (var entity in productEntities)
            {
                var product = _mapper.Map<Product>(entity);
                
                products.Add(product);
            }
            return products;
        }
        public Product GetProductById(int id)
        {
            var product = _trainingUnitOfWork.Products.GetById(id);
            if (product == null) return null;
            return _mapper.Map<Product>(product);

        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new InvalidOperationException("Product was not found");

            if (IsProductAlreadyExisted(product.Name, product.Id))
                throw new InvalidOperationException("Product Name Already Used");
            var productEntity = _trainingUnitOfWork.Products.GetById(product.Id);
            if (productEntity != null)
            {
                _mapper.Map(product, productEntity);

                /* productEntity.Name = product.ProductName;
                 productEntity.Price = product.Price;
                 productEntity.Date = product.Date;
                 productEntity.CategoryId = product.CategoryId;
                */
                _trainingUnitOfWork.Save();
            }
        }

        public void DeleteProduct(int id)
        {
            _trainingUnitOfWork.Products.Remove(id);
            _trainingUnitOfWork.Save();
        }
    }
}
