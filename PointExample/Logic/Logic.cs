using PointExample.Model;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// пространство имен Логика
/// </summary>
namespace PointExample.Logicum
{
    /// <summary>
    /// класс логики подключения к БД/серверу
    /// </summary>
    public class ConncetionLogic
    {
        /// <summary>
        /// объект параметров подключения к БД/серверу
        /// </summary>
        DbParams dbParams;
        /// <summary>
        /// метод подключения к серверу
        /// </summary>
        /// <param name="host">хост</param>
        /// <param name="port">порт</param>
        /// <param name="username">наименование юзера</param>
        /// <param name="pass">пароль юзера</param>
        public void SrvConnect(string host, string port, string username, string pass)
        {
            /// инициализируем объект параметров подключения к серверу
            dbParams = new DbParams();
            dbParams.Host = host;
            dbParams.Port = port;
            dbParams.DbName = "";
            dbParams.UserName = username;
            dbParams.Pass = pass;
            /// задаем подключение к серверу
            try { App.SrvConn = ConnectionClass.ConnectToServer(dbParams); }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
        }

        public void DbConnect(string source, string port, string dbName, string username, string pass)
        {
            /// инициализируем объект параметров подключения к БД
            dbParams = new DbParams();
            dbParams.Host = source;
            dbParams.Port = port;
            dbParams.DbName = dbName;
            dbParams.UserName = username;
            dbParams.Pass = pass;
            /// задаем подключение к БД
            try { App.DbConn = ConnectionClass.ConnectToDb(dbParams); }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
        }
    }

    /// <summary>
    /// класс логики работы с таблицами в БД
    /// </summary>
    public class Logic
    {
        /// <summary>
        /// массив имен заказчиков ( мужские )
        /// </summary>
        string[] mMaleFirstNames_;
        /// <summary>
        /// массив отчеств заказчиков ( мужские )
        /// </summary>
        string[] mMaleMiddleNames_;
        /// <summary>
        /// массив фамилий заказчиков ( мужские )
        /// </summary>
        string[] mMaleLastNames_;

        /// <summary>
        /// массив имен заказчиков ( женские )
        /// </summary>
        string[] mFemaleFirstNames_;
        /// <summary>
        /// массив отчеств заказчиков ( женские )
        /// </summary>
        string[] mFemaleMiddleNames_;
        /// <summary>
        /// массив фамилий заказчиков ( женские )
        /// </summary>
        string[] mFemaleLastNames_;

        /// <summary>
        /// массив полов заказчиков ( мужской/женский )
        /// </summary>
        string[] mSex_;

        /// <summary>
        /// колличесво заказчиков/заказов
        /// </summary>
        int mCountUsers_, mCountOrders_;

        /// <summary>
        /// объект статуса создания заказчиков
        /// </summary>
        IProgress < int > mNumCustomer_;

        /// <summary>
        /// объект статуса создания заказов
        /// </summary>
        IProgress < int > mNumOrder_;

        /// <summary>
        /// объект работы с БД
        /// </summary>
        DBWorker mDBWorker = new DBWorker();

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public Logic() {}

        /// <summary>
        /// конструктор задания полей класса
        /// </summary>
        /// <param name="countUsers">колличесво заказчиков</param>
        /// <param name="countOrders">колличесво заказов</param>
        /// <param name="numCustomer">объект статуса создания заказчиков</param>
        /// <param name="numOrder">объект статуса создания заказов</param>
        public Logic( int countUsers, int countOrders, IProgress < int > numCustomer, IProgress < int > numOrder )
        {
            /// задаем массив имен заказчиков ( мужские )
            mMaleFirstNames_ = new string[] { "Александр", "Алексей", "Борис",
                "Виктор", "Георгий", "Григорий", "Константин",
                "Иван", "Евгений", "Сергей" };
            /// задаем массив отчеств заказчиков ( мужские )
            mMaleLastNames_ = new string[] { "Иванов", "Петров", "Сидоров",
                "Александров", "Капилевич", "Батыгин", "Самсонов",
                "Егоров", "Школьник", "Баранов"};
            /// задаем массив фамилий заказчиков ( мужские )
            mMaleMiddleNames_ = new string[] { "Александрович", "Алексеевич", "Борисович",
                "Викторович", "Георгиевич", "Григорьевич", "Константинович",
                "Иванович", "Евгеньевич", "Сергеевич" };

            /// задаем массив имен заказчиков ( женские )
            mFemaleFirstNames_ = new string[] { "Александра", "Варвава", "Вероника",
                "Галина", "Дарья", "Елена", "Жанна",
                "Зоя", "Ирина", "Лия" };
            /// задаем массив отчеств заказчиков ( женские )
            mFemaleLastNames_ = new string[] { "Иванова", "Петрова", "Сидорова",
                "Александрова", "Койхман", "Батыгина", "Самсонова",
                "Егорова", "Пустова", "Баранова"};
            /// задаем массив фамилий заказчиков ( женские )
            mFemaleMiddleNames_ = new string[] { "Александровна", "Алексеевна", "Борисовна",
                "Викторовна", "Георгиевна", "Григорьевна", "Константиновна",
                "Ивановна", "Евгеньевна", "Сергеевна" };

            /// задаем массив полов заказчиков ( мужской/женский )
            mSex_ = new string[] { "Мужской", "Женский" };

            /// задаем колличесво заказчиков
            mCountUsers_ = countUsers;
            /// задаем колличесво заказов
            mCountOrders_ = countOrders;
            /// задаем объект статуса создания заказов
            mNumCustomer_ = numCustomer;
            /// задаем объект статуса создания заказов
            mNumOrder_ = numOrder;
        }

        /// <summary>
        /// метод создания БД
        /// </summary>
        /// <param name="dbName">наименование БД</param>
        public void DbCreation(string dbName)
        {
            try
            {
                /// проверка на подключение к серверу
                if (App.SrvConn != null)
                {
                    /// проверяем наличие БД и удаляем при наличии
                    mDBWorker.DeleteDb(dbName);
                    /// создаем БД
                    mDBWorker.CreateDB(dbName);
                }
                /// выдаем сообщение об отстутвии подключения к серверу
                else { Exception ex = new Exception("ServerConnection is NULL"); throw ex; }
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
        }

        /// <summary>
        /// метод удаления БД
        /// </summary>
        /// <param name="dbName">наименование БД</param>
        public void DbDeleting(string dbName)
        {
            try
            {
                /// проверка на подключение к серверу
                if (App.SrvConn != null)
                {
                    /// проверяем наличие БД и удаляем при наличии
                    mDBWorker.DeleteDb(dbName);
                }
                /// выдаем сообщение об отстутвии подключения к серверу
                else { Exception ex = new Exception("ServerConnection is NULL"); throw ex; }
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
        }

        /// <summary>
        /// метод создания таблиц в БД
        /// </summary>
        public void TablesCreation()
        {
            /// инициализируем строку создания таблицы заказчиков
            string customerStr = "CREATE TABLE dbo.Customers (" +
                "ID DECIMAL CONSTRAINT CUST_PK PRIMARY KEY NONCLUSTERED," +
                "LastName NVARCHAR(64)," +
                "FirstName NVARCHAR(64)," +
                "MiddleName NVARCHAR(64)," +
                "Sex NVARCHAR(32)," +
                "BirthDate DATETIME," +
                "RegistrationDate DATETIME )";
            /// инициализируем строку создания таблицы заказов
            string ordersStr = "CREATE TABLE dbo.Orders (" +
                    "ID DECIMAL CONSTRAINT ORDER_PK PRIMARY KEY NONCLUSTERED," +
                    "CustomerID DECIMAL," +
                    "OrderDate DATETIME," +
                    "OrderName NVARCHAR(256)," +
                    "Price DECIMAL )";
            /// выполняем запрос в БД по созданию таблицы заказчиков
            mDBWorker.ExecuteQuery(App.DbConn, customerStr);
            /// выполняем запрос в БД по созданию таблицы заказов
            mDBWorker.ExecuteQuery(App.DbConn, ordersStr);
        }

        /// <summary>
        /// метод вызова методов ( асинхронный  )
        /// </summary>
        public async Task FillTablesAsync()
        {
            /// асинхронный вызов метода заполнения таблицы заказчиков
            await Task.Run(() => { FillCustomerTables(); });
            /// асинхронный вызов метода заполнения таблицы заказов
            await Task.Run(() => { FillOrderTables();    });
        }

        /// <summary>
        /// метода заполнения таблицы заказчиков
        /// </summary>
        /// <remarks>
        /// <para>ID Decimal</para>
        /// <para>LastName Nvarchar(64)</para>
        /// <para>FirstName Nvarchar(64)</para>
        /// <para>MiddleName Nvarchar(64)</para>
        /// <para>Sex Nvarchar(32)</para>
        /// <para>BirthDate DateTime</para>
        /// <para>RegistrationDate DateTime</para>
        /// </remarks>
        public void FillCustomerTables()
        {
            /// инициализируем генератор случайных чисел
            Random rnd = new Random();
            /// инициализируем даты { рождения, регистрации }
            DateTime newDate, newDateReg = new DateTime();
            /// инициализируем дату начала генерации дат рождения заказчиков
            DateTime date = new DateTime(1990, 1, 1);
            /// инициализируем дату начала генерации дат регистрации заказчиков
            DateTime dateReg = new DateTime(2012, 1, 1);
            /// инициализируем диапазон дат рождения заказчиков
            int rangeDate = (DateTime.Today - date).Days;
            /// инициализируем диапазон дат регистрации заказчиков
            int rangeReg = (DateTime.Today - dateReg).Days;
            /// инициализируем объект подключения к БД
            var conn = App.DbConn;

            /// инициализируем строку запроса заполнения таблицы заказчиков
            string customerSql = 
                "INSERT INTO dbo.Customers(ID,LastName,FirstName,MiddleName,Sex,BirthDate,RegistrationDate) " +
                "VALUES(@arg1,@arg2,@arg3,@arg4,@arg5,@arg6,@arg7)";

            try
            {
                /// проверка на подключение к БД
                if (App.DbConn != null)
                {
                    /// цикл потока запросов заполнения таблицы заказчиков
                    for ( decimal numUser, i = 0; i < mCountUsers_; ++ i )
                    {
                        /// задаем дату рождения заказчика
                        newDate = date.AddDays(rnd.Next(rangeDate));
                        /// задаем дату регистрации заказчика
                        newDateReg = dateReg.AddDays(rnd.Next(rangeReg));

                        /// инициализируем запрос в БД
                        SqlCommand cmd = new SqlCommand(customerSql, conn);
                        /// задаем пареметр запроса в БД -> идентификатор заказчика
                        cmd.Parameters.AddWithValue("@arg1", i);
                        /// выбираем пол заказчика ( случайно )
                        if ( rnd.Next(0, 100) < 39 )
                        {
                            /// задаем пареметр запроса в БД -> фамилия заказчика ( мужские )
                            cmd.Parameters.AddWithValue("@arg2", mMaleLastNames_.GetValue(rnd.Next(10)));
                            /// задаем пареметр запроса в БД -> имя заказчика ( мужские )
                            cmd.Parameters.AddWithValue("@arg3", mMaleFirstNames_.GetValue(rnd.Next(10)));
                            /// задаем пареметр запроса в БД -> отчество заказчика ( мужские )
                            cmd.Parameters.AddWithValue("@arg4", mMaleMiddleNames_.GetValue(rnd.Next(10)));
                            /// задаем пареметр запроса в БД -> пол заказчика ( мужские )
                            cmd.Parameters.AddWithValue("@arg5", mSex_.GetValue(0));
                        }
                        else
                        {
                            /// задаем пареметр запроса в БД -> фамилия заказчика ( женские )
                            cmd.Parameters.AddWithValue("@arg2", mFemaleLastNames_.GetValue(rnd.Next(10)));
                            /// задаем пареметр запроса в БД -> имя заказчика ( женские )
                            cmd.Parameters.AddWithValue("@arg3", mFemaleFirstNames_.GetValue(rnd.Next(10)));
                            /// задаем пареметр запроса в БД -> отчество заказчика ( женские )
                            cmd.Parameters.AddWithValue("@arg4", mFemaleMiddleNames_.GetValue(rnd.Next(10)));
                            /// задаем пареметр запроса в БД -> пол заказчика ( женские )
                            cmd.Parameters.AddWithValue("@arg5", mSex_.GetValue(1));
                        }
                        /// задаем пареметр запроса в БД -> дата рождения заказчика
                        cmd.Parameters.AddWithValue("@arg6", newDate);
                        /// задаем пареметр запроса в БД -> дата регистрации заказчика
                        cmd.Parameters.AddWithValue("@arg7", newDateReg);

                        /// задаем номер заказчика
                        numUser = i + 1;
                        /// выполняем запрос в БД по заполнению таблицы заказчиков ( потоково )
                        mDBWorker.ExecuteStreamQuery( conn, cmd, numUser, mCountUsers_ );
                        /// передаем прогресс создания заказчиков в форму отображения создания заказчиков
                        mNumCustomer_.Report( Convert.ToInt32( numUser ) );

                        /// пауза в процессе, для корректоного отображения прогресса в форме отображения создания заказчиков
                        Thread.Sleep(100);
                    }
                }
                /// выдаем сообщение об отсутствии подключения к БД
                else { Exception ex = new Exception("Database connection is empty"); }
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
        }

        /// <summary>
        /// метода заполнения таблицы заказов
        /// </summary>
        /// <remarks>
        /// <para>ID Decimal</para>
        /// <para>CustomerID Decimal</para>
        /// <para>OrderDate DateTime</para>
        /// <para>OrderName NVARCHAR(256)</para>
        /// <para>Price Decimal</para>
        /// </remarks>
        public void FillOrderTables()
        {
            /// инициализируем генератор случайных чисел
            Random rnd = new Random();
            /// объявляем дату регистрации заказа
            DateTime newOrderDate;
            /// инициализируем дату начала генерации дат регистрации заказа
            DateTime orderDate = new DateTime(2021, 1, 1);
            /// инициализируем диапазон дат регистрации заказов
            int rangeReg = (DateTime.Today - orderDate).Days;
            /// инициализируем объект подключения к БД
            var conn = App.DbConn;

            /// инициализируем строку запроса заполнения таблицы заказов
            string countSql = "INSERT INTO dbo.Orders(ID,CustomerID,OrderDate,OrderName,Price) VALUES(@arg1,@arg2,@arg3,@arg4,@arg5)";

            try
            {
                /// проверка на подключение к БД
                if (App.DbConn != null)
                {
                    /// цикл потока запросов заполнения таблицы заказов
                    for ( decimal numOrder, i = 0; i < mCountOrders_; ++ i )
                    {
                        /// инициализируем запрос в БД
                        SqlCommand cmd = new SqlCommand(countSql, conn);
                        /// задаем дату регистрации заказа
                        newOrderDate = orderDate.AddDays(rnd.Next(rangeReg));

                        /// задаем пареметр запроса в БД -> идентификатор заказа
                        cmd.Parameters.AddWithValue("@arg1", i);
                        /// задаем пареметр запроса в БД -> идентификатор заказчика
                        cmd.Parameters.AddWithValue("@arg2", Convert.ToDecimal(rnd.Next(mCountUsers_)));
                        /// задаем пареметр запроса в БД -> дата регистрации заказа
                        cmd.Parameters.AddWithValue("@arg3", newOrderDate);
                        /// задаем пареметр запроса в БД -> наименование заказа
                        cmd.Parameters.AddWithValue("@arg4", "заказ от " + newOrderDate.ToString("dd MMM yyy HH’:’mm’:’ss"));
                        /// задаем пареметр запроса в БД -> оценочная стоимость заказа
                        cmd.Parameters.AddWithValue("@arg5", Convert.ToDecimal(rnd.Next(100000)));

                        /// задаем номер заказа
                        numOrder = i + 1;
                        /// выполняем запрос в БД по заполнению таблицы заказов ( потоково )
                        mDBWorker.ExecuteStreamQuery(conn, cmd, numOrder, mCountOrders_);
                        /// передаем прогресс создания заказчиков в форму отображения создания заказов
                        mNumOrder_.Report( Convert.ToInt32( numOrder ) );

                        /// пауза в процессе, для корректоного отображения прогресса в форме отображения создания заказов
                        Thread.Sleep(10);
                    }
                }
                /// выдаем сообщение об отсутствии подключения к БД
                else { Exception ex = new Exception("Database connection is empty"); }
            }
            /// отлавливаем исключение
            catch (System.Exception ex)
            /// выдаем исключение
            { throw ex; }
        }
    }
}
