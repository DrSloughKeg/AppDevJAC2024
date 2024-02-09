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

namespace Day02TempConvert
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputStr = TbxInput.Text;

            double input;
            if (!double.TryParse(inputStr, out input))
            {
                MessageBox.Show("Try using a real number");
                return;
            }

            bool inCels = InCels.IsChecked == true;
            bool inFahr = InFahr.IsChecked == true;
            bool inKelv = InKelv.IsChecked == true;
            bool outCels = OutCels.IsChecked == true;
            bool outFahr = OutFahr.IsChecked == true;
            bool outKelv = OutKelv.IsChecked == true;



            double output;
            if ((inCels && outCels) || (inFahr && outFahr) || (inKelv && outKelv)) {
                TbxOut.Text = inputStr;
            }
            else if (inCels && outFahr)
            {
                output = (input * 9 / 5) + 32;
                TbxOut.Text = output.ToString();
            } else if (inCels && outKelv)
            {
                output = input + 273.15;
                TbxOut.Text = output.ToString();
            } else if (inFahr && outCels)
            {
                output = (input - 32) * 5 / 9;
                TbxOut.Text = output.ToString();
            } else if (inFahr && outKelv)
            {
                output = (input - 32) * 5 / 9 + 273.15;
                TbxOut.Text = output.ToString();
            } else if (inKelv && outCels)
            {
                output = input - 273.15;
                TbxOut.Text = output.ToString();
            } else if (inKelv && outFahr)
            {
                output = (input - 273.15) * 9 / 5 + 32;
                TbxOut.Text = output.ToString();
            }

        }
    }
}
