using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Member;
using System;
using System.Threading.Tasks;

namespace PawsDayBackEnd.Interfaces
{
    public interface IMemberCountStatisticsService
    {
        Task<ApiResultDto> MemberCountStatisticsAsync(MemberAnalysisDto response);


    }
}
