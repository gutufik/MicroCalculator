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
        int loan;
        int daysCount;
        float[] percents;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loan = int.Parse(tbLoan.Text);
                daysCount = int.Parse(tbDays.Text);

                if (daysCount > 365)
                {
                    throw new Exception();
                }

                percents = new float[daysCount];
                float fullPercent = 0;
                tbDetails.Text = "";

                var perc = tbPercents.Text.Split(' ');
                for (int i = 0; i < daysCount; ++i)
                {
                    if (float.Parse(perc[i]) > 1)
                    {
                        throw new Exception();
                    }
                    percents[i] = float.Parse(perc[i]);
                    fullPercent += float.Parse(perc[i]);
                    tbDetails.Text += $"{i + 1} - {loan * (1 + fullPercent / 100)}\n";
                }

                tbItogSum.Text = $"{loan * (1 + fullPercent / 100)}";
                tbOverpay.Text = $"{fullPercent / 100 * loan}";
                tbEffectRate.Text = $"{fullPercent / daysCount}";
            }
            catch
            {
                MessageBox.Show("Неверные данные");
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
                    string[] temp = File.ReadAllText(ofDialog.FileName).Split('\n');
                    tbLoan.Text = temp[0];
                    tbDays.Text = temp[1];
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
