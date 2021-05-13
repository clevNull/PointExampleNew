using PointExample.Model;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace PointExample.Logicum
{
    public class ConncetionLogic
    {
        DbParams contact;
        public void SrvConnect(string source, string port, string username, string pass)
        {
            contact = new DbParams();
            contact.Source = source;
            contact.Port = port;
            contact.DbName = "";
            contact.UserName = username;
            contact.Pass = pass;
            try { App.SrvConn = ConnectionClass.ConnectToServer(contact); }
            catch (System.Exception ex) { throw; }
        }

        public void DbConnect(string source, string port, string dbName, string username, string pass)
        {
            contact = new DbParams();
            contact.Source = source;
            contact.Port = port;
            contact.DbName = dbName;
            contact.UserName = username;
            contact.Pass = pass;
            try { App.DbConn = ConnectionClass.ConnectToDb(contact); }
            catch (System.Exception ex) { throw; }
        }
    }
    public class Logic
    {
        private string[] maleFirstNames;
        private string[] maleMiddleNames;
        private string[] maleLastNames;

        private string[] femaleFirstNames;
        private string[] femaleMiddleNames;
        private string[] femaleLastNames;

        private string[] Sex;

        //public delegate void ProgressHandler(string message);
        //public event ProgressHandler Notify;

        // public DbParams contact;

        private DBWorker dbWorker = new DBWorker();

        int mCountUsers, mCountOrders;
        IProgress < string > mProgressStatus;

        public Logic()
        {
            maleFirstNames = new string[] { "Александр", "Алексей", "Борис",
                "Виктор", "Георгий", "Григорий", "Константин",
                "Иван", "Евгений", "Сергей" };
            maleLastNames = new string[] { "Иванов", "Петров", "Сидоров",
                "Александров", "Капилевич", "Батыгин", "Самсонов",
                "Егоров", "Школьник", "Баранов"};
            maleMiddleNames = new string[] { "Александрович", "Алексеевич", "Борисович",
                "Викторович", "Георгиевич", "Григорьевич", "Константинович",
                "Иванович", "Евгеньевич", "Сергеевич" };

            femaleFirstNames = new string[] { "Александра", "Варвава", "Вероника",
                "Галина", "Дарья", "Елена", "Жанна",
                "Зоя", "Ирина", "Лия" };
            femaleLastNames = new string[] { "Иванова", "Петрова", "Сидорова",
                "Александрова", "Койхман", "Батыгина", "Самсонова",
                "Егорова", "Пустова", "Баранова"};
            femaleMiddleNames = new string[] { "Александровна", "Алексеевна", "Борисовна",
                "Викторовна", "Георгиевна", "Григорьевна", "Константиновна",
                "Ивановна", "Евгеньевна", "Сергеевна" };

            Sex = new string[] { "Мужской", "Женский" };
        }

        public Logic( int countUsers, int countOrders, IProgress < string > progressStatus )
        {
            maleFirstNames = new string[] { "Александр", "Алексей", "Борис",
                "Виктор", "Георгий", "Григорий", "Константин",
                "Иван", "Евгений", "Сергей" };
            maleLastNames = new string[] { "Иванов", "Петров", "Сидоров",
                "Александров", "Капилевич", "Батыгин", "Самсонов",
                "Егоров", "Школьник", "Баранов"};
            maleMiddleNames = new string[] { "Александрович", "Алексеевич", "Борисович",
                "Викторович", "Георгиевич", "Григорьевич", "Константинович",
                "Иванович", "Евгеньевич", "Сергеевич" };

            femaleFirstNames = new string[] { "Александра", "Варвава", "Вероника",
                "Галина", "Дарья", "Елена", "Жанна",
                "Зоя", "Ирина", "Лия" };
            femaleLastNames = new string[] { "Иванова", "Петрова", "Сидорова",
                "Александрова", "Койхман", "Батыгина", "Самсонова",
                "Егорова", "Пустова", "Баранова"};
            femaleMiddleNames = new string[] { "Александровна", "Алексеевна", "Борисовна",
                "Викторовна", "Георгиевна", "Григорьевна", "Константиновна",
                "Ивановна", "Евгеньевна", "Сергеевна" };

            Sex = new string[] { "Мужской", "Женский" };

            mCountUsers = countUsers;
            mCountOrders = countOrders;
            mProgressStatus = progressStatus;
        }

        public void DbCreation(string dbName)
        {
            try
            {
                if (App.SrvConn != null)
                {
                    // if (dbworker.checkdbexist(dbname))
                    // проверяем наличие БД и удаляем при наличии
                    dbWorker.DeleteDb(dbName);
                    // создаем БД
                    dbWorker.CreateDB(dbName);
                }
                else { Exception ex = new Exception("ServerConnection is NULL"); throw ex; }
            }
            catch (System.Exception ex) { throw ex; }
        }

        public void DbDeleting(string dbName)
        {
            try
            {
                if (App.SrvConn != null)
                {
                    // if (dbWorker.checkDbExist(dbName))
                    // проверяем наличие БД и удаляем при наличии
                    dbWorker.DeleteDb(dbName);
                    // else { Exception ex = new Exception("Database is not existing."); throw ex; }
                }
                else { Exception ex = new Exception("ServerConnection is NULL"); throw ex; }
            }
            catch (System.Exception ex) { throw ex; }
        }

        public void TablesCreation()
        {
            string customerStr = "CREATE TABLE dbo.Customers (" +
                "ID DECIMAL CONSTRAINT CUST_PK PRIMARY KEY NONCLUSTERED," +
                "LastName NVARCHAR(64)," +
                "FirstName NVARCHAR(64)," +
                "MiddleName NVARCHAR(64)," +
                "Sex NVARCHAR(32)," +
                "BirthDate DATETIME," +
                "RegistrationDate DATETIME )";
            string ordersStr = "CREATE TABLE dbo.Orders (" +
                    "ID DECIMAL CONSTRAINT ORDER_PK PRIMARY KEY NONCLUSTERED," +
                    "CustomerID DECIMAL," +
                    "OrderDate DATETIME," +
                    "Price DECIMAL )";
            dbWorker.ExecuteQuery(App.DbConn, customerStr);
            dbWorker.ExecuteQuery(App.DbConn, ordersStr);
        }



        public async Task FillTablesAsync()
        {
            await Task.Run(() => { FillCustomerTables(); });
            await Task.Run(() => { FillOrderTables(); });
        }

        /*ID Decimal
        LastName Nvarchar(64)
        FirstName Nvarchar(64)
        MiddleName Nvarchar(64)
        Sex Nvarchar(32)
        BirthDate DateTime
        RegistrationDate DateTime*/
        public void FillCustomerTables()
        {
            string customerSql = "INSERT INTO dbo.Customers(ID,LastName,FirstName,MiddleName,Sex,BirthDate,RegistrationDate) VALUES(@pr1,@pr2,@pr3,@pr4,@pr5,@pr6,@pr7)";

            Random rnd = new Random();
            DateTime newDate, newDateReg = new DateTime();
            DateTime date = new DateTime(1946, 1, 1);
            DateTime dateReg = new DateTime(2010, 1, 1);
            var conn = App.DbConn;
            int rangeDate = (DateTime.Today - date).Days;
            int rangeReg = (DateTime.Today - dateReg).Days;
            try
            {
                if (App.DbConn != null)
                {
                    for ( decimal numUser, i = 0; i < mCountUsers; ++ i )
                    {
                        newDate = date.AddDays(rnd.Next(rangeDate));
                        newDateReg = dateReg.AddDays(rnd.Next(rangeReg));

                        numUser = i + 1;

                        SqlCommand cmd = new SqlCommand(customerSql);
                        cmd.Parameters.AddWithValue("@pr1", i);

                        Random sexNum = new Random();

                        if ( sexNum.Next(0, 1) == 1 )
                        {
                            cmd.Parameters.AddWithValue("@pr2", maleLastNames.GetValue(rnd.Next(10)));
                            cmd.Parameters.AddWithValue("@pr3", maleFirstNames.GetValue(rnd.Next(10)));
                            cmd.Parameters.AddWithValue("@pr4", maleMiddleNames.GetValue(rnd.Next(10)));
                            cmd.Parameters.AddWithValue("@pr5", Sex.GetValue(0));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@pr2", femaleLastNames.GetValue(rnd.Next(10)));
                            cmd.Parameters.AddWithValue("@pr3", femaleFirstNames.GetValue(rnd.Next(10)));
                            cmd.Parameters.AddWithValue("@pr4", femaleMiddleNames.GetValue(rnd.Next(10)));
                            cmd.Parameters.AddWithValue("@pr5", Sex.GetValue(1));
                        }
                        cmd.Parameters.AddWithValue("@pr6", newDate);
                        cmd.Parameters.AddWithValue("@pr7", newDateReg);

                        dbWorker.ExecuteStreamQuery( conn, cmd, mCountUsers, numUser );

                        mProgressStatus.Report( "Процесс создания заказчиков: " + numUser.ToString() + "/" + mCountUsers.ToString() );

                        Thread.Sleep(100);
                    }
                }
                else { Exception ex = new Exception("Database connection is empty"); }
            }
            catch (System.Exception ex) { throw ex; }
        }


        /*ID Decimal
        CustomerID Decimal
        OrderDate DateTime
        Price Decimal*/
        public void FillOrderTables()
        {
            Random rnd = new Random();
            DateTime newOrderDate;
            DateTime orderDate = new DateTime(2011, 1, 1);
            var conn = App.DbConn;
            int range;

            string countSql = "INSERT INTO dbo.Orders(ID,CustomerID,OrderDate,Price) VALUES(@pr1,@pr2,@pr3,@pr4)";

            try
            {
                if (App.DbConn != null)
                {
                    for ( decimal numOrder, i = 0; i < mCountOrders; ++ i )
                    {
                        SqlCommand cmd = new SqlCommand(countSql);
                        range = (DateTime.Today - orderDate).Days;
                        newOrderDate = orderDate.AddDays(rnd.Next(range));

                        numOrder = i + 1;

                        cmd.Parameters.AddWithValue("@pr1", i);
                        cmd.Parameters.AddWithValue("@pr2", Convert.ToDecimal(rnd.Next(mCountUsers)));
                        cmd.Parameters.AddWithValue("@pr3", newOrderDate);
                        cmd.Parameters.AddWithValue("@pr4", Convert.ToDecimal(rnd.Next(100000)));

                        dbWorker.ExecuteStreamQuery(conn, cmd, mCountOrders, numOrder);

                        mProgressStatus.Report( "Процесс создания заказов: " + numOrder.ToString() + "/" + mCountOrders.ToString() );

                        Thread.Sleep(10);
                    }
                }
                else { Exception ex = new Exception("Database connection is empty"); }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
