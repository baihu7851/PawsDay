using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Member;
using PawsDayBackEnd.DTO.Sitter;
using PawsDayBackEnd.Services.SendGrid;
using PawsDayBackEnd.Services.SendGrid.DTO;
using SendGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace PawsDayBackEnd.Services
{
    public class MemberServices
    {
        #region 建構式
        private readonly IRepository<Member> _member;
        private readonly IRepository<RegisterSitter> _registersitter;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly SendGridServices _sendGridService;
        private readonly IRepository<UserRole> _userrole;
        public MemberServices(IRepository<Member> member, IRepository<RegisterSitter> registersitter, IUserStatusRepository userStatusRepository,SendGridServices sendGridServices, IRepository<UserRole> userrole)
        {
            _member = member;
            _registersitter = registersitter;
            _userStatusRepository = userStatusRepository;
            _sendGridService = sendGridServices;
            _userrole = userrole;
        }
        #endregion

        string MemberApproved = "已恢復您的會員資格，請重新登入。";
        string MemberSuspended = "您的會員資格已被停權，如有異議請聯繫官方客服。";

        #region 查詢


        //以Status查詢(待審核為預設)
        public ApiResultDto GetMemberListByStatus(int status, int index,int takecount)
        {
            var searchstatus = (int)(SitterStatus)status;
            var raw = _member.GetAllReadOnly().Where(m => m.Status == searchstatus).Skip(index).Take(takecount).ToList();
            var count = _member.GetAllReadOnly().Count(m => m.Status == searchstatus);
            return GetMemberList(raw, count);
        }

        //以名字查詢
        public ApiResultDto GetMemberListByName(string name)
        {
            var raw = _member.GetAllReadOnly().Where(m => m.Name.Contains(name) || m.NickName.Contains(name)).ToList();
            var count = raw.Count();
            return GetMemberList(raw, count);
        }

        //以ID查詢
        public ApiResultDto GetMemberListByID(int id)
        {
            var raw = _member.GetAllReadOnly().Where(m=>m.MemberId==id).ToList();
            var count = raw.Count();
            return GetMemberList(raw, 1);
        }

        //以Email查詢
        public ApiResultDto GetMemberListByMail(string email)
        {
            var raw = _member.GetAllReadOnly().Where(m => m.Email.Contains(email)).ToList();
            var count = raw.Count();
            return GetMemberList(raw, count);
        }

        //共用method:組成Dto包出去
        public ApiResultDto GetMemberList(List<Member> raw, int count)
        {
            if (count == 0)
            {
                var response = new ApiResultDto();
                response.Message = "Not Found";
                return response;
            }

            var data = raw.Select(m => new MemberListDto
            {
                MemberId = m.MemberId,
                Name = m.NickName == null ? $"{m.Name}" : $"{m.Name}/{m.NickName}",
                RegisterType= EnumDisplayHelper.GetDisplayName<DisplayAttribute>((RegisterTypes)m.RegisterType),
                CreateTime = m.CreateTime.AddHours(8),
                Status = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((SitterStatus)m.Status),
                Count = count
            });

            return new ApiResultDto(data);
        }
        #endregion

        #region 個人資訊
        public ApiResultDto GetInfo(int id)
        {
            var member = _member.GetById(id);
            var roles = _userrole.GetAllReadOnly().Any(r=>r.UserId==id && r.RoleType== (int)UserType.Sitter);

            var response = new MemberInfoDto
            {
                MemberId = id,
                Name = member.NickName==null? $"{member.Name}" : $"{member.Name}/{member.NickName}",
                Image = member.ProfileImage,
                Sex= member.Sex==true?"男生":"女生",
                Birth=(DateTime)member.Birth,
                Status = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((SitterStatus)member.Status),
                Phone = member.Phone,
                Email = member.Email,
                CreateTime = member.CreateTime.AddHours(8),
                Role = roles?"保姆":"會員",
            };
            return new ApiResultDto(response);
        }

        #endregion

        #region 權限變更
        public ApiResultDto UpdateUniStatus(int id, int updatestatus)
        {
            var response = new ApiResultDto();
            var member = _member.GetById(id);
            if (member == null)
            {
                response.Status = StatusCode.Failed;
                response.Message = "輸入ID錯誤";
                return response;
            }

            member.Status = (int)(SitterStatus)updatestatus;

            if (updatestatus == (int)SitterStatus.Approved)
            {
                //處理transaction(更新狀態、增加userrole)
                response = TrytoUpdateStatus(member, true);
            }
            else if (updatestatus == (int)SitterStatus.Suspend)
            {
                //處理transaction(更新狀態、刪除userrole)
                response = TrytoUpdateStatus(member, false);
            }
            return response;
        }

        private ApiResultDto TrytoUpdateStatus(Member member,bool iscreate)
        {
            var response = new ApiResultDto();
            try
            {
                if (iscreate) 
                { 
                    _userStatusRepository.CreateMemberRole(member);
                    EmailApproved(member.Name, member.Email, MemberApproved);
                }
                else 
                { 
                    _userStatusRepository.DeleteMemberRole(member);
                    EmailApproved(member.Name, member.Email, MemberSuspended);
                }
                response.Status = StatusCode.Success;            
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"儲存失敗{ex}";
            }
            return response;
        }

        private async void EmailApproved(string name, string email, string context)
        {
            var mail = new EmailStatusDto
            {
                EmailContentDTO = new EmailContentDTO { UserName = name, UserEmail =email },
                StatusContent = context
            };
            await _sendGridService.UpdateStatus(mail);
        }
        #endregion
    }
}
