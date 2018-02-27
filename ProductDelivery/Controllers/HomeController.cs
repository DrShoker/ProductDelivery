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
using Microsoft.AspNetCore.Authorization;

namespace ProductDelivery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (EFUnitOfWork db = new EFUnitOfWork())
            {
                List<int> imageIds = db.Images.GetAll().Select(i => i.Id).ToList();
                return View(imageIds);
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
    }
}
