using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using GoGoApi.Mappers;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Services.Models.Common;
using Services.Services.Cache;
using Services.Services.Route;
using Services.Services.Shape;
using Services.Services.Trip;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly IShapeMapper _mapper;

        public TripsController(ITripService tripService, IShapeMapper mapper)
        {
            _tripService = tripService;
            _mapper = mapper;
        }

        [HttpGet("api/trips/list")]
        public async Task<IActionResult> GetTripList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var shapes = await _tripService.GetTrips();
                    return Ok(shapes);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpGet("api/trips")]
        public IActionResult UpdateTrips()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = GetData("D:\\GO\\trips.txt");
                    _tripService.UpdateTrips(data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        private List<TripsMappingData> GetData(string filename)
        {
            var data = new List<TripsMappingData>();
            var numRow = 0;
            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        if (numRow == 0)
                        {
                            numRow = 1;
                            continue;
                        }

                        var values = line.Split(',');
                        data.Add(new TripsMappingData
                        {
                            RouteId=values[0],
                            ServiceId = values[1],
                            TripId = values[2],
                            HeadSign = values[3],
                            ShortName = values[4],
                            DirectionId = values[5],
                            BlockId = values[6],
                            ShapeId = values[7],
                            WheelchairAccessible = Convert.ToBoolean(values[8]),
                            BikesAllowed = Convert.ToBoolean(values[9]),
                            Variant = values[10]
                        });
                    }
                }
            }

            return data;
        }
    }
}