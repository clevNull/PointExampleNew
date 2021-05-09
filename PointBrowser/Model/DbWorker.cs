using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PointBrowser.Model
{
    public class DbParams
    {
        String source;
        String port;
        String dbName;
        String userName;
        String pass;


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
        public String DbName
        {
            get { return dbName; }
            set { dbName = value; }
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
        public static SqlConnection ConnectToServer(DbParams contact)
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
            { throw ex; }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return connection;
        }

        public static SqlConnection ConnectToDb(DbParams contact)
        {
            SqlConnection connection = new SqlConnection();

            string connStr = @"Data Source=" + contact.Source + ","
                + contact.Port + ";" +
                "Initial Catalog = " + contact.DbName + ";" +
                 "Network Library=DBMSSOCN;" +
                "User=" + contact.UserName + ";" +
                "Password=" + contact.Pass + ";";

            try
            {
                connection.ConnectionString = connStr;
                connection.Open();
            }
            catch (System.Exception ex)
            { throw ex; }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return connection;
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

    public class DBWorker
    {
        public void CreateDB(string dbName)
        {
            string str = "CREATE DATABASE " + dbName;
            SqlCommand createDbComm = new SqlCommand(str, App.SrvConn);

            try
            {
                if (App.SrvConn.State != ConnectionState.Open)
                    App.SrvConn.Open();
                createDbComm.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            { throw ex; }
            finally
            {
                if (App.SrvConn.State == ConnectionState.Open)
                    App.SrvConn.Close();
            }
        }

        public bool checkDbExist(string dbName)
        {
            string cmdText = "select count(*) from master.dbo.sysdatabases where name=@database";
            SqlCommand sqlCmd = new SqlCommand(cmdText, App.SrvConn);
            sqlCmd.Parameters.Add("@database", System.Data.SqlDbType.NVarChar).Value = dbName;
            try
            {
                if (App.SrvConn.State != ConnectionState.Open)
                    App.SrvConn.Open();
                sqlCmd.ExecuteScalar();
                var i = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return Convert.ToBoolean(i);
            }
            catch (System.Exception ex)
            { throw ex; }
            finally
            {
                if (App.SrvConn.State == ConnectionState.Open)
                    App.SrvConn.Close();
            }

        }
        public void DeleteDb(string dbName)
        {
            string cmdText = "DROP DATABASE " + dbName;
            SqlCommand sqlCmd = new SqlCommand(cmdText, App.SrvConn);

            try
            {
                if (App.SrvConn.State != ConnectionState.Open)
                    App.SrvConn.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            { throw ex; }
            finally
            {
                if (App.SrvConn.State == ConnectionState.Open)
                    App.SrvConn.Close();
            }

        }

        public void ExecuteQuery(SqlConnection conn, string queryStr)
        {
            SqlCommand query = new SqlCommand(queryStr, conn);

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                query.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            { throw ex; }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public void ExecuteStreamQuery(SqlConnection conn, SqlCommand cmd, int iter, decimal count)
        {
            cmd.Connection = conn;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            { throw ex; }
            finally
            {
                if (iter == count)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }
    }


}

