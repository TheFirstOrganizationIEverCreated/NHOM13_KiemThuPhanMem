namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise15 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string? KmString { private get; set; }

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

    public Exercise15 ()
    {
        InitializeComponent();
        NotifyInvalidKm();
    }

    private void NotifyInvalidKm () =>
        Notification = "Số km phải là số thực dương (km > 0)";

    private void ClearNotification () =>
        Notification = string.Empty;

    private bool KmIsValid (out double km) =>
        double.TryParse(KmString, out km) && km > 0;

    private void KmTextBox_TextChanged (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!KmIsValid(out var km))
        {
            NotifyInvalidKm();
            Result = string.Empty;
        }
        else
        {
            ClearNotification();
            var fare = CalculateFare(km);
            Result = $"Số tiền phải trả: {fare:N0} đồng";
        }
    }

    private double CalculateFare (double km)
    {
        if (km <= 1)
        {
            return 10000;
        }
        else if (km <= 30)
        {
            return 10000 + (km - 1) * 13000;
        }
        else
        {
            return 10000 + 29 * 13000 + (km - 30) * 11000;
        }
    }
}
