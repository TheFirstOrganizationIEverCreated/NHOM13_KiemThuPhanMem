namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise4 : Window, INotifyPropertyChanged
{
    public string? InputString { private get; set; }

    private string? _output;
    public string? Output
    {
        get => _output;
        private set
        {
            _output = value;
            OnPropertyChanged(nameof(Output));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Exercise4 ()
    {
        InitializeComponent();

        ResetOutput();
    }
    private void ResetOutput ()
    {
        Output = "? n phải là số nguyên dương và n thuộc [5 ; 20]";
    }

    private void InputTextBoxChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!InputIsValid())
        {
            ResetOutput();
            return;
        }

        var inputValue = uint.Parse(InputString);
        Output = S(inputValue).ToString();
    }
    private bool InputIsValid ()
    {
        return uint.TryParse(InputString, out var n) && (n >= 5) && (n <= 20);
    }
    private double S (uint n)
    {
        uint numerator = 0;
        for (uint integer = 1; integer <= n; integer++)
        {
            numerator += integer;
        }

        return numerator / (double) n;
    }
}