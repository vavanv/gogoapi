using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Configuration;

using GoGoApi.Mappers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using Services.Models.AllStops;
using Services.Models.Common;
using Services.Models.StopDetail;
using Services.Services.Cache;
using Services.Services.Shape;
using Services.Services.Stop;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ShapesController : ControllerBase
    {
        private readonly IShapeService _shapeService;
        private readonly IShapeMapper _mapper;

        public ShapesController(IShapeService shapeService,IShapeMapper mapper)
        {
            _shapeService = shapeService;
            _mapper = mapper;
        }

        [HttpGet("api/shapes/list")]
        public async Task<IActionResult> GetShapeList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var shapes = await _shapeService.GetShapes();
                    return Ok(shapes);
                    //return Ok(_mapper.MapFrom(shapes));
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
        public IActionResult UpdateShapes()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = GetData("D:\\GO\\shapes.txt");
                    _shapeService.UpdateShape(data);
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

        private List<MappingData> GetData(string filename)
        {
            var data = new List<MappingData>();
            var numRow = 0;
            var reader = new StreamReader(filename);
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
                    data.Add(new MappingData
                    {
                        ShapeId = values[0],
                        Lat = Convert.ToDecimal(values[1]),
                        Lon = Convert.ToDecimal(values[2]),
                        Sec = Convert.ToInt32(values[3])
                    });
                }
            }

            return data;
        }
    }
}