namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise27 : Window, INotifyPropertyChanged
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

    public string? XString { private get; set; }

    private decimal X => decimal.Parse(XString);

    public string? YString { private get; set; }

    private decimal Y => decimal.Parse(YString);

    public string? AString { private get; set; }

    private int A => int.Parse(AString);

    private string? _conclusion;
    public string? Conclusion
    {
        get => _conclusion;
        private set
        {
            if (_conclusion == value)
            {
                return;
            }

            _conclusion = value;
            OnPropertyChanged(nameof(Conclusion));
        }
    }

    public Exercise27 ()
    {
        InitializeComponent();

        NotifyNotAllInputsAreValid();
    }

    private void XTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!ValidateAllInputs())
        {
            return;
        }

        ShowM();
    }

    private bool ValidateAllInputs ()
    {
        if (XStringIsValid && YStringIsValid && AStringIsValid)
        {
            ClearNotification();
            return true;
        }
        else
        {
            ClearMString();
            NotifyNotAllInputsAreValid();
            return false;
        }
    }
    private bool XStringIsValid => decimal.TryParse(XString, out _);
    private bool YStringIsValid => decimal.TryParse(YString, out _);
    private bool AStringIsValid => int.TryParse(AString, out var a) && (a > 0);
    private void ClearMString ()
    {
        Conclusion = string.Empty;
    }
    private void NotifyNotAllInputsAreValid ()
    {
        UpdateNotification("x và y phải là số thực, a phải là số nguyên dương");
    }

    private void ShowM ()
    {
        decimal newX = A + X,
            newY = A + Y;

        Conclusion = $"Tọa độ mới của M là ({newX} ; {newY})";
    }

    private void YTextBoxTextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!ValidateAllInputs())
        {
            return;
        }

        ShowM();
    }

    private void TextBoxATextChangedEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!ValidateAllInputs())
        {
            return;
        }

        ShowM();
    }
}