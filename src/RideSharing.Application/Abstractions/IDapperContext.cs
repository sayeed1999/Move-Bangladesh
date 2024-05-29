using System.Data;

namespace RideSharing.Application.Abstractions
{
	public interface IDapperContext
	{
		IDbConnection CreateConnection();
	}
}
