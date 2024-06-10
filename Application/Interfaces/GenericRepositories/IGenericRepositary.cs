using Domain.Comman.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.GenericRepositories
{
    public interface IGenericRepositary<T> where T : class,IEntity
    {
        IQueryable<T> Entities { get; }
        Task<List<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity,int id);
        Task DeleteAsync(T entity);
    }
}
