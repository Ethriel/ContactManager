using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerDB.Utility
{
    public static class ConnectionStrings
    {
        public static string DefaultConnection
        {
            get => @"Data Source=DESKTOP-E0MQ7M4\SQLEXPRESS;Initial Catalog=ContactsManagment;Integrated Security=True;Trusted_Connection=True";
        }
    }
}
