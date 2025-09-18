namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise25 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string? FString { private get; set; }
    public string? SString { private get; set; }

    private string? _notification;
    public string? Notification
    {
        get => _notification;
        private set
        {
            if (_notification == value)
            {
                return;
            }

            _notification = value;
            OnPropertyChanged(nameof(Notification));
        }
    }

    private string? _result;
    public string? Result
    {
        get => _result;
        private set
        {
            if (_result == value)
            {
                return;
            }

            _result = value;
            OnPropertyChanged(nameof(Result));
        }
    }

    public Exercise25 ()
    {
        InitializeComponent();
        NotifyInvalidInput();
    }

    private void NotifyInvalidInput () =>
        Notification = "F ≥ 0 và S > 0";

    private void ClearNotification () =>
        Notification = string.Empty;

    private bool InputIsValid (out double f, out double s)
    {
        var okF = double.TryParse(FString, out f) && f >= 0;
        var okS = double.TryParse(SString, out s) && s > 0;
        return okF && okS;
    }

    private void InputTextChanged (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!InputIsValid(out var f, out var s))
        {
            NotifyInvalidInput();
            Result = string.Empty;
        }
        else
        {
            ClearNotification();
            var p = f / s;
            Result = $"Áp suất p = {p}";
        }
    }
}
