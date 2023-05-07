using Microsoft.EntityFrameworkCore;
using RideSharing.Infrastructure;
using RideSharing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class BaseService<T> where T : class
    {
        protected IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _repository = baseRepository;

        }

        /// <summary>
        /// Need to be called after certain operations on db: add update delete, otherwise changes will not be saved..
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        #region basic crud operations

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T> FindByIdAsync(long id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public virtual async Task AddAsync(T item)
        {
            await _repository.AddAsync(item);
        }

        public virtual void UpdateById(long id, T item)
        {
            _repository.UpdateById(id, item);
        }

        public virtual void Update(T item)
        {
            _repository.Update(item);
        }

        public virtual void Delete(T item)
        {
            _repository.Delete(item);
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        #endregion

    }
}
