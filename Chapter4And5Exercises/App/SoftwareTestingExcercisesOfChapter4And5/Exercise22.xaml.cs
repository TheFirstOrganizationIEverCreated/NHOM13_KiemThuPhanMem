namespace SoftwareTestingExercisesOfChapter4And5;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

public partial class Exercise22 : Window, INotifyPropertyChanged
{
    private readonly List<double> _a = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string? TString { private get; set; }
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

    public Exercise22 ()
    {
        InitializeComponent();
        NotifyInvalidM();
    }

    private void NotifyInvalidM () =>
        Notification = "t phải thuộc Z+ và t thuộc [5 ; 20]";

    private void TTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!TIsValid())
        {
            NotifyInvalidM();
        }
        else
        {
            ClearNotification();
        }
    }

    private bool TIsValid () =>
        int.TryParse(TString, out var t) && (5 <= t) && (t <= 20);

    private bool AIsValid () =>
        double.TryParse(AString, out _);

    private void ClearNotification () =>
        Notification = string.Empty;

    private void ConfirmTButton_Click (object sender, RoutedEventArgs e)
    {
        if (!TIsValid())
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

        var m = int.Parse(TString);
        if (_a.Count != m)
        {
            UpdateATextBox();
        }
        else
        {
            ShowB();
        }
    }

    private void UpdateATextBox ()
    {
        var s = "{" + "√" + string.Join(" + √", _a) + "}";
        uTextBox.Text = s;
    }

    private void ShowB ()
    {
        resultSection.Visibility = Visibility.Visible;

        var sum = _a.Sum(x => Math.Sqrt(x));
        averageTextBlock.Text = $" S = {sum}";
    }
}
