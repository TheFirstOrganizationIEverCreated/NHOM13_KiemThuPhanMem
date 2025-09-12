namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise1 : Window, INotifyPropertyChanged
{
    private double _x;
    public double X
    {
        get => _x;
        set
        {
            _x = value;
            OnPropertyChanged(nameof(X));
        }
    }

    private double _functionFValue;
    public double FunctionFValue
    {
        get => _functionFValue;
        set
        {
            _functionFValue = value;
            OnPropertyChanged(nameof(FunctionFValue));
        }
    }

    public Exercise1 ()
    {
        InitializeComponent();

        DataContext = this;
    }

    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void ClickedCalculateFunctionFButton (object sender, RoutedEventArgs e)
    {
        if (X >= 1)
        {
            FunctionFValue = Math.Sqrt(Math.Pow(X, 2) + 1);
        }
        else if ((-1 < X) && (X < 1))
        {
            FunctionFValue = 3 * X + 5;
        }
        else
        {
            FunctionFValue = Math.Pow(X, 2) + 2 * X - 5;
        }
    }
}
