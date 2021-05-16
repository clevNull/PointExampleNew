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
using System.Threading;

using PointExample.Logicum;

/// <summary>
/// пространство имен Представление
/// </summary>
namespace PointExample.View
{
    /// <summary>
    /// класс описания DataGenerationWindow
    /// </summary>
    public partial class DataGenerationWindow : Window
    {
        /// <summary>
        /// объявляем объект логики подключения к БД/серверу
        /// </summary>
        ConnectionLogic mDBConn_;
        /// <summary>
        /// объявляем объект логики работы с таблицами в БД
        /// </summary>
        Logic mLogicClass_;
        /// <summary>
        /// объявляем объект статуса создания заказчиков
        /// </summary>
        IProgress < int > mNumCustomer_;
        /// <summary>
        /// объявляем объект статуса создания заказов
        /// </summary>
        IProgress < int > mNumOrder_;

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public DataGenerationWindow()
        {
            /// инициализируем основные компоненты виджета
            InitializeComponent();

            /// задаем объект статуса создания заказчиков
            mNumCustomer_ = new Progress < int > ( numCustomer => { setCustomersStatus( numCustomer ); } );
            /// задаем объект статуса создания заказов
            mNumOrder_ = new Progress < int > ( numOrder => { setOrdersStatus( numOrder ); } );
        }

        /// <summary>
        /// метод обработки изменения значения слайдера "Заказы"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void CountSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /// задаем значение текстового поля для отображения числа заказов
            OrderBlock.Text = Convert.ToInt32(CountSldr.Value).ToString();
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Покупатели"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void CustomerSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /// задаем значение текстового поля для отображения числа заказчиков
            CustomerBlock.Text = Convert.ToInt32(CustomerSldr.Value).ToString();
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Сформировать"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void DataGenBtn_Click(object sender, RoutedEventArgs e)
        {
            /// задаем объект логики подключения к БД/серверу
            mDBConn_ = new ConnectionLogic();
            /// задаем объект логики работы с таблицами в БД
            mLogicClass_ = new Logic( Convert.ToInt32( CustomerBlock.Text ), Convert.ToInt32( OrderBlock.Text ), mNumCustomer_, mNumOrder_ );

            /// заполняем таблицы заказчиков/заказов
            try { mLogicClass_.FillTablesAsync(); }
            /// отлавливаем исключение
            catch (Exception ex)
            /// выдаем сообщение
            { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// метод задания значения статуса создания заказчиков
        /// </summary>
        /// <param name="numCustomer">номер заказчика</param>
        private void setCustomersStatus( int numCustomer )
        {
            /// инициализируем колличесво заказчиков
            int countCustomers = Convert.ToInt32( CustomerBlock.Text );
            /// проверка на корректность максимального значения progress bar
            if ( pbStatus.Maximum != countCustomers ) pbStatus.Maximum = countCustomers;
            /// задаем текущее значение progress bar
            pbStatus.Value = numCustomer;

            /// задание значения статуса создания заказчиков
            tbStatus.Text = "Прогресс создания покупателей: " + numCustomer + "/" + countCustomers;
        }

        /// <summary>
        /// метод задания значения статуса создания заказов
        /// </summary>
        /// <param name="numOrder">номер заказа</param>
        private void setOrdersStatus( int numOrder )
        {
            /// инициализируем колличесво заказов
            int countOrders = Convert.ToInt32( OrderBlock.Text );
            /// проверка на корректность максимального значения progress bar
            if ( pbStatus.Maximum != countOrders ) pbStatus.Maximum = countOrders;
            /// задаем текущее значение progress bar
            pbStatus.Value = numOrder;
            
            /// задание значения статуса создания заказов
            tbStatus.Text = "Прогресс создания заказов: " + numOrder + "/" + countOrders;
        }
    }
}
