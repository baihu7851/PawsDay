using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;
using Infrastructure.Data;
using isRock.LineBot;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.Product;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Order = ApplicationCore.Entities.Order;

namespace PawsDay.Services.MemberCenter
{
    public class CollectViewModelServices
    {
        private readonly IRepository<Collect> _collect;
        private readonly IRepository<ApplicationCore.Entities.Product> _product;
        private readonly IRepository<Order> _order;
        private readonly IRepository<Evaluation> _evalution;
        private readonly IRepository<RegisterSitter> _sitter;
        private readonly IRepository<ProductImage> _image;
        private readonly IRepository<ServiceType> _servicetype;
        private readonly IRepository<ProductServicePetType> _pettype;
        private readonly IRepository<ProductServiceArea> _area;
        private readonly IRepository<County> _county;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CollectViewModelServices> _logger;

        public CollectViewModelServices(IRepository<Collect> collect, IRepository<ApplicationCore.Entities.Product> product, IRepository<Order> order, IRepository<Evaluation> evalution, IRepository<RegisterSitter> sitter, IRepository<ProductImage> image, IRepository<ServiceType> servicetype, IRepository<ProductServicePetType> pettype, IRepository<ProductServiceArea> area, IRepository<County> county, IConfiguration configuration, ILogger<CollectViewModelServices> logger)
        {
            _collect = collect;
            _product = product;
            _order = order;
            _evalution = evalution;
            _sitter = sitter;
            _image = image;
            _servicetype = servicetype;
            _pettype = pettype;
            _area = area;
            _county = county;
            _configuration = configuration;
            _logger = logger;
        }

        public ResultDto GetCollectList(int userId)
        {
            var result = new ResultDto();
            
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;

            using (var connection = new SqlConnection(connectionString))
            {                
                var collect = connection.Query<CollectViewModel>($@"EXECUTE dbo.MemberCollect @userId", new {userId=userId }).ToList();
                
                return new ResultDto(collect);
            }
        }
        public ResultDto GetCollectList2(int userId)
        {
            var result = new ResultDto();
            var datelist = (from co in _collect.GetAllReadOnly()
                            join p in _product.GetAllReadOnly() on co.ProductId equals p.ProductId
                            join pi in _image.GetAllReadOnly() on co.ProductId equals pi.ProductId
                            join s in _sitter.GetAllReadOnly() on p.SitterId equals s.MemberId
                            join se in _servicetype.GetAllReadOnly() on p.ServiceType equals se.ServiceTypeId
                            where co.MemberId == userId && pi.Sort == 1
                            select new CollectViewModel
                            {
                                CollectId = co.CollectId,
                                ProductId = co.ProductId,
                                ProductImage = pi.Image,
                                SitterName = s.SitterName,
                                TypeName = se.TypeName,
                                IsCollect = true,

                            }).ToList();

            foreach (var item in datelist)
            {
                var evaluation = GetEvaluation(item.ProductId);

                item.OrderCount = GetOrderCount(item.ProductId);
                item.EvaluationAverage = evaluation.Count != 0 ? evaluation.Average() : 0;
                item.EvaluationCount = evaluation.Count;
                item.Price = GetPrice(item.ProductId);
                item.County = GetCounty(item.ProductId);
            }

            if (datelist.Count() == 0)
            {
                result.Message = "Not Found";
                return result;
            }

            result.IsSuccess = true;
            result.Data = datelist;
            return result;
        }

        public void RemoveCollect(int Id)
        {
            try
            {
                var collect = _collect.GetById(Id);
                _collect.Delete(collect);
            }
            catch
            {
                _logger.LogError("我的收藏刪除失敗");
            }
        }

        private List<int> GetEvaluation(int productId)
        {
            var EvaluationQuantity = from p in _product.GetAllReadOnly()
                                     join o in _order.GetAllReadOnly() on p.ProductId equals o.ProductId
                                     join e in _evalution.GetAllReadOnly() on o.OrderId equals e.OrderId
                                     where e.UserType == (int)UserType.Member && o.ProductId == productId
                                     select e.EvaluationScore;

            return EvaluationQuantity.ToList();
        }
        private int GetOrderCount(int productId)
        {
            var count = _order.GetAllReadOnly().Where(x => x.ProductId == productId).Count();
            return count;
        }

        private decimal GetPrice(int productId)
        {
            var price = _pettype.GetAllReadOnly().Where(x => x.ProductId == productId).Min(x => x.Price);
            return price;
        }
        private string GetCounty(int productId)
        {
            var countys = (from c in _county.GetAllReadOnly()
                           join p in _area.GetAllReadOnly() on c.CountyId equals p.County
                           where p.ProductId == productId
                           select c.CountyName).ToList();

            var countyList = countys.GroupBy(x => x).ToList();

            List<string> list = new List<string>();

            foreach (var item in countyList)
            {
                string city = item.Key;
                list.Add(city);
            }

            return string.Join("、", list);
        }  
    }
}
