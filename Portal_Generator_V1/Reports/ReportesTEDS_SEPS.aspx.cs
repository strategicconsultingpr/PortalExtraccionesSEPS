using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System;

namespace Portal_Generator_V1.Reports
{
    public partial class ReportesTEDS_SEPS : System.Web.UI.Page
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
            if (ddlReportes.Text == "Informe Anual")
            {
                nombreReporte.Text = "INFORME ANUAL";
            }
            else
            {
                nombreReporte.Text = "REPORTES COMPARATIVOS TEDS – SEPS";
                descripcionReporte.Text = "Este sistema genera cuatro archivos: uno de admisión de salud mental, uno de admisiones de sustancias, uno de altas/evaluaciones de salud mental y uno de alta de sustancias. Los archivos contienen las variables de contenidas en los archivos de TEDS, según requeridas por SAMHSA. Las variables de TEDS se encuentran alineadas a las variables de SEPS a que ayuda a comparar ambas base de datos (“crosswalk). Estos archivos permiten realizan análisis de datos entre TEDS y SEPS para poder explicar/contestar las preguntas de los representantes de SAMHSA, con respecto a diferencias de registros entre años y preguntas sobre la calidad de los datos de Puerto Rico. Además, el informe ayuda a evaluar la calidad interna de archivos gereados para TEDS y comparación con los datos registrados en SEPS. ";
            }

            rvSiteMapping.Height = Unit.Pixel(800 - 58);
            rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("alexie.ortiz", "Alexito@987654321", "assmca.local"); // e.g.: ("demo-001", "123456789", "ifc")
            rvSiteMapping.ServerReport.ReportServerCredentials = irsc;
            rvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://192.168.100.24//ReportServer"); // Add the Reporting Server URL  
            rvSiteMapping.ServerReport.ReportPath = "/Informes de Portal Extracciones/" + ddlReportes.SelectedValue;
            rvSiteMapping.ServerReport.Refresh();
        }
    }

    //public class CustomReportCredentials : IReportServerCredentials
    //{
    //    private string _UserName;
    //    private string _PassWord;
    //    private string _DomainName;

    //    public CustomReportCredentials(string UserName, string PassWord, string DomainName)
    //    {
    //        _UserName = UserName;
    //        _PassWord = PassWord;
    //        _DomainName = DomainName;
    //    }

    //    public System.Security.Principal.WindowsIdentity ImpersonationUser
    //    {
    //        get { return null; }
    //    }

    //    public ICredentials NetworkCredentials
    //    {
    //        get { return new NetworkCredential(_UserName, _PassWord); }
    //    }

    //    //ICredentials IReportServerCredentials.NetworkCredentials => throw new NotImplementedException();

    //    public bool GetFormsCredentials(out Cookie authCookie, out string user,
    //     out string password, out string authority)
    //    {
    //        authCookie = null;
    //        user = password = authority = null;
    //        return false;
    //    }

    //    //public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}
}