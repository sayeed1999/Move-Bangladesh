using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace RideSharing.Infrastructure
{
	public class DapperContext
	{
		private readonly IConfiguration configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration)
		{
			this.configuration = configuration;
			_connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
		}

		public IDbConnection CreateConnection()
			=> new SqlConnection(_connectionString);
	}
}
