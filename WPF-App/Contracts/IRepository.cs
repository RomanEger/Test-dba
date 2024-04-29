using wpf_app.Models;
using wpf_app.Models.DTOs;

namespace wpf_app.Contracts;

public interface IRepository
{
    public Task<IEnumerable<AbonentDto>> GetAbonents(int pageNumber, int count = 15);

    public Task<int> GetAbonentsCount();
    
    public Task<IEnumerable<StreetDto>> GetStreets(int pageNumber, int count = 15, string search="");

    public Task<int> GetStreetsCount();
}
