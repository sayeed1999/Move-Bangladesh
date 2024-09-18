using Microsoft.Extensions.Configuration;
using Npgsql;
using MoveBangladesh.Application.Abstractions;
using System.Data;

namespace MoveBangladesh.Persistence
{
	public class DapperContext : IDapperContext
	{
		private readonly IConfiguration configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration)
		{
			this.configuration = configuration;
			_connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
		}

		public IDbConnection CreateConnection()
			=> new NpgsqlConnection(_connectionString);
	}
}
