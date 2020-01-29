using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.Models.ScheduleTrain;
using Services.Models.ServiceTrains;

namespace GoGoApi.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IOptions<BaseUrlKey> _baseUrl;
        private readonly IOptions<AccessKey> _accessKey;
        private readonly IOptions<ActionUrl> _actionUrl;

        public ScheduleController(IOptions<BaseUrlKey> baseUrl, IOptions<AccessKey> accessKey, IOptions<ActionUrl> actionUrl)
        {
            _baseUrl = baseUrl;
            _accessKey = accessKey;
            _actionUrl = actionUrl;
        }

        [HttpGet("api/schedule/line/trains")]
        public IActionResult GetServiceTrains()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var today = DateTime.Now.Year.ToString() + 
                                (DateTime.Now.Month<10 ? "0" + DateTime.Now.Month.ToString(): DateTime.Now.Month.ToString()) + 
                                (DateTime.Now.Day<10 ? "0" + DateTime.Now.Day.ToString():DateTime.Now.Day.ToString());
                    var scheduleLineTrains = $"{_baseUrl.Value.KeyValue}{_actionUrl.Value.ScheduleLineTrains}/{today}";
                    var urlParameters = _accessKey.Value.KeyValue;

                    var client = new HttpClient
                        { BaseAddress = new Uri(scheduleLineTrains), Timeout = new TimeSpan(0, 0, 10, 0, 0) };

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(urlParameters).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ScheduleTrains>(responseBody.Result);
                        return Ok(result.AllLines);
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