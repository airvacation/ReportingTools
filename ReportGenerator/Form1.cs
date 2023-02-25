using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                
                
                f.rptDoc.Load(ConfigurationManager.AppSettings["ReportServerPath"].ToString() + "AssessmentForm.rpt");
                f.rptDoc.Refresh();
                /*pass parameters and values here if there is*/
                f.rptDoc.SetParameterValue("@Bill_No", 12);
                //f.rptDoc.SetParameterValue("@NEW", false);
                //f.rptDoc.SetParameterValue("@Term", "1st");
                //f.rptDoc.SetParameterValue("@Course_Code", "BSCS");
                //f.rptDoc.SetParameterValue("@Subj_CY", 1);
                //f.rptDoc.SetParameterValue("@Subjects", "CWTS 1,ENG +,ENG 1,FIL 1,GE-Math1,HIST 1,Hum1L,Humanities 2,LOGIC,CS1");
                //f.rptDoc.SetParameterValue("@StartDate", null);
                //f.rptDoc.SetParameterValue("@Bill_SY", "2021-2022");


                f.ShowDialog();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            PrintReport.rptDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            PrintReport.rptDoc.Load(ConfigurationManager.AppSettings["ReportServerPath"].ToString() + "AssessmentForm.rpt");
            /*pass parameters and values here if there is*/
            PrintReport.rptDoc.SetParameterValue("@Bill_No", 10);
            PrintReport.DirectPrint();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            GenerateExcelFromGrid.ExportToExcelWithFormatting(this.dataGridView1, "sample");
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["TRN_DB"].ToString());
            string query = "INSERT INTO [dbo].[tblSystemSettings]([SchoolName],[Address],[Logo],[CashPaymentPercDisc]) ";
            query += " VALUES (@SchoolName,@Address,@Logo,@CashPaymentPercDisc)";

            SqlCommand myCommand = new SqlCommand(query, sqlconn);
            myCommand.Parameters.AddWithValue("@SchoolName", "RIZAL COLLEGE OF TAAL");
            myCommand.Parameters.AddWithValue("@Address", "G. MARELLA ST., TAAL, BATANGAS");
            myCommand.Parameters.AddWithValue("@Logo", ImageToByte(pictureBox1.Image));
            myCommand.Parameters.AddWithValue("@CashPaymentPercDisc", 10);
            // ... other parameters
            sqlconn.Open();
            myCommand.ExecuteNonQuery();
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            using (ReportPreview f = new ReportPreview())
            {
                f.rptDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();


                f.rptDoc.Load(ConfigurationManager.AppSettings["ReportServerPath"].ToString() + "AssessmentForm.rpt");
                f.rptDoc.Refresh();
                /*pass parameters and values here if there is*/
                f.rptDoc.SetParameterValue("@Bill_No", 12);
                //f.rptDoc.SetParameterValue("@NEW", false);
                //f.rptDoc.SetParameterValue("@Term", "1st");
                //f.rptDoc.SetParameterValue("@Course_Code", "BSCS");
                //f.rptDoc.SetParameterValue("@Subj_CY", 1);
                //f.rptDoc.SetParameterValue("@Subjects", "CWTS 1,ENG +,ENG 1,FIL 1,GE-Math1,HIST 1,Hum1L,Humanities 2,LOGIC,CS1");
                //f.rptDoc.SetParameterValue("@StartDate", null);
                //f.rptDoc.SetParameterValue("@Bill_SY", "2021-2022");

                var printerSettings = new System.Drawing.Printing.PrinterSettings();
                var pageSettings = new System.Drawing.Printing.PageSettings(printerSettings);
                pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("newsize", 450, 800); // Custom size (100=1 inch)
                pageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                f.rptDoc.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                f.rptDoc.PrintOptions.CopyFrom(printerSettings, pageSettings);

                f.ShowDialog();
            }
        }
    }
}
