using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GoGoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();

        //[HttpGet]
        //public IEnumerable<Station> GetStations()
        //{
        //    client.BaseAddress = new Uri("http://goapi.openmetrolinx.com/OpenDataAPI/api/V1/Stop/All?key=30020230");
        //    var response = client.GetAsync()
        //}

        //static async Task<Station> GetStationAsync(string path)
        //{
        //    Station station = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        station = await response.Content.ReadAsAsync<station>();
        //    }
        //    return station;
        //}
    }
}