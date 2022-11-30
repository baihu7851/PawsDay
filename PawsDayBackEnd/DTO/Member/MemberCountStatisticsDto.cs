using System.Collections.Generic;

namespace PawsDayBackEnd.DTO.Member
{
    public class MemberCountStatisticsDto
    {
        public int TotalMemberCount { get; set; }

        public List<int> AccumulateMemberAmount { get; set; }
        public List<int> NewMemberPerMonth { get; set; }
        public List<string> MonthList { get;  set; }

    }
}
