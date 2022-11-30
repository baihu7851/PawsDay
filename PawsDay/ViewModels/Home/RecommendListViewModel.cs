using System.Collections.Generic;

namespace PawsDay.ViewModels.Home
{
    public class RecommendListViewModel
	{
        public List<RecommendViewModel> RecommendList { get; set; }
        public List<RecommendViewModel> CareList { get; set; }
        public List<RecommendViewModel> SalonList { get; set; }
        public List<RecommendViewModel> WalkingList { get; set; }
    }
}
