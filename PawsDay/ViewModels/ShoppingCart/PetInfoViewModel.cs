namespace PawsDay.ViewModels.ShoppingCart
{
    public class PetInfoViewModel
    {
        public string PetName { get; set; }
        public int PetSex { get; set; }
        public int PetTypeId { get; set; }
        public string PetType { get; set; }
        public int ShapeTypeId { get; set; }
        public string ShapeType { get; set; }
        public int BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public bool Ligation { get; set; }
        public bool Vaccine { get; set; }
        public string Remark { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

}

