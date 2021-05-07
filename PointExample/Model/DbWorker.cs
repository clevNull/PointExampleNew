using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PointExample.Model
{
    public class DbParams
    {
        String dbName;
        String source;
        String port;
        String userName;
        String pass;

        public String DbName
            {
                get { return dbName; }
                set { dbName = value; }
            }
        public String Source
        {
            get { return source; }
            set { source = value; }
        }
        public String Port
        {
            get { return port; }
            set { port = value; }
        }
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public String Pass
        {
            get { return pass; }
            set { pass = value; }
        }
    }

    public class ConnectionClass
    {
        public static SqlConnection ConnectToServer (DbParams contact)
        {
            SqlConnection connection = new SqlConnection();

            string connStr = @"Data Source=" + contact.Source + ","
                + contact.Port + ";" +
                "Network Library=DBMSSOCN;" + 
                "User=" + contact.UserName + ";" +
                "Password=" + contact.Pass + ";";
                
            try
            {
                connection.ConnectionString = connStr;
                connection.Open();
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                //nothing yet
            }   
            return connection;
        }

        public static void CloseConnection (SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

    public class DBWorker
    {
        public void CreateDB (SqlConnection conn, DbParams contact)
        {
            string str = "CREATE DATABASE" + contact.DbName;
            SqlCommand createDbComm = new SqlCommand(str, conn); 
            
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                createDbComm.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                //nothing yet
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        
        //Перенести в логику
       /* string customerStr = "CREATE TABLE dbo.Customers + (" +
                "ID DECIMAL CONSTRAINT CUST_PK PRIMARY KEY NONCLUSTERED," +
                "LastName NVARCHAR(64)," +
                "FirstName NVARCHAR(64)," +
                "MiddleName NAVRCHAR(64)," +
                "Sex NVARCHAR(32)," +
                "BirthDate DATETIME," +
                "RegistrationDate DATETIME )";
        string ordersStr = "CREATE TABLE dbo.Orders (" +
                "ID DECIMAL CONSTRAINT ORDER_PK PRIMARY KEY NONCLUSTERED," +
                "CustomerID DECIMAL," +
                "OrderDate DATETIME" +
                "Price DECIMAL )";*/
        public void ExecuteQuery(string queryStr,  SqlConnection conn, DbParams contact)
        {
            SqlCommand query = new SqlCommand(queryStr, conn);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                query.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                //nothing yet
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public void ExecuteQuery(SqlCommand cmd, SqlConnection conn, DbParams contact)
        {
            cmd.Connection = conn;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                //nothing yet
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }

}

