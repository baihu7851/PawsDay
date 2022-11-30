using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using isRock.LineBot;
using PawsDay.Interfaces.Account;
using PawsDay.Models;
using PawsDay.Models.SitterCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.ViewModels.SitterCenter;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Services.SitterCenter
{
    public  class SitterCenterBasicServices
    {
        private readonly IRepository<RegisterSitter> _registersitter;
        private readonly IRepository<Member> _member;
        private readonly IAccountManager _accountManager;

        int memberid;

        public SitterCenterBasicServices(IRepository<RegisterSitter> registersitter,  IRepository<Member> member, IAccountManager accountManager)
        {
            _registersitter = registersitter;
            _member = member;
            _accountManager = accountManager;
            memberid = _accountManager.GetLoginMemberId();
        }



        public TransResultDto<SitterBasicDto> GetBasic()
        {
            var info = _registersitter.GetAllReadOnly().Where(s => s.MemberId == memberid)
                .Select(s=>new SitterBasicDto 
                {
                    MemberID = s.MemberId,
                    SitterName = s.SitterName,
                    SitterDescription = s.SitterInfo
                }).First();
            
            return SitterCenterResponseHelper.ReadResponse(info);
        }

        
        public async Task<TransResultDto<SitterBasicDto>> CreateBasic(SitterBasicDto request)
        {
            var response = new TransResultDto<SitterBasicDto>();
            var info = _registersitter.GetAllReadOnly().First(s => s.MemberId == memberid);
            info.SitterName = request.SitterName;
            info.SitterInfo = request.SitterDescription;
            info.EditTime = DateTime.UtcNow;
            try
            {
                await _registersitter.UpdateAsync(info);
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.Message = "Failed";
                return response;
            }
        }



        public TransResultDto<SitterAccountDto> GetAccount() 
        {
            var response = new TransResultDto<SitterAccountDto>();
            var info = _registersitter.GetAllReadOnly().Where(s => s.MemberId == memberid)
                .Select(s=> new SitterAccountDto
                {
                    MemberID = s.MemberId,
                    Account = s.Account,
                    Bank = s.Bank
                }).First();
            
            return SitterCenterResponseHelper.ReadResponse(info);
        }

        public async Task<TransResultDto<SitterAccountDto>> CreateAccout(SitterAccountDto request)
        {
            var response = new TransResultDto<SitterAccountDto>();
            var info = _registersitter.GetAllReadOnly().First(s => s.MemberId == memberid);
            info.Account = request.Account;
            info.Bank = request.Bank;
            info.EditTime = DateTime.UtcNow;
            try
            {
                await _registersitter.UpdateAsync(info);
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.Message = "Failed";
                return response;
            }
        }


        //從service的方法將partialview要使用的資料傳過去
        public SitterSideBarViewModel Partial()
        {
            var source = _member.GetById(memberid);
            var sitterrresource = _registersitter.GetAllReadOnly().First(s => s.MemberId == memberid);
            var data = new SitterSideBarViewModel
            {
                MemberID = source.MemberId,
                Email = source.Email,
                Name = sitterrresource.SitterName is null ? source.Name : sitterrresource.SitterName,
                ImageUrl = sitterrresource.SitterPicture is null ? source.ProfileImage : sitterrresource.SitterPicture,
                IsEmail = source.RegisterType == (int)RegisterTypes.Email,
                RegisterTypeDTO = SitterCenterResponseHelper.GetRegisterType(source.RegisterType)
            };
            return data;
        }

        public async Task<ResultDto> UpdateUserImage(UserImageDto request)
        {
            var response = new ResultDto();
            var info = _registersitter.GetAllReadOnly().First(s => s.MemberId == request.Userid);

            info.SitterPicture = request.Image;
            try
            {
                var res = await _registersitter.UpdateAsync(info);
                return new ResultDto(res);
            }
            catch (Exception ex)
            {
                response.Message = $"更新失敗:{ex.Message}";
                return response;
            }
        }


    }
}
