using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Member : BaseEntity
    {
        public Member()
        {
            Carts = new HashSet<Cart>();
            Collects = new HashSet<Collect>();
            Evaluations = new HashSet<Evaluation>();
            FacebookRegisters = new HashSet<FacebookRegister>();
            GoogleRegisters = new HashSet<GoogleRegister>();
            LineRegisters = new HashSet<LineRegister>();
            OfficialContacts = new HashSet<OfficialContact>();
            OrderCustomers = new HashSet<Order>();
            OrderSitters = new HashSet<Order>();
            PetInfomations = new HashSet<PetInfomation>();
            Products = new HashSet<Product>();
            RegisterSitters = new HashSet<RegisterSitter>();
            UserContactMembers = new HashSet<UserContact>();
            UserContactSitters = new HashSet<UserContact>();
            UserRoles = new HashSet<UserRole>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        public int RegisterType { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public bool? Sex { get; set; }
        public DateTime? Birth { get; set; }
        public int? County { get; set; }
        public int? District { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public int Status { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public int? AccountInfoId { get; set; }

        public virtual AccountInfo AccountInfoNavigation { get; set; }
        public virtual County CountyNavigation { get; set; }
        public virtual District DistrictNavigation { get; set; }
        public virtual RegisterType RegisterTypeNavigation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Collect> Collects { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<FacebookRegister> FacebookRegisters { get; set; }
        public virtual ICollection<GoogleRegister> GoogleRegisters { get; set; }
        public virtual ICollection<LineRegister> LineRegisters { get; set; }
        public virtual ICollection<OfficialContact> OfficialContacts { get; set; }
        public virtual ICollection<Order> OrderCustomers { get; set; }
        public virtual ICollection<Order> OrderSitters { get; set; }
        public virtual ICollection<PetInfomation> PetInfomations { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<RegisterSitter> RegisterSitters { get; set; }
        public virtual ICollection<UserContact> UserContactMembers { get; set; }
        public virtual ICollection<UserContact> UserContactSitters { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
