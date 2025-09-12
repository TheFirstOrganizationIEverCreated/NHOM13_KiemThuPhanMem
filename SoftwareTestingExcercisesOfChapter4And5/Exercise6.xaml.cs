namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise6 : Window, INotifyPropertyChanged
{
    public string? NString { private get; set; }

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

    private Visibility _formConfirmNAmountOfIntegersVisibility;
    public Visibility FormConfirmNAmountOfIntegersVisibility
    {
        get => _formConfirmNAmountOfIntegersVisibility;
        private set
        {
            _formConfirmNAmountOfIntegersVisibility = value;
            OnPropertyChanged(nameof(FormConfirmNAmountOfIntegersVisibility));
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
        FormConfirmNAmountOfIntegersVisibility = Visibility.Collapsed;
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

        ConfirmNAmountOfIntegers();
    }
    private bool NIsValid ()
    {
        return uint.TryParse(NString, out var nValue) && (5 <= nValue) && (nValue <= 20);
    }
    private void NotifyNIsNotValid ()
    {
        Notification = "n phải thuộc Z+ và n thuộc [5 ; 20]";
    }
    private void ConfirmNAmountOfIntegers ()
    {
        RemoveFormConfirmN();
        ShowFormConfirmNAmountOfIntegersVisibility();
    }

    private void RemoveFormConfirmN ()
    {
        FormConfirmNVisibility = Visibility.Collapsed;
    }
    private void ShowFormConfirmNAmountOfIntegersVisibility ()
    {
        FormConfirmNAmountOfIntegersVisibility = Visibility.Visible;
    }
}