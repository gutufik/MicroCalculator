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
            int loan = 0;
            int daysCount = 0;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loan = int.Parse(tbLoan.Text);
            daysCount = int.Parse(tbDays.Text);
            percents = new float[daysCount];


            var perc = tbPercents.Text.Split(' ');
            for (int i = 0; i < daysCount; ++i)
            {
                percents[i] = int.Parse(perc[i]) / 10;
            }

            tbItogSum.Text = "grgr \n gg";

        }
    }
}
