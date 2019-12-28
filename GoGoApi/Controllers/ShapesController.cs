using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Services.CreateData;
using Services.Models.Common;
using Services.Services.Shape;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ShapesController : ControllerBase
    {
        private readonly IShapeService _shapeService;
        private readonly ICreateDataFactory _createDataFactory;

        public ShapesController(IShapeService shapeService, ICreateDataFactory createDataFactory)
        {
            _shapeService = shapeService;
            _createDataFactory = createDataFactory;
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
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpGet("api/shapes/{shapeId}")]
        public async Task<IActionResult> GetShapeListByShapeId(string shapeId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var shapes = await _shapeService.GetShapesByShapeId(shapeId);
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
        public IActionResult UpdateShapes()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = _createDataFactory.Create(MappingDataType.Shapes).BuildData("D:\\GO\\shapes.txt");
                    _shapeService.UpdateShapes(data);
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
    }
}