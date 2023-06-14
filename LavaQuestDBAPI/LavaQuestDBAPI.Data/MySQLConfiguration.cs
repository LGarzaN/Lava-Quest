using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaQuestDBAPI.Data
{
    public class MySQLConfiguration
    {
        public MySQLConfiguration()
        {
        }

        public MySQLConfiguration(string connectionString) 
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }
    }
}
