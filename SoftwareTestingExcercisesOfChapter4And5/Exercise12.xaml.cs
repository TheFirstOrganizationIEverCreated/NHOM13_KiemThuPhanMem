namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise12 : Window, INotifyPropertyChanged
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
    private void UpdateNotification (string notification)
    {
        Notification = notification;
    }

    private void ClearNotification ()
    {
        UpdateNotification(string.Empty);
    }

    private string? _nString;
    public string? NString
    {
        private get => _nString;
        set
        {
            if (_nString == value)
            {
                return;
            }

            _nString = value;
            OnPropertyChanged(nameof(NString));
        }
    }
    private int GetN ()
    {
        return int.Parse(NString);
    }

    private string? _aString;
    public string? AString
    {
        private get => _aString;
        set
        {
            if (_aString == value)
            {
                return;
            }

            _aString = value;
            OnPropertyChanged(nameof(AString));
        }
    }
    private int GetA ()
    {
        return int.Parse(AString);
    }

    private readonly List<int> _u;

    private string? _uString;
    public string? UString
    {
        get => _uString;
        private set
        {
            if (_uString == value)
            {
                return;
            }

            _uString = value;
            OnPropertyChanged(nameof(UString));
        }
    }

    public Exercise12 ()
    {
        InitializeComponent();

        _u = new List<int>();
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
        return int.TryParse(NString, out var n) && (5 <= n) && (n <= 100);
    }

    private void NotifyInvalidN ()
    {
        UpdateNotification("n phải là số nguyên thuộc [5 ; 100]");
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
    }

    private void TextBoxATextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!AIsValid())
        {
            NotifyInvalidA();
        }
        else
        {
            ClearNotification();
        }
    }

    private bool AIsValid ()
    {
        return int.TryParse(AString, out _);
    }

    private void NotifyInvalidA ()
    {
        UpdateNotification("a phải là số nguyên");
    }

    private void ClickedConfirmButtonAEventHandler (object sender, RoutedEventArgs e)
    {
        if (!AIsValid())
        {
            return;
        }

        var a = GetA();
        _u.Add(a);

        UpdateUString();
        if (_u.Count == GetN())
        {
            ShowConclusion();
        }
    }
    private void UpdateUString ()
    {
        var uString = string.Join(" ; ", _u);
        uString = "{" + uString + "}";

        UString = uString;
    }
    private void ShowConclusion ()
    {
        confirmUSection.Visibility = Visibility.Collapsed;
        conclusionSection.Visibility = Visibility.Visible;


    }
}
