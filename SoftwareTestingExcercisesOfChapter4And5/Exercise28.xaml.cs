namespace SoftwareTestingExercisesOfChapter4And5;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

public partial class Exercise28 : Window, INotifyPropertyChanged
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

    private string? _averageOfXString;
    public string? AverageOfXString
    {
        get => _averageOfXString;
        private set
        {
            if (_averageOfXString == value)
            {
                return;
            }

            _averageOfXString = value;
            OnPropertyChanged(nameof(AverageOfXString));
        }
    }

    public Exercise28 ()
    {
        InitializeComponent();
        ShowResults();
    }

    private void ShowResults ()
    {
        var x = GetX();

        // hiển thị dãy
        XString = string.Join(" ; ", x);

        // hiển thị trung bình cộng
        AverageOfXString = (x.Average()).ToString("F2");
    }

    private List<decimal> GetX ()
    {
        var x = new List<decimal>();
        var xi = 0m;

        for (var i = 0; i <= 11; i++)
        {
            x.Add(xi);
            xi += 0.2m;
        }

        return x;
    }
}
