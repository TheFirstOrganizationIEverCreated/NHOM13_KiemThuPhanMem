namespace SoftwareTestingExercisesOfChapter4And5;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

public partial class Exercise24 : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string? _xString;
    public string? XString
    {
        get => _xString;
        private set
        {
            if (_xString == value)
            {
                return;
            }

            _xString = value;
            OnPropertyChanged(nameof(XString));
        }
    }

    private string? _sumOfXString;
    public string? SumOfXString
    {
        get => _sumOfXString;
        private set
        {
            if (_sumOfXString == value)
            {
                return;
            }

            _sumOfXString = value;
            OnPropertyChanged(nameof(SumOfXString));
        }
    }

    public Exercise24 ()
    {
        InitializeComponent();
        ShowResults();
    }

    private void ShowResults ()
    {
        var x = GetX();

        // hiện dãy
        XString = string.Join(" ; ", x);

        // hiện tổng
        SumOfXString = x.Sum().ToString();
    }

    private List<decimal> GetX ()
    {
        var x = new List<decimal>();
        var xi = 1m;

        for (var i = 0; i <= 11; i++)
        {
            x.Add(xi);
            xi = 2m * xi;
        }

        return x;
    }
}
