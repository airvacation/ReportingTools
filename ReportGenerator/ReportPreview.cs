using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ReportGenerator
{
    public partial class ReportPreview : Form
    {
        private string ServerName;
        private string DatabaseName;
        private string DBUserId;
        private string DBPassword;
        private bool IntegratedSecurity;
        public ReportDocument rptDoc;
        public ReportPreview()
        {
            InitializeComponent();
            
        }

        private void ReportPreview_Load(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["TRN_DB"].ToString());
            ServerName = builder.DataSource;
            DatabaseName = builder.InitialCatalog;
            DBUserId = builder.UserID;
            DBPassword = builder.Password;
            IntegratedSecurity = builder.IntegratedSecurity;
            if (IntegratedSecurity==false)
                rptDoc.SetDatabaseLogon(DBUserId, DBPassword, @ServerName, DatabaseName);
            else
                rptDoc.SetDatabaseLogon(null, null, @ServerName, DatabaseName);
            ReportViewer.ReportSource = rptDoc;
        }
    }
}
