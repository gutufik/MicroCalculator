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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loan = int.Parse(tbLoan.Text);
            daysCount = int.Parse(tbDays.Text);
            percents = new float[daysCount];
            float fullPercent = 0;

            tbDetails.Text = "";
            var perc = tbPercents.Text.Split(' ');
            for (int i = 0; i < daysCount; ++i)
            {
                percents[i] = float.Parse(perc[i]);
                fullPercent += float.Parse(perc[i]);
                tbDetails.Text += $"{i+1} - {loan * (1 + fullPercent / 100)}\n";
            }

            tbItogSum.Text = $"{loan * (1 + fullPercent / 100)}";
            tbOverpay.Text = $"{fullPercent / 100 * loan}";
            tbEffectRate.Text = $"{fullPercent / daysCount}";
            

        }

        private void tbDays_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (int.Parse(tbDays.Text) > 365)
            {
                lblDays.Foreground = Brushes.Red;
            }
        }
    }
}
