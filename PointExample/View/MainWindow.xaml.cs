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

namespace PointExample.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dbConnBtn_Click(object sender, RoutedEventArgs e)
        {
            /// инициализируем класс логики подключения к БД/серверу
            DbConnWindow dbConnWindow = new DbConnWindow();
            /// отображаем виджет подключения к БД/серверу
            dbConnWindow.Show();
        }

        private void pbUpdateData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pbDataGen_Click(object sender, RoutedEventArgs e)
        {
            DataGenerationWindow dgWindow = new DataGenerationWindow();
            dgWindow.Show();
        }
    }
}
