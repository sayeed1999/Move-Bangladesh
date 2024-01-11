using System.Text;

namespace RideSharing.Common.Entities;

public class Response<T>
{
	public T Data { get; set; }
	public StringBuilder Message { get; set; } = new("Success!");
	public short Status { get; set; } = 200;
}