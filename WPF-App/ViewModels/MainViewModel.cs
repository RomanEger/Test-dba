using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using wpf_app.Contracts;
using wpf_app.Models.DTOs;
using wpf_app.Views;

namespace wpf_app.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IRepository _repository;

    private string _phoneNumber = "";

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;

            Task.Run(async () =>
            {
                await SetAbonents();

                if(Abonents is null || !Abonents.Any())
                    MessageBox.Show("Нет абонентов, удовлетворяющих критерию поиска",
                        "Информация",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
            });

            OnPropertyChanged();
        }
    }

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

    public MainViewModel(IRepository repository, int countPerPage = 15)
    {
        _repository = repository;
        CurrentPage = 0;
        CountPerPage = countPerPage;
        Task.WhenAll(SetAbonents(), SetCount());
    }

    private async Task SetAbonents() =>
        Abonents = await _repository.GetAbonents(CurrentPage, CountPerPage, _phoneNumber);

    private async Task SetCount()
    {
        Count = await _repository.GetAbonentsCount();
        UpdateNavigation();
    }

    private void UpdateNavigation()
    {
        CanGoBack = CurrentPage > 0;
        CanGoForward = Count / CountPerPage > CurrentPage;
    }

    public RelayCommand GoForward => new(async _ =>
    {
        if (CanGoForward)
        {
            CurrentPage++;
            UpdateNavigation();
            await SetAbonents();
        }
    });

    public RelayCommand GoBack => new(async _ =>
    {
        if (CanGoBack)
        {
            CurrentPage--;
            UpdateNavigation();
            await SetAbonents();
        }
    });

    public RelayCommand NavToStreetsPage => new(_ => new Streets(_repository).Show());

    public RelayCommand NavToSearchByPhoneNumberPage => new(_ => new SearchByPhoneNumber(this).Show());

    public RelayCommand ResetFilters => new(_ =>
    {
        if (!string.IsNullOrWhiteSpace(PhoneNumber))
            PhoneNumber = string.Empty;
    });

    public RelayCommand GenerateCsv => new(async _ =>
    {
        var sb = new StringBuilder();

        var listForReport = await _repository.GetAbonents(0, int.MaxValue, _phoneNumber);

        sb.AppendLine("ФИО;Улица;Номер дома;Номер телефона (домашний);Номер телефона (рабочий);Номер телефона (мобильный)");
        
        foreach (var item in listForReport)
        {
            sb.AppendLine($"{item.FullName};{item.Street};{item.HouseNumber};{item.HousePhoneNumber};{item.WorkPhoneNumber};{item.PersonalPhoneNumber}");
        }

        var dateTime = DateTime.Now;
        
        var folder = @"C:\Users\User";
        var fileName = $"report_{dateTime.ToString().Replace(':', '_')}.csv";
        OpenFileDialog();

        try
        {
            var path = folder + @"\" + fileName;
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            MessageBox.Show($"Файл сохранен по адресу:\n {path}");
        }
        catch
        {
            MessageBox.Show("Не удалось сохранить файл");
        }
        return;


        void OpenFileDialog()
        {
            var openFolderDialog = new OpenFolderDialog();
            if (openFolderDialog.ShowDialog() == true)
                folder = openFolderDialog.FolderName;
        }
    });
}