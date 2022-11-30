using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.ChartProduct;
using PawsDayBackEnd.DTO.SitterChart;
using System;
using System.Collections.Generic;
using System.Linq;
using CreateQuantityDto = PawsDayBackEnd.DTO.SitterChart.CreateQuantityDto;

namespace PawsDayBackEnd.Services
{
    public class SitterChartService
    {
        #region 建構式

        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public SitterChartService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
        }

        #endregion 建構式

        /// <summary>
        /// 審核通過保姆總數
        /// </summary>
        /// <returns></returns>
        public ApiResultDto GetApprovedSitter()
        {
            var result = new ApiResultDto();
            try
            {
                dynamic sitterCount = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    string quantity = @"
                                        SELECT
	                                        COUNT(*) AS ApprovedSitter
                                        FROM RegisterSitter
                                        WHERE RegisterStatus = 1";
                    sitterCount = connection.QueryFirst(quantity);
                    result.Data = sitterCount;
                    result.Status = StatusCode.Success;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = StatusCode.Failed;
            }
            return result;
        }

        /// <summary>
        ///每月保姆申請數量
        /// </summary>
        /// <returns></returns>
        public ApiResultDto CountCreateQuantity()
        {
            var result = new ApiResultDto();
            try
            {
                List<CreateQuantityDto> quantitys = new List<CreateQuantityDto>();
                using (var connection = new SqlConnection(connectionString))
                {
                    string list = @"
                                    SELECT
	                                    DATEPART(MONTH,CreateTime) AS CreateMonth,
	                                    COUNT(*) AS CreateQuantity
                                    FROM RegisterSitter
                                    WHERE DATEPART(YEAR,CreateTime) = DATEPART(YEAR,GETDATE())
                                    GROUP BY DATEPART(MONTH,CreateTime)";
                    quantitys = connection.Query<CreateQuantityDto>(list).ToList();
                    result.Data = quantitys;
                    result.Status = StatusCode.Success;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = StatusCode.Failed;
            }
            return result;
        }

        /// <summary>
        /// 保姆狀態分析
        /// </summary>
        /// <returns></returns>
        public ApiResultDto CountStatusQuantity()
        {
            var result = new ApiResultDto();
            try
            {
                StatusQuantityDto quantitys = new StatusQuantityDto();
                using (var connection = new SqlConnection(connectionString))
                {
                    string quantity = @"
                                    SELECT
	                                    (SELECT
		                                    COUNT(*)
	                                    FROM RegisterSitter
	                                    WHERE RegisterStatus = 0)AS AwaitToCheck,
	                                    (SELECT
		                                    COUNT(*)
	                                    FROM RegisterSitter
	                                    WHERE RegisterStatus = 1)AS Approved,
	                                    (SELECT
		                                    COUNT(*)
	                                    FROM RegisterSitter
	                                    WHERE RegisterStatus = 2)AS Reject,
	                                    (SELECT
		                                    COUNT(*)
	                                    FROM RegisterSitter
	                                    WHERE RegisterStatus = 3)AS Suspend";
                    quantitys = connection.QueryFirst<StatusQuantityDto>(quantity);
                    result.Data = quantitys;
                    result.Status = StatusCode.Success;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = StatusCode.Failed;
            }
            return result;
        }

        /// <summary>
        /// 訂單數前三保姆
        /// </summary>
        /// <returns></returns>
        public ApiResultDto GetSitterTop()
        {
            var result = new ApiResultDto();
            try
            {
                var sitterTops = new List<SitterTopDto>();
                using (var connection = new SqlConnection(connectionString))
                {
                    string top = @"
                                SELECT TOP 3
	                                o.SitterID AS MemberID,
	                                (SELECT
		                                s.SitterName
	                                FROM RegisterSitter s
	                                WHERE s.MemberID IN (o.SitterID)) AS SitterName,
	                                COUNT(*) AS OrderCount
                                FROM [Order] o
                                WHERE o.SitterID IN (
	                                SELECT s.MemberID
	                                FROM RegisterSitter s)
                                GROUP BY SitterID
                                ORDER BY OrderCount DESC";
                    sitterTops = connection.Query<SitterTopDto>(top).ToList();
                    result.Data = sitterTops;
                    result.Status = StatusCode.Success;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = StatusCode.Failed;
            }
            return result;
        }
    }
}