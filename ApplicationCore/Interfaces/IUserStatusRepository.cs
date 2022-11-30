using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserStatusRepository
    {
        void CreateRangeSitterRole(IEnumerable<RegisterSitter> sitters);

        void CreateSitterRole(RegisterSitter sitter);

        void DeleteSitterRole(RegisterSitter sitter);

        void CreateMemberRole(Member member);
        void DeleteMemberRole(Member member);
    }
}
