using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using PawsDayBackEnd.DTO.OrderChart;
using PawsDayBackEnd.DTO.OrderChart.QueryDTOs;
using PawsDayBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace PawsDayBackEnd.Services
{
    public class ChartOrderServices
    {
        private readonly IRepository<Order> _orderRepos;
        private readonly IRepository<OrderSchedule> _orderScheduleRepos;
        private readonly IRepository<OrderPetDetail> _orderDetailRepos;
        private readonly IConfiguration _configuration;

        

        public ChartOrderServices(
            IRepository<Order> orderRepos,
            IRepository<OrderSchedule> orderScheduleRepos,
            IRepository<OrderPetDetail> orderDetailRepos
,
            IConfiguration configuration)
        {
            _orderRepos = orderRepos;
            _orderScheduleRepos = orderScheduleRepos;
            _orderDetailRepos = orderDetailRepos;
            _configuration = configuration;
        }

        
        public List<salesQueryDto> GetTotalSales()
        {
            string ConnStr = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;

            List<salesQueryDto> queryOrders = new List<salesQueryDto>();

            //開始
            using (var connection = new SqlConnection(ConnStr))
            {
                string orderQuery = @"
                                        SELECT 
	                                        OrderMonth, 
	                                        COUNT(OrderID) AS TotalCount,
	                                        SUM(Amount) AS TotalSales
                                        FROM
                                        (
                                        SELECT DISTINCT
	                                        o.OrderID AS OrderId,
	                                        o.Amount AS Amount,
	                                        MONTH(os.ServiceDate) AS OrderMonth
                                        FROM [Order] as o
                                        JOIN OrderSchedule AS os ON o.OrderID = os.OrderID
                                        WHERE o.OrderStatus != 1 AND YEAR(os.ServiceDate) = YEAR(GETDATE())
                                        ) t
                                        GROUP BY OrderMonth
                                    ";
                queryOrders = connection.Query<salesQueryDto>(orderQuery).ToList();

            }
            


            return queryOrders;
            
        }

        public int GetTotalSalesQuan()
        {
            string ConnStr = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            int totalNum = 0;
            using (var connection = new SqlConnection(ConnStr))
            {
                string sqlQuery = @"
                                    SELECT 
	                                    COUNT(t.OrderId)
                                    FROM
                                    (
                                    SELECT DISTINCT
	                                    o.OrderID AS OrderId
                                    FROM [Order] as o
                                    JOIN OrderSchedule AS os ON o.OrderID = os.OrderID
                                    WHERE o.OrderStatus != 1 AND YEAR(os.ServiceDate) = YEAR(GETDATE())
                                    ) t 
                                  ";
                totalNum = connection.ExecuteScalar<int>(sqlQuery);
            }

                return totalNum;
        }

        public int GetGrossTotalRevenu()
        {
            string ConnStr = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            int totalRevenue = 0;
            using (var connection = new SqlConnection(ConnStr))
            {
                string sqlQuery = @"
                                        SELECT 
	                                        SUM(Amount) AS TotalSales
                                        FROM
                                        (
                                        SELECT DISTINCT
	                                        o.OrderID AS OrderId,
	                                        o.Amount AS Amount,
	                                        MONTH(os.ServiceDate) AS OrderMonth
                                        FROM [Order] as o
                                        JOIN OrderSchedule AS os ON o.OrderID = os.OrderID
                                        WHERE o.OrderStatus = 2 AND YEAR(os.ServiceDate) = YEAR(GETDATE())
                                        ) t
                                        GROUP BY OrderMonth
                                  ";
                totalRevenue = connection.ExecuteScalar<int>(sqlQuery);

            }
            int grossMargin = (int)Math.Ceiling(totalRevenue * 0.2);
                return grossMargin;
        }


        public List<serviceQueryDto> GetServicePie()
        {
            string ConnStr = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;

            var serviceQuery = new List<serviceQueryDto>();
            using (var connection = new SqlConnection(ConnStr))
            {
                string sqlQuery = @"
                                        SELECT  
	                                        SUBSTRING(o.OrderNumber, 1,1) AS ServiceType,
	                                        COUNT(*) AS Quan
                                        FROM [order] AS o
                                        WHERE o.OrderStatus != 1
                                        GROUP BY SUBSTRING(o.OrderNumber, 1,1)
                                   ";
                serviceQuery = connection.Query<serviceQueryDto>(sqlQuery).ToList();


            }

            return serviceQuery;

        }

    }
}
