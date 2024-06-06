using Application.Interfaces.GenericRepositories;
using Domain.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UnitofworkRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositary<T> Repositary<T>() where T : BaseAuditableEntity;
        Task<int> Save(CancellationToken cancellationToken);
        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cachekeys);
        Task Rollback();
    }
}
