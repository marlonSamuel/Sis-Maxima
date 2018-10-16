using Model;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Transport.Areas.Admin.Filters;

namespace SCP_MAXIMA.Areas.Admin.Controllers
{
    [Autenticado]
    public class BoletaController : Controller
    {
        private tbl_registro_pago registro = new tbl_registro_pago();
        private tbl_empleado empleado = new tbl_empleado();
        // GET: Admin/Boleta
        public ActionResult Index(int id = 0)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Guardar(tbl_registro_pago model)
        {
            var rm = new ResponseModel();

            var response = model.SaveRegistro();

            return Json(response);

        }

        public ActionResult ExportaAPDF(int id_registro)
        {
            return new ActionAsPdf("PDF", new { id_registro = id_registro});
        }

        public ActionResult PDF(int id_registro)
        {
            return View(
                registro.ObtenerPorId(id_registro)
            );
        }

    }
}