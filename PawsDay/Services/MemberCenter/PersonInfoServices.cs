using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration.UserSecrets;
using PawsDay.Models.Account;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.Account;
using PawsDay.ViewModels.Account;
using PawsDay.ViewModels.MemberCenter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PawsDay.Services.MemberCenter
{
    public class PersonInfoServices
    {
       
        private readonly IRepository<Member> _member;
        private readonly IRepository<AccountInfo> _accountInfo;
        private readonly IRepository<County> _county;
        private readonly IRepository<District> _district;
        private readonly IAppPasswordHasher _sHA256Hasher;

        public PersonInfoServices(IRepository<Member> member, IRepository<AccountInfo> accountInfo, IRepository<County> county, IRepository<District> district, IAppPasswordHasher sHA256Hasher)
        {
            _member = member;
            _accountInfo = accountInfo;
            _county = county;
            _district = district;
            _sHA256Hasher = sHA256Hasher;
        }
        
        public PersonInformationViewModel GETPersonInfo(int userId)
        { 
            var person=(from m in _member.GetAllReadOnly()
                       where m.MemberId==userId
                       select new PersonInformationViewModel
                       { 
                           MemberId= userId,
                           Name = m.Name,
                           NickName = m.NickName,
                           Gender = m.Sex,
                           Birth = (DateTime)m.Birth,
                           Address = m.Address,
                           Phone = m.Phone,
                           Email = m.Email,
                           EditTime = m.EditTime == null ? m.CreateTime : m.EditTime,
                           RegisterType = m.RegisterType
                       }).ToList().First();

            return person;
        }
        public ResultDto GetCountyList(int userId)
        {
            var result = new ResultDto();
            var allCounty = _county.GetAllReadOnly().ToList();
            var allDistrict = _district.GetAllReadOnly().ToList();


            var countyList = allCounty.Select(c => new CountyListDTO
            {
                County = c.CountyName,
                CountyId = c.CountyId,
                Areas = (from ad in allDistrict
                         join ac in allCounty
                         on ad.CountyId equals ac.CountyId
                         where ac.CountyId == c.CountyId
                         select new DistrictDto
                         {                             
                             District = ad.DistrictName,
                             DistrictId = ad.DistrictId
                         }).ToList()
            }).ToList();
            
            var member=_member.GetById(userId);
            var personDTO = new PersonCountyDTO
            {
                CountyDTO = countyList,
                personCountyListDTO = new PersonCountyListDTO {
                    CountyId = member.County,
                    DistrictId = member.District
                },
                
            };

            result.IsSuccess = true;
            result.Data = personDTO;
            return result;
        }

        public bool UpdatePersonInfo(PersonInformationViewModel input,int userId)
        {
            bool IsSuccess;
            var person=_member.GetById(userId);

            person.Name = input.Name;
            person.NickName = input.NickName;
            person.Sex = input.Gender;
            person.Birth = input.Birth;
            person.Address = input.Address;
            person.Phone = input.Phone;
            person.Email = input.Email;
            person.EditTime = DateTime.UtcNow;
            person.County = input.MemberCounty;
            person.District=input.MemberDistrict;
            
            try
            {
                _member.Update(person);
                IsSuccess = true;
                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }           
        }

        public UpdateAccount UpdateAccountSafety(int userId, AccountSafetyViewModel input)
        {
            var infoId = _member.GetById(userId).AccountInfoId;
            var userAccount=_accountInfo.GetById(infoId);            
            var updatemsg=new UpdateAccount { IsUpdate = true };
            if (_sHA256Hasher.HashPasseword(input.OldPassword) != userAccount.Password )
            {
                updatemsg.Message = "請確認舊密碼";
                return updatemsg;
            }
            
            try
            {
                userAccount.Password = _sHA256Hasher.HashPasseword(input.NewPassword);
                updatemsg.Message = "密碼更新成功";
                _accountInfo.Update(userAccount);                
            }
            catch
            {
                updatemsg.Message = "密碼更新失敗，請在試一次";
            }
            return updatemsg;
        }  
        
    }
}
