using InventorySystem.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost("create-product")]
        public IActionResult CrateBook([FromBody] CreateProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateProduct();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed To Add Product");                    
                }
            }
            return Ok();
        }
        [HttpGet("get-all-products")]
        public IActionResult GetAllProducts()
        {
            var model = new ProductListModel();
            model.GetAllProudcts();
            return Ok(model);
        }
        [HttpGet("get-product-by-id/{id}")]
        public IActionResult GetProductById(int id)
        {
            var model = new GetProductByIdModel();
             model.GetProductById(id);
            return Ok(model);
        }
        [HttpPut("update-product-by-id/{id}")]
        public IActionResult UpdateProductById(int id, [FromBody]UpdateProductModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update(id);
            }
            return Ok(model);
        }
        [HttpDelete("delete-product-by-id/{id}")]
        public IActionResult DeleteProductById(int id)
        {
            var model = new DeleteProductModel();
            model.Delete(id);
            return Ok();
        }

    }
}
