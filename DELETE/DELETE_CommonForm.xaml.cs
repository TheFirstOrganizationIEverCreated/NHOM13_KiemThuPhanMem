namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class DELETE_CommonForm : Window, INotifyPropertyChanged
{
    private string? _notification;
    public string? Notification
    {
        get => _notification;
        private set
        {
            _notification = value;
            OnPropertyChanged(nameof(Notification));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public DELETE_CommonForm()
    {
        InitializeComponent();

        ResetNotification();
    }
    private void ResetNotification()
    {
        Notification = string.Empty;
    }

    private void InputTextBoxChangedText(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!InputIsValid())
        {
            ResetOutput();
            return;
        }

        var inputValue = .Parse(InputString);

        OutputString = GetOutput(inputValue);
    }
    private bool InputIsValid()
    {
        return .TryParse(InputString, out var inputValue) && () && ();
    }
    private string GetOutput(double n)
    {
        double numerator = 0;
        for (uint integer = 1; integer <= n; integer++)
        {
            numerator += integer;
        }

        var outputValue = numerator / n;

        return .ToString();
    }
}