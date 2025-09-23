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

    public QuadraticEquationSolver ()
    {
        InitializeComponent();
    }
}
