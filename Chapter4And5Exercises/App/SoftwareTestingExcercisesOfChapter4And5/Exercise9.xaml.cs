namespace SoftwareTestingExercisesOfChapter4And5;

using System.Windows;

public partial class Exercise9 : Window
{
    private readonly (double x, double y) _aCoordinate;
    private readonly (double x, double y) _bCoordinate;
    private readonly double _aB;

    public string? RString { private get; set; }

    public Exercise9 ()
    {
        InitializeComponent();

        double x1 = 0, y1 = 0,
            x2 = 7, y2 = 0;
        _aCoordinate = (x1, y1);
        _bCoordinate = (x2, y2);
        _aB = GetAB();
        NotifyRIsInvalid();
    }
    private double GetAB ()
    {
        return Math.Sqrt(Math.Pow((_bCoordinate.x - _aCoordinate.x), 2) + Math.Pow((_bCoordinate.y - _aCoordinate.y), 2));
    }
    private void NotifyRIsInvalid ()
    {
        notificationTextBlock.Text = "R phải là số thực dương";
    }

    private void RTextBoxChangedTextEventHandler (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!RIsValid())
        {
            NotifyRIsInvalid();
            ClearConclusion();
            return;
        }
        ClearNotification();

        conclusionTextBlock.Text = GetConclusion();
    }
    private bool RIsValid ()
    {
        return double.TryParse(RString, out var r) && (r > 0);
    }
    private void ClearConclusion ()
    {
        conclusionTextBlock.Text = string.Empty;
    }
    private void ClearNotification ()
    {
        notificationTextBlock.Text = string.Empty;
    }
    private string GetConclusion ()
    {
        var r = double.Parse(RString);

        if (_aB < r)
        {
            return $"B({_bCoordinate.x} ; {_bCoordinate.y}) nằm trong đường tròn tâm A({_aCoordinate.x} ; {_aCoordinate.y}) bán kính R";
        }
        else if (_aB == r)
        {
            return $"B({_bCoordinate.x} ; {_bCoordinate.y}) nằm trên đường tròn tâm A({_aCoordinate.x} ; {_aCoordinate.y}) bán kính R";
        }
        else
        {
            return $"B({_bCoordinate.x} ; {_bCoordinate.y}) không nằm trong đường tròn tâm A({_aCoordinate.x} ; {_aCoordinate.y}) bán kính R";
        }
    }
}