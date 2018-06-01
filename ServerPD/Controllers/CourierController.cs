using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerPD.Controllers
{
    [Produces("application/json")]
    [Route("api/Courier")]
    public class CourierController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IEnumerable<Courier> GetCouriers()
        {
            List<Courier> couriers = db.Couriers.GetAll().ToList();
            return couriers;
        }

        [HttpGet("getcourierbyid/{id}")]
        public IActionResult GetCourierById(int id)
        {
            Courier courier = db.Couriers.Get(id);
            if (courier == null)
                return NotFound();
            return new ObjectResult(courier);
        }

        [HttpGet("getcourierbyname/{name}")]
        public IEnumerable<Courier> GetCourierByName(string name)
        {
            List<Courier> couriers = db.Couriers.GetAll().Where(c => c.Name.Contains(name)).ToList();
            return couriers;
        }

        public IActionResult CreateCourier(Courier courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Couriers.Create(courier);
            db.Save();
            return CreatedAtRoute("DefaultApi", new { id = courier.Id }, courier);
        }

        [HttpDelete("deletecourier/{id}")]
        public IActionResult DeleteCourier(int id)
        {
            Courier courier = db.Couriers.FirstOrDefault(c => c.Id == id);
            if (courier == null)
                return NotFound();
            db.Products.Delete(id);
            db.Save();
            return Ok(courier);
        }

        [HttpPut]
        public IActionResult EditCourier(int id, Courier courier)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != courier.Id)
                return BadRequest();

            db.Couriers.Update(courier);
            db.Save();

            return Ok(courier);
        }
    }
}