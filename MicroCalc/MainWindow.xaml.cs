using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MicroCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int daysCount;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double loan = double.Parse(tbLoan.Text);
                double firstDay = loan;
                daysCount = int.Parse(tbDays.Text);

                if (daysCount > 365)
                {
                    throw new Exception("Count days > 365");
                }

                double fullPercent = 0;
                string details = "";
                double prevDay = loan;
                var percents = tbPercents.Text.Split(' ');

                int today = 1;
                foreach (var percent in percents)
                {
                    if (double.Parse(percent) > 1)
                    {
                        throw new Exception("Percent > 1");
                    }
                    fullPercent += double.Parse(percent);
                    details += $"Day {today++} : " +
                        $"{loan * (1 + double.Parse(percent) / 100) - prevDay} = " +
                        $"{loan * (1 + double.Parse(percent) / 100)}\n";
                    prevDay = loan * (1 + double.Parse(percent) / 100);
                    loan = prevDay;
                }

                tbItogSum.Text = $"{firstDay + loan - firstDay}";
                tbOverpay.Text = $"{loan - firstDay}";
                tbEffectRate.Text = $"{fullPercent / daysCount}";

                tbDetails.Text = details;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = "Text file (*.txt)|*.txt";
            if (sfDialog.ShowDialog() == true)
                File.WriteAllText(sfDialog.FileName, $"{tbDetails.Text}" +
                    $"Полная выплата {tbItogSum.Text}\n" +
                    $"Переплата {tbOverpay.Text}\n" +
                    $"Эффективная ставка {tbEffectRate.Text}");
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofDialog = new OpenFileDialog();
                if (ofDialog.ShowDialog() == true)
                {
                    string[] temp = 
                        File.ReadAllText(ofDialog.FileName).Split('\n');
                    tbLoan.Text = temp[0].Trim();
                    tbDays.Text = temp[1].Trim();
                    tbPercents.Text = temp[2];
                }
            }
            catch 
            {
                MessageBox.Show("Неверный файл");
            }
                
        }
    }
}
