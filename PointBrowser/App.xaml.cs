using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PointBrowser
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static SqlConnection dbConn;
        static SqlConnection srvConn;

        public static SqlConnection DbConn
        {
            get { return dbConn; }
            set
            {
                dbConn = value;
                /*if (dbConn == null)
                {  }
                else
                { dbConn.ConnectionString = value.ConnectionString; }*/
            }
        }
        public static SqlConnection SrvConn
        {
            get { return srvConn; }
            set
            {
                srvConn = value;
                /*if (srvConn == null)
                { //srvConn = new SqlConnection(value.ConnectionString);
                    
                }
                else
                { srvConn.ConnectionString = value.ConnectionString; }*/
            }
        }

    }
}
