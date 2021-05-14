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
        ConncetionLogic mDBConn_;
        /// <summary>
        /// объявляем объект логики работы с таблицами в БД
        /// </summary>
        Logic mLogicClass_;
        /// <summary>
        /// объявляем объект статуса создания заказчиков/заказов
        /// </summary>
        IProgress < string > mProgressStatus_;

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public DataGenerationWindow()
        {
            /// инициализируем основные компоненты виджета
            InitializeComponent();

            /// задаем объект статуса создания заказчиков/заказов
            mProgressStatus_ = new Progress<string>(msg => { ProcessStatusOut(msg); });
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Подключение к БД"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void DbConnBtn_Click(object sender, RoutedEventArgs e)
        {
            /// инициализируем класс логики подключения к БД/серверу
            DbConnWindow dbConnWindow = new DbConnWindow();
            /// отображаем виджет подключения к БД/серверу
            dbConnWindow.Show();
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
            /// задаем значение текстового поля для отображения числа Покупателей
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
            mDBConn_ = new ConncetionLogic();
            /// задаем объект логики работы с таблицами в БД
            mLogicClass_ = new Logic(Convert.ToInt32(CustomerBlock.Text), Convert.ToInt32(OrderBlock.Text), mProgressStatus_);
            
            /// заполняем таблицы покупателей/заказов
            try { mLogicClass_.FillTablesAsync(); }
            /// отлавливаем исключение
            catch (Exception ex)
            /// выдаем сообщение
            { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// метод задания значения статуса создания покупателей/заказов
        /// </summary>
        /// <param name="message"></param>
        private void ProcessStatusOut( string message )
        {
            /// задание значения статуса создания покупателей/заказов
            tbStatus.Text = message;
        }
    }
}
