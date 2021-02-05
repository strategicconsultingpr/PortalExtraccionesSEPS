using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_Generator_V1.Reports
{
    public partial class ReportTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    GenerarReportes();
                    //String reportFolder = "http://vhermes/ReportServer?/";

                    //rvSiteMapping.Height = Unit.Pixel(800 - 58);
                    //rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    //IReportServerCredentials irsc = new CustomReportCredentials("alexie.ortiz", "Alexie@10", "assmca.local"); // e.g.: ("demo-001", "123456789", "ifc")
                    //rvSiteMapping.ServerReport.ReportServerCredentials = irsc;
                    //rvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://192.168.100.24//ReportServer"); // Add the Reporting Server URL  
                    //rvSiteMapping.ServerReport.ReportPath = "/Informes de Portal Extracciones/Informe Anual";


                    //rvSiteMapping.Height = Unit.Pixel(800 - 58);
                    //rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    //rvSiteMapping.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");

                    //dsReport dsrep = new dsReport();

                    //SqlCommand cmd = new SqlCommand();

                    //var thisConnString = ConfigurationManager.ConnectionStrings["SEPSConnectionString"].ConnectionString;

                    //SqlConnection thisConn = new SqlConnection(thisConnString);


                    //thisConn.Open();

                    //cmd.Connection = thisConn;

                    //cmd.CommandText = string.Format("[dbo].[SP_Informe_Datos_Anuales_Internet]");
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);

                    //System.Data.DataSet thisDataSet = new System.Data.DataSet();
                    //da.Fill(dsrep,"PORTAL_INFORME");
                    //thisConn.Close();

                    //ReportDataSource datasource = new ReportDataSource("dsReport",dsrep.Tables[0]);
                    //rvSiteMapping.LocalReport.DataSources.Clear();
                    //rvSiteMapping.LocalReport.DataSources.Add(datasource);

                    //rvSiteMapping.ServerReport.Refresh();
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void ddlReportes_Cambio(object sender, EventArgs e)
        {
            GenerarReportes();
        }

        void GenerarReportes()
        {
            if(ddlReportes.Text == "INFORME ANUAL")
            {
                nombreReporte.Text = "INFORME ANUAL";
                descripcionReporte.Text = "";
            }
            else if (ddlReportes.Text == "SAEP")
            {
                nombreReporte.Text = "SAEP";
                descripcionReporte.Text = "El SAEP es un sistema de generación de informes de alertas por programa/centro para mantener actualizadas las evaluaciones de progreso de los clientes. Los analistas de la UEE utilizan el sistema como herramienta para reportar el registro de los clientes admitidos o evaluados en SEPS en un periodo de 5 meses. El informe se envía a los representantes de los programas/centro para que puedan mantener el progreso de los clientes.";
            }
            else if (ddlReportes.Text == "Reporte de Calidad (SEPS)")
            {
                nombreReporte.Text = "Reporte de Calidad (SEPS)";
                descripcionReporte.Text = "";
            }
            else
            {
                nombreReporte.Text = "TEDS-SEPS MATCH";
            }
            
            rvSiteMapping.Height = Unit.Pixel(800 - 58);
            rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("alexie.ortiz", "Alexie@1912", "assmca.local"); // e.g.: ("demo-001", "123456789", "ifc")
            rvSiteMapping.ServerReport.ReportServerCredentials = irsc;
            rvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://192.168.100.24//ReportServer"); // Add the Reporting Server URL  
            rvSiteMapping.ServerReport.ReportPath = "/Informes de Portal Extracciones/" + ddlReportes.SelectedValue;
            rvSiteMapping.ServerReport.Refresh();
        }


    }

    public class CustomReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord); }
        }

        //ICredentials IReportServerCredentials.NetworkCredentials => throw new NotImplementedException();

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }

        //public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        //{
        //    throw new NotImplementedException();
        //}
    }
}