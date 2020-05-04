using RethinkDb.Driver;
using System;

namespace Fidelity_RealTime_DB.classes
{
    class Connection
    {

        private int port;
        private String hostname;
        private String password;
        

        public Connection(){
            this.hostname = "localhost";
            this.port = 28015;
        }

        public Connection(String host, int port, String password)
        {
            this.hostname = host;
            this.port = port;
            this.password = password;
        }


        public virtual dynamic getConnection()
        {
            RethinkDB R = RethinkDB.R;
            var conn = R.Connection()
             .Hostname(this.hostname)
             .Port(this.port)
             .AuthKey(this.password)
             .Timeout(60)
             .Connect();

            
            return conn;
        }

        
    }
}
