namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class Exercise3 : Window, INotifyPropertyChanged
{
    public string? UserInputN { private get; set; }

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

    public Exercise3 ()
    {
        InitializeComponent();

        ResetOutput();
    }
    private void ResetOutput ()
    {
        Output = "n phải là số nguyên dương thuộc [50 ; 100]";
    }

    private void UserInputNTextBoxChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!UserInputNIsValid())
        {
            ResetOutput();
            return;
        }

        var n = int.Parse(UserInputN);

        Output = NIsSquareNumber(n) ?
            "n là số chính phương" :
            "n không là số chính phương";
    }
    private bool UserInputNIsValid ()
    {
        return int.TryParse(UserInputN, out var n) && (n >= 50) && (n <= 100);
    }
    private bool NIsSquareNumber (int n)
    {
        var roundedSqrtN = (int) Math.Sqrt(n);

        return Math.Pow(roundedSqrtN, 2) == n;
    }
}