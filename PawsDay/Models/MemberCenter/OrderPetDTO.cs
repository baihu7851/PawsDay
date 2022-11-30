using ApplicationCore.Common;

namespace PawsDay.Models.MemberCenter
{
    public class OrderPetDTO
    {
        public int PetType { get; set; }
        public int ShapeType { get; set; }
    }
    public class OrderPetLListDTO
    {
        public int PetType { get; set; }
        public int ShapeType { get; set; }
        public int Count { get; set; }
    }
}
