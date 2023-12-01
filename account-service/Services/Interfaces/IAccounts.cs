public interface IAccounts
{
    bool IsEmailUnique(string email);
    IEnumerable<Account> GetAllAccounts();
    Account GetAccountById(int id);
    void AddAccount(Account account);
    void UpdateAccount(Account account);
    void DeleteAccount(int id);
}
