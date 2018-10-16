using Model;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Areas.Admin.Filters;

namespace SCP_MAXIMA.Areas.Admin.Controllers
{
    [Autenticado]
    public class ReportesController : Controller
    {
        private readonly Reportes reporte = new Reportes();
        // GET: Admin/Reportes
        public ActionResult Index()
        {
            return View();
        }


        //planilla por numero de planilla
        public ActionResult ExportaPlanillaNum(string num_pla)
        {
            return new ActionAsPdf("PDFPlanillaNum", new {num_pla = num_pla });
        }

        public ActionResult PDFPlanillaNum(string num_pla)
        {
            return View(
                reporte.ListarPlanillaNum(num_pla)
            );
        }

        //Resument empleados
        public ActionResult ResumenEmpleados()
        {
            return new ActionAsPdf("PDFEmpleados");
        }

        public ActionResult PDFEmpleados()
        {
            return View(
                reporte.ListarEmpleados()
            );
        }


        //resument general de planillas
        public ActionResult ExportaPlanilla()
        {
            return new ActionAsPdf("PDFPlanilla");
        }

        public ActionResult PDFPlanilla()
        {
            return View(
                reporte.ResumenPlanillas()
            );
        }
    }
}