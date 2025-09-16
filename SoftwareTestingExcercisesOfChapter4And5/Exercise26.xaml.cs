namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;
using SoftwareTestingExercisesOfChapter4And5.Commons;

public partial class Exercise26 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string? _yString;
    public string? YString
    {
        get => _yString;
        private set
        {
            if (_yString == value)
            {
                return;
            }

            _yString = value;
            OnPropertyChanged(nameof(YString));
        }
    }

    private string? _averageOfYSequenceString;
    public string? AverageOfYSequenceString
    {
        get => _averageOfYSequenceString;
        private set
        {
            if (_averageOfYSequenceString == value)
            {
                return;
            }

            _averageOfYSequenceString = value;
            OnPropertyChanged(nameof(AverageOfYSequenceString));
        }
    }

    public Exercise26 ()
    {
        InitializeComponent();

        ShowResults();
    }
    private void ShowResults ()
    {
        var y = GetY();

        YString = Common.CollectionString(y);

        AverageOfYSequenceString = GetAverageOfYSequence(y).ToString();
    }

    private List<decimal> GetY ()
    {
        var y = new List<decimal>();

        {
            var yI = 2m;
            for (var i = 0; i <= 10; i++, yI = 0.5m * yI)
            {
                y.Add(yI);
            }
        }

        return y;
    }
    private decimal GetAverageOfYSequence (List<decimal> y)
    {
        return y.Average();
    }
}
