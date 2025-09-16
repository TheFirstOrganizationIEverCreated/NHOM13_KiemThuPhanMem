namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise21 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string? UString { private get; set; }

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

    public Exercise21 ()
    {
        InitializeComponent();
        NotifyInvalidU();
    }

    private void NotifyInvalidU () =>
        Notification = "U phải là số thực trong [0 ; 5]";

    private void UTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!UIsValid())
        {
            NotifyInvalidU();
        }
        else
        {
            ChangeNotification();
        }
    }

    private bool UIsValid () =>
        double.TryParse(UString, out var u) && (0 <= u) && (u <= 5);

    private void ChangeNotification ()
    {
        var u = double.Parse(UString);
        Notification = u <= 3.5 ? "Điện áp thấp" : "Điện áp cao";
    }
}
