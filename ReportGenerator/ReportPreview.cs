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
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Tables = CrystalDecisions.CrystalReports.Engine.Tables;

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
            try
            {
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["TRN_DB"].ToString());
                ServerName = @builder.DataSource;
                DatabaseName = builder.InitialCatalog;
                DBUserId = builder.UserID;
                DBPassword = builder.Password;
                IntegratedSecurity = builder.IntegratedSecurity;

                //if (IntegratedSecurity == false)
                //    rptDoc.SetDatabaseLogon(DBUserId, DBPassword, @ServerName, DatabaseName);
                //else
                //    rptDoc.SetDatabaseLogon(null, null, @ServerName, DatabaseName);

                //for (int i = 0; i < rptDoc.Subreports.Count; i++)
                //{
                //    rptDoc.Subreports[i].SetDatabaseLogon(DBUserId, DBPassword, @ServerName, DatabaseName);
                //}


                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                crConnectionInfo.ServerName = ServerName;// ReportConnection.Server;
                crConnectionInfo.DatabaseName = DatabaseName;// ReportConnection.DataBase;
                if (crConnectionInfo.IntegratedSecurity == false)
                {
                    crConnectionInfo.UserID = DBUserId;
                    crConnectionInfo.Password = DBPassword;
                }

                //rptDoc.Load(ConfigurationManager.AppSettings["ReportServerPath"].ToString() + "AssessmentForm.rpt");
                //rptDoc.Refresh();
                CrTables = rptDoc.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = DatabaseName + ".dbo." + CrTable.Name;
                }

                for (int i = 0; i < rptDoc.Subreports.Count; i++)
                {
                    Tables CrTablesSubReport = rptDoc.Subreports[i].Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTablesSubReport)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                        CrTable.Location = DatabaseName + ".dbo." + CrTable.Name;
                    }
                }
                //rptDoc.SetParameterValue("@Bill_No", 10);

                var cr = new ReportDocument();

                ReportViewer.ReportSource = rptDoc;

                
            }
            catch (Exception ex) { throw; }
        }
    }
}
