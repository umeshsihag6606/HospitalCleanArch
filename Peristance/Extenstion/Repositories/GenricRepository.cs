using Application.Interfaces.GenericRepositories;
using Domain.Comman;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Peristance.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.Extenstion.Repositories
{
    public class GenricRepository<T>:IGenericRepositary<T> where T : BaseAuditableEntity
    {
        private readonly ApplicationdbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public GenricRepository(ApplicationdbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
        }
        public IQueryable<T> Entities=>_dbContext.Set<T>();

       

        public async Task<T>AddAsync(T entity)
        {
            var userid = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirstValue("id"));
            entity.CreateDate = DateTime.Now;
            entity.CreateBy = userid;
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public Task DeleteAsync(T entity)
        {
            if(entity is BaseAuditableEntity autotableEntity)
            {
                autotableEntity.IsDeleted = true;
                autotableEntity.UpdateDate = DateTime.Now;
                autotableEntity.UpdateDateBy= Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirstValue("id"));
                _dbContext.Entry(entity).State=EntityState.Modified;
            }
            return Task.CompletedTask;
        }
        public async Task<List<T>> GetAll()
        {
            var data = await _dbContext
                .Set<T>()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return data;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var data = await _dbContext
                 .Set<T>()
                 .Where(x => !x.IsDeleted)
                 .ToListAsync();
            return data;
        }

        public async Task<T>GetById(int id)
        {
            var data = await _dbContext
                .Set<T>()
                .Where(e => !e.IsDeleted && e.Id == id)
                .FirstOrDefaultAsync();
            return data;
        }
        public Task UpdateAsync(T entity, int id)
        {
            var userid = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirstValue("id"));
            T exist = _dbContext.Set<T>().Find(id);
            entity.UpdateDate = DateTime.Now;
            entity.UpdateDateBy = userid;
            _dbContext.Entry(exist).CurrentValues.SetValues(exist);
            return Task.CompletedTask;
        }

     
    }
}
