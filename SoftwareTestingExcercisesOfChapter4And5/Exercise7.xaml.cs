namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Text;
using System.Windows;

public partial class Exercise7 : Window, INotifyPropertyChanged
{
    private string? _notification;
    public string? Notification
    {
        get => _notification;
        private set
        {
            _notification = value;
            OnPropertyChanged(nameof(Notification));
        }
    }

    private string? _nString;
    public string? NString
    {
        get => _nString;
        set
        {
            _nString = value;
            OnPropertyChanged(nameof(NString));
        }
    }

    private Visibility _outputsSectionVisibility;
    public Visibility OutputsSectionVisibility
    {
        get => _outputsSectionVisibility;
        private set
        {
            _outputsSectionVisibility = value;
            OnPropertyChanged(nameof(OutputsSectionVisibility));
        }
    }

    private List<int> _allMultipleOf3From1ToN;

    private string? _stringAllMultipleOf3From1ToN;
    public string? StringAllMultipleOf3From1ToN
    {
        get => _stringAllMultipleOf3From1ToN;
        private set
        {
            _stringAllMultipleOf3From1ToN = value;
            OnPropertyChanged(nameof(StringAllMultipleOf3From1ToN));
        }
    }

    private string? _amountOfMultipleOf3From1ToN;
    public string? AmountOfMultipleOf3From1ToN
    {
        get => _amountOfMultipleOf3From1ToN;
        private set
        {
            _amountOfMultipleOf3From1ToN = value;
            OnPropertyChanged(nameof(AmountOfMultipleOf3From1ToN));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Exercise7 ()
    {
        InitializeComponent();

        _allMultipleOf3From1ToN = new List<int>();
        NotifyNIsNotValid();
        ClearAllOutputs();
    }

    private void NTextBoxChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!NIsValid)
        {
            NotifyNIsNotValid();
            ClearAllOutputs();
            return;
        }
        ShowOutputsSection();
        ClearNotification();

        StringAllMultipleOf3From1ToN = GetStringAllMultipleOf3From1ToN();
        AmountOfMultipleOf3From1ToN = _allMultipleOf3From1ToN.Count.ToString();
    }
    private bool NIsValid => int.TryParse(NString, out var n) && (50 <= n) && (n <= 100);
    private void NotifyNIsNotValid ()
    {
        Notification = "n phải thuộc Z+ và thuộc [50 ; 100]";
    }
    private void ClearAllOutputs ()
    {
        OutputsSectionVisibility = Visibility.Collapsed;
    }
    private void ShowOutputsSection ()
    {
        OutputsSectionVisibility = Visibility.Visible;
    }
    private void ClearNotification ()
    {
        Notification = string.Empty;
    }
    private string GetStringAllMultipleOf3From1ToN ()
    {
        var stringBuilderAllMultipleOf3From1ToN = new StringBuilder();

        var n = int.Parse(NString);

        _allMultipleOf3From1ToN = new List<int>(n);
        for (var integer = 1; integer <= n; integer++)
        {
            if (integer % 3 == 0)
            {
                _allMultipleOf3From1ToN.Add(integer);
            }
        }

        stringBuilderAllMultipleOf3From1ToN.Append('{');
        for (var index = 0; index < _allMultipleOf3From1ToN.Count; index++)
        {
            if (index != _allMultipleOf3From1ToN.Count - 1)
            {
                stringBuilderAllMultipleOf3From1ToN.Append($"{_allMultipleOf3From1ToN[index]} ; ");
            }
            else
            {
                stringBuilderAllMultipleOf3From1ToN.Append($"{_allMultipleOf3From1ToN[index]}");
            }
        }
        stringBuilderAllMultipleOf3From1ToN.Append('}');

        return stringBuilderAllMultipleOf3From1ToN.ToString();
    }
}