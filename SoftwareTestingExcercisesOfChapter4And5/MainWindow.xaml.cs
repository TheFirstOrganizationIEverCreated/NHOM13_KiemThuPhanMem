namespace SoftwareTestingExercisesOfChapter4And5;

using System.Windows;

public partial class MainWindow : Window
{
    public double X { private get; set; }

    public MainWindow ()
    {
        InitializeComponent();
    }

    private void ClickedShowFunctionFValueButton (

    private double FunctionF ()
    {
        if (X >= 1)
        {
            return Math.Sqrt(Math.Pow(X, 2) + 1);
        }
        else if ((-1 < X) && (X < 1))
        {
            return 3 * X + 5;
        }
        else
        {
            return Math.Pow(X, 2) + 2 * X - 5;
        }
    }
}