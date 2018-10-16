using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Areas.Admin.Filters;

namespace SCP_MAXIMA.Areas.Admin.Controllers
{
    [Autenticado]
    public class CargoController : Controller
    {
        private tbl_cargo cargo = new tbl_cargo();
        // GET: Admin/Cargo
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(cargo.Listar(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(cargo.ObtenerPorId(id));
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                   id == 0 ? new tbl_cargo() : cargo.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_cargo model)
        {
            var rm = new ResponseModel();
            model.estado = "A";
            if (ModelState.IsValid)
            {
                rm = model.SaveCargo();

                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/cargo/");
                }
            }
            return Json(rm);
        }


        public ActionResult Eliminar(int id)
        {
            return Json(cargo.EliminarCargo(id));
        }
    }
}