public class Account
{
  public int Id { get; set; }
  public Guid UserId { get; set; } = Guid.NewGuid();

  public required string Name { get; set; }

  public required string Email { get; set; }

  public required string Password { get; set; }

  public string? City { get; set; }
}