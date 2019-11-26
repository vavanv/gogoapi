using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using GoGoApi.Mappers;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Services.Models.AllStops;
using Services.Models.StopDetail;
using Services.Services.Cache;
using Services.Services.Stop;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    public class StationsController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IStopService _stopService;
        private readonly IStopDetailMapper _mapper;

        public StationsController(ICacheService cacheService, IStopService stopService, IStopDetailMapper mapper)
        {
            _cacheService = cacheService;
            _stopService = stopService;
            _mapper = mapper;
        }

        [HttpGet("api/stop/list")]
        public async Task<IActionResult> GetStopList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var stops = await _stopService.GetStops();
                    return Ok(_mapper.MapFrom(stops));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpGet("api/stop/bus/list")]
        public async Task<IActionResult> GetStopBusList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var stops = await _stopService.GetBusStops();
                    return Ok(_mapper.MapFrom(stops));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpGet("api/stop/train/list")]
        public async Task<IActionResult> GetStopTrainList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var stops = await _stopService.GetTrainStops();
                    return Ok(_mapper.MapFrom(stops));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
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
                    var urlParameters = "?key=30020230";

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