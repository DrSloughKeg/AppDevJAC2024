using System;
using System.Collections.Generic;
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

namespace Day03ListView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age);
            LsvPeopleStrings.Items.Add($"{name} is {age} years old.");
            TbxAge.Text = "";
            TbxName.Text = "";

            //list of object
            LsvPeopleObjects.Items.Add(new Person(name, age));
        }
    }
}
