using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using PointExample;
using PointExample.Model;

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

        // public DbParams contact;

        private DBWorker dbWorker = new DBWorker();

        public Logic ()
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

        public void DbCreation (string dbName)
        {
            try
            {
                if (App.SrvConn != null)
                {
                    if (dbWorker.checkDbExist(dbName))
                        dbWorker.DeleteDb(dbName);

                    dbWorker.CreateDB(dbName);
                }
                else { Exception ex = new Exception("ServerConnection is NULL"); throw ex; }
            }
            catch (System.Exception ex) { throw ex; }
        }

        public void DbDeleting (string dbName)
        {
            try
            {
                if (App.SrvConn != null)
                {
                    if (dbWorker.checkDbExist(dbName))
                        dbWorker.DeleteDb(dbName);
                    else { Exception ex = new Exception("Database is not existing."); throw ex; }
                }
                else { Exception ex = new Exception("ServerConnection is NULL"); throw ex; }
            }
            catch (System.Exception ex) { throw ex; }
        }

        public void TablesCreation ()
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



        /*ID Decimal
        LastName Nvarchar(64)
        FirstName Nvarchar(64)
        MiddleName Nvarchar(64)
        Sex Nvarchar(32)
        BirthDate DateTime
        RegistrationDate DateTime*/
        public void FillCustomerTables(int countUsers)
        {
            string customerSql = "INSERT INTO dbo.Customers(ID,LastName,FirstName,MiddleName,Sex,BirthDate,RegistrationDate) VALUES(@pr1,@pr2,@pr3,@pr4,@pr5,@pr6,@pr7)";

            decimal i;
            Random rnd;
            DateTime date, dateReg;
            var conn = App.DbConn;
            try
            {
                for (i = 0; i < countUsers; i++)
                {
                    rnd = new Random();
                    date = new DateTime(rnd.Next(1946, 2020), rnd.Next(1, 12), rnd.Next(1, 30));
                    dateReg = new DateTime(rnd.Next(2010, 2020), rnd.Next(1, 12), rnd.Next(1, 30));

                    SqlCommand cmd = new SqlCommand(customerSql);
                    cmd.Parameters.AddWithValue("@pr1", i);

                    if (i % 2 == 0)
                    {
                        cmd.Parameters.AddWithValue("@pr2", maleLastNames.GetValue(rnd.Next(10)));
                        cmd.Parameters.AddWithValue("@pr3", maleFirstNames.GetValue(rnd.Next(10)));
                        cmd.Parameters.AddWithValue("@pr4", maleMiddleNames.GetValue(rnd.Next(10)));
                        cmd.Parameters.AddWithValue("@pr5", Sex.GetValue(0));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pr2", maleLastNames.GetValue(rnd.Next(10)));
                        cmd.Parameters.AddWithValue("@pr3", maleFirstNames.GetValue(rnd.Next(10)));
                        cmd.Parameters.AddWithValue("@pr4", maleMiddleNames.GetValue(rnd.Next(10)));
                        cmd.Parameters.AddWithValue("@pr5", Sex.GetValue(0));
                    }
                    cmd.Parameters.AddWithValue("@pr6", date);
                    cmd.Parameters.AddWithValue("@pr7", dateReg);

                    dbWorker.ExecuteStreamQuery(conn, cmd, countUsers, i);
                }
            }
            catch (System.Exception ex) { throw ex; }
        }


        /*ID Decimal
        CustomerID Decimal
        OrderDate DateTime
        Price Decimal*/
        public void FillOrderTables(int countOrders, int countUsers)
        {
            Random rnd;
            DateTime orderDate;

            string countSql = "INSERT INTO dbo.Orders(ID,CustomerID,OrderDate,Price) VALUES(@pr1,@pr2,@pr3,@pr4)";

            for (decimal i = 0; i < countOrders; i++)
            {
                SqlCommand cmd = new SqlCommand(countSql);
                rnd = new Random();
                orderDate = new DateTime(rnd.Next(2011, 2020), rnd.Next(1, 12), rnd.Next(1, 30));

                cmd.Parameters.AddWithValue("@pr1", i);
                cmd.Parameters.AddWithValue("@pr2", Convert.ToDecimal(rnd.Next(countUsers)));
                cmd.Parameters.AddWithValue("@pr3", orderDate);
                cmd.Parameters.AddWithValue("@pr3", Convert.ToDecimal(rnd.Next(100000)));
            }
         }
    }
}
