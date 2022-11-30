using PawsDay.Models.Account;
using PawsDay.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PawsDay.Interfaces.Account
{
    public interface IRegisterViewModelService
    {        
        List<CountyDto> GetCounty();
        bool ConfirmData(int accountInfoId, string intputExpirationTime);
    }
}
