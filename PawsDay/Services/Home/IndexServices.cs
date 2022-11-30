using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;
using isRock.LineBot;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.Data.SqlClient;
using PawsDay.Interfaces.Account;
using PawsDay.Services.Product;
using PawsDay.ViewModels.Home;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;
using PawsDay.Interfaces.Index;
using PawsDay.Models.SitterCenter.WebApi;

namespace PawsDay.Services.Home
{
    public class IndexServices : IIndexServices
    {
        private readonly IAccountManager _accountManager;
        private readonly IConfiguration _configuration;
        public IndexServices(IAccountManager accountManager, IConfiguration configuration)
        {
            _accountManager = accountManager;
            _configuration = configuration;
        }
        public RecommendListViewModel ServicesRecommend()
        {
            var hotList = GetRecommendDapper().OrderByDescending(p => p.EvaluationAverage).ToList();
            var recommendsList = hotList.Take(8).ToList();
            List<RecommendViewModel> recommends = GetRank(recommendsList);

            var careList = hotList.Where(p => p.ServiceType == "到府照顧").Take(8).ToList();
            List<RecommendViewModel> carerecommends = GetRank(careList);

            var salonList = hotList.Where(p => p.ServiceType == "到府洗澡").Take(8).ToList();
            List<RecommendViewModel> salonrecommends = GetRank(salonList);

            var walkingList = hotList.Where(p => p.ServiceType == "陪伴散步").Take(8).ToList();
            List<RecommendViewModel> walkingrecommends = GetRank(walkingList);

            RecommendListViewModel recommendList = new RecommendListViewModel()
            {
                RecommendList = recommends,
                CareList = carerecommends,
                SalonList = salonrecommends,
                WalkingList = walkingrecommends
            };
            return recommendList;
        }
        public List<RecommendViewModel> Recommend()
        {
            var cardList = GetRecommendDapper().OrderByDescending(p => p.EvaluationAverage).Take(8).ToList();
            List<RecommendViewModel> recommendList = new List<RecommendViewModel>();
            var rank = 1;
            foreach (var card in cardList)
            {
                RecommendViewModel item = new RecommendViewModel()
                {
                    ProductId = card.ProductId,
                    SitterName = card.SitterName,
                    ServiceType = card.ServiceType,
                    Image = card.Image,
                    EvaluationAverage = card.EvaluationAverage,
                    EvaluationQuantity = card.EvaluationQuantity,
                    OrderQuantity = card.OrderQuantity,
                    Collect = card.Collect,
                    Price = card.Price,
                    County = card.County,
                    Rank = rank
                };
                recommendList.Add(item);
                rank++;
            }
            return recommendList;
        }

        #region 取資料
        public List<RecommendViewModel> GetRecommendDapper()
        {
            var cardList = new List<RecommendViewModel>();
            var customerId = _accountManager.GetLoginMemberId();
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                string cardsql = @"SELECT * FROM view_ProductSimpleCard
                                   ORDER BY EvaluationAvg DESC,OrderCount DESC";
                List<RecommendDto> recommends = connection.Query<RecommendDto>(cardsql).ToList();

                foreach (RecommendDto r in recommends)
                {
                    var item = new RecommendViewModel
                    {
                        ProductId = r.ProductId,
                        SitterName = r.SitterName,
                        ServiceType = r.ServiceType,
                        Image = r.Image,
                        EvaluationAverage = (double)r.EvaluationAvg,
                        EvaluationQuantity = r.EvaluationCount,
                        OrderQuantity = r.OrderCount,
                        Collect = false,
                        Price = r.Price.ToString("#,###"),
                        County = r.County
                    };
                    cardList.Add(item);
                }
            }
            return cardList;
        }
        public List<RecommendViewModel> GetRank(List<RecommendViewModel> cardList)
        {
            List<RecommendViewModel> recommends = new List<RecommendViewModel>();
            var rank = 1;
            foreach (var card in cardList)
            {
                RecommendViewModel item = new RecommendViewModel()
                {
                    ProductId = card.ProductId,
                    SitterName = card.SitterName,
                    ServiceType = card.ServiceType,
                    Image = card.Image,
                    EvaluationAverage = card.EvaluationAverage,
                    EvaluationQuantity = card.EvaluationQuantity,
                    OrderQuantity = card.OrderQuantity,
                    Collect = card.Collect,
                    Price = card.Price,
                    County = card.County,
                    Rank = rank
                };
                recommends.Add(item);
                rank++;
            }
            return recommends;
        }

        public ResultDto GetCollect(int memberId ,int productId)
        {

            var response = new ResultDto();
            try
            {

                bool collected = false;
                string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
                using (var connection = new SqlConnection(connectionString))
                {
                    string collectssql = "SELECT* FROM Collect WHERE MemberID=@memberID AND ProductID=@productID";
                    var collect = connection.Query(collectssql, new { memberID = memberId, productID = productId }).ToList();

                    collected = collect.Any();
                }
                return new ResultDto(collected);
            }
            catch (Exception ex)
            {
                response.Message = $"新增失敗:{ex.Message}";
                return response;
            }
        }
        #endregion
    }
}
