using Microsoft.AspNetCore.Mvc;
using Sayeed.NTier.Generic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sayeed.NTier.Generic.Controller
{
    public interface IBaseController<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetById(int id);
        Task Add([FromBody] T body);
        Task UpdateById(int id, [FromBody] T body);        
        Task DeleteAsync(int id);
    }
}
