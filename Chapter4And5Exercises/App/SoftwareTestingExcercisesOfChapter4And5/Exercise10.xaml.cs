namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise10 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

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

    public string? NString { private get; set; }

    public string? XString { private get; set; }

    private string? _sString;
    public string? SString
    {
        get => _sString;
        private set
        {
            if (_sString == value)
            {
                return;
            }

            _sString = value;
            OnPropertyChanged(nameof(SString));
        }
    }

    public Exercise10 ()
    {
        InitializeComponent();

        NotifyInvalidN();
    }

    private void NotifyInvalidN ()
    {
        Notification = "n phải thuộc Z+ và n thuộc [50 ; 100]";
    }

    private void NotifyInvalidX ()
    {
        Notification = "x phải là số nguyên khác 0";
    }

    private void NTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
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
        return int.TryParse(NString, out var n) && (50 <= n) && (n <= 100);
    }

    private void ClearNotification ()
    {
        Notification = string.Empty;
    }

    private void ClickedConfirmNButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (!NIsValid())
        {
            return;
        }

        ConfirmX();
    }

    private void ConfirmX ()
    {
        RemoveNSection();
        ShowXSection();
    }
    private void RemoveNSection ()
    {
        section1.Visibility = Visibility.Collapsed;
    }
    private void ShowXSection ()
    {
        section2.Visibility = Visibility.Visible;
    }

    private void XTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!XIsValid())
        {
            NotifyInvalidX();
            ClearS();
            return;
        }
        ClearNotification();

        ShowS();
    }
    private bool XIsValid ()
    {
        return int.TryParse(XString, out var x) && (x != 0);
    }
    private void ClearS ()
    {
        SString = string.Empty;
    }
    private void ShowS ()
    {
        SString = S().ToString();
    }
    private double S ()
    {
        double numerator = 0;

        {
            var n = double.Parse(NString);
            for (double positiveInteger = 1; positiveInteger <= n; positiveInteger++)
            {
                numerator += 2 * positiveInteger - 1;
            }
        }

        var x = double.Parse(XString);
        return numerator / x;
    }
}
