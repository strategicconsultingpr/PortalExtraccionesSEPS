using Portal_Generator_V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Portal_Generator_V1.Controllers
{
    public class HomeController : Controller
    {
       // private DatepickerModel dp = new DatepickerModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Manager, TedsR")]
        public ActionResult Teds_Update()
        {
            if ((String)Session["alert"] != null)
            {
                ViewBag.alert = (String)Session["alert"];

                
            }
            if ((String)Session["alertupdate"] != null)
            {
                ViewBag.alertupdate = (String)Session["alertupdate"];

                ViewData["links"] = "true";
            }
            else
            {
                Session["links"] = "false";
            }
            Session.Remove("alert");
            Session.Remove("alertupdate");
            ViewData["Files"] = Directory.EnumerateFiles(ConfigurationManager.AppSettings["TEDSFolderPath"]);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Manager, TedsR")]
        public ActionResult Teds_Update(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(ConfigurationManager.AppSettings["TEDSFolderPath"], filename);
                    file.SaveAs(filepath);
                }
                //Session["alert"] = "good";
                Session.Add("alert", "good");
                //ViewBag.Message = "Se pudo transferir el archivo al folder correctamente.";
                //ViewData["Files"] = Directory.EnumerateFiles(ConfigurationManager.AppSettings["TEDSFolderPath"]);
                return RedirectToAction("Teds_Update");
            }
            catch
            {
                //Session["alert"] = "bad";
                Session.Add("alert", "bad");
                //ViewBag.Message = "Error! No se pudo transferir el archivo al folder correcto.";
               // ViewData["Files"] = Directory.EnumerateFiles(ConfigurationManager.AppSettings["TEDSFolderPath"]);
                return RedirectToAction("Teds_Update");
            }
                       
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Manager, TedsR")]
        public ActionResult Teds()
        {
            Session["Action"] = ControllerContext.RouteData.Values["Action"].ToString();
            Session["Controller"] = ControllerContext.RouteData.Values["Controller"].ToString();
            if ((String)Session["Teds"] != null)
            {
                ViewBag.date = (String)Session["Teds"];               
                ViewData["links"] = "true";
            }
            else
            {
                ViewBag.Message = "Press run before continuing.";
                ViewData["links"] = "false";
                ViewBag.date = "";
            }
            Session.Remove("Teds");

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Manager, TedsR")]
        public ActionResult Teds(DatepickerModel model)
        {
            

            Thread.Sleep(2000);

            try
            {


                DataLayer.saveJobParams("start", string.Format("{0:MM/dd/yyyy}", model.dtmDateStart.ToString()));

                DataLayer.saveJobParams("end", string.Format("{0:MM/dd/yyyy}", model.dtmDateEnd.ToString()));

                
                Class1 mycls = new Class1();
                mycls.SQLExecuteTEDS();
                mycls.SQLjob2();

                do
                {
                    // ViewBag.Message = "TEDS files are being generated.";
                   // Msg();
                   
                }
                while (!DataLayer.statusETL());
            }
            catch (Exception ex)
            { ex.Message.ToString();  }
            Session.Add("Teds", "TEDS files from: " + string.Format("{0:MM/dd/yyyy}", model.dtmDateStart) + " to " + string.Format("{0:MM/dd/yyyy}", model.dtmDateEnd) + " has been generated");
            //Session["links"] = "true";
            //ViewBag.date = "TEDS files from: "+ string.Format("{0:MM/dd/yyyy}", model.dtmDateStart) + " to "+ string.Format("{0:MM/dd/yyyy}", model.dtmDateEnd);
            ModelState.Clear();
           // ViewBag.Message = "Post";
                return RedirectToAction("Teds");
        }

        public ActionResult Page_1()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Page_SEPS_DB()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Page_Teds_to_Seps()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Teds", "Home");
        }

        public void File(String Oldfile, String Tedsfile)
        {
            if (DataLayer.GetFileStatus(Oldfile).Equals("1"))
            {

                String fileName = null;
                DirectoryInfo dir = new DirectoryInfo(ConfigurationManager.AppSettings["Job3FolderPath"]);
                FileInfo[] find = dir.GetFiles();

                foreach (FileInfo item in find)
                {
                    if (item.Name.Contains(Tedsfile))
                        fileName = item.FullName;
                }

                String sFilePath = fileName;
                //  System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));

                string filePath = sFilePath;
                FileInfo file = new FileInfo(filePath);

                byte[] Content = System.IO.File.ReadAllBytes(sFilePath);
                Response.ContentType = "text/csv";
                Response.AddHeader("content-disposition", "attachment; filename=" + file.Name);
                Response.BufferOutput = true;
                Response.OutputStream.Write(Content, 0, Content.Length);
                Response.End();


            }
            else { }

            //   Label2.Text = "Please Wait. Still processing.";
            //   StatusUpdatePanel.Update();
        //    return View("Teds");

        }

        public ActionResult UpdateTeds()
        {


            Thread.Sleep(2000);

            try
            {

                Class1 mycls = new Class1();
                mycls.SQLjobTedsUpdate();

                do
                {
                    // ViewBag.Message = "TEDS files are being generated.";
                    // Msg();

                }
                while (!Class1.statusFolder());
            }
            catch (Exception ex)
            { ex.Message.ToString();
                Session.Add("alertupdate", "bad");
                return RedirectToAction("Teds_Update");
            }
            Session.Add("alertupdate", "good");
            //Session["links"] = "true";
            //ViewBag.date = "El sistema SEPS fue actualizado con Exito!";
            // ModelState.Clear();
            // ViewBag.Message = "Post";
            //ViewData["Files"] = Directory.EnumerateFiles(ConfigurationManager.AppSettings["TEDSFolderPath"]);
            return RedirectToAction("Teds_Update");
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Manager, SepsR")]
        public ActionResult SEPS_DB()
        {
            if ((String)Session["Sepsdb"] != null)
            {
                ViewBag.date = (String)Session["Sepsdb"];
                ViewBag.text = (String)Session["text"];
                ViewData["links"] = "true";
            }
            else
            {
                ViewBag.Message = "Press run before continuing.";
                ViewData["links"] = "false";
                ViewBag.date = "";
                ViewBag.text = "";
            }
            Session.Remove("Sepsdb");
            Session.Remove("text");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Manager, SepsR")]
        public ActionResult SEPS_DB(DatepickerModel2 model)
        {         

            Thread.Sleep(2000);

            try
            {


                DataLayer.saveJobParamsSEPSDB("start", string.Format("{0:MM/dd/yyyy}", model.dtmDateStart.ToString()));

                DataLayer.saveJobParamsSEPSDB("end", string.Format("{0:MM/dd/yyyy}", model.dtmDateEnd.ToString()));

              //  DataLayer.saveJobParamsSEPSDB("file", model.file);


                Class1 mycls = new Class1();
                mycls.ResetStatusSepsDB();
                mycls.SQLjobSepsDB();
                
                do
                {
                    // ViewBag.Message = "TEDS files are being generated.";
                    // Msg();

                }
                while (!Class1.statusSEPS_DB());
                string error = Class1.GetFileError();
                Session.Add("text", error); 
            }
            catch (Exception ex)
            { }
            if (Class1.GetFileErrorID() == "2")
            {
                Session.Add("Sepsdb", "Informe Anual con Fecha Inicial: " + string.Format("{0:MM/dd/yyyy}", model.dtmDateStart) + " y Fecha Final: " + string.Format("{0:MM/dd/yyyy}", model.dtmDateEnd) + " ha sido generado");
                //ViewBag.date = "SEPS DB files from: " + string.Format("{0:MM/dd/yyyy}", model.dtmDateStart) + " to " + string.Format("{0:MM/dd/yyyy}", model.dtmDateEnd)+" has been generated";
            }
            else
            {
                Session.Add("Sepsdb", "ERROR!");
            }
                ModelState.Clear();
            // ViewBag.Message = "Post";
            return RedirectToAction("SEPS_DB");
        }

        public ActionResult ReportTemplate()
        {
            return View();
        }

        public ActionResult ReportesTEDS_SEPS()
        {
            return View();
        }

    }
        
    
}