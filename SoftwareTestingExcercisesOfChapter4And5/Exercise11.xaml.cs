namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise11 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string? _notification;
    public string? Notification
    {
        private get => _notification;
        set
        {
            if (_notification == value)
            {
                return;
            }

            _notification = value;
            OnPropertyChanged(nameof(Notification));
        }
    }

    private string? _aCoordinateString;
    public string? ACoordinateString
    {
        private get => _aCoordinateString;
        set
        {
            if (_aCoordinateString == value)
            {
                return;
            }

            _aCoordinateString = value;
            OnPropertyChanged(nameof(ACoordinateString));
        }
    }

    private string? _bCoordinateString;
    public string? BCoordinateString
    {
        private get => _bCoordinateString;
        set
        {
            if (_bCoordinateString == value)
            {
                return;
            }

            _bCoordinateString = value;
            OnPropertyChanged(nameof(BCoordinateString));
        }
    }

    private string? _cCoordinateString;
    public string? CCoordinateString
    {
        private get => _cCoordinateString;
        set
        {
            if (_cCoordinateString == value)
            {
                return;
            }

            _cCoordinateString = value;
            OnPropertyChanged(nameof(CCoordinateString));
        }
    }

    public Exercise11 ()
    {
        InitializeComponent();
    }
}
