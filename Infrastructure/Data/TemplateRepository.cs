using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TemplateRepository : ITemplateRepository
    {
        protected readonly PawsDayContext Dbcontext;

        public TemplateRepository(PawsDayContext pawsdaycontext)
        {
            Dbcontext = pawsdaycontext;
        }
        public async Task<bool> UpdateTemplate(LineBotTemplate lineBotTemplate, List<LineBotTemplateDetail> lineBotTemplateDetails)
        {

            var olddetail = Dbcontext.LineBotTemplateDetail.Where(d => d.Template == lineBotTemplate.TemplateId);

            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    Dbcontext.LineBotTemplate.Update(lineBotTemplate);
                    Dbcontext.LineBotTemplateDetail.RemoveRange(olddetail);
                    Dbcontext.LineBotTemplateDetail.AddRange(lineBotTemplateDetails);

                    await Dbcontext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

        }
    }
}
