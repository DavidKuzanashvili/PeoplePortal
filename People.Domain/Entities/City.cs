namespace People.Domain.Entities;

public class City : BaseEntity<Guid>
{
	// For EF core
	private City() {}

	private City(string name)
	{
		EntityId = Guid.NewGuid();
		Name = name;
	}

	public string Name { get; private set; } = null!;

	public static City Create(string name) 
	{
		return new City(name);
	}
}
