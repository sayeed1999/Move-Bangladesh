using System.Data;

namespace MoveBangladesh.Application.Abstractions
{
	public interface IDapperContext
	{
		IDbConnection CreateConnection();
	}
}
