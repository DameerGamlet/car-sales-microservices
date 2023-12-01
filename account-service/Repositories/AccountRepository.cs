using Microsoft.EntityFrameworkCore;

public class AccountRepository : IAccounts
{
  private readonly ApplicationDbContext context;

  public AccountRepository(ApplicationDbContext _context)
  {
    context = _context;
  }

  public void AddAccount(Account account)
  {
    context.Accounts.Add(account);
    context.SaveChanges();
  }

  public void DeleteAccount(int id)
  {
    var account = context.Accounts.FirstOrDefault(account => account.Id == id);
    if (account != null)
    {
      context.Accounts.Remove(account);
      context.SaveChanges();
    }
  }

  public Account GetAccountById(int id) =>
      context.Accounts.FirstOrDefault(account => account.Id == id) ??
      throw new NotAccountFoundException("Account not found by id:" + id);

  public IEnumerable<Account> GetAllAccounts() => context.Accounts.ToList();

  public bool IsEmailUnique(string email) =>
      !context.Accounts.Any(account => account.Email == email);

  public void UpdateAccount(Account account)
  {
    context.Accounts.Update(account);
    context.SaveChanges();
  }
}