namespace SoftwareTestingExercisesOfChapter4And5;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

public partial class Exercise18 : Window, INotifyPropertyChanged
{
    private readonly List<double> _a = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string? MString { private get; set; }
    public string? AString { private get; set; }

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

    public Exercise18 ()
    {
        InitializeComponent();
        NotifyInvalidM();
    }

    private void NotifyInvalidM () =>
        Notification = "m phải thuộc Z+ và m thuộc [3 ; 30]";

    private void MTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!MIsValid())
        {
            NotifyInvalidM();
        }
        else
        {
            ClearNotification();
        }
    }

    private bool MIsValid () =>
        int.TryParse(MString, out var m) && (3 <= m) && (m <= 30);

    private bool AIsValid () =>
        double.TryParse(AString, out _);

    private void ClearNotification () =>
        Notification = string.Empty;

    private void ConfirmMButton_Click (object sender, RoutedEventArgs e)
    {
        if (!MIsValid())
        {
            return;
        }

        confirmMSection.Visibility = Visibility.Collapsed;
        confirmASection.Visibility = Visibility.Visible;
        uTextBox.Text = "()";
    }

    private void ConfirmAButton_Click (object sender, RoutedEventArgs e)
    {
        if (!AIsValid())
        {
            return;
        }

        var a = double.Parse(AString);
        _a.Add(a);

        var m = int.Parse(MString);
        if (_a.Count != m)
        {
            UpdateATextBox();
        }
        else
        {
            ShowAverage();
        }
    }

    private void UpdateATextBox ()
    {
        var s = "{" + string.Join(" + ", _a) + "}";
        uTextBox.Text = s;
    }

    private void ShowAverage ()
    {
        confirmASection.Visibility = Visibility.Collapsed;
        resultSection.Visibility = Visibility.Visible;

        var avg = _a.Average();
        averageTextBlock.Text = $"Trung bình cộng = {avg}";
    }
}
