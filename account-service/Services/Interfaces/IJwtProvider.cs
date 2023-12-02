public interface IJwtProvider
{
    string GenerateJwtToken(Account account);
}