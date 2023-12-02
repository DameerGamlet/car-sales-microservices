using Microsoft.AspNetCore.Mvc;

public interface IAccounts
{
    IActionResult SignUp(SignUpRequest request);
    IActionResult Login(LoginRequest request);

    Account GetAccountByUserId(Guid userId);
    void UpdateAccount(Account account);
    void DeleteAccount(int id);
}
