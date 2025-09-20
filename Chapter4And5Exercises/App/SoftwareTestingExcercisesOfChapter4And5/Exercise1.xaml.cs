namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise1 : Window, INotifyPropertyChanged
{
    public string? ArgumentString { private get; set; }

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
        if (!ArgumentStringIsValid(ArgumentString))
        {
            ResetFunctionValueString();
            return;
        }

        var argument = double.Parse(ArgumentString);
        FunctionValueString = FunctionValue(argument).ToString();
    }
    private bool ArgumentStringIsValid (string? argumentString)
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
