namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class QuadraticEquationSolver : Window, INotifyPropertyChanged
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

    public string? UserName { private get; set; }

    private string? Password => passwordBox.Password;

    private string? _aString;
    public string? AString
    {
        get => _aString;
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

    private decimal _a;

    private string? _bString;
    public string? BString
    {
        get => _bString;
        set
        {
            if (_bString == value)
            {
                return;
            }

            _bString = value;
            OnPropertyChanged(nameof(BString));
        }
    }

    private decimal _b;

    private string? _cString;
    public string? CString
    {
        get => _cString;
        set
        {
            if (_cString == value)
            {
                return;
            }

            _cString = value;
            OnPropertyChanged(nameof(CString));
        }
    }

    private decimal _c;

    private string? _deltaString;
    public string? DeltaString
    {
        get => _deltaString;
        private set
        {
            if (_deltaString == value)
            {
                return;
            }

            _deltaString = value;
            OnPropertyChanged(nameof(DeltaString));
        }
    }

    private decimal _delta;

    private string? _xString;
    public string? XString
    {
        get => _xString;
        private set
        {
            if (_xString == value)
            {
                return;
            }

            _xString = value;
            OnPropertyChanged(nameof(XString));
        }
    }

    private string? _x1String;
    public string? X1String
    {
        get => _x1String;
        private set
        {
            if (_x1String == value)
            {
                return;
            }

            _x1String = value;
            OnPropertyChanged(nameof(X1String));
        }
    }

    private string? _x2String;
    public string? X2String
    {
        get => _x2String;
        private set
        {
            if (_x2String == value)
            {
                return;
            }

            _x2String = value;
            OnPropertyChanged(nameof(X2String));
        }
    }

    public QuadraticEquationSolver ()
    {
        InitializeComponent();
    }

    private void ClickedExitButtonEventHandler (object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ClickedLoginButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (ValidateLoginInputs())
        {
            ConfirmEquationConstants();
        }
    }
    private bool ValidateLoginInputs ()
    {
        if (!(UserNameIsValid() && PasswordIsValid()))
        {
            NotifyNotAllLoginInputsAreValid();
            return false;
        }

        return true;
    }
    private void ConfirmEquationConstants ()
    {
        loginSection.Visibility = Visibility.Collapsed;
        equationConstantsSection.Visibility = Visibility.Visible;
    }

    private void NotifyNotAllLoginInputsAreValid ()
    {
        UpdateNotification("User name hoặc Password không hợp lệ");
    }
    private bool UserNameIsValid ()
    {
        // mang tính tượng trưng, khỏi cần dùng database
        return UserName == "username";
    }
    private bool PasswordIsValid ()
    {
        // mang tính tượng trưng, khỏi cần dùng database
        return Password == "password";
    }

    private void UserNameTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ClearNotification();
    }

    private void PasswordBoxPasswordChangedEventHandler (object sender, RoutedEventArgs e)
    {
        ClearNotification();
    }

    private void TextBoxATextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ClearNotification();
    }

    private void BTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ClearNotification();
    }

    private void CTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ClearNotification();
    }

    private void ClickedInputButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (ValidateEquationConstants())
        {
            ShowConclusionSection();
        }
    }
    private bool ValidateEquationConstants ()
    {
        if (AllEquationConstantsAreValid)
        {
            return true;
        }
        else
        {
            NotifyNotAllEquationConstantsAreValid();

            return false;
        }
    }
    private void ShowConclusionSection ()
    {
        equationConstantsSection.Visibility = Visibility.Collapsed;

        ParseEquationConstantsToNumericalValues();
        conclusionSection.Visibility = Visibility.Visible;
    }
    private void ParseEquationConstantsToNumericalValues ()
    {
        _a = decimal.Parse(AString);
        _b = decimal.Parse(BString);
        _c = decimal.Parse(CString);
        _delta = ((decimal) (Math.Pow((double) _b, 2))) - 4m * _a * _c;
    }

    private bool AllEquationConstantsAreValid => AStringIsValid && BStringIsValid && CStringIsValid;
    private void NotifyNotAllEquationConstantsAreValid ()
    {
        UpdateNotification("Tất cả hằng số đều phải hợp lệ");
    }
    private bool AStringIsValid => decimal.TryParse(AString, out _);
    private bool BStringIsValid => decimal.TryParse(BString, out _);
    private bool CStringIsValid => decimal.TryParse(CString, out _);

    private void ClickedAgainButtonEventHandler (object sender, RoutedEventArgs e)
    {
        ClearEquationConstantStrings();
    }
    private void ClearEquationConstantStrings ()
    {
        AString = string.Empty;
        BString = string.Empty;
        CString = string.Empty;
    }

    private void ClickedCalculateDeltaButtonEventHandler (object sender, RoutedEventArgs e)
    {
        DeltaString = _delta.ToString();
    }

    private void ClickedCalculateSolutionsButtonEventHandler (object sender, RoutedEventArgs e)
    {
        if (EquationHasInfiniteAmountOfSolutions())
        {
            XString = "bất cứ x nào cũng thỏa mãn phương trình";
        }
        else if (EquationHasNoRealSolutionValue())
        {
            XString = "không tồn tại x thuộc R nào thỏa mãn phương trình";
        }
        else if (EquationHasOnlyOneSolution())
        {
            XString = GetTheOnlyX().ToString();
        }
        else
        {
            X1String = GetX1().ToString();
            X2String = GetX2().ToString();
        }
    }
    private bool EquationHasInfiniteAmountOfSolutions ()
    {
        return (_a == 0) && (_b == 0) && (_c == 0);
    }
    private bool EquationHasNoRealSolutionValue ()
    {
        return ((_a == 0) && (_b == 0) && (_c != 0)) || (_delta < 0);
    }
    private bool EquationHasOnlyOneSolution ()
    {
        return ((_a == 0) && (_b != 0)) || (_delta == 0);
    }
    private decimal GetTheOnlyX ()
    {
        if (_a == 0)
        {
            return -_c / _b;
        }

        return GetX1();
    }
    private decimal GetX1 ()
    {
        return (-_b + ((decimal) Math.Sqrt((double) _delta))) / (2m * _a);
    }
    private decimal GetX2 ()
    {
        return (-_b - ((decimal) Math.Sqrt((double) _delta))) / (2m * _a);
    }
}