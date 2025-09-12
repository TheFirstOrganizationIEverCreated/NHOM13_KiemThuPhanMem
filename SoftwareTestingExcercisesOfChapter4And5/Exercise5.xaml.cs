namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise5 : Window, INotifyPropertyChanged
{
    public string? InputString { private get; set; }

    private string? _output;
    public string? OutputString
    {
        get => _output;
        private set
        {
            _output = value;
            OnPropertyChanged(nameof(OutputString));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Exercise5 ()
    {
        InitializeComponent();

        ResetOutput();
    }
    private void ResetOutput ()
    {
        OutputString = "? k phải thuộc Z+ và k thuộc (10 ; 100)";
    }

    private void InputTextBoxChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!InputIsValid())
        {
            ResetOutput();
            return;
        }

        var inputValue = uint.Parse(InputString!);

        OutputString = GetOutputString(inputValue);
    }
    private bool InputIsValid ()
    {
        return uint.TryParse(InputString, out var inputValue) && (inputValue > 10) && (inputValue < 100);
    }
    private string GetOutputString (uint inputValue)
    {
        uint resultValue = 0;

        for (uint factor = 1; factor <= inputValue; factor++)
        {
            resultValue += factor * (factor + 1) * (factor + 2);
        }

        return resultValue.ToString();
    }
}
