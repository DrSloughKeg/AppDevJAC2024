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

namespace Day02AllInputs
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

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            if (name == "")
            {
                MessageBox.Show("Name must not be empty", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //also ugly fix
            string age;
            if (Rbn1835.IsChecked == true)
            {
                age = "18-35";
            } else if (Rbn18Under.IsChecked == true)
            {
                age = "under 18";
            } else if (Rbn35Up.IsChecked == true) { age = "over 35"; }
            else
            {
                MessageBox.Show("How did you unselect that?", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //very ugly fix
            string pets = "";
            if (CbxCat.IsChecked == true || CbxDog.IsChecked == true || CbxOther.IsChecked == true) {
                bool first = true;
                if (CbxCat.IsChecked == true)
                {
                    pets += "Cat";

                    first = false;
                }
                if (CbxDog.IsChecked == true)
                {
                    if (!first)
                    {
                        pets += ",";
                    }
                    pets += "Dog";
                    first = false;
                }
                if (CbxOther.IsChecked == true)
                {
                    if (!first)
                    {
                        pets += ",";
                    }
                    pets += "Other";
                }
            }
            int caseStr = Cmb.SelectedIndex;
            string continent;
            switch (caseStr){
                case 0:
                    continent = "North America";
                    break;
                case 1:
                    continent = "South America";
                    break;
                case 2:
                    continent = "Europe";
                    break;
                case 3:
                    continent = "Africa";
                    break;
                case 4:
                    continent = "Asia";
                    break;
                case 5:
                    continent = "Australia";
                    break;
                default:
                    continent = "";
                    break;
            } 

            string temp = TbxTemp.Text;

            string info = name + ";" + age + ";";
            if (pets != "")
            {
                info += pets + ";";
            }
            info += continent + ";" + temp;

            string[] infoAry = { info };

            const string filePath = @"..\..\info.txt";
            try
            {

                File.WriteAllLines(filePath, infoAry);
            }
            catch (SystemException ex) { Console.WriteLine("Error writing to file: " + ex.Message); }

        }

    }
}
