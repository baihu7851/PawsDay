using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.ChartProduct;
using System.Collections.Generic;
using System.Linq;

namespace PawsDayBackEnd.Services
{
    public class ChartProductServices
    {
        private readonly IConfiguration _configuration;

        public ChartProductServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApiResultDto GetProductQuantity()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            dynamic quantitys = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string quantity = @"SELECT Count(ProductID) AS ProductCount FROM Product
                                    WHERE IsDelete='False' AND ProductStatus='0'";
                quantitys = connection.QueryFirst(quantity);
            }
            return new ApiResultDto(quantitys);
        }
        public ApiResultDto GetSaleQuantity()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            var quantitys = new SaleQuantityDto();
            using (var connection = new SqlConnection(connectionString))
            {
                string quantity = @"SELECT 
                                    (SELECT (Count(*)) FROM Product WHERE ProductStatus='1' AND IsDelete='False')AS OffSale,
                                    (SELECT (Count(*)) FROM Product WHERE ProductStatus='0' AND IsDelete='False')AS OnSale, 
                                    (SELECT (Count(*)) FROM Product WHERE IsDelete='True')AS IsDelete";
                quantitys = connection.QueryFirst<SaleQuantityDto>(quantity);
            }
            return new ApiResultDto(quantitys);
        }
        public ApiResultDto GetCreateQuantity()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            var quantitys = new List<CreateQuantityDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                string quantity = @"SELECT
	                                    CreateMonth,
	                                    COUNT(ProductID) AS TotalCount
                                    FROM
	                                    (SELECT DISTINCT
	                                    p.ProductID as ProductID,
	                                    MONTH(p.CreateTime) as CreateMonth
	                                    FROM Product as p
	                                    WHERE YEAR(p.CreateTime) = DATEADD( YEAR , 0 , YEAR(GETDATE()) )
	                                    )p
                                    GROUP BY CreateMonth";
                quantitys = connection.Query<CreateQuantityDto>(quantity).ToList();
            }
            return new ApiResultDto(quantitys);
        }
        public ApiResultDto GetServiceTypeQuantity()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            var quantitys = new List<ServiceTypeDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                string quantity = @"SELECT
                                     (SELECT
                                      ServiceType
                                      FROM ServiceType t
                                      WHERE t.ServiceTypeID=p.ServiceType)as ServiceType,
                                     COUNT(ProductID) AS TotalCount
                                    FROM Product p
                                    WHERE p.IsDelete='False' AND p.ProductStatus='0'
                                    GROUP BY p.ServiceType";
                quantitys = connection.Query<ServiceTypeDto>(quantity).ToList();
            }
            return new ApiResultDto(quantitys);
        }
        public ApiResultDto GetEvaluationTop()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            var quantitys = new List<EvaluationTopDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                string quantity = @"SELECT Top 3
                                     p.ProductId as ProductId,
                                     s.MemberID,
                                     s.SitterName,
                                     COALESCE((SELECT
                                      COUNT(e.Evaluation) as EvaluationCount
                                      FROM Product as p2
                                      INNER JOIN [Order] as o ON  o.ProductID=p2.ProductID
                                      INNER JOIN Evaluation as e ON e.OrderID = o.OrderID AND UserType='1'
                                      WHERE p2.ProductID=p.ProductID
                                      GROUP BY p2.ProductID),'0') as EvaluationCount,
                                     COALESCE((SELECT
                                      AVG(e.Evaluation) as EvaluationAVG
                                      FROM Product as p2
                                      INNER JOIN [Order] as o ON  o.ProductID=p2.ProductID
                                      INNER JOIN Evaluation as e ON e.OrderID = o.OrderID AND UserType='1'
                                      WHERE p2.ProductID=p.ProductID
                                      GROUP BY p2.ProductID),'0') as EvaluationAvg
                                    FROM Product as p
                                    INNER JOIN RegisterSitter  as s ON  s.MemberID=p.SitterID
                                    WHERE p.IsDelete='False' AND p.ProductStatus='0'
                                    ORDER BY EvaluationAvg DESC,EvaluationCount DESC";
                quantitys = connection.Query<EvaluationTopDto>(quantity).ToList();
            }
            return new ApiResultDto(quantitys);
        }
        public ApiResultDto GetOrderTop()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            var quantitys = new List<OrderTopDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                string quantity = @"SELECT Top 3
                                     p.ProductId as ProductId,
                                     s.MemberID,
                                     s.SitterName,
                                     COALESCE((SELECT
                                      COUNT(o.OrderID) as OrderCount
                                      FROM Product as p2
                                      INNER JOIN [Order] as o ON  o.ProductID=p2.ProductID
                                      WHERE p2.ProductID=p.ProductID
                                      GROUP BY p2.ProductID),'0') as OrderCount
                                    FROM Product as p
                                    INNER JOIN RegisterSitter  as s ON  s.MemberID=p.SitterID
                                    WHERE p.IsDelete='False' AND p.ProductStatus='0'
                                    ORDER BY OrderCount DESC";
                quantitys = connection.Query<OrderTopDto>(quantity).ToList();
            }
            return new ApiResultDto(quantitys);
        }
    }
}
