using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDay.ViewModels.BecomePetsitter;
using System.Threading.Tasks;
using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Razor.Language;
using PawsDay.Interfaces.Account;
using isRock.LIFF;

namespace PawsDay.Services.BecomePetSitter
{
    public class BecomePetSitterService
    {
        #region 建構式

        private readonly IRepository<Member> _member;
        private readonly IRepository<RegisterSitter> _registerSitter;
        private readonly IRepository<Answer> _answer;
        private readonly IRepository<Aptitude> _aptitude;
        private readonly IRepository<License> _license;
        private readonly IAccountManager _accountManager;

        public BecomePetSitterService(
            IRepository<Member> member,
            IRepository<RegisterSitter> registerSitter,
            IRepository<Answer> answer,
            IRepository<Aptitude> aptitude,
            IRepository<License> license,
            IAccountManager accountManager)
        {
            _member = member;
            _registerSitter = registerSitter;
            _answer = answer;
            _aptitude = aptitude;
            _license = license;
            _accountManager = accountManager;
        }

        #endregion 建構式

        public int GetMemberId()
        {
            int memberId = _accountManager.GetLoginMemberId();
            return memberId;
        }

        public Member GetMember(int memberId)
        {
            var member = _member.GetAllReadOnly().First(m => m.MemberId == memberId);
            return member;
        }

        private string GetMemberName(PetsitterFormViewModel petSitterVM)
        {
            var member = GetMember(petSitterVM.MemberId);

            string memberName;
            if (member.NickName != null)
            {
                memberName = member.NickName;
            }
            else
            {
                memberName = member.Name;
            }

            return memberName;
        }

        public RegisterSitter GetSitter(int memberId)
        {
            RegisterSitter sitter = _registerSitter.GetAll().FirstOrDefault(m => m.MemberId == memberId);
            return sitter;
        }

        public async Task isCreateOrUpdate(PetsitterFormViewModel petsitterVM)
        {
            int memberIsExist;
            RegisterSitter sitter = GetSitter(petsitterVM.MemberId);
            if (sitter == null)
            {
                memberIsExist = 0;
            }
            else
            {
                memberIsExist = sitter.MemberId;
            }

            if (memberIsExist == 0)
            {
                await CreatePetSitter(petsitterVM);
            }
            else
            {
                await UpdatePetsitter(petsitterVM);
            }
        }

        #region Create 建立新的保姆資料

        public async Task CreatePetSitter(PetsitterFormViewModel petSitterVM)
        {
            // 取得使用者資料
            string memberName = GetMemberName(petSitterVM);
            try
            {
                // 資料放入_registerSitter
                var petsitter = new RegisterSitter()
                {
                    MemberId = petSitterVM.MemberId,
                    Id = petSitterVM.IdNumber,
                    Idimagefont = petSitterVM.IdCardFront,
                    Idimageback = petSitterVM.IdCardBack,
                    PetExperience = petSitterVM.PetExperience,
                    Score = CountTestPoint(petSitterVM),
                    RegisterStatus = (int)SitterStatus.AwaitToCheck,
                    CreateTime = petSitterVM.CreateTime,
                    SitterName = memberName,
                };

                // 資料放入_answer
                List<Answer> answerList = new List<Answer>();
                for (int i = 0; i < petSitterVM.QuestionId.Count; i++)
                {
                    var answer = new Answer()
                    {
                        MemberId = petSitterVM.MemberId,
                        QuestionId = petSitterVM.QuestionId[i],
                        AnswerInput = petSitterVM.Answer[i]
                    };
                    answerList.Add(answer);
                }

                // 資料放入_aptitude
                var aptitudePoints = CountAptitudePoint(petSitterVM);
                var aptitude = new Aptitude()
                {
                    MemberId = petSitterVM.MemberId,
                    AptitudeExtrovert = aptitudePoints[AptitudeType.Extrovert],
                    AptitudeIntrovert = aptitudePoints[AptitudeType.Introvert],
                    AptitudeThinking = aptitudePoints[AptitudeType.Thinking],
                    AptitudeFeeling = aptitudePoints[AptitudeType.Feeling]
                };

                // 資料放入_license
                List<License> licenseList = new List<License>();

                if (petSitterVM.License != null && petSitterVM.License[0] != null)
                {
                    for (int i = 0; i < petSitterVM.License.Count; i++)
                    {
                        var license = new License()
                        {
                            LicenseUrl = petSitterVM.License[i],
                            MemberId = petSitterVM.MemberId
                        };
                        licenseList.Add(license);
                    }
                }

                // 執行Add方法
                await _registerSitter.AddAsync(petsitter);
                await _answer.AddRangeAsync(answerList);
                await _aptitude.AddAsync(aptitude);
                await _license.AddRangeAsync(licenseList);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion Create 建立新的保姆資料

        #region Update 更新現有保姆資料

        public async Task UpdatePetsitter(PetsitterFormViewModel petSitterVM)
        {
            try
            {// 取得使用者資料
                string memberName = GetMemberName(petSitterVM);

                // 更新_registerSitter
                var petsitter = _registerSitter.GetAll().First(p => p.MemberId == petSitterVM.MemberId);
                petsitter.Id = petSitterVM.IdNumber;
                petsitter.Idimagefont = petSitterVM.IdCardFront;
                petsitter.Idimageback = petSitterVM.IdCardBack;
                petsitter.PetExperience = petSitterVM.PetExperience;
                petsitter.Score = CountTestPoint(petSitterVM);
                petsitter.RegisterStatus = (int)SitterStatus.AwaitToCheck;
                petsitter.CreateTime = petSitterVM.CreateTime;
                petsitter.SitterName = memberName;

                // 更新_answer
                var answerList = _answer.GetAll().Where(a => a.MemberId == petSitterVM.MemberId).ToList();
                for (int i = 0; i < petSitterVM.QuestionId.Count; i++)
                {
                    Answer answer = answerList[i];
                    answer.QuestionId = petSitterVM.QuestionId[i];
                    answer.AnswerInput = petSitterVM.Answer[i];
                }

                // 更新_aptitude
                var aptitude = _aptitude.GetAll().First(a => a.MemberId == petSitterVM.MemberId);
                var aptitudePoints = CountAptitudePoint(petSitterVM);
                aptitude.AptitudeExtrovert = aptitudePoints[AptitudeType.Extrovert];
                aptitude.AptitudeIntrovert = aptitudePoints[AptitudeType.Introvert];
                aptitude.AptitudeThinking = aptitudePoints[AptitudeType.Thinking];
                aptitude.AptitudeFeeling = aptitudePoints[AptitudeType.Feeling];

                // 刪除舊license，建立新license
                var oldLicense = _license.GetAll().Where(l => l.MemberId == petSitterVM.MemberId).ToList();
                await _license.DeleteRangeAsync(oldLicense);
                List<License> licenseList = new List<License>();

                if (petSitterVM.License != null && petSitterVM.License[0] != null)
                {
                    for (int i = 0; i < petSitterVM.License.Count; i++)
                    {
                        var newLicense = new License()
                        {
                            LicenseUrl = petSitterVM.License[i],
                            MemberId = petSitterVM.MemberId
                        };
                        licenseList.Add(newLicense);
                    }
                }

                // 執行Update方法
                await _registerSitter.UpdateAsync(petsitter);
                await _answer.UpdateRangeAsync(answerList);
                await _aptitude.UpdateAsync(aptitude);
                await _license.AddRangeAsync(licenseList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Update 更新現有保姆資料

        #region 計分

        public int CountTestPoint(PetsitterFormViewModel petsitterVM)
        {
            int point = petsitterVM.Quiz.Sum();
            return point;
        }

        public Dictionary<AptitudeType, int> CountAptitudePoint(PetsitterFormViewModel petsitterVM)
        {
            var group = petsitterVM.Aptitude.GroupBy(a => a);
            Dictionary<AptitudeType, int> result = new Dictionary<AptitudeType, int>();

            foreach (var g in group)
            {
                result[g.Key] = g.Count();
            }

            return result;
        }

        #endregion 計分
    }
}