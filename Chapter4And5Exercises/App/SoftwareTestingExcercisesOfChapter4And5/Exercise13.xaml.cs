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
        if (!(NIsInteger()
            && NIsInRange()))
        {
            UpdateMessageText("n phải là số nguyên thuộc (10, 50)");
            ClearResultText();
            return;
        }
        ClearMessageText();

        var n = int.Parse(InputN.Text.Trim());

        double s = 0;
        for (var count = 1;
            count <= n;
            count++)
        {
            s += 1.0 / Math.Sqrt(count * (count + 1));
        }
        UpdateResultText($"S = {s}");
    }

    private bool NIsInteger ()
    {
        return int.TryParse(InputN.Text.Trim(), out _);
    }
    private bool NIsInRange ()
    {
        var n = int.Parse(InputN.Text.Trim());
        return (10 < n) && (n < 50);
    }

    private void UpdateMessageText (string message)
    {
        MessageText.Text = message;
    }

    private void ClearMessageText ()
    {
        UpdateMessageText(string.Empty);
    }

    private void UpdateResultText (string result)
    {
        ResultText.Text = result;
    }

    private void ClearResultText ()
    {
        UpdateResultText(string.Empty);
    }
}