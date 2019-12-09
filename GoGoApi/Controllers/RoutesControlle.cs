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

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class RoutesControlle : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly IShapeMapper _mapper;

        public RoutesControlle(IRouteService routeService, IShapeMapper mapper)
        {
            _routeService = routeService;
            _mapper = mapper;
        }

        [HttpGet("api/shapes/list")]
        public async Task<IActionResult> GetShapeList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var shapes = await _routeService.GetRoutes();
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

        [HttpGet("api/shapes")]
        public IActionResult UpdateRoutes()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = GetData("D:\\GO\\routes.txt");
                    _routeService.UpdateRoutes(data);
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

        private List<RoutesMappingData> GetData(string filename)
        {
            var data = new List<RoutesMappingData>();
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
                        data.Add(new RoutesMappingData
                        {
                            RouteId = values[0],
                            AgencyId = values[1],
                            ShotName = values[2],
                            LongName = values[3],
                            Type = Convert.ToInt32(values[4]),
                            Color = values[5],
                            TextColor = values[6]
                        });
                    }
                }
            }

            return data;
        }
    }
}