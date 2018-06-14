using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerPD.Services;

namespace ServerPD.Controllers
{
    [Produces("application/json")]
    [Route("api/Tracker")]
    [EnableCors("MyPolicy")]
    public class TrackerController : Controller
    {
        private readonly ITrackService tracker;

        public TrackerController(ITrackService tracker)
        {
            this.tracker = tracker;
        }

        [HttpGet("{deliveryId}")]
        public IActionResult GetCoords(int deliveryId)
        {
            return new ObjectResult(tracker.GetDeliveryCorrds(deliveryId));
        }

        [HttpPost("{deliveryId}/{x}/{y}")]
        public IActionResult Update(int deliveryId,double x,double y)
        {
            tracker.UpdateCoords(deliveryId, x, y);
            return Ok();
        }
    }
}