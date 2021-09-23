using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PostMaker.DataAccessLayer
{
    public static class ConnectionHelper
    {
        public static string Con(string input)
        {
            return ConfigurationManager.ConnectionStrings[input].ConnectionString;
        }
    }
}
