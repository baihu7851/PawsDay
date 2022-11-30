using System.Collections.Generic;

namespace PawsDay.Models.MemberCenter
{
    public class PersonCountyDTO 
    {
        public PersonCountyListDTO personCountyListDTO { get; set; }
        public List<CountyListDTO> CountyDTO { get; set; }

    }
    public class PersonCountyListDTO 
    {
        public int? CountyId { get; set; }
        public int? DistrictId { get; set; }
    }
    public class CountyListDTO
    {
        public int CountyId { get; set; }
        public string County { get; set; }

        public List<DistrictDto> Areas { get; set; }
    }
    public class DistrictDto
    { 
        public int DistrictId { get; set; }
        public string District { get; set; }
    }
}
