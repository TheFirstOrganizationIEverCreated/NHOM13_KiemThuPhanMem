namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise25 : Window, INotifyPropertyChanged
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

    private string? _fString;
    public string? FString
    {
        private get => _fString;
        set
        {
            if (_fString == value)
            {
                return;
            }

            _fString = value;
        }
    }
    private decimal? F => decimal.Parse(FString);

    private string? _sString;
    public string? SString
    {
        private get => _sString;
        set
        {
            if (_sString == value)
            {
                return;
            }

            _sString = value;
        }
    }
    private decimal? S => decimal.Parse(SString);

    private string? _pString;
    public string? PString
    {
        get => _pString;
        private set
        {
            if (_pString == value)
            {
                return;
            }

            _pString = value;
            OnPropertyChanged(nameof(PString));
        }
    }

    public Exercise25 ()
    {
        InitializeComponent();

        NotifyNotAllInputsAreValid();
    }

    private void NotifyNotAllInputsAreValid ()
    {
        UpdateNotification("F và S đều phải là số thực thỏa mãn F >= 0 và S > 0");
    }

    private void FTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!ValidateInputs())
        {
            return;
        }
        ShowP();
    }

    private void ShowP ()
    {
        PString = (F / S).ToString();
    }

    private bool ValidateInputs ()
    {
        if (!AllInputsAreValid)
        {
            NotifyNotAllInputsAreValid();
            ClearPString();
            return false;
        }
        else
        {
            ClearNotification();
            return true;
        }
    }
    private bool AllInputsAreValid => FStringIsValid && SStringIsValid;
    private void ClearPString ()
    {
        PString = string.Empty;
    }

    private bool FStringIsValid => decimal.TryParse(FString, out var f) && (f >= 0);
    private bool SStringIsValid => decimal.TryParse(SString, out var s) && (s > 0);

    private void STextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!ValidateInputs())
        {
            return;
        }
        ShowP();
    }
}
