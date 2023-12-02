using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/accounts/")]
public class AccountController : ControllerBase
{
    private readonly IAccounts accountService;

    public AccountController(IAccounts accountService)
    {
        this.accountService = accountService;
    }

    [HttpGet("test")]
    [Authorize]
    public IActionResult Test()
    {
        return Ok("I am test user");
    }

    [HttpPost("register")]
    public IActionResult SignUp([FromBody] SignUpRequest request)
    {
        IActionResult result = accountService.SignUp(request);
        return result;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        return accountService.Login(request); ;
    }

    [HttpGet("{userId}")]
    public IActionResult GetAccount(Guid userId)
    {
        Account account = accountService.GetAccountByUserId(userId);

        if (account != null)
        {
            return Ok(account);
        }
        else
        {
            return NotFound($"Account with userId {userId} not found.");
        }
    }

    // удаление и обновление аккаунта будет только с JWT токена
}
