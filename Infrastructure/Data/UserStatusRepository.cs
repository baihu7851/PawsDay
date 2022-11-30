using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserStatusRepository : IUserStatusRepository
    {
        protected readonly PawsDayContext Dbcontext;

        public UserStatusRepository(PawsDayContext pawsdaycontext)
        {
            Dbcontext = pawsdaycontext;
        }

       
        //新增保母權限(批次)
        public void CreateRangeSitterRole(IEnumerable<RegisterSitter> sitters)
        {
            var roles = sitters.Select(s => new UserRole { UserId = s.MemberId, RoleType = (int)UserType.Sitter });

            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    Dbcontext.RegisterSitters.UpdateRange(sitters);
                    Dbcontext.UserRoles.AddRange(roles);
                    Dbcontext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        //新增保母權限(個別)
        public void CreateSitterRole(RegisterSitter sitter)
        {
            var role = new UserRole { UserId=sitter.MemberId,RoleType=(int)UserType.Sitter};

            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    Dbcontext.RegisterSitters.Update(sitter);
                    Dbcontext.UserRoles.Add(role);
                    Dbcontext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                
            }
        }
        
        //刪除保姆權限
        public void DeleteSitterRole(RegisterSitter sitter)
        {
            var role = Dbcontext.UserRoles.First(r => r.UserId == sitter.MemberId && r.RoleType == (int)UserType.Sitter);

            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    Dbcontext.RegisterSitters.Update(sitter);
                    Dbcontext.UserRoles.Remove(role);
                    Dbcontext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        //恢復會員權限
        public void CreateMemberRole(Member member)
        {
            var role = new UserRole { UserId = member.MemberId, RoleType = (int)UserType.Member };

            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    Dbcontext.Members.Update(member);
                    Dbcontext.UserRoles.Add(role);
                    Dbcontext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }

            }
        }

        //刪除會員權限
        public void DeleteMemberRole(Member member)
        {
            var role = Dbcontext.UserRoles.First(r => r.UserId == member.MemberId && r.RoleType == (int)UserType.Member);

            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    Dbcontext.Members.Update(member);
                    Dbcontext.UserRoles.Remove(role);
                    Dbcontext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
