namespace RideSharing.Domain.Entities;

public abstract class Human
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Location { get; set; }
}
