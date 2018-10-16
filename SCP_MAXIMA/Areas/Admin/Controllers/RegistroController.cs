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
    public class RegistroController : Controller
    {
        private tbl_registro_pago registro = new tbl_registro_pago();
        // GET: Admin/Registro
        public ActionResult Index(int id = 0)
        {
            ViewData["id_estacion"] = id;
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(registro.Listar(grid));
        }

        [HttpPost]
        public ActionResult Obtener(tbl_registro_pago model)
        {
            return Redirect("~/Admin/Registro/crud?id_empleado="+model.id_empleado);
           
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                   id == 0 ? new tbl_registro_pago() : registro.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_registro_pago model)
        {
            var rm = new ResponseModel();
            model.estado = "A";
            model.total = model.total - model.faltante;
            if (ModelState.IsValid)
            {
                model = model.SaveRegistro();

                if (model != null)
                {

                    rm.SetResponse(true, "registro registrado con exito");
                    rm.href = Url.Content("~/admin/registro/");
                }
                else
                {
                    rm.SetResponse(false, "no se ha podido ingresar registro");
                }
            }
            return Json(rm);
        }

        public ActionResult Eliminar(int id)
        {
            return Json(registro.EliminarRegistro(id));
        }
    }
}