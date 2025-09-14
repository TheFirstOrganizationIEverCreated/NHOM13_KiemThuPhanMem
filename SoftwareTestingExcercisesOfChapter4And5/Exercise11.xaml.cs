namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise11 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string? _notification;
    public string? Notification
    {
        private get => _notification;
        set
        {
            if (_notification == value)
            {
                return;
            }

            _notification = value;
            OnPropertyChanged(nameof(Notification));
        }
    }

    private string? _stringAbscissaOfA;
    public string? StringAbscissaOfA
    {
        private get => _stringAbscissaOfA;
        set
        {
            if (_stringAbscissaOfA == value)
            {
                return;
            }

            _stringAbscissaOfA = value;
            OnPropertyChanged(nameof(StringAbscissaOfA));
        }
    }

    private string? _stringOrdinateOfA;
    public string? StringOrdinateOfA
    {
        private get => _stringOrdinateOfA;
        set
        {
            if (_stringOrdinateOfA == value)
            {
                return;
            }

            _stringOrdinateOfA = value;
            OnPropertyChanged(nameof(StringOrdinateOfA));
        }
    }

    private string? _stringAbscissaOfB;
    public string? StringAbscissaOfB
    {
        private get => _stringAbscissaOfB;
        set
        {
            if (_stringAbscissaOfB == value)
            {
                return;
            }

            _stringAbscissaOfB = value;
            OnPropertyChanged(nameof(StringAbscissaOfB));
        }
    }

    private string? _stringOrdinateOfB;
    public string? StringOrdinateOfB
    {
        private get => _stringOrdinateOfB;
        set
        {
            if (_stringOrdinateOfB == value)
            {
                return;
            }
            _stringOrdinateOfB = value;
            OnPropertyChanged(nameof(StringOrdinateOfB));
        }
    }

    private string? _stringAbscissaOfC;
    public string? StringAbscissaOfC
    {
        private get => _stringAbscissaOfC;
        set
        {
            if (_stringAbscissaOfC == value)
            {
                return;
            }

            _stringAbscissaOfC = value;
            OnPropertyChanged(nameof(StringAbscissaOfC));
        }
    }

    private string? _stringOrdinateOfC;
    public string? StringOrdinateOfC
    {
        private get => _stringOrdinateOfC;
        set
        {
            if (_stringOrdinateOfC == value)
            {
                return;
            }
            _stringOrdinateOfC = value;
            OnPropertyChanged(nameof(StringOrdinateOfC));
        }
    }

    public Exercise11 ()
    {
        InitializeComponent();

        NotifyNotAllCoordinatesAreValid();
    }

    private void NotifyNotAllCoordinatesAreValid ()
    {
        UpdateNotification("Tọa độ của tất cả A,B,C đều phải hợp lệ");
    }

    private void UpdateNotification (string notification)
    {
        Notification = notification;
    }

    private void AbscissaOfATextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ValidateCoordinates();
    }

    private void OrdinateOfATextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ValidateCoordinates();
    }

    private void AbscissaOfBTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ValidateCoordinates();
    }

    private void OrdinateOfBTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ValidateCoordinates();
    }

    private void AbscissaOfCTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ValidateCoordinates();
    }

    private void OrdinateOfCTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        ValidateCoordinates();
    }

    private void ValidateCoordinates ()
    {
        if (!AllCoordinatesAreValid())
        {
            NotifyNotAllCoordinatesAreValid();
        }
        else
        {
            ClearNotification();
        }
    }
    private bool AllCoordinatesAreValid ()
    {
        return double.TryParse(StringAbscissaOfA, out _) &&
               double.TryParse(StringOrdinateOfA, out _) &&
               double.TryParse(StringAbscissaOfB, out _) &&
               double.TryParse(StringOrdinateOfB, out _) &&
               double.TryParse(StringAbscissaOfC, out _) &&
               double.TryParse(StringOrdinateOfC, out _);
    }

    private void ClearNotification ()
    {
        UpdateNotification(string.Empty);
    }
}
