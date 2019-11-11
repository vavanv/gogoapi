using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Services.Models.AllStops;
using Services.Models.StopDetail;
using Services.Services.Cache;

namespace GoGoApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Produces("application/json")]
    public class StationsController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public StationsController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }


        [HttpGet("api/update")]
        public IActionResult UpdateData()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    const string allStopsUrl = "http://goapi.openmetrolinx.com/OpenDataAPI/api/V1/Stop/All";
                    const string allStopsDetailUrl = "http://goapi.openmetrolinx.com/OpenDataAPI/api/V1/Stop/Details/";
                    string urlParameters = "?key=30020230";

                    var client = new HttpClient {BaseAddress = new Uri(allStopsUrl)};

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var clientDetail = new HttpClient {BaseAddress = new Uri(allStopsDetailUrl)};
                    clientDetail.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(urlParameters).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<AllStops>(responseBody.Result);

                        foreach (var station in result.Stations.Station)
                        {
                            var responseDetail = clientDetail.GetAsync(station.LocationCode + urlParameters).Result;
                            if (responseDetail.IsSuccessStatusCode)
                            {
                                var rb = responseDetail.Content.ReadAsStringAsync();
                                var r = JsonConvert.DeserializeObject<StopDetail>(rb.Result);
                                _cacheService.UpdateCache(r.Stop.Code, JsonConvert.SerializeObject(r.Stop));
                            }
                        }
                    }
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