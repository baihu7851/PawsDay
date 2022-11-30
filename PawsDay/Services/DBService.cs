using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace PawsDay.Services
{
    public class DBService
    {
        private readonly IRepository<County> _county;
        private readonly IRepository<District> _district;
        public DBService(IRepository<County> county, IRepository<District> district)
        {
            _county = county;
            _district = district;
        }

        public void CreateCounty(List<string> countyname)
        {
            var counties = new List<County>();
            foreach (var item in countyname)
            {
                var county = new County { CountyName = item };
                counties.Add(county);
            }

            _county.AddRange(counties);
        }

        public void CreateDistrict(int countyid, List<string> districtname)
        {
            var districts = new List<District>();
            foreach (var item in districtname)
            {
                var district = new District { DistrictName=item,CountyId= countyid };
                districts.Add(district);
            }

            _district.AddRange(districts);
        }
    }
}
