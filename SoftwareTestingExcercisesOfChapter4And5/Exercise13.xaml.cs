namespace SoftwareTestingExercisesOfChapter4And5;

using System;
using System.Windows;

public partial class Exercise13 : Window
{
    public Exercise13 ()
    {
        InitializeComponent();
    }

    private void Compute_Click (object sender, RoutedEventArgs e)
    {
        MessageText.Text = string.Empty;
        ResultText.Text = string.Empty;

        if (!int.TryParse(InputN.Text.Trim(), out var n))
        {
            MessageText.Text = "Please enter an integer.";
            return;
        }

        if (!((10 < n) && (n < 50)))
        {
            MessageText.Text = "Condition not satisfied. Require 10 < n < 50.";
            return;
        }

        var sum = 0.0;
        for (var i = 1; i <= n; i++)
        {
            sum += 1.0 / Math.Sqrt(i * (i + 1));
        }

        ResultText.Text = $"n = {n}\nS = {sum:F12}";
    }
}
