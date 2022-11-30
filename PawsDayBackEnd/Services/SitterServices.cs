using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Sitter;
using PawsDayBackEnd.Services.SendGrid;
using PawsDayBackEnd.Services.SendGrid.DTO;
using SendGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace PawsDayBackEnd.Services
{
    public class SitterServices
    {
        #region 建構式
        private readonly IRepository<Member> _member;
        private readonly IRepository<RegisterSitter> _registersitter;
        private readonly IRepository<Answer> _answer;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly SendGridServices _sendGridService;
        private readonly IRepository<License> _license;
        private readonly IRepository<OfficialContact> _contact;
        public SitterServices(IRepository<Member> member, IRepository<RegisterSitter> registersitter, IRepository<Answer> answer, IUserStatusRepository userStatusRepository, SendGridServices sendGridServices, IRepository<License> license, IRepository<OfficialContact> contact)
        {
            _member = member;
            _registersitter = registersitter;
            _answer = answer;
            _userStatusRepository = userStatusRepository;
            _sendGridService = sendGridServices;
            _license = license;
            _contact = contact;
        }
        #endregion

        string SitterApproved= "已通過保姆審核，請重新登入。\r\n 至保母中心填寫基本資料後即可開始上架服務。";
        string SitterRejected= "未通過保姆審核，請確認保姆申請條件後再行送件。";
        string SitterSuspended= "您的保姆資格已被停權，如有異議請聯繫官方客服。";

        #region 查詢

        //以Status查詢(待審核為預設)
        public ApiResultDto GetSitterListByStatus(int status, int index, int takecount)
        {
            var searchstatus = (int)(SitterStatus)status;
            var raw = _registersitter.GetAllReadOnly().Where(s => s.RegisterStatus == searchstatus).Skip(index).Take(takecount).ToList();
            var count = _registersitter.GetAllReadOnly().Count(s => s.RegisterStatus == searchstatus);
            return GetSitterList(raw, count);
        }

        //以名字查詢
        public ApiResultDto GetSitterListByName(string name)
        {
            var raw = _registersitter.GetAllReadOnly().Where(s => s.SitterName.Contains(name)).ToList();
            var count = raw.Count();

            return GetSitterList(raw, count);
        }

        //以ID查詢
        public ApiResultDto GetSitterListByID(int id)
        {
            var raw = _registersitter.GetAllReadOnly().Where(s => s.MemberId == id).ToList();
            var count = raw.Count();

            return GetSitterList(raw, count);
        }

        //以Email查詢
        public ApiResultDto GetSitterListByMail(string email)
        {
            var raw = _registersitter.GetAllReadOnly().Where(s => _member.GetAllReadOnly().Where(m => m.Email == email).Select(m => m.MemberId).Contains(s.MemberId)).ToList();
            var count = raw.Count();

            return GetSitterList(raw, count);
        }

        //共用method:組成Dto包出去
        public ApiResultDto GetSitterList(List<RegisterSitter> raw, int count)
        {
            if (count == 0)
            {
                var response = new ApiResultDto();
                response.Message = "Not Found";
                return response;
            }

            var data = raw.Select(s => new SitterListDto
            {
                MemberId = s.MemberId,
                Name = s.SitterName,
                Score = s.Score,
                SubmitTime = s.CreateTime.AddHours(8),
                Status = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((SitterStatus)s.RegisterStatus),
                Count = count
            });

            return new ApiResultDto(data);
        }

        #endregion

        #region 個人資訊
        public ApiResultDto GetInfo(int id)
        {
            var sitter = _registersitter.GetAllReadOnly().First(s => s.MemberId == id);
            var member = _member.GetById(id);
            var answer = _answer.GetAllReadOnly().Where(a => a.MemberId == id).Select(a=>a.AnswerInput);
            var license = _license.GetAllReadOnly().Where(l => l.MemberId == id).Select(l => l.LicenseUrl);
            var documents = new  List<string>();
            documents.Add(sitter.Idimagefont);
            documents.Add(sitter.Idimageback);

            if (license.Count() != 0) 
            {
                foreach (var li in license)
                { documents.Add(li); }
            } 

            var response = new SitterInfoDto 
            {
                MemberId=id,
                Name=sitter.SitterName,
                ImageUrl=sitter.SitterPicture==null? member.ProfileImage : sitter.SitterPicture,
                Score=sitter.Score,
                Status= EnumDisplayHelper.GetDisplayName<DisplayAttribute>((SitterStatus)sitter.RegisterStatus),
                SubmitTime=sitter.CreateTime.AddHours(8),
                Experience=sitter.PetExperience,
                Answer=answer.ToList(),
                DocumentUrl=documents
            };
            return new ApiResultDto(response); 
        }

        #endregion

        #region 權限變更
        public ApiResultDto UpdateUniStatus(int id, int updatestatus)
        {
            var response = new ApiResultDto();
            var sitter = _registersitter.GetAllReadOnly().FirstOrDefault(s => s.MemberId == id);
            if (sitter == null)
            {
                response.Status = StatusCode.Failed;
                response.Message = "輸入ID錯誤";
                return response;
            }

            sitter.RegisterStatus = (int)(SitterStatus)updatestatus;
            var mail = _member.GetAllReadOnly().First(m => m.MemberId == id).Email;

            //原本待審核> 回傳Approved / Reject
            //原本已通過> 回傳Suspend
            //原本未通過> 回傳回傳Approved
            TrytoUpdateStatus(sitter, mail, updatestatus);
            
            response.Status = StatusCode.Success;
            return response;
        }

        private ApiResultDto TrytoUpdateStatus(RegisterSitter sitter,string mail, int status)
        {
            var response = new ApiResultDto();
            try
            {
                if (status == (int)SitterStatus.Approved)
                {
                    //處理transaction(更新狀態、增加userrole)
                    _userStatusRepository.CreateSitterRole(sitter);
                    EmailApproved(sitter.SitterName, mail, SitterApproved);
                }
                else if (status == (int)SitterStatus.Reject)
                {
                    //處理更新狀態即可
                    _registersitter.Update(sitter);
                    EmailApproved(sitter.SitterName, mail, SitterRejected);
                }
                else
                {
                    //處理transaction(更新狀態、刪除userrole)
                    _userStatusRepository.DeleteSitterRole(sitter);
                    EmailApproved(sitter.SitterName, mail, SitterSuspended);
                }

                var contact = ContactApproved(sitter.MemberId, (int)status);
                _contact.Add(contact);

                response.Status = StatusCode.Success;

            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"儲存失敗{ex}";
            }
            return response;
        }

        public ApiResultDto UpdateRangeStatus(List<int> id)
        {
            var response = new ApiResultDto();
            var sitters = _registersitter.GetAllReadOnly().Where(s => id.Contains(s.MemberId)).ToArray();

            for (var index = 0; index < sitters.Length; index++)
            {
                sitters[index].RegisterStatus = (int)SitterStatus.Approved;
            }

            try
            {
                _userStatusRepository.CreateRangeSitterRole(sitters);
                response.Status = StatusCode.Success;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"儲存失敗{ex}";
            }

            return response;
        }

        private async void EmailApproved(string name, string email,string context)
        {
            var mail = new EmailStatusDto
            {
                EmailContentDTO = new EmailContentDTO { UserName = name, UserEmail = email },
                StatusContent = context
            };
            await _sendGridService.UpdateStatus(mail);
        }

        private OfficialContact ContactApproved(int id, int status)
        {           
            var contact = new OfficialContact 
            { 
                UserType=(int)UserType.Member,
                UserId=id,
                Message= GetContactString(status),
                CreateTime= DateTime.UtcNow,
                IsUserSpeak=false,
            };
           return contact;
        }

        private string GetContactString(int status)
        {
            switch (status)
            {
                case (int)SitterStatus.Approved:
                    return "保姆審核已通過";
                case (int)SitterStatus.Reject:
                    return "保姆審核未通過";
                case (int)SitterStatus.Suspend:
                    return "保姆已被停權";
                default:
                    return "請聯繫客服";
            }
        }
        #endregion



    }


}
