using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDay.ViewModels.BecomePetsitter;
using System.IO;
using System.Text.Json;

namespace PawsDay.WebApi.BecomePetsitter
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsitterTestWebApiController : ControllerBase
    {
        [HttpGet]
        public string GetTestJson()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "json", "Test.json");

            if (!System.IO.File.Exists(path))
            {
                return null;
            }
            else
            {
                string jsonData = System.IO.File.ReadAllText(path);
                return jsonData;
            }
        }
    }
}