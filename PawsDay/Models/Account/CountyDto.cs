using System.Collections.Generic;

namespace PawsDay.Models.Account
{
    public class CountyDto
    {
        public int CountyId { get; set; }
        public string County { get; set; }

        public List<CountyandDistrictDto> Areas { get; set; }
    }
}
