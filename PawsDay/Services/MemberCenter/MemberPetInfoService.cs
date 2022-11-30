using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet.Actions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter;
using PawsDay.ViewModels.Account;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.SitterCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace PawsDay.Services.MemberCenter
{
    public class MemberPetInfoService
    {
        private readonly IRepository<Member> _member;
        private readonly IRepository<PetInfomation> _petInfomationRepo;
        private readonly IRepository<PetDisposition> _petDispositionRepo;
        private readonly IRepository<Disposition> _dispositionRepo;

        public MemberPetInfoService(IRepository<Member> member,
               IRepository<PetInfomation> petInfomation,
               IRepository<PetDisposition> petDisposition,
               IRepository<Disposition> dispositionRepo)
        {
            _member = member;
            _petInfomationRepo = petInfomation;
            _petDispositionRepo = petDisposition;
            _dispositionRepo = dispositionRepo;
        }

        public IEnumerable<MemberPetInfoViewModel> GetPetList(int memberID )
        {
            var petInfomations = _petInfomationRepo.GetAllReadOnly().Where(s => s.MemberId == memberID).ToList();
            var petdisposition = _petDispositionRepo.GetAllReadOnly().Where(p => petInfomations.Select(x => x.PetId).Contains(p.PetId)).ToList();
            var disposition = _dispositionRepo.GetAllReadOnly().ToList();

            var result = petInfomations
                .Select(pet => {

                    var currentPetdisposition = petdisposition.Where(x => x.PetId == pet.PetId).ToList();

                    return new MemberPetInfoViewModel
                    {
                        PetID = pet.PetId,
                        PetName = pet.PetName,
                        PetGender = pet.PetSex ? "1":"0",
                        PetType = EnumDisplayHelper.GetDisplayName<PetType>((PetType)pet.PetType),
                        ShapeType = EnumDisplayHelper.GetDisplayName<ShapeType>((ShapeType)pet.ShapeType),
                        BirthYear = pet.BirthYear,
                        BirthMonth = pet.BirthMonth,
                        Ligation = pet.Ligation ? "true" : "false",
                        Vaccine = pet.Vaccine ? "true" : "false",
                        Description = pet.Intro,
                        PetDispositionType = currentPetdisposition.Select(x => x.DispositionType.ToString()).ToList()
                    };
                });

            return result;
        }
        
        public List<SelectListItem> GetPetDispositionList()
        {
            var DispositionList = _dispositionRepo.GetAllReadOnly().Select(p => new SelectListItem
            {
                Text = p.DispositionType, Value = $"{p.DispositionId}", Selected = false,
            }).ToList();

            return DispositionList;
        }

        public void CreatePetData(MemberPetInfoViewModel petInfoVM ,int memberId)
        {
            var petInfomation = new PetInfomation
            {
                MemberId = memberId,
                PetName = petInfoVM.PetName,
                PetSex = petInfoVM.PetGender == "1",
                PetType = int.Parse(petInfoVM.PetType),
                ShapeType = int.Parse(petInfoVM.ShapeType),
                BirthMonth = petInfoVM.BirthMonth,
                BirthYear = petInfoVM.BirthYear,
                Ligation = petInfoVM.Ligation == "true" ? true : false,
                Vaccine = petInfoVM.Vaccine == "true" ? true : false,
                Intro = petInfoVM.Description,
                CreateTime = DateTime.UtcNow
            };

            var petInfoEntities = _petInfomationRepo.Add(petInfomation);
            var petId = petInfoEntities.PetId;
            
            foreach (var petDisposition in petInfoVM.PetDispositionType)
            {
                var data = new PetDisposition
                {
                     PetId = petId,
                     DispositionType = int.Parse(petDisposition)
                };
                _petDispositionRepo.Add(data);
            }

            return;
        }

        public void UpdatePetInfo(PetInfoDTO petInfoDto)
        {
            var petData = _petInfomationRepo.GetById(petInfoDto.PetID);

            petData.PetName = petInfoDto.PetName;
            petData.PetSex = petInfoDto.PetGender == "true";
            petData.PetType = int.Parse(petInfoDto.PetType);
            petData.ShapeType = int.Parse(petInfoDto.ShapeType);
            petData.BirthMonth = int.Parse(petInfoDto.BirthMonth);
            petData.BirthYear = int.Parse(petInfoDto.BirthYear);
            petData.Ligation = petInfoDto.Ligation;
            petData.Vaccine = petInfoDto.Vaccine;
            petData.Intro = petInfoDto.Description;
            petData.EditTime = DateTime.UtcNow;

            
            try
            {
                DeleteDisposition(petInfoDto.PetID);

                foreach (var disposition in petInfoDto.PetDispositionType)
                {
                    var data = new PetDisposition
                    {
                        PetId = petInfoDto.PetID,
                        DispositionType = int.Parse(disposition)
                    };
                    _petDispositionRepo.Add(data);
                }

                _petInfomationRepo.Update(petData);
                
                return;
            }
            catch
            {
                return;
            }
        }

        public void DeletePetInfo(int petId)
        {
            var petInfoEntity = _petInfomationRepo.GetAll().First(x => x.PetId == petId);
            _petInfomationRepo.Delete(petInfoEntity);
        }

        public void DeleteDisposition(int petId)
        {
            //個性要刪除多筆資料
            var petDispositionEntity = _petDispositionRepo.GetAll().Where(x => x.PetId == petId);
            _petDispositionRepo.DeleteRange(petDispositionEntity);

            //foreach (var disposition in petDispositionEntity)
            //{
            //    _petDispositionRepo.Delete(disposition);
            //}
        }
    }
}
