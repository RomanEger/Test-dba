using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace wpf_app.Contracts;

public abstract class DapperBase
{
    protected string _connectionString;
    public DapperBase(IConfigurationBuilder configurationBuilder)
    {
        var config = configurationBuilder
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
        _connectionString = config.GetConnectionString("DefaultConnection");
    }
}