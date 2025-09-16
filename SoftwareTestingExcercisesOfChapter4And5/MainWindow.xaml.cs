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

    public MainWindow ()
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
            case Exercise.Exercise2:
                (new Exercise2()).ShowDialog();
                break;
            case Exercise.Exercise3:
                (new Exercise3()).ShowDialog();
                break;
            case Exercise.Exercise4:
                (new Exercise4()).ShowDialog();
                break;
            case Exercise.Exercise5:
                (new Exercise5()).ShowDialog();
                break;
            case Exercise.Exercise6:
                (new Exercise6()).ShowDialog();
                break;
            case Exercise.Exercise7:
                (new Exercise7()).ShowDialog();
                break;
            case Exercise.Exercise8:
                (new Exercise8()).ShowDialog();
                break;
            case Exercise.Exercise9:
                (new Exercise9()).ShowDialog();
                break;
            case Exercise.Exercise10:
                (new Exercise10()).ShowDialog();
                break;
            case Exercise.Exercise11:
                (new Exercise11()).ShowDialog();
                break;
            case Exercise.Exercise18:
                (new Exercise18()).ShowDialog();
                break;
            case Exercise.Exercise20:
                (new Exercise20()).ShowDialog();
                break;
            case Exercise.Exercise21:
                (new Exercise21()).ShowDialog();
                break;
            case Exercise.Exercise22:
                (new Exercise22()).ShowDialog();
                break;
            default:
                MessageBox.Show("Hiện chưa có bài này hoặc đã có lỗi.");
                break;
        }
    }
}