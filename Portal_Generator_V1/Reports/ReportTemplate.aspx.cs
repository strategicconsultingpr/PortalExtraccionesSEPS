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
            if(ddlReportes.Text == "INFORME ANUAL ")
            {
                nombreReporte.Text = "Informe Anual de SEPS";
                descripcionReporte.Text = "Este reporte es una extracción de la base de datos de clientes registrados en el Sistema Electrónico de Perfiles Sociodemográfico de la ASSMCA (SEPS). El propósito del reporte genera una matriz por líneas (caso ) y columnas (variables). El reporte tiene varias opciones de selección:  programas de la ASSMCA, rango de fecha del perfil y  tipo de perfil, sea admisión (AD), Evaluación (EV) y/o Alta (AL). El reporte puede ser extraídos en varios formatos entre ellos XLM, CSV, Excel y TIFF file.  Para detalles de la base de datos y cómo trabarla, el usuario debe hacer referencia al Manual de Recopilación de Datos Estadísticos preparado por la UEE, que contiene el diccionario de datos de la base.  Los usuarios son profesionales especializado en análisis de datos estadísticos, como bioestadísticos,  evaluadores, epidemiólogos, demógrafos entre otros de la UEE. ";
            }
            else if (ddlReportes.Text == "SAEP")
            {
                nombreReporte.Text = "SAEP";
                descripcionReporte.Text = "El SAEP es un sistema de generación de informes de alertas por programa/centro para mantener actualizadas las evaluaciones de progreso de los clientes. Los analistas de la UEE utilizan el sistema como herramienta para reportar el registro de los clientes admitidos o evaluados en SEPS en un periodo de 5 meses. El informe se envía a los representantes de los programas/centro para que puedan mantener el progreso de los clientes.";
            }
            else if (ddlReportes.Text == "Reporte de Calidad (SEPS)")
            {
                nombreReporte.Text = "Reporte de Calidad (SEPS)";
                descripcionReporte.Text = "Con este sistema se pueden generar  generar informes seleccionado por Programa de la ASSMCA, rango de fecha, tipo de perfil (Admisión,  Evaluación y Alta). Además, se puede hacer selección de los niveles de cuidado de los programas, sea por Nivel de Cuidado de Salud Mental, Nivel de Cuidado de Sustancias o ambos. El informe es utilizado por el personal de la UEE, para monitorear la calidad de los datos de clientes registrados en el SEPS. Los informes podrán ser utilizados para podrán utilizados para comparar y reconcialiar o cuadrar  los informes mensuales de clientela atendida (IMCA) enviados por los programas de la ASSMCA al analista de calidad de la UEE.  El analista tiene la facilidad de poder enviar el  informe en varios formatos: Excel, PDF, Word al personal designado en los Programas.  El reporte contiene la siguiente información: Nombre del Programa, Nivel de Cuidado, Tipo de Perfil, Fecha de último contacto con el paciente, número de identificador unico (IUP), Número de Expediente, utimos cuatro dígitos de seguro social, nombre del paciente, número de episodio, fecha de alta, fecha de edición y el usuario que registró el perfil. ";
            }
            else if (ddlReportes.Text == "Admisiones SEPS para Observatorio de Sustancias")
            {
                nombreReporte.Text = "Admisiones SEPS para Observatorio de Sustancias";
                descripcionReporte.Text = "Este reporte es una extracción de la base de datos de admsiones en el Sistema Electrónico de Perfiles Sociodemográfico de la ASSMCA (SEPS). El propósito del reporte genera un entregable al Observatorio de Abuso de SUstancias. El reporte tiene la opcion de selección:  rango de fecha del perfil.";

            }
            else
            {
                nombreReporte.Text = "Informe Anual de SEPS";
                descripcionReporte.Text = "Este reporte es una extracción de la base de datos de clientes registrados en el Sistema Electrónico de Perfiles Sociodemográfico de la ASSMCA (SEPS). El propósito del reporte genera una matriz por líneas (caso ) y columnas (variables). El reporte tiene varias opciones de selección:  programas de la ASSMCA, rango de fecha del perfil y  tipo de perfil, sea admisión (AD), Evaluación (EV) y/o Alta (AL). El reporte puede ser extraídos en varios formatos entre ellos XLM, CSV, Excel y TIFF file.  Para detalles de la base de datos y cómo trabarla, el usuario debe hacer referencia al Manual de Recopilación de Datos Estadísticos preparado por la UEE, que contiene el diccionario de datos de la base.  Los usuarios son profesionales especializado en análisis de datos estadísticos, como bioestadísticos,  evaluadores, epidemiólogos, demógrafos entre otros de la UEE. ";

            }
            

            rvSiteMapping.Height = Unit.Pixel(800 - 58);
            rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("alexie.ortiz", "Alexito@210987654321", "assmca.local"); // e.g.: ("demo-001", "123456789", "ifc")
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