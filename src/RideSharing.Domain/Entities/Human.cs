using RideSharing.Common.ValueObjects;

namespace RideSharing.Domain.Entities;

public abstract class Human
{
	public Human()
	{

	}

	public Human(long id, long userId, string name, Email email, string phone, string location)
	{
		Id = id;
		UserId = userId;
		Name = name;
		Email = email.Value;
		Phone = phone;
		Location = location;
	}

	public long Id { get; set; }
	public long UserId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Location { get; set; }
}
