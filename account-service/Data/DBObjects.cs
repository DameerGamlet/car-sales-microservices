public static class DBObject
{
	private static Lazy<Dictionary<int, Account>> accounts = new(() =>
	{
		var initialAccount = new Account
		{
			Id = int.MaxValue - 1,
			UserId = Guid.NewGuid(),
			Email = "admin@admin.com",
			Name = "Main Admin",
			Password = "PASSWORD123",
			City = "Саратов"
		};

		return new Dictionary<int, Account>
		{
			{ 1, initialAccount }
		};
	});

	public static Dictionary<int, Account> GetAccounts => accounts.Value;

	public static void Initialize(ApplicationDbContext context)
	{
		if (!context.Accounts.Any())
		{
			context.Accounts.AddRange(GetAccounts.Values);
			context.SaveChanges();
		}
	}
}
