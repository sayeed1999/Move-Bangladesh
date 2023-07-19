using Microsoft.AspNetCore.Mvc;

namespace Sayeed.NTier.Generic.Controller
{
    public interface IBaseController<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10);
        Task<T> GetById(int id);
        Task Add([FromBody] T body);
        Task UpdateById(int id, [FromBody] T body);        
        Task DeleteAsync(int id);
    }
}
