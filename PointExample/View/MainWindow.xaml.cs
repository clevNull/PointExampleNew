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
    /// класс обозревателя покупателей и заказов
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// инициализируем класс логики работы с таблицами в БД
        /// </summary>
        Logic dbWorkLogic = new Logic();

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public MainWindow()
        {
            /// инициализируем основные компоненты виджета
            InitializeComponent();
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Подключение к БД"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void dbConnBtn_Click(object sender, RoutedEventArgs e)
        {
            /// инициализируем класс подключения к БД/серверу
            DbConnWindow dbConnWindow = new DbConnWindow();
            /// отображаем виджет подключения к БД/серверу
            dbConnWindow.Show();
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Формирование данных"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void pbDataGen_Click(object sender, RoutedEventArgs e)
        {
            /// инициализируем класс генерации данных для БД
            DataGenerationWindow dgWindow = new DataGenerationWindow();
            /// отображаем виджет генерации данных для БД
            dgWindow.Show();
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Загрузить данные"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void pbUpdateData_Click(object sender, RoutedEventArgs e)
        {
            /// наполняем окно отображения покупателей
            customerDataGen();
            /// наполняем окно отображения заказов
            orderDataGen();
        }

        /// <summary>
        /// метод наполнения окна отображения покупателей
        /// </summary>
        private void customerDataGen()
        {
            /// задаем контент окна отображения покупателей
            UserGrid.DataContext = dbWorkLogic.getDataCustomers().DefaultView;
        }

        /// <summary>
        /// метод наполнения окна отображения заказов
        /// </summary>
        private void orderDataGen()
        {
            /// задаем контент окна отображения заказов
            OrderGrid.DataContext = dbWorkLogic.getDataOrders().DefaultView;
        }

        /// <summary>
        /// метод обработки выбора пользователя
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void UserGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            /// Получаем номер выделенной строчки
            DataRow customerRow = ( ( DataRowView ) ( UserGrid.SelectedValue ) ).Row;
            /// задаем контент окна отображения заказов
            OrderGrid.DataContext = dbWorkLogic.getDataCustomOrders( Convert.ToInt32( customerRow["ID"] ) ).DefaultView;
        }
    }
}
