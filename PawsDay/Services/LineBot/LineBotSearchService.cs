using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawsDay.ViewModels.Home;
using PawsDay.WebApi.LineBot.Dto;
using SendGrid;
using System.Collections.Generic;
using System.Linq;

namespace PawsDay.Services.LineBot
{
    public class LineBotSearchService
    {
        private readonly IConfiguration _configuration;
        public LineBotSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        //要傳出的值 productid、img、title、intro
        public List<LineBotSearchDto> LineBotSearch(string input)
        {
            //輸入保姆名字，否則為推薦
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            List<RecommendDto> recommends;

            using (var connection = new SqlConnection(connectionString)) 
            {
                string cardsql = "SELECT * FROM view_ProductSimpleCard AS p WHERE p.ProductStatus='0' ORDER BY EvaluationAvg DESC,OrderCount DESC";
                recommends = connection.Query<RecommendDto>(cardsql).ToList();
            }
            var sitterresult = recommends.Where(p => p.SitterName.Contains(input));
            if (sitterresult.Count()!=0) 
            {
                return  sitterresult.Take(5).Select(p =>
                    new LineBotSearchDto
                    {
                        ProductId = p.ProductId,
                        SitterName = p.SitterName,
                        Image = $"https://{p.Image.Split("//")[1]}",
                        ServiceType = p.ServiceType,
                        Price = p.Price
                    }).ToList();
            }
                
            return recommends.Take(5).Select(p =>
                    new LineBotSearchDto
                    {
                        ProductId = p.ProductId,
                        SitterName = p.SitterName,
                        Image = $"https://{p.Image.Split("//")[1]}",
                        ServiceType = p.ServiceType,
                        Price = p.Price
                    }).ToList();
        }

    }
}
