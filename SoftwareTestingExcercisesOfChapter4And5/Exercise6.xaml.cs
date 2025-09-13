namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise6 : Window, INotifyPropertyChanged
{
    private string? _notification;
    public string? Notification
    {
        get => _notification;
        private set
        {
            _notification = value;
            OnPropertyChanged(nameof(Notification));
        }
    }

    private Visibility _formConfirmNVisibility;
    public Visibility FormConfirmNVisibility
    {
        get => _formConfirmNVisibility;
        private set
        {
            _formConfirmNVisibility = value;
            OnPropertyChanged(nameof(FormConfirmNVisibility));
        }
    }

    private int _n;

    public string? NString { private get; set; }

    private Visibility _formConfirmUVisibility;
    public Visibility FormConfirmUVisibility
    {
        get => _formConfirmUVisibility;
        private set
        {
            _formConfirmUVisibility = value;
            OnPropertyChanged(nameof(FormConfirmUVisibility));
        }
    }

    public string? AString { private get; set; }

    private readonly List<int> _u;

    private string? _uString;
    public string? UString
    {
        get => _uString;
        private set
        {
            _uString = value;
            OnPropertyChanged(nameof(UString));
        }
    }

    private Visibility _groupEvenValuesInUAndTheirSumVisibility;
    public Visibility GroupEvenValuesInUAndTheirSumVisibility
    {
        get => _groupEvenValuesInUAndTheirSumVisibility;
        private set
        {
            _groupEvenValuesInUAndTheirSumVisibility = value;
            OnPropertyChanged(nameof(GroupEvenValuesInUAndTheirSumVisibility));
        }
    }

    private readonly List<int> _allEvenValuesInU;

    private string? _stringAllEvenValuesInU;
    public string? StringAllEvenValuesInU
    {
        get => _stringAllEvenValuesInU;
        private set
        {
            _stringAllEvenValuesInU = value;
            OnPropertyChanged(nameof(StringAllEvenValuesInU));
        }
    }

    private string? _stringSumOfAllEvenValuesInU;
    public string? StringSumOfAllEvenValuesInU
    {
        get => _stringSumOfAllEvenValuesInU;
        private set
        {
            _stringSumOfAllEvenValuesInU = value;
            OnPropertyChanged(nameof(StringSumOfAllEvenValuesInU));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Exercise6 ()
    {
        InitializeComponent();

        FormConfirmNVisibility = Visibility.Visible;
        FormConfirmUVisibility = Visibility.Collapsed;
        GroupEvenValuesInUAndTheirSumVisibility = Visibility.Collapsed;
        _u = new List<int>();
        UString = "{}";
        _allEvenValuesInU = new List<int>();
        ResetNotification();
    }
    private void ResetNotification ()
    {
        Notification = string.Empty;
    }

    private void TextBoxNChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ResetNotification();
    }

    private void ClickedButtonConfirmN (object sender, RoutedEventArgs e)
    {
        if (!NIsValid())
        {
            NotifyNIsNotValid();
            return;
        }

        _n = int.Parse(NString);
        ConfirmU();
    }
    private bool NIsValid ()
    {
        return uint.TryParse(NString, out var nValue) && (5 <= nValue) && (nValue <= 20);
    }
    private void NotifyNIsNotValid ()
    {
        Notification = "n phải thuộc Z+ và n thuộc [5 ; 20]";
    }
    private void ConfirmU ()
    {
        RemoveFormConfirmN();
        ShowFormConfirmU();
    }

    private void RemoveFormConfirmN ()
    {
        FormConfirmNVisibility = Visibility.Collapsed;
    }
    private void ShowFormConfirmU ()
    {
        FormConfirmUVisibility = Visibility.Visible;
    }

    private void TextBoxAChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ResetNotification();
    }

    private void ClickedButtonAddIntegerToU (object sender, RoutedEventArgs e)
    {
        if (!AStringIsValid())
        {
            NotifyAIsNotValid();
            return;
        }

        var a = int.Parse(AString);
        _u.Add(a);

        UString = GetUString();
        if (_u.Count == _n)
        {
            RemoveFormConfirmU();
            ShowEvenValuesInUAndTheirSum();
            return;
        }
    }
    private bool AStringIsValid ()
    {
        return int.TryParse(AString, out _);
    }
    private void NotifyAIsNotValid ()
    {
        Notification = "a phải thuộc Z";
    }
    private string GetUString ()
    {
        var uString = "{";
        for (var index = 0; index < _u.Count; index++)
        {
            if (index < _u.Count - 1)
            {
                uString += _u[index] + " ; ";
            }
            else
            {
                uString += _u[index];
            }
        }
        uString += "}";

        return uString;
    }
    private void RemoveFormConfirmU ()
    {
        FormConfirmUVisibility = Visibility.Collapsed;
    }
    private void ShowEvenValuesInUAndTheirSum ()
    {
        foreach (var value in _u)
        {
            if (int.IsEvenInteger(value))
            {
                _allEvenValuesInU.Add(value);
            }
        }
        // avoid calling OnPropertyChanged too many times
        StringAllEvenValuesInU = GetStringAllEvenValuesInU();
        StringSumOfAllEvenValuesInU = GetStringSumOfAllEvenValuesInU();

        GroupEvenValuesInUAndTheirSumVisibility = Visibility.Visible;
    }

    private string GetStringAllEvenValuesInU ()
    {
        if (UHasNoEvenValue)
        {
            return "KHÔNG TỒN TẠI";
        }

        var stringAllEvenValuesInU = "{";
        for (var index = 0; index < _allEvenValuesInU.Count; index++)
        {
            if (index != _allEvenValuesInU.Count - 1)
            {
                stringAllEvenValuesInU += $"{_allEvenValuesInU[index]} ; ";
            }
            else
            {
                stringAllEvenValuesInU += _allEvenValuesInU[index];
            }
        }
        stringAllEvenValuesInU = "}";

        return stringAllEvenValuesInU;
    }

    private bool UHasNoEvenValue => _allEvenValuesInU.Count == 0;

    private string GetStringSumOfAllEvenValuesInU ()
    {
        return UHasNoEvenValue ?
            "KHÔNG TỒN TẠI" :
            _allEvenValuesInU.Sum().ToString();
    }
}