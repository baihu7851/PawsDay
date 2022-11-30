using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.Layout
{
    public class NotLoginCartDto
    {
        public int ProductId { get; set; }
        public string Date { get; set; }
        public string Times { get; set; }
        public string PetTypes { get; set; }
    }
}
