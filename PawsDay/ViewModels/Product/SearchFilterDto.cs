using System.Collections.Generic;

namespace PawsDay.ViewModels.Product
{
    public class SearchFilterDto
    {
        public List<string> County { get; set; }
        public List<AreaDto> Areas { get; set; }
    }
}
