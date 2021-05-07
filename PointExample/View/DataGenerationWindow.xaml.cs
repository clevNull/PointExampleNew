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
using System.Windows.Shapes;

namespace PointExample.View
{
    /// <summary>
    /// Логика взаимодействия для DataGenerationWindow.xaml
    /// </summary>
    public partial class DataGenerationWindow : Window
    {
        DbConnWindow dbConnWindow;
        public DataGenerationWindow()
        {
            InitializeComponent();
            dbConnWindow = new DbConnWindow();
        }

        private void DbConnBtn_Click(object sender, RoutedEventArgs e)
        {
            dbConnWindow.Show();
        }

        private void CountSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CountBlock.Text = Convert.ToInt32(CountSldr.Value).ToString();
        }

        private void CustomerSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CustomerBlock.Text = Convert.ToInt32(CustomerSldr.Value).ToString();
        }
    }
}
