using System;
using System.Collections.Generic;
using System.IO;
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

namespace Day03ListViewGridView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFile = @"..\..\people.txt";
        List<Person> peopleList = new List<Person>();

        public MainWindow()
        {
            ReadAllPeopleFromFile();
            InitializeComponent();
            LblStatus.Text = "Hello!";
            LvPeople.ItemsSource = peopleList;
        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //validation thing
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null)
            {
                ResetFields();
                return;
            } else
            {
                TbxName.Text = currSelPer.Name;
                TbxAge.Text = currSelPer.Age.ToString();
            }
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (!ArePersonInputsValid(TbxName.Text, TbxAge.Text)) return;
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age);
            peopleList.Add(new Person(name, age));
            LvPeople.Items.Refresh();
            ResetFields();
            LblStatus.Text = "Person added!";
        }

        private void BtnDeletePerson_Click(Object sender, RoutedEventArgs e)
        {
            //Person currSelPer = LvPeople.SelectedIndex;
            //if person null bad

            peopleList.RemoveAt(LvPeople.SelectedIndex);
            LvPeople.Items.Refresh();
            ResetFields();
            LblStatus.Text = "Person deleted!";
        }

        private void BtnUpdatePerson_Click(Object obj, RoutedEventArgs e)
        {
            if (!ArePersonInputsValid(TbxName.Text, TbxAge.Text)) return;
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text,out int age);

            //create temp person
            Person selectedPerson = peopleList[LvPeople.SelectedIndex];
            //assign new values
            selectedPerson.Age = age;
            selectedPerson.Name = name;
            //assign those values to selected index
            peopleList[LvPeople.SelectedIndex] = selectedPerson;

            LvPeople.Items.Refresh();
            ResetFields();
            LblStatus.Text = "Person updated!";
        }

        private void MiExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".text"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                SaveAllPeopleToFile(filename);
                
            }
            LblStatus.Text = "Data exported!";
        }

        private void ResetFields()
        {
            LblError.Visibility = Visibility.Hidden;
            TbxAge.Text = "";
            TbxName.Text = "";
        }

        private bool ArePersonInputsValid(string name, string ageStr)
        {
            int.TryParse(ageStr, out int age);
            if (name.Length < 2 || name.Length > 50 || age < 0 || age > 100)
            {
                LblStatus.Text = "Invalid Input!";
                LblError.Visibility = Visibility.Visible;
                return false;
            } else
            {
                return true;
            }
                                        
        }

        private bool ArePersonInputsValid2()
        {
            List<string> errorList = new List<string>();

            if (!Person.IsNameValid(TbxName.Text, out string errorName)) 
            {
                errorList.Add(errorName);
            }
            if (errorList.Count > 0) 
            {
                MessageBox.Show(this, String.Join("\n", errorList), "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void ReadAllPeopleFromFile()
        {
            try //greg adds validation again before saving, using error list
            {
                if (!File.Exists(DataFile))
                {
                    Console.WriteLine("Warning: no data file found at " + DataFile);
                    return; // it's okay if the file does not exist yet
                }
                string[] linesArray = File.ReadAllLines(DataFile); // ex IOException, SystemException
                foreach (string line in linesArray)
                {
                    try
                    {
                        string[] data = line.Split(';');
                        if (data.Length != 2)
                        {
                            throw new FormatException("Invalid number of items");
                            // or: Console.WriteLine("Error..."); continue;
                        }
                        string name = data[0];
                        int age = int.Parse(data[1]); // ex FormatException
                        Person person = new Person(name, age); // ex ArgumentException
                        peopleList.Add(person);
                    }
                    catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
                    {
                        Console.WriteLine($"Error (skipping line): {ex.Message} in:\n  {line}");
                    }
                }
            }
            // catch (Exception ex) when (ex is IOException || ex is SystemException)
            catch (SystemException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        private void SaveAllPeopleToFile(string path)
        {
            try
            {
                List<string> linesList = new List<String>();
                foreach (Person person in peopleList)
                {
                    linesList.Add($"{person.Name};{person.Age}");
                }
                File.WriteAllLines(path, linesList); // ex IOException, SystemException
            }
            // catch (Exception ex) when (ex is IOException || ex is SystemException)
            catch (SystemException ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveAllPeopleToFile(DataFile);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            peopleList.Sort((x, y) => string.Compare(x.Name, y.Name));
            LvPeople.Items.Refresh();
            LblStatus.Text = "Sorted by Name.";
        }
        private void SortByAge_Click(object sender, RoutedEventArgs e)
        {
            peopleList.Sort((x, y) => x.Age.CompareTo(y.Age));
            LvPeople.Items.Refresh();
            LblStatus.Text = "Sorted by Age.";
        }
    }
}
