namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise23 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string? BMIString { private get; set; }

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

    public Exercise23 ()
    {
        InitializeComponent();

        NotifyInvalidBMI();
    }

    private void NotifyInvalidBMI () =>
        Notification = "BMI phải là số thực trong [17 ; 40]";

    private void UTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!BMIIsValid())
        {
            NotifyInvalidBMI();
        }
        else
        {
            ChangeNotification();
        }
    }

    private bool BMIIsValid () =>
        double.TryParse(BMIString, out var bmi) && (17 <= bmi) && (bmi <= 40);

    private void ChangeNotification ()
    {
        var bmi = double.Parse(BMIString);
        if (bmi < 18.5)
        {
            Notification = "Cân nặng thấp";
        }
        else if (bmi < 25)
        {
            Notification = "Bình thường";
        }
        else
        {
            Notification = "Thừa cân";
        }
    }
}
