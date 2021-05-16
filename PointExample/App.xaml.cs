using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// пространство имен Базовое
/// </summary>
namespace PointExample
{
    /// <summary>
    /// класс Базовый
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///  объявляем подключения к БД
        /// </summary>
        static SqlConnection dbConn;
        /// <summary>
        /// объявляем подключения к серверу
        /// </summary>
        static SqlConnection srvConn;

        /// <summary>
        /// метод получения/задания объекта подключения к БД
        /// </summary>
        public static SqlConnection DbConn
        {
            /// метод получения объекта подключения к БД
            get { return dbConn; }
            /// метод задания объекта подключения к БД
            set { dbConn = value; }
        }

        /// <summary>
        /// метод получения/задания объекта подключения к серверу
        /// </summary>
        public static SqlConnection SrvConn
        {
            /// метод получения объекта подключения к серверу
            get { return srvConn; }
            /// метод задания объекта подключения к серверу
            set { srvConn = value; }
        }
    }
}
