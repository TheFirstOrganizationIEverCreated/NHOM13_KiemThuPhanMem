namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise8 : Window, INotifyPropertyChanged
{
    private string? _nString;
    public string? NString
    {
        private get => _nString;
        set
        {
            if (value != _nString)
            {
                _nString = value;
                OnPropertyChanged(nameof(NString));
            }
        }
    }

    private readonly List<int> _u;

    public string? AString { private get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Exercise8 ()
    {
        InitializeComponent();

        NotifyNIsInvalid();
        _u = new List<int>();
        aNotificationTextBlock.Text = "a phải thuộc Z";
    }

    private void NTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!NIsValid())
        {
            NotifyNIsInvalid();
        }
        else
        {
            ClearNNotification();
        }
    }
    private bool NIsValid ()
    {
        return int.TryParse(NString, out var n) && (5 <= n) && (n <= 20);
    }
    private void NotifyNIsInvalid ()
    {
        const string nIsInvalidNotification = "n phải thuộc Z+ và n thuộc [5 ; 20]";
        if (nNotificationTextBlock.Text != nIsInvalidNotification)
        {
            UpdateNNotification(nIsInvalidNotification);
        }
    }
    private void ClearNNotification ()
    {
        UpdateNNotification(string.Empty);
    }

    private void UpdateNNotification (string notification)
    {
        nNotificationTextBlock.Text = notification;
    }

    private void ClickedConfirmNButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (!NIsValid())
        {
            return;
        }

        ConfirmU();
    }
    private void ConfirmU ()
    {
        confirmNSection.Visibility = Visibility.Collapsed;

        confirmUSection.Visibility = Visibility.Visible;
        uTextBox.Text = "{}";
    }

    private void TextBoxAChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        const string aIsInvalidNotification = "a phải thuộc Z";

        if (!AIsValid() && (aNotificationTextBlock.Text != aIsInvalidNotification))
        {
            aNotificationTextBlock.Text = aIsInvalidNotification;
        }
        else
        {
            aNotificationTextBlock.Text = string.Empty;
        }
    }

    private bool AIsValid ()
    {
        return int.TryParse(AString, out _);
    }

    private void ClickedConfirmAButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (!AIsValid())
        {
            return;
        }

        var a = int.Parse(AString);
        _u.Add(a);

        var n = int.Parse(NString);
        if (_u.Count != n)
        {
            UpdateUTextBox();
        }
        else
        {
            ShowAmountOfEvenValuesInUAndTheirSum();
        }
    }
    private void UpdateUTextBox ()
    {
        var uString = "{";
        for (var index = 0; index < _u.Count; index++)
        {
            if (index != _u.Count - 1)
            {
                uString += _u[index] + " ; ";
            }
            else
            {
                uString += _u[index];
            }
        }
        uString += "}";

        uTextBox.Text = uString;
    }
    private void ShowAmountOfEvenValuesInUAndTheirSum ()
    {
        confirmUSection.Visibility = Visibility.Collapsed;

        var evenValuesInU = GetEvenValuesInU();
        if (evenValuesInU.Count == 0)
        {
            const string uHasNoEvenValue = "không có số chẵn nào trong U";
            amountOfEvenValuesInUTextBlock.Text = uHasNoEvenValue;
            sumOfEvenValuesInUTextBlock.Text = uHasNoEvenValue;
        }
        else
        {
            amountOfEvenValuesInUTextBlock.Text = evenValuesInU.Count.ToString();
            sumOfEvenValuesInUTextBlock.Text = evenValuesInU.Sum().ToString();
        }
        amountOfEvenValuesInUAndTheirSumSection.Visibility = Visibility.Visible;
    }
    private List<int> GetEvenValuesInU ()
    {
        var evenValuesInU = new List<int>();
        foreach (var a in _u)
        {
            if (int.IsEvenInteger(a))
            {
                evenValuesInU.Add(a);
            }
        }

        return evenValuesInU;
    }
}