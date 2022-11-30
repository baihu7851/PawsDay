using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDay.Interfaces.Account;
using System.Linq;

namespace PawsDay.Services.SitterCenter
{
    public class SitterCenterAptitudeService
    {
        private readonly IRepository<Aptitude> _aptitude;
        private readonly IAccountManager _accountManager;

        public SitterCenterAptitudeService(IRepository<Aptitude> aptitude, IAccountManager accountManager)
        {
            _aptitude = aptitude;
            _accountManager = accountManager;
        }

        public int GetMemberId()
        {
            int memberId = _accountManager.GetLoginMemberId();
            return memberId;
        }

        public int[] GetAptitudePoint(int memberId)
        {
            Aptitude aptitude = _aptitude.GetAllReadOnly().First(m => m.MemberId == memberId);
            int[] result = { aptitude.AptitudeExtrovert, aptitude.AptitudeIntrovert, aptitude.AptitudeThinking, aptitude.AptitudeFeeling };
            return result;
        }
    }
}