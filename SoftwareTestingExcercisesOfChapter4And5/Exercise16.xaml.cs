namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise16 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public string? NString { private get; set; }

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

    public Exercise16 ()
    {
        InitializeComponent();

        NotifyInvalidN();
    }

    private void NotifyInvalidN ()
    {
        Notification = "n phải thuộc Z+ và n thuộc [10 ; 60]";
    }

    private void NTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!NIsValid())
        {
            NotifyInvalidN();
        }
        else
        {
            ClearNotification();
        }
    }
    private bool NIsValid ()
    {
        return int.TryParse(NString, out var n) && (10 <= n) && (n <= 60);
    }
    private void ClearNotification ()
    {
        Notification = string.Empty;
    }
}
