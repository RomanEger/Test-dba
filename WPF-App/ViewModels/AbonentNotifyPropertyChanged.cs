using System.Collections.ObjectModel;
using wpf_app.Contracts;
using wpf_app.Models.DTOs;

namespace wpf_app.ViewModels;

public class AbonentNotifyPropertyChanged : BaseNotifyPropertyChanged
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

    private IEnumerable<AbonentDto> _abonents = new List<AbonentDto>();
    
    public IEnumerable<AbonentDto> Abonents
    {
        get => _abonents;
        set
        {
            _abonents = value;
            OnPropertyChanged();
        }
    }
    
    public AbonentNotifyPropertyChanged(IRepository repository, int countPerPage = 15)
    {
        _repository = repository;
        CurrentPage = 0;
        CountPerPage = countPerPage;
        Task.WhenAll(SetAbonents(), SetCount());
    }

    private async Task SetAbonents() => Abonents = await _repository.GetPart(CurrentPage, CountPerPage);

    private async Task SetCount()
    {
        Count = await _repository.GetCount();
        UpdateNavigation();
    }

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
            await SetAbonents();
        }
    });
    
    public RelayCommand GoBack => new (async _ =>
    {
        if (CanGoBack)
        {
            CurrentPage--;
            UpdateNavigation();
            await SetAbonents();
        }
    });

}