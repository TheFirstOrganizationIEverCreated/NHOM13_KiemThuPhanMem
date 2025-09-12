public partial class DELETE_CommonForm : Window, INotifyPropertyChanged
{


    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public DELETE_CommonForm ()
    {
        InitializeComponent();
    }
}