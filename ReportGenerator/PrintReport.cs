using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator
{
     public static class PrintReport
    {
        public static ReportDocument rptDoc;
        public static string ReportFileName { get; set; }
        private static string ServerName;
        private static string DatabaseName;
        private static string DBUserId;
        private static string DBPassword;
        private static bool IntegratedSecurity;
        public static void DirectPrint ()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["TRN_DB"].ToString());
            ServerName = builder.DataSource;
            DatabaseName = builder.InitialCatalog;
            DBUserId = builder.UserID;
            DBPassword = builder.Password;
            IntegratedSecurity = builder.IntegratedSecurity;
           
            if (IntegratedSecurity == false)
                rptDoc.SetDatabaseLogon(DBUserId, DBPassword, @ServerName, DatabaseName);
            else
                rptDoc.SetDatabaseLogon(null, null, @ServerName, DatabaseName);
            rptDoc.PrintToPrinter(1, false, 0, 0);
        }
    }
}
