namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise1 : Window, INotifyPropertyChanged
{
    public string? ArgumentString { private get; set; }

    private double _argument;
    public double Argument
    {
        private get => _argument;
        set
        {
            _argument = value;
            OnPropertyChanged(nameof(Argument));
        }
    }

    private string? _functionValueString;
    public string? FunctionValueString
    {
        get => _functionValueString;
        private set
        {
            _functionValueString = value;
            OnPropertyChanged(nameof(FunctionValueString));
        }
    }

    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public Exercise1 ()
    {
        InitializeComponent();

        ResetFunctionValueString();
    }
    private void ResetFunctionValueString ()
    {
        FunctionValueString = string.Empty;
    }

    private void ArgumentTextBoxChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!ArgumentIsValid(ArgumentString))
        {
            ResetFunctionValueString();
            return;
        }

        Argument = double.Parse(ArgumentString);
        FunctionValueString = FunctionValue(Argument).ToString();
    }
    private bool ArgumentIsValid (string? argumentString)
    {
        return double.TryParse(argumentString, out _);
    }
    private double FunctionValue (double argument)
    {
        if (argument >= 1)
        {
            return Math.Sqrt(Math.Pow(argument, 2) + 1);
        }
        else if ((-1 < argument) && (argument < 1))
        {
            return 3 * argument + 5;
        }
        else
        {
            return Math.Pow(argument, 2) + 2 * argument - 5;
        }
    }
}
