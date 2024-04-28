using Microsoft.Extensions.Configuration;

namespace wpf_app.Models;

public abstract class DapperBase
{
    protected string _connectionString;
    public DapperBase(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
}