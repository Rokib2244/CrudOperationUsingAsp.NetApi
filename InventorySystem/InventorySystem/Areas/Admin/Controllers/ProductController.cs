using InventorySystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var model = new AddProductModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(AddProductModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.AddProduct();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Failed To Add Product");
                    _logger.LogError(ex, "Add Product Failed");
                }
            }
           
            return View();
        }
    }
}
