using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductDelivery.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ProductDelivery.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private readonly ICardService card;

        public CardController(ICardService card)
        {
            this.card = card;
        }

        [Route("card")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("card/Add/{productId}")]
        public IActionResult AddToCard(int productId)
        {
            card.Add(User.Identity.Name,productId);
            return Ok(card.GetCard(User.Identity.Name).Length);
        }

        [Route("card/Remove/{productId}")]
        public IActionResult RemoveFromCard(int productId)
        {
            card.Remove(User.Identity.Name, productId);
            return Ok();
        }
    }
}