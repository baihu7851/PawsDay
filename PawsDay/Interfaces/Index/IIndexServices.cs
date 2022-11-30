using PawsDay.ViewModels.Home;
using System.Collections.Generic;

namespace PawsDay.Interfaces.Index
{
    public interface IIndexServices
    {
        List<RecommendViewModel> GetRank(List<RecommendViewModel> cardList);
        List<RecommendViewModel> GetRecommendDapper();
        List<RecommendViewModel> Recommend();
        RecommendListViewModel ServicesRecommend();
    }
}