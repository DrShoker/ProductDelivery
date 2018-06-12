using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerPD.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace ServerPD.Controllers
{
    [Produces("application/json")]
    [Route("api/Client")]
    public class ClientController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IEnumerable<Client> GetClients()
        {
            List<Client> clients = db.Clients.GetAll().ToList();
            return clients;
        }

        [HttpGet("getclientbyid/{id}")]
        public IActionResult GetClientById(int id)
        {
            Client client = db.Clients.GetWithDeliveries(id);
            if (client == null)
                return NotFound();
            return new ObjectResult(client);
        }

        [HttpGet("getclientbyname/{name}")]
        public IEnumerable<Client> GetClientByName(string name)
        {
            List<Client> clients = db.Clients.GetAll().Where(c => c.Name.Contains(name)).ToList();
            return clients;
        }

        public IActionResult CreateClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Create(client);
            db.Save();
            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }

        [HttpDelete("deleteclient/{id}")]
        public IActionResult DeleteClient(int id)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            db.Products.Delete(id);
            db.Save();
            return Ok(product);
        }

        [HttpPut]
        public IActionResult EditClient(int id, Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != client.Id)
                return BadRequest();

            db.Clients.Update(client);
            db.Save();

            return Ok(client);
        }   
    }
}