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
using System.Data;

using PointExample.Logicum;

/// <summary>
/// пространство имен Представление
/// </summary>
namespace PointExample.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Logic dbWorkLogic;

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            dbWorkLogic = new Logic();
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Подключение к БД"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void dbConnBtn_Click(object sender, RoutedEventArgs e)
        {
            /// инициализируем класс логики подключения к БД/серверу
            DbConnWindow dbConnWindow = new DbConnWindow();
            /// отображаем виджет подключения к БД/серверу
            dbConnWindow.Show();
        }

        private void pbDataGen_Click(object sender, RoutedEventArgs e)
        {
            DataGenerationWindow dgWindow = new DataGenerationWindow();
            dgWindow.Show();
        }

        private void pbUpdateData_Click(object sender, RoutedEventArgs e)
        {
            customerDataGen();
            orderDataGen();
        }

        private void customerDataGen()
        {

            UserGrid.DataContext = dbWorkLogic.getDataCustomers().DefaultView;
        }

        private void orderDataGen()
        {

            OrderGrid.DataContext = dbWorkLogic.getDataOrders().DefaultView;
        }

        private void UserGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            DataRow customerRow = ( ( DataRowView ) ( UserGrid.SelectedValue ) ).Row;

            OrderGrid.DataContext = dbWorkLogic.getDataCustomOrders( Convert.ToInt32( customerRow["ID"] ) ).DefaultView;
        }
    }
}
