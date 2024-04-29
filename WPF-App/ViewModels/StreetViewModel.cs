using wpf_app.Contracts;
using wpf_app.Models.DTOs;

namespace wpf_app.ViewModels;

public class StreetViewModel : BaseViewModel
{
    private readonly IRepository _repository;
    
    private int Count { get; set; }
    
    private int CurrentPage { get; set; }

    private bool _canGoForward;

    public bool CanGoForward
    {
        get => _canGoForward;
        private set
        {
            _canGoForward = value;
            OnPropertyChanged();
        }
    }

    private bool _canGoBack;
    public bool CanGoBack 
    { 
        get => _canGoBack;
        private set
        {
            _canGoBack = value;
            OnPropertyChanged();
        }
    }
    
    private int CountPerPage { get; set; }
    
    private IEnumerable<StreetDto> _streets = new List<StreetDto>();

    public IEnumerable<StreetDto> Streets
    {
        get => _streets;
        set
        {
            _streets = value;
            OnPropertyChanged();
        }
    }

    private string _textSearch = string.Empty;

    public string TextSearch
    {
        get => _textSearch;
        set
        {
            _textSearch = value;
            OnPropertyChanged();
            Task.Run(async () => SetStreets());
        }
    }
    
    public StreetViewModel(IRepository repository, int countPerPage = 15)
    {
        _repository = repository;
        CurrentPage = 0;
        CountPerPage = countPerPage;
        
        Task.WhenAll(SetStreets(), SetCount());
        UpdateNavigation();
    }

    private async Task SetStreets() => Streets = await _repository.GetStreets(0, CountPerPage, _textSearch);
    
    private async Task SetCount() => Count = await _repository.GetStreetsCount();
    
    private void UpdateNavigation()
    {
        CanGoBack = CurrentPage > 0;
        CanGoForward = Count/CountPerPage > CurrentPage;
    }
    
    public RelayCommand GoForward => new (async _ =>
    {
        if (CanGoForward)
        {
            CurrentPage++;
            UpdateNavigation();
            await SetStreets();
        }
    });
    
    public RelayCommand GoBack => new (async _ =>
    {
        if (CanGoBack)
        {
            CurrentPage--;
            UpdateNavigation();
            await SetStreets();
        }
    });
}