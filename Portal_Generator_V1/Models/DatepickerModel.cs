using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace Portal_Generator_V1.Models
{
    public class DatepickerModel
    {
        [Required]
        [Display(Name = "START")]
        public DateTime dtmDateStart { get; set; }

        [Required]
        [Display(Name = "END")]
        public DateTime dtmDateEnd { get; set; }

        public String status { get; set; }

        //[Required]
        //[Display(Name = "FILE")]
        //public String file { get; set; }
    }

    public class DatepickerModel2
    {
        [Required]
        [Display(Name = "START")]
        public DateTime dtmDateStart { get; set; }

        [Required]
        [Display(Name = "END")]
        public DateTime dtmDateEnd { get; set; }

        public String status { get; set; }

        //[Required]
        //[Display(Name = "FILE")]
        //public String file { get; set; }
    }

    public class DataLayer
    {
        public DataLayer()
        {

        }

        public static void saveJobParams(String Parameter, String Value)
        {
            string cmdText = "UPDATE [dbo].[SSIS_Parameters] SET [Value] = '"
                + Value + "' WHERE [Parameter] = '"
                + Parameter
                + "'";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public static void saveJobParamsSEPSDB(String Parameter, String Value)
        {
            string cmdText = "UPDATE [dbo].[SEPS-DB_Parameters] SET [Value] = '"
                + Value + "' WHERE [Parameter] = '"
                + Parameter
                + "'";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public static void SQLjob2()
        {
            string cmdText = "exec StartAgentJobAndWait 'TEDS_SSIS',3600 ";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public static Boolean statusETL()
        {
            Boolean value = false;

            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            String sql = null;
            String result = "";

            sql = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_SA_DIS.txt'";

            sqlCnn = new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                result = sqlReader.GetValue(0).ToString();
            }
            sqlReader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();

            if (result.Equals("1")) { value = true; }

            return value;
        }

        public static string GetFileStatus(String file)
        {
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            String sql = null;
            String result = "";

            sql = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = '" + file + "'";

            sqlCnn = new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                result = sqlReader.GetValue(0).ToString();
            }
            sqlReader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();

            return result;
        }


    }

    public class Class1
    {
        public static Boolean statusFolder()
        {
            Boolean value = false;

            if(Directory.GetFiles(ConfigurationManager.AppSettings["TEDSFolderPath"]).Length == 0) { value = true; }

            return value;
        }

        public static Boolean statusSEPS_DB()
        {
            Boolean value = false;

            SqlConnection sqlCnn;
            SqlCommand sqlCmd, CmdFileName;
            String sql = null, sqlFileName = null;
            String result = "", resultFileName = "";

            sql = "SELECT [Value] FROM [dbo].[SEPS-DB_Parameters] WHERE[Parameter] = 'status'";
         //   sqlFileName = "SELECT [Value] FROM [dbo].[SEPS-DB_Parameters] WHERE[Parameter] = 'filename'";

            sqlCnn = new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                result = sqlReader.GetValue(0).ToString();
            }
            sqlReader.Close();
            sqlCmd.Dispose();

            //CmdFileName = new SqlCommand(sqlFileName, sqlCnn);
            //SqlDataReader sqlReaderFile = CmdFileName.ExecuteReader();
            //while (sqlReaderFile.Read())
            //{
            //    resultFileName = sqlReaderFile.GetValue(0).ToString();
            //}
            //sqlReaderFile.Close();
            //CmdFileName.Dispose();



            sqlCnn.Close();

            //if (result.Equals("1") && !resultFileName.Equals("0")) { value = true; }
            if (result.Equals("1")) { value = true; }

            return value;
        }

        public static string GetFileError()
        {
            SqlConnection sqlCnn;
            SqlCommand CmdFileName, sqlCmd;
            String sqlFileName = null, sql = null;
            String  resultFileName = "", error = "", result = "";
            
            sqlFileName = "SELECT [Value] FROM [dbo].[SEPS-DB_Parameters] WHERE[Parameter] = 'filename'";
            sql = "SELECT [Value] FROM [dbo].[SEPS-DB_Parameters] WHERE[Parameter] = 'file'";

            sqlCnn = new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            sqlCnn.Open();          

            CmdFileName = new SqlCommand(sqlFileName, sqlCnn);
            SqlDataReader sqlReaderFile = CmdFileName.ExecuteReader();
            while (sqlReaderFile.Read())
            {
                resultFileName = sqlReaderFile.GetValue(0).ToString();
            }
            sqlReaderFile.Close();
            CmdFileName.Dispose();

            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                result = sqlReader.GetValue(0).ToString();
            }
            sqlReader.Close();
            sqlCmd.Dispose();

            sqlCnn.Close();

            switch (resultFileName)
            {
                case "1":
                    error = "El archivo "+ result+ " ya existe, favor insertar otro nombre.";
                    break;
                case "2":
                    //error = "El archivo " + result + " fué creado exitosamente. Favor acceder al documento 'ReportesAnuales' en el archivo 'Planificacion2' para poder utilizar la tabla creada.";
                    error = "El archivo fué creado exitosamente. Favor acceder al documento 'ReportesAnuales' en el archivo 'INFORMES_ANUALES_PORTAL' dentro del archivo 'Planificacion2' para poder utilizar la tabla creada.";
                    break;
                default:
                    error = "Hubo un error";
                    break;
            }
            return error;
        }

        public static string GetFileErrorID()
        {
            SqlConnection sqlCnn;
            SqlCommand CmdFileName;
            String sqlFileName = null;
            String resultFileName = "";

            sqlFileName = "SELECT [Value] FROM [dbo].[SEPS-DB_Parameters] WHERE[Parameter] = 'filename'";
          

            sqlCnn = new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            sqlCnn.Open();

            CmdFileName = new SqlCommand(sqlFileName, sqlCnn);
            SqlDataReader sqlReaderFile = CmdFileName.ExecuteReader();
            while (sqlReaderFile.Read())
            {
                resultFileName = sqlReaderFile.GetValue(0).ToString();
            }
            sqlReaderFile.Close();
            CmdFileName.Dispose();

          

            sqlCnn.Close();

            
            return resultFileName;
        }

        public Class1()
        {
            //
            // TODO: Add constructor logic here
            //


        }
        public void SQLjob2()
        {

            string myjob = ConfigurationManager.AppSettings["ExecJob1"];
            string cmdText = "exec StartAgentJobAndWait '" + myjob + "',3600 ";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.CommandTimeout = 0;

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void SQLExecuteTEDS()
        {
           

            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString))
            {
                String MH_ADresult = "", MH_DISresult = "", SA_ADresult = "", SA_DISresult = "";
                String MH_AD = "", MH_DIS = "", SA_AD = "", SA_DIS = "";
                String sqlMH_AD = null, sqlMH_DIS = null, sqlSA_AD = null, sqlSA_DIS = null;
                sqlMH_AD = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_MH_AD.txt'";
                sqlSA_AD = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_SA_AD.txt'";
                sqlMH_DIS = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_MH_DIS.txt'";
                sqlSA_DIS = "SELECT [Value] FROM [dbo].[SSIS_Parameters] WHERE[Parameter] = 'TEDS_SA_DIS.txt'";


                MH_AD = "execute [dbo].[SC_VW_RPT_TEDS_MH_AD] ";
                SA_AD = "execute [dbo].[SC_VW_RPT_TEDS_SA_AD] ";
                MH_DIS = "execute [dbo].[SC_VW_RPT_TEDS_MH_DIS] ";
                SA_DIS = "execute [dbo].[SC_VW_RPT_TEDS_SA_DIS] ";

                try
                {
                    connection.Open();


                    SqlCommand cmdMH_AD = new SqlCommand(MH_AD, connection);
                    cmdMH_AD.CommandTimeout = 0;

                    SqlCommand cmdSA_AD = new SqlCommand(SA_AD, connection);
                    cmdSA_AD.CommandTimeout = 0;

                    SqlCommand cmdMH_DIS = new SqlCommand(MH_DIS, connection);
                    cmdMH_DIS.CommandTimeout = 0;

                    SqlCommand cmdSA_DIS = new SqlCommand(SA_DIS, connection);
                    cmdSA_DIS.CommandTimeout = 0;

                    //cmdMH_AD.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdMH_AD.ExecuteNonQuery();

                    SqlCommand MH_ADcmd = new SqlCommand(sqlMH_AD, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFile = MH_ADcmd.ExecuteReader();
                        while (sqlReaderFile.Read())
                        {
                            MH_ADresult = sqlReaderFile.GetValue(0).ToString();
                        }
                        sqlReaderFile.Close();
                    } while (MH_ADresult != "1");


                    cmdSA_AD.ExecuteNonQuery();

                    SqlCommand SA_ADcmd = new SqlCommand(sqlSA_AD, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFileSA_AD = SA_ADcmd.ExecuteReader();
                        while (sqlReaderFileSA_AD.Read())
                        {
                            SA_ADresult = sqlReaderFileSA_AD.GetValue(0).ToString();
                        }
                        sqlReaderFileSA_AD.Close();
                    } while (SA_ADresult != "1");

                    cmdMH_DIS.ExecuteNonQuery();

                    SqlCommand MH_DIScmd = new SqlCommand(sqlMH_DIS, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFileMH_DIS = MH_DIScmd.ExecuteReader();
                        while (sqlReaderFileMH_DIS.Read())
                        {
                            MH_DISresult = sqlReaderFileMH_DIS.GetValue(0).ToString();
                        }
                        sqlReaderFileMH_DIS.Close();
                    } while (MH_DISresult != "1");

                    cmdSA_DIS.ExecuteNonQuery();

                    SqlCommand SA_DIScmd = new SqlCommand(sqlSA_DIS, connection);
                    //cmd.CommandTimeout = 0;            

                    do
                    {
                        SqlDataReader sqlReaderFileSA_DIS = SA_DIScmd.ExecuteReader();
                        while (sqlReaderFileSA_DIS.Read())
                        {
                            SA_DISresult = sqlReaderFileSA_DIS.GetValue(0).ToString();
                        }
                        sqlReaderFileSA_DIS.Close();
                    } while (SA_DISresult != "1");


                    connection.Close();
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
            }

                


            //return text;
        }

        public void SQLjobTedsUpdate()
        {

            string myjob = ConfigurationManager.AppSettings["ExecJob2"];
            string cmdText = "exec StartAgentJobAndWait '" + myjob + "',3600 ";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.CommandTimeout = 0;

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void ResetStatusSepsDB()
        {


            string cmdStatus = "UPDATE [dbo].[SEPS-DB_Parameters] SET [Value] = '0' WHERE [Parameter] = 'status' ";
            string cmdFileName = "UPDATE [dbo].[SEPS-DB_Parameters] SET [Value] = '2' WHERE [Parameter] = 'filename' ";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmdS = new SqlCommand(cmdStatus, connection);
            cmdS.CommandTimeout = 0;

            cmdS.ExecuteNonQuery();

            SqlCommand cmdF = new SqlCommand(cmdFileName, connection);
            cmdF.CommandTimeout = 0;

            cmdF.ExecuteNonQuery();

            connection.Close();
        }

        public void SQLjobSepsDB()
        {

            //string text;
            string cmdText = "execute [dbo].[SP_Informe_Datos_Anuales_sin_View] ";

            SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[2].ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.CommandTimeout = 0;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //SqlParameter Error = new SqlParameter("@ERROR", System.Data.SqlDbType.NVarChar,100);
            //Error.Direction = System.Data.ParameterDirection.Output;
            //cmd.Parameters.Add(Error);
            //cmd.Parameters["@ERROR"].Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            //text = (string)cmd.Parameters["@ERROR"].Value;
            //try
            //{
            //    cmd.ExecuteNonQuery();
            //}
            //catch (System.Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.InnerException);
            //    Console.WriteLine(ex.StackTrace);
            //}
            

            connection.Close();

            //return text;
        }


    }
}