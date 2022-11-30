using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDay.Services;
using System.Collections.Generic;

namespace PawsDay.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DbWebApiController : ControllerBase
    {
        private readonly DBService _dbService;
        public DbWebApiController(DBService dBService)
        {
            _dbService = dBService;
        }

        [HttpPost]
        public void CreateCounty(List<string> input)
        {
            _dbService.CreateCounty(input);
        }

        [HttpPost]
        public void CreateDistrict(int countyid, List<string> districtname)
        {
            _dbService.CreateDistrict(countyid, districtname);
        }
    }
}
