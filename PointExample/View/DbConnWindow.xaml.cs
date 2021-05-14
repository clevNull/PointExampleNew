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

using PointExample;
using PointExample.Logicum;

/// <summary>
/// пространство имен Представление
/// </summary>
namespace PointExample.View
{
    /// <summary>
    /// класс описания DbConnWindow
    /// </summary>
    public partial class DbConnWindow : Window
    {
        /// <summary>
        /// инициализируем класс логики подключения к БД/серверу
        /// </summary>
        ConncetionLogic DbConn = new ConncetionLogic();
        /// <summary>
        /// инициализируем класс логики работы с таблицами в БД
        /// </summary>
        Logic logicClass = new Logic();

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public DbConnWindow()
        {
            /// инициализируем основные компоненты виджета
            InitializeComponent();
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Подключиться"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void SrvConnBtn_Click(object sender, RoutedEventArgs e)
        {
            /// проверка на пустые поля { хост, юзер, пароль }
            if (addrBox.Text != "" &&
            userNameBox.Text != "" &&
            passwordBox.Password != "")
            {
                /// подключаемся к серверу
                try { DbConn.SrvConnect(addrBox.Text, portBox.Text, userNameBox.Text, passwordBox.Password); }
                /// отлавливаем исключение
                catch (Exception ex)
                /// выдаем сообщение
                { MessageBox.Show(ex.Message); }
            }
            /// выдаем сообщение об некорректности заполнения полей виджета
            else { MessageBox.Show(
                "Поля, необходимые для подключения заполнены не верно/ не заполнены.",
                "Подключение к БД", MessageBoxButton.OK, MessageBoxImage.Error ); }
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Создать"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void CrtDbBtn_Click(object sender, RoutedEventArgs e)
        {
            /// проверка на пустые поля { наименование }
            if (nameDbBox.Text != "")
            {
                try
                {
                    /// создаем БД
                    logicClass.DbCreation(nameDbBox.Text);
                    /// подключаемся к БД
                    DbConn.DbConnect(addrBox.Text, portBox.Text, nameDbBox.Text, userNameBox.Text, passwordBox.Password);
                    /// создаем таблицы в БД
                    logicClass.TablesCreation();
                }
                /// отлавливаем исключение
                catch (Exception ex)
                /// выдаем сообщение
                { MessageBox.Show(ex.Message); }
            }
            /// выдаем сообщение об некорректности заполнения полей виджета
            else { MessageBox.Show("наименование БД, заполнено не верно/ не заполнено."); }
        }

        /// <summary>
        /// метод обработки нажатия на кнопку "Удалить"
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void DelDbBtn_Click(object sender, RoutedEventArgs e)
        {
            /// проверка на пустые поля { наименование }
            if (nameDbBox.Text != "")
            {
                /// удаляем БД
                try { logicClass.DbDeleting(nameDbBox.Text); }
                /// отлавливаем исключение
                catch (Exception ex)
                /// выдаем исключение
                { MessageBox.Show(ex.Message); }
            }
        }
    }
}
