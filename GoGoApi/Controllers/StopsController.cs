using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Services.Models.Common;
using Services.Services.Stop;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class StopsController : ControllerBase
    {
        private readonly IStopService _stopService;

        public StopsController(IStopService stopService)
        {
            _stopService = stopService;
        }

        [HttpGet("api/stop/list")]
        public async Task<IActionResult> GetStopList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var stops = await _stopService.GetStops();
                    return Ok(stops);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        //[HttpGet("api/stops")]
        //public IActionResult UpdateTrips()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var data = GetData("D:\\GO\\stops.txt");
        //            _stopService.UpdateStops(data);
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

        //private List<StopsMappingData> GetData(string filename)
        //{
        //    var data = new List<StopsMappingData>();
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
        //                data.Add(new StopsMappingData
        //                {
        //                    StopId = values[0],
        //                    Name = values[1],
        //                    Latitude = Convert.ToDecimal(values[2]),
        //                    Longitude = Convert.ToDecimal(values[3]),
        //                    ZoneId = values[4],
        //                    Url = values[5],
        //                    Type = Convert.ToInt32(values[6]),
        //                    ParentStation = values[7],
        //                    WheelchairBoarding = values[8] == "1",
        //                    Code = values[9]
        //                });
        //            }
        //        }
        //    }

        //    return data;
        //}
    }
}