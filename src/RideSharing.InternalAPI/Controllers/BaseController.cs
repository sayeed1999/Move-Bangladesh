﻿using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.Abstractions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideSharing.InternalAPI.Controllers
{
	[Route("api/internal/v1/[controller]")]
	[ApiController]
	public abstract class BaseController<T> : ControllerBase where T : class
	{
		protected readonly IBaseRepository<T> repository;

		public BaseController(IBaseRepository<T> repository) : base()
		{
			this.repository = repository;
		}

		// GET: api/<BaseController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<T>>> Get()
		{
			var res = await this.repository.FindAllAsync();
			return Ok(res);
		}

		// GET api/<BaseController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<T>> Get(Guid id)
		{
			var res = await this.repository.FindByIdAsync(id);
			return Ok(res);
		}

		// POST api/<BaseController>
		[HttpPost]
		public async Task<ActionResult<T>> Post([FromBody] T value)
		{
			var res = await this.repository.AddAsync(value);
			// TODO:- use created at
			return Ok(res);
		}

		// TODO: check PUT vs PATCH; which is more close!
		// PUT api/<BaseController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<T>> Put(Guid id, [FromBody] T value)
		{
			var res = await this.repository.UpdateByIdAsync(id, value);
			return Ok(res);
		}

		// DELETE api/<BaseController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<T>> Delete(Guid id)
		{
			var res = await this.repository.DeleteByIdAsync(id);
			return Ok(res);
		}
	}
}
