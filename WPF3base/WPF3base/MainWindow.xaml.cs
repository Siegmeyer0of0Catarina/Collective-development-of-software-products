using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF3base
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> students = new
       ObservableCollection<Student>();
        public MainWindow()
        {
            InitializeComponent();
            StudentsListBox.ItemsSource = students;
            CommandBinding saveCommandBinding = new CommandBinding(ApplicationCommands.Save,
           SaveCommandExecuted, SaveCommandCanExecute);
            this.CommandBindings.Add(saveCommandBinding);
            this.KeyDown += Window_KeyDown;

        }
        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveToFile();
        }
        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string patronemic = PatronymicTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string dateOfBirth = DateOfBirthTextBox.Text;
            string faculty = FacultyTextBox.Text;
            string group = GroupTextBox.Text;
            Student newStudent = new Student(firstName, patronemic, lastName, dateOfBirth,
           faculty, group);
            students.Add(newStudent);
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsListBox.SelectedItems.Count > 0)
            {
                List<Student> selectedStudents = new List<Student>();
                foreach (var item in StudentsListBox.SelectedItems)
                {
                    selectedStudents.Add((Student)item);
                }
                foreach (var student in selectedStudents)
                {
                    students.Remove(student);
                }
            }
        }
        private void StudentsListBox_SelectionChanged(object sender,
       SelectionChangedEventArgs e)
        {
            if (StudentsListBox.SelectedItem != null)
            {
                Student selectedStudent = (Student)StudentsListBox.SelectedItem;
                FirstNameTextBox.Text = selectedStudent.FirstName;
                PatronymicTextBox.Text = selectedStudent.Patronymic;
                LastNameTextBox.Text = selectedStudent.LastName;
                DateOfBirthTextBox.Text = selectedStudent.DateOfBirth;
                FacultyTextBox.Text = selectedStudent.Faculty;
                GroupTextBox.Text = selectedStudent.Group;
            }
            else
            {
                FirstNameTextBox.Clear();
                PatronymicTextBox.Clear();
                LastNameTextBox.Clear();
                DateOfBirthTextBox.Clear();
                FacultyTextBox.Clear();
                GroupTextBox.Clear();
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteButton_Click(this, new RoutedEventArgs());
            }
            else if (e.Key == Key.A && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (StudentsListBox.SelectionMode == SelectionMode.Multiple ||
               StudentsListBox.SelectionMode == SelectionMode.Extended)
                {
                    StudentsListBox.SelectAll();
                }
            }
            else if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (StudentsListBox.SelectedItem != null)
                {
                    Student selectedStudent = (Student)StudentsListBox.SelectedItem;
                    Clipboard.SetText(selectedStudent.ToString());
                }
            }
            else if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                string copiedText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(copiedText))
                {
                    string[] studentInfo = copiedText.Split(' ');
                    if (studentInfo.Length == 2)
                    {
                        string firstName = studentInfo[0];
                        string lastName = studentInfo[1];
                        Student copiedStudent = new Student(firstName, "", lastName, "", "",
                       "");
                        students.Add(copiedStudent);
                    }
                }
            }
            else if (e.Key == Key.A && Keyboard.Modifiers == ModifierKeys.Control)
            {
                StudentsListBox.SelectAll();
            }
            else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                LoadFromFile();
            }
            else if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                SaveToFile();
            }
        }
        private void LoadFromFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "students";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            if (dlg.ShowDialog() == true)
            {
                string filePath = dlg.FileName;
                using (System.IO.StreamReader file = new System.IO.StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] studentInfo = line.Split(',');
                        if (studentInfo.Length == 6)
                        {
                            string firstName = studentInfo[0].Trim();
                            string patronymic = studentInfo[1].Trim();
                            string lastName = studentInfo[2].Trim();
                            string dateOfBirth = studentInfo[3].Trim();
                            string faculty = studentInfo[4].Trim();
                            string group = studentInfo[5].Trim();
                            Student loadedStudent = new Student(firstName, patronymic,
                           lastName, dateOfBirth, faculty, group);
                            students.Add(loadedStudent);
                        }
                    }
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile();
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFromFile();
        }
        private void SaveToFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "students";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            if (dlg.ShowDialog() == true)
            {
                string filePath = dlg.FileName;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                {
                    foreach (var student in students)
                    {
                        file.WriteLine(student.ToString());
                    }
                }
            }
        }
        private void CopyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (StudentsListBox.SelectedItem != null)
            {
                Student selectedStudent = (Student)StudentsListBox.SelectedItem;
                Clipboard.SetText(selectedStudent.ToString());
            }
        }
        private void PasteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string copiedText = Clipboard.GetText();
            if (!string.IsNullOrEmpty(copiedText))
            {
                string[] studentInfo = copiedText.Split(',');
                if (studentInfo.Length == 6)
                {
                    string firstName = studentInfo[0].Trim();
                    string patronymic = studentInfo[1].Trim();
                    string lastName = studentInfo[2].Trim();
                    string dateOfBirth = studentInfo[3].Trim();
                    string faculty = studentInfo[4].Trim();
                    string group = studentInfo[5].Trim();
                    Student copiedStudent = new Student(firstName, patronymic, lastName,
                   dateOfBirth, faculty, group);
                    students.Add(copiedStudent);
                }
            }
        }
    }
    public class Student
    {
        public string FirstName { get; }
        public string Patronymic { get; }
        public string LastName { get; }
        public string DateOfBirth { get; }
        public string Faculty { get; }
        public string Group { get; }
        public Student(string firstName, string patronemic, string lastName, string
       dateOfBirth, string faculty, string group)
        {
            FirstName = firstName;
            Patronymic = patronemic;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Faculty = faculty;
            Group = group;
        }
        public override string ToString()
        {
            return $"{FirstName},{Patronymic},{LastName},{DateOfBirth},{Faculty},{Group}";
        }
    }
}
