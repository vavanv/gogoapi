using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using Configuration;

using GoGoApi.Mappers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using Services.Models.ServiceTrains;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ServiceAtGlanceController : Controller
    {
        private readonly IOptions<BaseUrlKey> _baseUrl;
        private readonly IOptions<ActionUrl> _actionUrl;
        private readonly IOptions<AccessKey> _accessKey;
        private readonly IServiceTripsMapper _mapper;

        public ServiceAtGlanceController(IOptions<BaseUrlKey> baseUrl, IOptions<ActionUrl> actionUrl,
            IOptions<AccessKey> accessKey, IServiceTripsMapper mapper)
        {
            _baseUrl = baseUrl;
            _actionUrl = actionUrl;
            _accessKey = accessKey;
            _mapper = mapper;
        }

        [HttpGet("api/service/trains")]
        public IActionResult GetServiceTrains()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trainsService = $"{_baseUrl.Value.KeyValue}{_actionUrl.Value.TrainsServiceAtGlance}";
                    var urlParameters = _accessKey.Value.KeyValue;

                    var client = new HttpClient
                        {BaseAddress = new Uri(trainsService), Timeout = new TimeSpan(0, 0, 10, 0, 0)};

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(urlParameters).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceTrains>(responseBody.Result);
                        //foreach (var t in result.Trips)
                        //{

                        //}
                        //return Ok(_mapper.MapFrom(result));
                        return Ok(result.Trips);
                    }

                    ModelState.AddModelError("error", response.StatusCode.ToString());
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