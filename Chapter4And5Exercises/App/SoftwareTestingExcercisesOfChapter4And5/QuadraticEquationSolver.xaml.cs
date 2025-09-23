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
        if (!ValidateLoginInputs())
        {
            return;
        }

        ConfirmQuadraticEquationConstants();
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
    private void ConfirmQuadraticEquationConstants ()
    {
        loginSection.Visibility = Visibility.Collapsed;
        quadraticArgumentsSection.Visibility = Visibility.Visible;
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
}