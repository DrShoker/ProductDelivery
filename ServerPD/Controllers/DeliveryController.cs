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
    [Route("api/Delivery")]
    public class DeliveryController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IEnumerable<Delivery> GetDeliveries()
        {
            List<Delivery> deliveries = db.Deliveries.GetAll().ToList();
            return deliveries;
        }

        [HttpGet("getclientdeliveries/{client}")]
        public IEnumerable<Delivery> GetClientDeliveries(Client client)
        {
            List<Delivery> deliveries = db.Deliveries.GetAll().Where(d => d.Client == client).OrderBy(d => d.Date).ToList();
            return deliveries;
        }

        [HttpGet("/getcourierdeliveries{courier}")]
        public IEnumerable<Delivery> GetCourierDeliveries(Courier courier)
        {
            List<Delivery> deliveries = db.Deliveries.GetAll().Where(d => d.Courier == courier).OrderBy(d => d.Date).ToList();
            return deliveries;
        }

        [HttpGet("getdelivery/{id}")]
        public IActionResult GetDelivery(int id)
        {
            Delivery delivery = db.Deliveries.Get(id);
            if (delivery == null)
                return NotFound();
            return new ObjectResult(delivery);
        }

        [HttpPost]
        public IActionResult CreateDelivery(Delivery delivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Deliveries.Create(delivery);
            db.Save();
            return CreatedAtRoute("DefaultApi", new { id = delivery.Id }, delivery);
        }
        [HttpDelete("deletedelivery/{id}")]
        public IActionResult DeleteDelivery(int id)
        {
            Delivery delivery = db.Deliveries.FirstOrDefault(d => d.Id == id);
            if (delivery == null)
                return NotFound();
            db.Deliveries.Delete(id);
            db.Save();
            return Ok(delivery);
        }
        [HttpPut]
        public IActionResult EditDelivery(int id, Delivery delivery)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != delivery.Id)
                return BadRequest();

            db.Deliveries.Update(delivery);
            db.Save();

            return Ok(delivery);
        }

        public IActionResult FinishDelivery(int id, Delivery delivery)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != delivery.Id)
                return BadRequest();

            delivery.Status = DeliveryStatus.Completed;

            db.Deliveries.Update(delivery);
            db.Save();

            return Ok(delivery);
        }

    }
}