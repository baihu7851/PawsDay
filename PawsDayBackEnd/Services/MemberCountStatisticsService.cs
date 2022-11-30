using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Member;
using PawsDayBackEnd.Interfaces;
using SendGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace PawsDayBackEnd.Services
{
    public class MemberCountStatisticsService : IMemberCountStatisticsService
    {
        private readonly IRepository<Member> _memberRepo;
        private DateTime _tempDateTime;
        private readonly string _connectionString;

        public MemberCountStatisticsService(IRepository<Member> memberRepo, IConfiguration config)
        {
            _memberRepo = memberRepo;
            _tempDateTime = new DateTime();
            _connectionString = config.GetConnectionString("PawsDayConnection");
        }

        public async Task<ApiResultDto> MemberCountStatisticsAsync(MemberAnalysisDto response)
        {
            var stringYearAndMonth = response.stringStartDate ?? DateTime.UtcNow.AddMonths(-7).ToString("yyyy-MM");
            var yearAndMonth = stringYearAndMonth.Split("-", 2).Select(int.Parse).ToArray();
            // 解析開始統計的時間(年、月)
            var startDate = new DateTime(yearAndMonth[0], yearAndMonth[1], 1);

            // 存一個暫存的時間
            _tempDateTime = startDate;

            // 現在的時間
            var nowDate = DateTime.UtcNow.AddMonths(1);

            // 做一個從指定時間到現在的月累積時間
            List<DateTime> dateList = new List<DateTime>();
            while (DateTime.Compare(nowDate, _tempDateTime) > 0)
            {
                dateList.Add(_tempDateTime);
                _tempDateTime = _tempDateTime.AddMonths(1);
            }

            // 所有會員數量
            // LINQ
            //var totalMembercount = await _memberRepo.GetAllReadOnly().CountAsync();

            // Dapper
            //string totalMembercountQueryString = "SELECT Count(*) FROM Member";
            //using (SqlConnection conn = new SqlConnection(_connectionString))
            //{
            //    // 可以自己手動開關連線，也可以讓他自動
            //    //conn.Open();
            //    //var result=await conn.QueryAsync<Member>(sqlQuery,parameters);
            //    //conn.Close();
            //};

            await using var conn = new SqlConnection(_connectionString);
            var totalMembercountQueryString = @"SELECT Count(*) FROM Member";
            var totalMembercount = await conn.QuerySingleAsync<int>(totalMembercountQueryString);

            // 指定時間到現在的所有的會員資料
            // LINQ
            //var memberList = await _memberRepo.GetAllReadOnly().Where(x => DateTime.Compare(startDate, x.CreateTime) < 0).ToListAsync();
            // Dapper
            var parameters = new { startDate = startDate.ToString("yyyyMMdd") };
            var memberListQueryString = @"SELECT CreateTime
                                                FROM Member
                                                WHERE CreateTime>=@startDate";
            var memberList = await conn.QueryAsync<MemberCreateTime>(memberListQueryString, parameters);




            // 指定時間到現在的會員月累積量 (還缺非指定時間的會員量)
            var accumulateMemberAmount = dateList.Select(d => memberList.Count(m => DateTime.Compare(d, m.CreateTime) > 0)).ToList();

            // 所有會員數量 減 最大累積量，得到非指定時間的會員量
            var buttomAmount = totalMembercount - accumulateMemberAmount.Max();

            // 實際上的會員累積量  (忽略第一筆數據，資料從XX月開始抓取，沒有小於XX月的會員)
            var realAccumulateMemberAmount = accumulateMemberAmount.Skip(1).Select(x => x + buttomAmount).ToList();



            List<int> differenceAccumulateMemberAmount = new List<int>();

            //for (var i = accumulateMemberAmount.Count - 2; i >= 0; i--)
            //{
            //    var differenceAmount = accumulateMemberAmount[i + 1] - accumulateMemberAmount[i];

            //    differenceAccumulateMemberAmount.Add(differenceAmount);
            //}

            // 從會員月累積量計算每個月的新增會員
            for (var i = 0; i < accumulateMemberAmount.Count - 1; i++)
            {
                var differenceAmount = accumulateMemberAmount[i + 1] - accumulateMemberAmount[i];

                differenceAccumulateMemberAmount.Add(differenceAmount);
            }

            var memberCountStatisticsDto = new MemberCountStatisticsDto
            {
                TotalMemberCount = totalMembercount,
                AccumulateMemberAmount = realAccumulateMemberAmount,
                NewMemberPerMonth = differenceAccumulateMemberAmount,
                MonthList = dateList.SkipLast(1).Select(x => x.ToString("yyyy-MM")).ToList()
            };

            return new ApiResultDto(memberCountStatisticsDto);

        }


        private class MemberCreateTime
        {
            public DateTime CreateTime { get; set; }
        }
    }
}
