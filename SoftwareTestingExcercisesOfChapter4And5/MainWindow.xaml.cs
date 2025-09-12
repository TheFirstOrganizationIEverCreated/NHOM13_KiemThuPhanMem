namespace SoftwareTestingExercisesOfChapter4And5;

using System.ComponentModel;
using System.Windows;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private List<Exercise> _exercises;
    public List<Exercise> Exercises
    {
        get => _exercises;
        private set
        {
            _exercises = value;
            OnPropertyChanged(nameof(Exercises));
        }
    }

    private Exercise _selectedExercise;
    public Exercise SelectedExercise
    {
        get => _selectedExercise;
        set
        {
            _selectedExercise = value;
            OnPropertyChanged(nameof(SelectedExercise));
        }
    }

    private void OnPropertyChanged (string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler? PropertyChanged;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public MainWindow ()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        InitializeComponent();

        Exercises = Enum.GetValues<Exercise>().ToList();
        SelectedExercise = Exercises[0];
    }

    private void ClickedRunButton (object sender, RoutedEventArgs e)
    {
        switch (SelectedExercise)
        {
            case Exercise.Exercise1:
                (new Exercise1()).ShowDialog();
                break;
            default:
                MessageBox.Show("Hiện chưa có bài này hoặc đã có lỗi.");
                break;
        }
    }
}