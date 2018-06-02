using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductDelivery.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ProductDelivery.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private readonly ICardService cardservice;

        public CardController(ICardService card)
        {
            this.cardservice = card;
        }

        [Route("card")]
        public IActionResult Index()
        {
            return View(cardservice.GetCard(User.Identity.Name));
        }

        [HttpGet]
        [Route("card/length")]
        public IActionResult CardLength()
        {
            int res = 0;
            var card = cardservice.GetCard(User.Identity.Name);
            if (card != null)
                res = card.Length;
            return Ok(res);
        }

        [HttpPost]
        [Route("card/CheckOut")]
        public IActionResult CheckOut()
        {
            cardservice.CheckOut(User.Identity.Name);
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [Route("card/Add/{productId}")]
        public IActionResult AddToCard(int productId)
        {
            cardservice.Add(User.Identity.Name,productId);
            return Ok(cardservice.GetCard(User.Identity.Name).Length);
        }

        [HttpPost]
        [Route("card/Remove/{productId}")]
        public IActionResult RemoveFromCard(int productId)
        {
            cardservice.Remove(User.Identity.Name, productId);
            return RedirectToAction("Index");
        }
    }
}