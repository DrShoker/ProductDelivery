using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Enums;

namespace ServerPD.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = db.Products.GetAll().ToList();
            return products;
        }
        [HttpGet("{dep}")]
        public IEnumerable<Product> GetCatalog(Departments dep)
        {
            Departments department = (Departments)dep;
            List<Product> products = db.Products.GetAll().Where(p => p.Department == department).OrderBy(p => p.Type).ToList();
            return products;
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            Product product = db.Products.Get(id);
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }
        [HttpGet("{name}")]
        public IEnumerable<Product> GetProductsForName(string name)
        {
            List<Product> products = db.Products.GetAll().Where(p => p.Name.Contains(name) == true).OrderBy(p =>p.Type).ToList();
            return products;
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Create(product);
            db.Save();
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePdroduct(int id)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            db.Products.Delete(id);
            db.Save();
            return Ok(product);
        }
        [HttpPut]
        public IActionResult EditProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != product.Id)
                return BadRequest();

            db.Products.Update(product);
            db.Save();

            return Ok(product);
        }
    }
}