﻿using System.Data.SqlClient;

namespace HDWDotNetCore.NLayer.DataAccess
{
    public static class ConnectionStrings
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

