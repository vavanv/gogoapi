using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using Configuration;

using GoGoApi.Mappers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using Services.Models.ScheduleTrain;

namespace GoGoApi.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IOptions<AccessKey> _accessKey;
        private readonly IOptions<ActionUrl> _actionUrl;
        private readonly IOptions<BaseUrlKey> _baseUrl;
        private readonly IScheduleMapper _mapper;

        public ScheduleController(IOptions<BaseUrlKey> baseUrl, IOptions<AccessKey> accessKey,
            IOptions<ActionUrl> actionUrl, IScheduleMapper mapper)
        {
            _baseUrl = baseUrl;
            _accessKey = accessKey;
            _actionUrl = actionUrl;
            _mapper = mapper;
        }

        [HttpGet("api/schedule/line/trains")]
        public IActionResult GetServiceTrains()
        {
            if (ModelState.IsValid)
                try
                {
                    var today = DateTime.Now.Year +
                                (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month.ToString()) +
                                (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day : DateTime.Now.Day.ToString());
                    var scheduleLineTrains = $"{_baseUrl.Value.KeyValue}{_actionUrl.Value.ScheduleLineTrains}/{today}";
                    var urlParameters = _accessKey.Value.KeyValue;

                    var client = new HttpClient
                        {BaseAddress = new Uri(scheduleLineTrains), Timeout = new TimeSpan(0, 0, 10, 0, 0)};

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(urlParameters).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ScheduleTrains>(responseBody.Result);
                        var res = result.AllLines.Line.Where(l => l.IsTrain.Equals(true));
                        return Ok(_mapper.MapFrom(res));
                    }

                    ModelState.AddModelError("error", response.StatusCode.ToString());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }
    }
}