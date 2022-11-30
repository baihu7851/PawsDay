namespace PawsDay.Models.SitterCenter
{
    public class SitterOrderPetDto
    {
        public string PetName { get; set; }
        public string PetSex { get; set; }
        public int PetType { get; set; }
        public int ShapeType { get; set; }
        public string Description { get; set; }
        public int BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public string Ligation { get; set; }
        public string Vaccine { get; set; }
        public string Remark { get; set; }
    }
}
