using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Services.Services.Trip;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet("api/trips/list")]
        public async Task<IActionResult> GetTripList()
        {
            if (ModelState.IsValid)
                try
                {
                    var shapes = await _tripService.GetTrips();
                    return Ok(shapes);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        //[HttpGet("api/trips")]
        //public IActionResult UpdateTrips()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var data = GetData("D:\\GO\\trips.txt");
        //            _tripService.UpdateTrips(data);
        //            return Ok();
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("error", ex.Message);
        //        }
        //    }

        //    return BadRequest(ModelState.ToDictionary(k => k.Key,
        //        k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        //}

        //private List<TripsMappingData> GetData(string filename)
        //{
        //    var data = new List<TripsMappingData>();
        //    var numRow = 0;
        //    using (var reader = new StreamReader(filename))
        //    {
        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            if (!string.IsNullOrWhiteSpace(line))
        //            {
        //                if (numRow == 0)
        //                {
        //                    numRow = 1;
        //                    continue;
        //                }

        //                var values = line.Split(',');
        //                data.Add(new TripsMappingData
        //                {
        //                    RouteId = values[0],
        //                    ServiceId = values[1],
        //                    TripId = values[2],
        //                    HeadSign = values[3],
        //                    ShortName = values[4],
        //                    DirectionId = Convert.ToInt32(values[5]),
        //                    BlockId = values[6],
        //                    ShapeId = values[7],
        //                    WheelchairAccessible = values[8] == "1",
        //                    BikesAllowed = values[9] == "1",
        //                    Variant = values[10]
        //                });
        //            }
        //        }
        //    }

        //    return data;
        //}
    }
}