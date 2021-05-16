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
        ConnectionLogic DbConn = new ConnectionLogic();
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
                try {
                    /// подключаемся к серверу
                    DbConn.SrvConnect(addrBox.Text, portBox.Text, userNameBox.Text, passwordBox.Password);
                    /// выдаем сообщение об успешном подключении
                    MessageBox.Show(
                        "Подключение установлено!",
                        "Подключение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                /// отлавливаем исключение
                catch (Exception ex)
                /// выдаем сообщение
                { MessageBox.Show(ex.Message, "Подключение", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            /// выдаем сообщение об некорректности заполнения полей виджета
            else { MessageBox.Show(
                "Поля, необходимые для подключения, заполнены не корректно.",
                "Подключение", MessageBoxButton.OK, MessageBoxImage.Error );
            }
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
                    /// выдаем сообщение об успешном создание БД
                    MessageBox.Show(
                        "Создание БД \"" + nameDbBox.Text + "\" завершено успешно!",
                        "Создание БД", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                /// отлавливаем исключение
                catch (Exception ex)
                /// выдаем сообщение
                { MessageBox.Show(ex.Message, "Создание БД", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            /// выдаем сообщение об некорректности заполнения полей виджета
            else { MessageBox.Show(
                "Наименование БД заполнено не корректно.",
                "Создание БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                try { logicClass.DbDeleting(nameDbBox.Text);
                    /// выдаем сообщение об успешном удалении БД
                    MessageBox.Show(
                        "Удаление БД \"" + nameDbBox.Text + "\" завершено успешно!",
                        "Удаление БД", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                /// отлавливаем исключение
                catch (Exception ex)
                /// выдаем исключение
                { MessageBox.Show(ex.Message, "Удаление БД", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
    }
}
