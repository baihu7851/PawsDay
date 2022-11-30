using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly PawsDayContext Dbcontext;

        public EFRepository(PawsDayContext pawsdaycontext)
        {
            Dbcontext = pawsdaycontext;
        }

        public T Add(T entity)
        {
            Dbcontext.Set<T>().Add(entity);
            Dbcontext.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            Dbcontext.Set<T>().AddRange(entities);
            Dbcontext.SaveChanges();
            return entities;
        }

        public async Task<T> AddAsync(T entity)
        {
            Dbcontext.Set<T>().Add(entity);
            await Dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            Dbcontext.Set<T>().AddRange(entities);
            await Dbcontext.SaveChangesAsync();
            return entities;
        }

        public T Update(T entity)
        {
            Dbcontext.Set<T>().Update(entity);
            Dbcontext.SaveChanges();
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            Dbcontext.Set<T>().UpdateRange(entities);
            Dbcontext.SaveChanges();
            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Dbcontext.Set<T>().Update(entity);
            await Dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            Dbcontext.Set<T>().UpdateRange(entities);
            await Dbcontext.SaveChangesAsync();
            return entities;
        }

        public void Delete(T Entity)
        {
            Dbcontext.Set<T>().Remove(Entity);
            Dbcontext.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> Entities)
        {
            Dbcontext.Set<T>().RemoveRange(Entities);
            Dbcontext.SaveChanges();
        }

        public async Task<T> DeleteAsync(T entity)
        {
            Dbcontext.Set<T>().Remove(entity);
            await Dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities)
        {
            Dbcontext.Set<T>().RemoveRange(entities);
            await Dbcontext.SaveChangesAsync();
            return entities;
        }

        public IQueryable<T> GetAll()
        {
            return Dbcontext.Set<T>();
        }

        public IQueryable<T> GetAllReadOnly()
        {
            return Dbcontext.Set<T>().AsNoTracking();
        }

        public T GetById<Tid>(Tid id)
        {
            return Dbcontext.Set<T>().Find(new object[] { id });
        }

        public async Task<T> GetByIdAsnyc<Tid>(Tid id) 
        {
            return await Dbcontext.Set<T>().FindAsync(new object[] { id });
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return Dbcontext.Set<T>().Any(expression);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await Dbcontext.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await Dbcontext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Dbcontext.Set<T>().Where(expression);
        }
    }
}