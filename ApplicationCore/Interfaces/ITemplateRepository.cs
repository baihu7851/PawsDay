using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITemplateRepository
    {
        Task<bool> UpdateTemplate(LineBotTemplate lineBotTemplate, List<LineBotTemplateDetail> lineBotTemplateDetails);
    }
}
