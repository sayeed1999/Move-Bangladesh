using Microsoft.AspNetCore.Mvc;
using RideSharing.Service;
using Sayeed.NTier.Generic.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideSharing.API
{
    // abstract class won't create endpoints in swagger
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        private IBaseService<T> baseService;
        public BaseController(IBaseService<T> baseService)
        {
            this.baseService = baseService;
        }

        // GET: api/<BaseController>
        [HttpGet]
        public async Task<IEnumerable<T>> GetAsync()
        {
            return await baseService.GetAllAsync();
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
            await baseService.SaveChangesAsync();
        }

        // PUT api/<BaseController>/5
        [HttpPut("{id}")]
        public async Task UpdateById(int id, [FromBody] T body)
        {
            baseService.UpdateById(id, body);
            await baseService.SaveChangesAsync();
        }

        // DELETE api/<BaseController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await baseService.DeleteByIdAsync(id);
            await baseService.SaveChangesAsync();
        }
    }
}
