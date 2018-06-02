using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerPD.Controllers
{
    [Produces("application/json")]
    [Route("api/Admin")]
    public class AdminController : Controller
    {
        [Authorize(Roles = "admin")]
        public class RegisterModel
        {
            
        }

        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IEnumerable<Admin> GetAdmins()
        {
            List<Admin> admins = db.Admins.GetAll().ToList();
            return admins;
        }

        [HttpGet("getadminbyid/{id}")]
        public IActionResult GetAdminById(int id)
        {
            Admin admin = db.Admins.Get(id);
            if (admin == null)
                return NotFound();
            return new ObjectResult(admin);
        }

        [HttpGet("getadminbyname/{name}")]
        public IEnumerable<Admin> GetAdminByName(string name)
        {
            List<Admin> admins = db.Admins.GetAll().Where(a => a.Name.Contains(name)).ToList();
            return admins;
        }
        [HttpPost]
        public IActionResult CreateAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Admins.Create(admin);
            db.Save();
            return CreatedAtRoute("DefaultApi", new { id = admin.Id }, admin);
        }

        [HttpDelete("deleteadmin/{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            Admin admin = db.Admins.FirstOrDefault(a => a.Id == id);
            if (admin == null)
                return NotFound();
            db.Admins.Delete(id);
            db.Save();
            return Ok(admin);
        }

        [HttpPut]
        public IActionResult EditAdmin(int id, Admin admin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != admin.Id)
                return BadRequest();

            db.Admins.Update(admin);
            db.Save();

            return Ok(admin);
        }

    }
}