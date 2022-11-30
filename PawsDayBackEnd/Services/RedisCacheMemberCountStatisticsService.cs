using Microsoft.Extensions.Caching.Distributed;

using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Member;
using PawsDayBackEnd.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PawsDayBackEnd.Services
{
    public class RedisCacheMemberCountStatisticsService : IMemberCountStatisticsService
    {
        private readonly IDistributedCache _cache;
        private readonly MemberCountStatisticsService _memberCountStatisticsService;
        //private static readonly string _memberCountStatisticsAsyncKey = "memberCountStatistics";
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public RedisCacheMemberCountStatisticsService(IDistributedCache cache, MemberCountStatisticsService memberCountStatisticsService)
        {
            _cache = cache;
            _memberCountStatisticsService = memberCountStatisticsService;
        }

        public async Task<ApiResultDto> MemberCountStatisticsAsync(MemberAnalysisDto response)
        {
            var cacheKey = $"statistics-{response.stringStartDate}";
            var cacheMemberCountStatistics = ByteArrayToObj<MemberCountStatisticsDto>(await _cache.GetAsync(cacheKey));



            if (cacheMemberCountStatistics is null)
            {
                var realMemberCountStatistics = await _memberCountStatisticsService.MemberCountStatisticsAsync(response);
                var toByte = (MemberCountStatisticsDto)realMemberCountStatistics.Data;
                var byteArrResult = ObjectToByteArray(toByte);
                await _cache.SetAsync(cacheKey, byteArrResult, new DistributedCacheEntryOptions
                {
                    // 失效時間
                    // 滑動到期
                    SlidingExpiration = _defaultCacheDuration,
                    // 絕對失效
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)

                });
                return realMemberCountStatistics;
            }
            return new ApiResultDto { Data= cacheMemberCountStatistics };
        }

        private byte[] ObjectToByteArray(object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        private T ByteArrayToObj<T>(byte[] byteArr) where T : class
        {
            return byteArr is null ? null : JsonSerializer.Deserialize<T>(byteArr);
        }


    }
}
