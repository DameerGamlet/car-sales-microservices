
using Microsoft.AspNetCore.Mvc;

public class AccountService : IAccounts
{
    private readonly ILogger<AccountService> log;
    private readonly AccountRepository repository;
    private readonly IJwtProvider jwt;

    public AccountService(ILogger<AccountService> log, AccountRepository repository, IJwtProvider jwt)
    {
        this.log = log;
        this.repository = repository;
        this.jwt = jwt;
    }

    public IActionResult SignUp(SignUpRequest request)
    {
        if (request == null)
        {
            return new BadRequestObjectResult("Invalid request data");
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
            log.LogInformation(
                $"Input data: Name: {name}, Email: {email}, Password: {password}, Confirm Password: {confirmPassword}"
            );

            if (isPasswordsMatch)
            {
                if (repository.IsEmailUnique(email))
                {
                    Account account = new Account
                    {
                        Name = name,
                        Email = email,
                        Password = password
                    };
                    repository.AddAccount(account);

                    log.LogInformation(
                        $"User with name '{name}' and email '{email}' registered successfully!"
                    );
                    return new OkObjectResult("Registration successful!");
                }
                else
                {
                    log.LogWarning($"User with email: '{email}' already exists");
                    return new ConflictObjectResult("User with this email already exists");
                }
            }
            else
            {
                log.LogWarning("Passwords do not match. Access denied!");
                return new BadRequestObjectResult("Passwords do not match. Access denied!");
            }
        }

        return new BadRequestObjectResult("Not all mandatory fields are filled");
    }

    public void DeleteAccount(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateAccount(Account account)
    {
        throw new NotImplementedException();
    }


    public Account GetAccountByUserId(Guid userId)
    {
        return repository.GetAccountByUserId(userId);
    }

    public IActionResult Login(LoginRequest request)
    {
        string email = request.Email;
        string password = request.Password;

        Account account = repository.GetAccountByEmail(email);

        if (account != null && account.Password == password)
        {
            var token = jwt.GenerateJwtToken(account);

            return new OkObjectResult(new { Token = token, Message = $"Login successful for user {email}." });

        }
        else
        {
            return new UnauthorizedObjectResult("Incorrect username or password.");
        }
    }
}