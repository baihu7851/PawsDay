using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Member;
using PawsDayBackEnd.Interfaces;
using SendGrid;
using System.Threading.Tasks;

namespace PawsDayBackEnd.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberAnalyzeApiController : BaseApiController
    {

        private readonly IMemberCountStatisticsService _memberCountStatisticsService;

        public MemberAnalyzeApiController(IMemberCountStatisticsService memberCountStatisticsService)
        {
            _memberCountStatisticsService = memberCountStatisticsService;
        }

        [HttpPost]
        public async Task<ApiResultDto> ReadMemberCountStatistics(MemberAnalysisDto input)
        {            
            var response =await _memberCountStatisticsService.MemberCountStatisticsAsync(input);
            return response;
        }

    }
}
