using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductDelivery.Models;
using DataAccessLayer.Repositories;
using System.IO;
using DataAccessLayer.Entities;

namespace ProductDelivery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (EFUnitOfWork db = new EFUnitOfWork())
            {
                List<int> AdminsImg = db.Admins.GetAll().Select(i => i.Id).ToList();
                return View(AdminsImg);
            }
        }

        [HttpPost]
        public IActionResult UploadImage(IList<IFormFile> files)
        {
            
            IFormFile uploadedImage = files.FirstOrDefault();
            if(uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
                using (EFUnitOfWork db = new EFUnitOfWork())
                {
                    Product product = new Product()
                    {
                        Name="Tomatos"
                    };
                    db.Products.Create(product);

                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    DataAccessLayer.Entities.Products.Image productImage = new DataAccessLayer.Entities.Products.Image()
                    {
                        Name = uploadedImage.Name,
                        Data = ms.ToArray(),
                        Width = image.Width,
                        Height = image.Height,
                        ContentType = uploadedImage.ContentType,
                        ProductId = product.Id
                    };
                    db.Images.Create(productImage);
                    db.Save();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileStreamResult ViewImage(int id)
        {
            using (EFUnitOfWork db = new EFUnitOfWork())
            {
                DataAccessLayer.Entities.Products.Image image = db.Images.Get(id);
                MemoryStream ms = new MemoryStream(image.Data);
                return new FileStreamResult(ms,image.ContentType);
            }
        }

        [HttpPost]
        public  IActionResult CreateUser(IList<IFormFile> files, Admin admin)
        {
            IFormFile uploadedImage = files.FirstOrDefault();
            if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
                using (EFUnitOfWork db = new EFUnitOfWork())
                {
                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    Admin administrator = new Admin()
                    {
                        Name = "Dadya",

                        ImgData = ms.ToArray(),
                        ImgWidth = image.Width,
                        ImgHeight = image.Height,
                        ImgContentType = uploadedImage.ContentType
                    };
                    db.Admins.Create(administrator);
                    db.Save();

                }

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileStreamResult ViewUserImage(int id)
        {
            using (EFUnitOfWork db = new EFUnitOfWork())
            {
                DataAccessLayer.Entities.Admin admin = db.Admins.Get(id);
                MemoryStream ms = new MemoryStream(admin.ImgData);
                return new FileStreamResult(ms, admin.ImgContentType);
            }
        }
    }
}
