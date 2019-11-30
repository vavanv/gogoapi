using System;
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
using Services.Models.StopDetail;
using Services.Services.Cache;
using Services.Services.Stop;

namespace GoGoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IStopService _stopService;
        private readonly IStopDetailMapper _mapper;
        private readonly IOptions<BaseUrlKey> _baseUrl;
        private readonly IOptions<ActionUrl> _actionUrl;
        private readonly IOptions<AccessKey> _accessKey;
        public StationsController(ICacheService cacheService, IStopService stopService, IStopDetailMapper mapper, IOptions<BaseUrlKey> baseUrl, IOptions<ActionUrl> actionUrl, IOptions<AccessKey> accessKey)
        {
            _cacheService = cacheService;
            _stopService = stopService;
            _mapper = mapper;
            _baseUrl = baseUrl;
            _actionUrl = actionUrl;
            _accessKey = accessKey;
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

        //[HttpGet("api/stop/bus/list")]
        //public async Task<IActionResult> GetStopBusList()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var stops = await _stopService.GetBusStops();
        //            return Ok(_mapper.MapFrom(stops));
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("error", ex.Message);
        //        }
        //    }

        //    return BadRequest(ModelState.ToDictionary(k => k.Key,
        //        k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        //}

        //[HttpGet("api/stop/train/list")]
        //public async Task<IActionResult> GetStopTrainList()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var stops = await _stopService.GetTrainStops();
        //            return Ok(_mapper.MapFrom(stops));
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("error", ex.Message);
        //        }
        //    }

        //    return BadRequest(ModelState.ToDictionary(k => k.Key,
        //        k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        //}

        [HttpGet("api/update")]
        public IActionResult UpdateData()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var allStopsUrl = $"{_baseUrl.Value.KeyValue}{_actionUrl.Value.StopAll}";
                    var allStopsDetailUrl = $"{_baseUrl.Value.KeyValue}{_actionUrl.Value.StopDetails}";
                    var urlParameters = _accessKey.Value.KeyValue;

                    var client = new HttpClient {BaseAddress = new Uri(allStopsUrl), Timeout = new TimeSpan(0, 0, 10, 0, 0) };

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var clientDetail = new HttpClient {BaseAddress = new Uri(allStopsDetailUrl), Timeout = new TimeSpan(0, 0, 10, 0, 0) };
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