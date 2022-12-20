using CrystalDecisions.Shared;
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

namespace ReportGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (ReportPreview f = new ReportPreview())
            {
                f.rptDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string ReportServerPath = ConfigurationManager.AppSettings["ReportServerPath"];
                f.rptDoc.Load(@ReportServerPath + "CrystalReport1.rpt");
                /*pass parameters and values here if there is*/
                f.rptDoc.SetParameterValue("@UserName", "oliver");
                f.ShowDialog();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            PrintReport.rptDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string ReportServerPath = ConfigurationManager.AppSettings["ReportServerPath"];
            PrintReport.rptDoc.Load(@ReportServerPath + "CrystalReport1.rpt");
            /*pass parameters and values here if there is*/
            PrintReport.rptDoc.SetParameterValue("@UserName", "oliver");
            PrintReport.DirectPrint();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            GenerateExcelFromGrid.ExportToExcelWithFormatting(this.dataGridView1, "sample");
        }
    }
}
