using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ChristopherBriddock.AspNetCore.Extensions.Options;

public class JwtBearerConfigurationOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private IConfiguration Configuration { get; }

    public virtual string SectionName { get; set; } = "Authentication";
    public JwtBearerConfigurationOptions(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void Configure(string? name, JwtBearerOptions options) => Configure(options);

    public void Configure(JwtBearerOptions options)
    {
        Configuration.GetSection(SectionName).Bind(options);
    }
}