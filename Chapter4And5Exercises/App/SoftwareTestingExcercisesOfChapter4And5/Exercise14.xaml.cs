namespace SoftwareTestingExercisesOfChapter4And5;

using System;
using System.Linq;
using System.Windows;

public partial class Exercise14 : Window
{
    public Exercise14 ()
    {
        InitializeComponent();
    }

    private void Process_Click (object sender, RoutedEventArgs e)
    {
        ResultText.Text = string.Empty;

        if (!int.TryParse(InputN.Text.Trim(), out var n))
        {
            ResultText.Text = "Please enter an integer n.";
            return;
        }

        if (n < 5 || n > 100)
        {
            ResultText.Text = "Condition not satisfied. Require 5 ≤ n ≤ 100.";
            return;
        }

        var parts = InputNumbers.Text.Trim().Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != n)
        {
            ResultText.Text = $"You must enter exactly {n} integers.";
            return;
        }

        int[] numbers;
        try
        {
            numbers = parts.Select(int.Parse).ToArray();
        }
        catch
        {
            ResultText.Text = "Invalid input. Please enter integers only.";
            return;
        }

        var original = string.Join(" ", numbers);
        var reversed = string.Join(" ", numbers.Reverse());

        ResultText.Text = $"Original sequence: {original}\nReversed sequence: {reversed}";
    }
}
