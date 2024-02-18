using Microsoft.Win32;
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

namespace Day05ToDos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new TodoDbContext();
                LvTodos.ItemsSource = Globals.dbContext.Todos.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DueDate.SelectedDate == null)
                {
                    throw new ArgumentException("Please select a due date");
                }
                Todo newToDo = new Todo(TaskInput.Text, (int)DiffSlr.Value, (DateTime)DueDate.SelectedDate, (Todo.StatusEnum)CmbBxSt.SelectedIndex);
                Globals.dbContext.Todos.Add(newToDo); //SystemException
                Globals.dbContext.SaveChanges(); //SystemException
                LvTodos.ItemsSource = Globals.dbContext.Todos.ToList(); //SystemException

                ResetFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Todo curSelTodo = LvTodos.SelectedItem as Todo;
            if (curSelTodo == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.dbContext.Todos.Remove(curSelTodo); //SystemException
                Globals.dbContext.SaveChanges(); //SystemException
                LvTodos.ItemsSource = Globals.dbContext.Todos.ToList(); //SystemException
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Todo curSelTodo = LvTodos.SelectedItem as Todo;
            if (curSelTodo == null) return;
            try
            {
                curSelTodo.Task = TaskInput.Text; //ArgumentException
                curSelTodo.Difficulty = (int)DiffSlr.Value; //ArgumentException
                curSelTodo.DueDate = (DateTime)DueDate.SelectedDate; //ArgumentException
                curSelTodo.Status = (Todo.StatusEnum)CmbBxSt.SelectedIndex; //ArgumentException
                Globals.dbContext.SaveChanges(); //SystemException
                LvTodos.ItemsSource = Globals.dbContext.Todos.ToList(); //SystemException
                ResetFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() != true) return;

            List<Todo> toExport = Globals.dbContext.Todos.ToList();
            List<string> lines = new List<string>();
            foreach (Todo t in toExport)
            {
                lines.Add($"{t.Task};{t.Difficulty};{t.DueDate};{t.Status}");
            }
            try
            {
                File.WriteAllLines(saveFileDialog.FileName, lines);
                MessageBox.Show(this, "Export complete!", "Export Status", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Export failed\n" + ex.Message, "Export Status", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LvTodos_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            Todo curSelTodo = LvTodos.SelectedItem as Todo;
            if (curSelTodo == null) { ResetFields(); }
            else
            {
                BtnDelete.IsEnabled = true;
                BtnUpdate.IsEnabled = true;
                TaskInput.Text = curSelTodo.Task;
                DiffSlr.Value = curSelTodo.Difficulty;
                DueDate.SelectedDate = curSelTodo.DueDate;
                CmbBxSt.SelectedIndex = (int)curSelTodo.Status;
            }

        }

        private void ResetFields()
        {
            TaskInput.Text = "";
            DiffSlr.Value = 1;
            DueDate.SelectedDate = DateTime.Today;
            CmbBxSt.SelectedIndex = 0;
            BtnDelete.IsEnabled = false;
            BtnUpdate.IsEnabled = false;

        }
    }
}
