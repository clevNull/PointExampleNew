using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// пространство имен Модель
/// </summary>
namespace PointExample.Model
{
    /// <summary>
    /// класс хранения параметров подключения
    /// </summary>
    public class DbParams
    {
        /// <summary>
        /// параметр подключения к БД -> хост
        /// </summary>
        String host;
        /// <summary>
        /// параметр подключения к БД -> порт
        /// </summary>
        String port;
        /// <summary>
        /// параметр подключения к БД -> наименование БД
        /// </summary>
        String dbName;
        /// <summary>
        /// параметр подключения к БД -> наименование юзера
        /// </summary>
        String userName;
        /// <summary>
        /// параметр подключения к БД ->  пароль юзера
        /// </summary>
        String pass;

        /// <summary>
        /// метод получения/задания значения поля хост
        /// </summary>
        public String Host
        {
            /// метод получения значения поля хост
            get { return host; }
            /// метод задания значения поля хост
            set { host = value; }
        }
        /// <summary>
        /// метод получения/задания значения поля порт
        /// </summary>
        public String Port
        {
            /// метод получения значения поля порт
            get { return port; }
            /// метод задания значения поля порт
            set { port = value; }
        }
        /// <summary>
        /// метод получения/задания значения поля наименования БД
        /// </summary>
        public String DbName
        {
            /// метод получения значения поля наименования БД
            get { return dbName; }
            /// метод задания значения поля наименования БД
            set { dbName = value; }
        }
        /// <summary>
        /// метод получения/задания значения поля наименования юзера
        /// </summary>
        public String UserName
        {
            /// метод получения значения поля наименования юзера
            get { return userName; }
            /// метод задания значения поля наименования юзера
            set { userName = value; }
        }
        /// <summary>
        /// метод получения/задания значения поля пароль юзера
        /// </summary>
        public String Pass
        {
            /// метод получения значения поля пароль юзера
            get { return pass; }
            /// метод задания значения поля пароль юзера
            set { pass = value; }
        }
    }

    /// <summary>
    /// класс подключения к БД/серверу
    /// </summary>
    public class ConnectionClass
    {
        /// <summary>
        /// метод подключения к серверу (для создания БД)
        /// </summary>
        /// <param name="dbParams">параметры подключения к БД</param>
        /// <returns>объект подключения к БД</returns>
        public static SqlConnection ConnectToServer (DbParams dbParams)
        {
            /// инициализируем новый объект подключения
            SqlConnection connection = new SqlConnection();

            /// инициализируем строку подключения к серверу
            string connStr = @"Data Source=" + dbParams.Host + ","
                + dbParams.Port + ";" +
                "Network Library=DBMSSOCN;" + 
                "User=" + dbParams.UserName + ";" +
                "Password=" + dbParams.Pass + ";";
                
            try
            {
                /// задаем запрос подключения к серверу
                connection.ConnectionString = connStr;
                /// создаем подключение к серверу
                connection.Open();
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
            /// выполняем очистку всех ресурсов, выделенных в блоке try
            finally
            {
                /// если подключение открыто -> закрываем подключение
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            /// возвращаем объект подключения
            return connection;
        }

        /// <summary>
        /// метод подключения к БД (для создания таблиц)
        /// </summary>
        /// <param name="dbParams">параметры подключения к БД</param>
        /// <returns></returns>
        public static SqlConnection ConnectToDb(DbParams dbParams)
        {
            /// инициализируем новый объект подключения
            SqlConnection connection = new SqlConnection();

            /// инициализируем строку подключения к БД
            string connStr = @"Data Source=" + dbParams.Host + ","
                + dbParams.Port + ";" +
                "Initial Catalog = " + dbParams.DbName + ";" +
                "Network Library=DBMSSOCN;" +
                "User=" + dbParams.UserName + ";" +
                "Password=" + dbParams.Pass + ";";

            try
            {
                /// задаем запрос подключения к БД
                connection.ConnectionString = connStr;
                /// создаем подключение к БД
                connection.Open();
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
            /// выполняем очистку всех ресурсов, выделенных в блоке try
            finally
            {
                /// если подключение открыто -> закрываем подключение
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            /// возвращаем объект подключения
            return connection;
        }

        /// <summary>
        /// метод закрытия подключения к БД/серверу
        /// </summary>
        /// <param name="connection">объект подключения</param>
        public static void CloseConnection (SqlConnection connection)
        {
            /// если подключение открыто -> закрываем подключение
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

    /// <summary>
    /// класс работы с БД
    /// </summary>
    public class DBWorker
    {
        /// <summary>
        /// метод создания БД
        /// </summary>
        /// <param name="dbName">наименование БД</param>
        public void CreateDB (string dbName)
        {
            /// инициализируем строку создания БД
            string str = "CREATE DATABASE " + dbName;
            /// инициализируем запрос создания БД
            SqlCommand createDbComm = new SqlCommand(str, App.SrvConn);
            
            try
            {
                /// если подключение закрыто -> открываем подключение
                if (App.SrvConn.State != ConnectionState.Open)
                    App.SrvConn.Open();
                /// выполняем запрос создания БД
                createDbComm.ExecuteNonQuery();
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
            /// выполняем очистку всех ресурсов, выделенных в блоке try
            finally
            {
                /// если подключение открыто -> закрываем подключение
                if (App.SrvConn.State == ConnectionState.Open)
                    App.SrvConn.Close();
            }
        }

        /// <summary>
        /// метод удаления БД
        /// </summary>
        /// <param name="dbName">наименование БД</param>
        public void DeleteDb (string dbName)
        {
            /// инициализируем строку удаления БД
            string cmdText = "DROP DATABASE IF EXISTS " + dbName;
            /// инициализируем запрос удаления БД
            SqlCommand sqlCmd = new SqlCommand(cmdText, App.SrvConn);

            try
            {
                /// если подключение закрыто -> открываем подключение
                if (App.SrvConn.State != ConnectionState.Open)
                    App.SrvConn.Open();
                /// выполняем запрос удаления БД
                sqlCmd.ExecuteNonQuery();
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
            /// выполняем очистку всех ресурсов, выделенных в блоке try
            finally
            {
                /// если подключение открыто -> закрываем подключение
                if (App.SrvConn.State == ConnectionState.Open)
                    App.SrvConn.Close();
            }

        }

        /// <summary>
        /// метод выполнения запроса в БД ( INSERT )
        /// </summary>
        /// <param name="conn">объект подключения</param>
        /// <param name="queryStr">строка запроса</param>
        public void ExecuteQuery(SqlConnection conn, string queryStr)
        {
            /// инициализируем запрос в БД
            SqlCommand query = new SqlCommand(queryStr, conn);

            try
            {
                /// если подключение закрыто -> открываем подключение
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                /// выполняем запрос в БД
                query.ExecuteNonQuery();
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
            /// выполняем очистку всех ресурсов, выделенных в блоке try
            finally
            {
                /// если подключение открыто -> закрываем подключение
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// метод выполнения потоковых запросов в БД ( INSERT )
        /// </summary>
        /// <param name="conn">объект подключения</param>
        /// <param name="cmd">объект запроса</param>
        /// <param name="iter">текущая позиция</param>
        /// <param name="count">общее число</param>
        public void ExecuteStreamQuery(SqlConnection conn, SqlCommand cmd, decimal iter, int count)
        {
            try
            {
                /// если подключение закрыто -> открываем подключение
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                /// выполняем запрос в БД
                cmd.ExecuteNonQuery();
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
            /// выполняем очистку всех ресурсов, выделенных в блоке try
            finally
            /// если запрос является последним
            { if (iter == count)
                {
                    /// если подключение открыто -> закрываем подключение
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        /// <summary>
        /// метод выполнения запроса в БД ( SELECT )
        /// </summary>
        /// <param name="conn">объект подключения</param>
        /// <param name="queryStr">строка запроса</param>
        /// <returns>адаптер данных</returns>
        public SqlDataAdapter ExecuteDataQuery(SqlConnection conn, string queryStr)
        {
            /// инициализируем запрос в БД
            SqlCommand query = new SqlCommand(queryStr, conn);
            /// инициализируем адаптер данных для запроса в БД
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter( query );

            try
            {
                /// если подключение закрыто -> открываем подключение
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
            /// выполняем очистку всех ресурсов, выделенных в блоке try
            finally
            {
                /// если подключение открыто -> закрываем подключение
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            /// возвращаем адаптер данных
            return sqlDataAdapter;
        }
    } 
}
