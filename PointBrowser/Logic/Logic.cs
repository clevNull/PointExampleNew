using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PointBrowser.Model;

namespace PointBrowser.Logicum
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
    class Logic
    {
    }
}
