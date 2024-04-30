using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDWDotNetCore.ConsoleApp.Services
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-P3MAGTK",
            InitialCatalog = "DotNetTraining",
            UserID = "sa",
            Password = "sa@123",

            TrustServerCertificate = true
        };
    }
}

