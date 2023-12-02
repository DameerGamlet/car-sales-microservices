using Microsoft.EntityFrameworkCore;

public class AccountRepository
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

  public Account GetAccountByEmail(string email) =>
  context.Accounts.FirstOrDefault(a => a.Email == email) ??
        throw new NotAccountFoundException("Account not found by email:" + email);

  public Account GetAccountById(int id) =>
      context.Accounts.FirstOrDefault(account => account.Id == id) ??
      throw new NotAccountFoundException("Account not found by id:" + id);

  public Account GetAccountByUserId(Guid userId) =>
      context.Accounts.FirstOrDefault(account => account.UserId == userId) ??
      throw new NotAccountFoundException("Account not found by user id:" + userId);

  public IEnumerable<Account> GetAllAccounts() => context.Accounts.ToList();

  public bool IsEmailUnique(string email) =>
      !context.Accounts.Any(account => account.Email == email);

  public void UpdateAccount(Account account)
  {
    context.Accounts.Update(account);
    context.SaveChanges();
  }
}