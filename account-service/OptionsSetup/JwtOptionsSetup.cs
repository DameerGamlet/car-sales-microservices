using Microsoft.Extensions.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration configureOptions;
    private const string SectionName = "Jwt";

    public JwtOptionsSetup(IConfiguration configureOptions)
    {
        this.configureOptions = configureOptions;
    }

    public void Configure(JwtOptions options)
    {
        configureOptions.GetSection(SectionName).Bind(options);
    }
}