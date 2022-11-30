using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDay.Models.SitterCenter;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.StaticWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Services.StaticWeb
{
    public class ContactUsService
    {
        private readonly IRepository<Contact> _contactRepo;

        public ContactUsService(IRepository<Contact> contactRepo)
        {
            _contactRepo = contactRepo;
        }

        //public IEnumerable<ContactUs> GetContactDto(ContactUsViewModel contactVM)
        //{
        //    var contactDto = _contactRepo.GetAllReadOnly().Select(result => new ContactUsDto
        //    {
        //        ContactId = result.ContactId,
        //        Name = result.Name,
        //        Email = result.Email,
        //        Phone = result.Phone,
        //        Title = result.Title,
        //        ContactContent = result.ContactContent,
        //        Status = result.Status,
        //        CreateTime = DateTime.UtcNow,

        //    }).ToList();

        //    return contactDto;

        //}

        public void CreateContact(ContactUsViewModel contactVM)
        {
            //var response = new ContactUsDto
            //{
            //    IsSuccess = true,
            //    Message = "",
            //}; 
            try
            {
                _contactRepo.Add(new Contact
                {
                    //Id會自己長
                    Name = contactVM.Name,
                    Email = contactVM.Mail,
                    Phone = contactVM.Phone,
                    Title = contactVM.Title,
                    CreateTime = DateTime.Now,
                    ContactContent = contactVM.ContactContent,
                    Status = false
                });
            }
            catch
            {
                return;
            }
            

        }



    }
}
