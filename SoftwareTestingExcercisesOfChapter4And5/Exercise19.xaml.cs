namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

public partial class Exercise19 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string? PString { private get; set; }

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

    public Exercise19 ()
    {
        InitializeComponent();
        NotifyInvalidP();
    }

    private void NotifyInvalidP () =>
        Notification = "p chỉ nhận giá trị 0 hoặc 1";

    private void ClearNotification () =>
        Notification = string.Empty;

    private bool PIsValid (out int p) =>
        int.TryParse(PString, out p) && (p == 0 || p == 1);

    private void PTextBox_TextChanged (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!PIsValid(out var p))
        {
            NotifyInvalidP();
            Result = string.Empty;
            ResultTextBlock.Foreground = Brushes.Black;
            ResultTextBlock.Background = Brushes.Transparent;
        }
        else
        {
            ClearNotification();

            if (p == 0)
            {
                Result = "Màu đen";
                ResultTextBlock.Foreground = Brushes.White;
                ResultTextBlock.Background = Brushes.Black;
            }
            else
            {
                Result = "Màu trắng";
                ResultTextBlock.Foreground = Brushes.Black;
                ResultTextBlock.Background = Brushes.White;
            }
        }
    }
}
