using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointExample.Model
{
    public class DbConnect
    {
        string dbName;
        string connString;
        string userName;
        string passHash;

        public string DbName
            {
                get { return dbName; }
                set { dbName = value; }
            }
        public string ConnString
        {
            get { return connString; }
            set { connString = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string PassHash
        {
            get { return passHash; }
            set { passHash = value; }
        }
    }

    
}

