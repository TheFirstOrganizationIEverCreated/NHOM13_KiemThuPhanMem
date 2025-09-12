public partial class DELETE_CommonForm : Window, INotifyPropertyChanged
{
    public string? InputString { private get; set; }

    private string? _output;
    public string? OutputString
    {
        get => _output;
        private set
        {
            _output = value;
            OnPropertyChanged(nameof(OutputString));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public DELETE_CommonForm ()
    {
        InitializeComponent();

        ResetOutput();
    }
    private void ResetOutput ()

    private void InputTextBoxChangedText (object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        if (!InputIsValid())
        {
            ResetOutput();
            return;
        }

        var inputValue = .Parse(InputString);
        OutputString = GetOutput(inputValue);
    }
    private bool InputIsValid ()
    {
        return .TryParse(InputString, out var inputValue) &&  && ;
    }
    private string GetOutput (double n)
    {
        double numerator = 0;
        for (uint integer = 1; integer <= n; integer++)
        {
            numerator += integer;
        }

        var outputValue = numerator / n;

        return .ToString();
    }
}