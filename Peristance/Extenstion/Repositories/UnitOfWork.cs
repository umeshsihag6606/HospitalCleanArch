using Application.Interfaces.GenericRepositories;
using Application.Interfaces.UnitofworkRepositories;
using Domain.Comman;
using Microsoft.AspNetCore.Http;
using Peristance.DataContexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.Extenstion.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationdbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Hashtable _repositary;
        private bool _disposed;

        public UnitOfWork(ApplicationdbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if(disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public IGenericRepositary<T> Repositary<T>() where T : BaseAuditableEntity
        {
           if( _repositary == null ) 
                _repositary = new Hashtable();
           var type=typeof(T).Name;
            if( !_repositary.ContainsKey(type))
            {
                var repositaryType = typeof(GenricRepository<>);
                var repositaryInstance=Activator.CreateInstance(repositaryType.MakeGenericType(typeof(T)),_dbContext,_httpContextAccessor);
                _repositary.Add(type, repositaryInstance);
            }
            return (IGenericRepositary<T>)_repositary[type];
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cachekeys)
        {
            throw new NotImplementedException();
        }
    }
}
