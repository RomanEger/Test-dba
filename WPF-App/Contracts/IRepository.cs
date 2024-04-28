using wpf_app.Models;
using wpf_app.Models.DTOs;

namespace wpf_app.Contracts;

public interface IRepository
{
    public Task<IEnumerable<AbonentDto>> GetPart(int pageNumber, int count=10);

    public Task<int> GetCount();
}