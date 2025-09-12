namespace SoftwareTestingExercisesOfChapter4And5;
using System.ComponentModel;
using System.Windows;

public partial class Exercise2 : Window, INotifyPropertyChanged
{
    public string? UserInputN { private get; set; }

    private string? _outputS;
    public string? OutputS
    {
        get => _outputS;
        private set
        {
            _outputS = value;
            OnPropertyChanged(nameof(OutputS));
        }
    }

    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public Exercise2 ()
    {
        InitializeComponent();

        ResetOutputS();
    }

    private void OutputSTextBoxChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!UserInputIsValid())
        {
            ResetOutputS();
            return;
        }

        var n = int.Parse(UserInputN);
        OutputS = S(n).ToString();
    }
    private bool UserInputIsValid ()
    {
        return int.TryParse(UserInputN, out var n) && (n >= 5) && (n <= 100);
    }
    private void ResetOutputS ()
    {
        OutputS = "? n phải là số nguyên thuộc [5 ; 100]";
    }
    private double S (int n)
    {
        var radicand = 0;

        for (var integer = 1; integer <= n; integer++)
        {
            radicand += (int) Math.Pow(integer, 2);
        }

        return Math.Sqrt(radicand);
    }
}