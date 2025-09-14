namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise10 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string? _nNotification;
    public string? NNotification
    {
        get => _nNotification;
        private set
        {
            if (_nNotification != value)
            {
                _nNotification = value;
                OnPropertyChanged(nameof(NNotification));
            }
        }
    }

    private string? _xNotification;
    public string? XNotification
    {
        get => _xNotification;
        private set
        {
            if (_xNotification != value)
            {
                _xNotification = value;
                OnPropertyChanged(nameof(XNotification));
            }
        }
    }

    public string? NString { private get; set; }

    public string? XString { private get; set; }

    private string? _sString;
    public string? SString
    {
        get => _sString;
        private set
        {
            if (_sString != value)
            {
                _sString = value;
                OnPropertyChanged(nameof(SString));
            }
        }
    }

    public Exercise10 ()
    {
        InitializeComponent();

        NotifyInvalidN();
        NotifyInvalidX();
    }

    private void NotifyInvalidN ()
    {
        NNotification = "n phải thuộc Z+ và n thuộc [50 ; 100]";
    }

    private void NotifyInvalidX ()
    {

    }
}