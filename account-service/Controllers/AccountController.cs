using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/accounts/")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccounts _accounts;

    public AccountController(ILogger<AccountController> logger, IAccounts accounts)
    {
        _logger = logger;
        _accounts = accounts;
    }

    [HttpGet("test")]
    public IActionResult test()
    {
        return Ok("I am");
    }

    [HttpPost("registration")]
    public IActionResult SignUp([FromBody] SignUpRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid request data");
        }

        string name = request.Name;
        string email = request.Email;
        string password = request.Password;
        string confirmPassword = request.ConfirmPassword;

        bool isPasswordsMatch = string.Equals(password, confirmPassword);

        if (
            !string.IsNullOrEmpty(name)
            && !string.IsNullOrEmpty(email)
            && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(confirmPassword)
        )
        {
            _logger.LogInformation(
                $"Входные данные: Name: {name}, Email: {email}, Password: {password}, Confirm Password: {confirmPassword}"
            );

            if (isPasswordsMatch)
            {
                if (_accounts.IsEmailUnique(email))
                {
                    Account account = new Account
                    {
                        Name = name,
                        Email = email,
                        Password = password
                    };
                    _accounts.AddAccount(account);

                    _logger.LogInformation(
                        $"Пользователь с именем '{name}' и почтовым адресом '{email}' зарегистрирован!"
                    );
                    return Ok("Регистрация успешна!");
                }
                else
                {
                    _logger.LogWarning($"Пользователь с email: '{email}' уже существует");
                    return Conflict("Пользователь с таким email уже существует");
                }
            }
            else
            {
                _logger.LogWarning("Пароли не совпадают. Доступ запрещен!");
                return BadRequest("Пароли не совпадают. Доступ запрещен!");
            }
        }

        return BadRequest("Не все обязательные поля заполнены");
    }

    // [HttpPost("login")]
    // public IActionResult Login([FromBody] AccountModel accountModel)
    // {
    //     // Проверка наличия аккаунта и соответствия пароля
    //     Account account = accountManager.GetAccount(accountModel.Username);

    //     if (account != null && account.Password == accountModel.Password)
    //     {
    //         return Ok($"Вход выполнен успешно для пользователя {accountModel.Username}.");
    //     }
    //     else
    //     {
    //         return Unauthorized("Неверное имя пользователя или пароль.");
    //     }
    // }

    // [HttpGet("{userId}")]
    // public IActionResult GetAccount(Guid userId)
    // {
    //     Account account = accountManager.GetAccountByUserId(userId);

    //     if (account != null)
    //     {
    //         return Ok(account);
    //     }
    //     else
    //     {
    //         return NotFound($"Аккаунт с userId {userId} не найден.");
    //     }
    // }
}
