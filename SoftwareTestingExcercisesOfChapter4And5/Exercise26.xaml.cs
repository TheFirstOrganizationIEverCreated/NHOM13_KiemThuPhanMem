namespace SoftwareTestingExercisesOfChapter4And5;

using System.Collections.Generic;
using System.Linq;
using System.Windows;

public partial class Exercise26 : Window
{
    public Exercise26 ()
    {
        InitializeComponent();
        CalculateAndShow();
    }

    private void BtnCalc_Click (object sender, RoutedEventArgs e)
    {
        CalculateAndShow();
    }

    private void CalculateAndShow ()
    {
        decimal y0 = 2;
        var values = new List<decimal> { y0 };
        for (var i = 0; i < 10; i++)
        {
            values.Add(values.Last() * 0.5m);
        }

        var display = values.Select((v, idx) => $"y{idx} = {v.ToString("G6")}");
        ListY.ItemsSource = display;
        TxtAverage.Text = values.Average().ToString("G6");
        LblCount.Text = $"Số phần tử: {values.Count}";
    }
}
