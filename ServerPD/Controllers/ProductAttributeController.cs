using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repositories;

namespace ServerPD.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductAttribute")]
    public class ProductAttributeController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IEnumerable<ProductAttribute> GetProductAttibutes()
        {
            List<ProductAttribute> productAttributes = db.ProductAttributes.GetAll().ToList();
            return productAttributes;
        }

        [HttpGet("{id}")]
        public IActionResult GetProductAttribute(int id)
        {
            ProductAttribute productAttribute = db.ProductAttributes.Get(id);
            if (productAttribute == null)
                return NotFound();
            return new ObjectResult(productAttribute);
        }

        [HttpPost]
        public IActionResult CreateProductAttribute(ProductAttribute productAttribute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductAttributes.Create(productAttribute);
            db.Save();
            return CreatedAtRoute("DefaultApi", new { id = productAttribute.Id }, productAttribute);
        }
    }
}