using Microsoft.AspNetCore.Mvc;
using Sayeed.NTier.Generic.Logic;

namespace Sayeed.NTier.Generic.Controller
{
    // abstract class won't create endpoints in swagger
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase, IBaseController<T> where T : class
    {
        private IBaseService<T> baseService;

        public BaseController(IBaseService<T> baseService)
        {
            this.baseService = baseService;
        }

        // GET: api/<BaseController>
        [HttpGet]
        public async Task<IEnumerable<T>> GetAllAsync([FromQuery]int page = 1, [FromQuery] int pageSize = 10)
        {
            return await baseService.GetAllAsync(page, pageSize);
        }

        // GET api/<BaseController>/5
        [HttpGet("{id}")]
        public async Task<T> GetById(int id)
        {
            return await baseService.FindByIdAsync(id);
        }

        // POST api/<BaseController>
        [HttpPost]
        public async Task Add([FromBody] T body)
        {
            await baseService.AddAsync(body);
        }

        // PUT api/<BaseController>/5
        [HttpPut("{id}")]
        public async Task UpdateById(int id, [FromBody] T body)
        {
            await baseService.UpdateById(id, body);
        }

        // DELETE api/<BaseController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await baseService.DeleteByIdAsync(id);
        }
    }
}
