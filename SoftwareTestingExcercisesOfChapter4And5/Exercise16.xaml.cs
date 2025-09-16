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

    private readonly List<int> _numbers = new();

    public string? AString { private get; set; }

    private void ClickedConfirmNButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (!NIsValid())
        {
            return;
        }

        confirmNSection.Visibility = Visibility.Collapsed;
        ClickedConfirmNButton.Visibility = Visibility.Collapsed;
        confirmUSection.Visibility = Visibility.Visible;
        uTextBox.Text = "{}";
    }

    // --- Kiểm tra A ---
    private bool AIsValid ()
    {
        return int.TryParse(AString, out _);
    }

    private void TextBoxAChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        const string aIsInvalidNotification = "a phải thuộc Z";
        if (!AIsValid())
        {
            aNotificationTextBlock.Text = aIsInvalidNotification;
        }
        else
        {
            aNotificationTextBlock.Text = string.Empty;
        }
    }

    private void ClickedConfirmAButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (!AIsValid())
        {
            return;
        }

        var a = int.Parse(AString);
        _numbers.Add(a);

        var n = int.Parse(NString);
        if (_numbers.Count != n)
        {
            UpdateUTextBox();
        }
        else
        {
            ShowMaxElementAndPosition();
        }
    }

    private void UpdateUTextBox ()
    {
        uTextBox.Text = "{" + string.Join(" ; ", _numbers) + "}";
    }

    private void ShowMaxElementAndPosition ()
    {
        confirmUSection.Visibility = Visibility.Collapsed;

        var max = _numbers.Max();
        var index = _numbers.IndexOf(max) + 1; // vị trí tính từ 1

        maxElementTextBlock.Text = $"Phần tử lớn nhất = {max}";
        positionTextBlock.Text = $"Vị trí trong dãy = {index}";

        resultSection.Visibility = Visibility.Visible;
    }
}