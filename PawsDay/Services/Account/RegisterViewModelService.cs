using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PawsDay.Interfaces.Account;
using PawsDay.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Services.Account
{
    public class RegisterViewModelService : IRegisterViewModelService
    {    
        private readonly IRepository<County> _county;
        private readonly IRepository<District> _district;
        private readonly IRepository<AccountInfo> _accountInfo;

        public RegisterViewModelService(IRepository<County> county, IRepository<District> district, IRepository<AccountInfo> accountInfo)
        {
            _county = county;
            _district = district;
            _accountInfo = accountInfo;
        }

        public List<CountyDto> GetCounty()
        {           
            var allCounty = _county.GetAllReadOnly().ToList();
            var allDistrict = _district.GetAllReadOnly().ToList();


            var countyList = allCounty.Select(c => new CountyDto
            {
                County = c.CountyName,
                CountyId = c.CountyId,
                Areas = (from ad in allDistrict
                         join ac in allCounty
                         on ad.CountyId equals ac.CountyId
                         where ac.CountyId == c.CountyId
                         select new CountyandDistrictDto
                         {
                             County = ac.CountyName,
                             CountyId = ac.CountyId,
                             District = ad.DistrictName,
                             DistrictId = ad.DistrictId
                         }).ToList()
            }).ToList();

            return countyList;
        }

        public bool ConfirmData(int accountInfoId, string intputExpirationTime)
        {
            return _accountInfo.GetById(accountInfoId).ExpirationTime.ToString("yyyy-MM-dd-HH-mm-ss") == intputExpirationTime;
        }

      
    }
}
