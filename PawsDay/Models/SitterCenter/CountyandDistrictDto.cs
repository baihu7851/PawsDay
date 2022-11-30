namespace PawsDay.Models.SitterCenter
{
    public class CountyandDistrictDto
    {
        public string County { get; set; }
        public string District { get; set; }

        public int ? DistrictId { get; set; }
        public int? CountyId { get; set; }
    }
}
