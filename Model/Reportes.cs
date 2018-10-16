using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Reportes
    {
        public List<ResumenPlanillaDto> ResumenPlanillas()
        {
            var resumen = new List<ResumenPlanillaDto>();

            try
            {
                using(var ctx = new MaximaContext())
                {
                    resumen = (from r in ctx.tbl_registro_pago
                                     group r by new {r.num_planilla } into grouping
                                     orderby grouping.Key.num_planilla
                                     select new ResumenPlanillaDto
                                     {
                                         num_planilla = grouping.Key.num_planilla,
                                         total = ctx.tbl_registro_pago.Sum(x=>x.total)
                                     }).ToList();
                }
            }
            catch
            {
                throw;
            }

            return resumen;
        }


        //lista de planilla
        public List<ResumenPlanillaNumDto> ListarPlanillaNum(string num_pla)
        {
            var resumen = new List<ResumenPlanillaNumDto>();

            try
            {
                using (var ctx = new MaximaContext())
                {
                    resumen = (from r in ctx.tbl_registro_pago
                               join p in ctx.tbl_empleado on r.id_empleado equals p.id_empleado
                               join c in ctx.tbl_cargo on p.id_cargo equals c.id_cargo
                               join e in ctx.tbl_estacion on p.id_estacion equals e.id_estacion
                               join es in ctx.tbl_estacion on e.id_estacion equals es.id_estacion
                               where r.num_planilla == num_pla
                               select new
                               {
                                   recibo = r.no_recibo,
                                   num_pla = r.num_planilla,
                                   empleado = p.primer_nombre + " " + p.primer_apellido,
                                   cargo = c.nombre_cargo,
                                   total_empleado = r.total,
                                   estacion = es.nombre_estacion,
                                   total = ctx.tbl_registro_pago.Where(x => x.num_planilla == num_pla).Sum(x => x.total)
                               }).Select( x=> new ResumenPlanillaNumDto {
                                   no_recibo = x.recibo,
                                   num_planilla = x.num_pla,
                                   empleado = x.empleado,
                                   cargo = x.cargo,
                                   total_empleado = x.total_empleado,
                                   estacion = x.estacion,
                                   total = x.total
                               }).ToList();
                }
            }
            catch
            {
                throw;
            }

            return resumen;
        }

        //resumen empleados
        public List<ResumenEmpleadoDto> ListarEmpleados()
        {
            var list = new List<ResumenEmpleadoDto>();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    list = (from e in ctx.tbl_empleado
                            join c in ctx.tbl_cargo on e.id_cargo equals c.id_cargo
                            join es in ctx.tbl_estacion on e.id_estacion equals es.id_estacion
                           join p in ctx.tbl_prestamo
                           on e.id_empleado equals p.id_empleado into prestamo
                           from pre in prestamo.DefaultIfEmpty()
                           join a in ctx.tbl_anticipo on e.id_estacion equals a.id_empleado into anticipo
                           from ant in anticipo.DefaultIfEmpty()
                           join s in ctx.tbl_sancion on e.id_empleado equals s.id_empleado into sanscion
                           from sans in sanscion.DefaultIfEmpty()
                           select new
                           {
                               empleado = e.primer_nombre + " " + e.primer_apellido,
                               cargo = c.nombre_cargo,
                               estacion = es.nombre_estacion,
                               prestamo = ctx.tbl_prestamo.Count(x=>x.id_empleado == e.id_empleado),
                               anticipo = ctx.tbl_anticipo.Count(x=>x.id_empleado == e.id_empleado),
                               sancion = ctx.tbl_sancion.Count(x=>x.id_empleado == e.id_empleado)
                           }).Select(x=> new ResumenEmpleadoDto {
                               empleado = x.empleado,
                               cargo = x.cargo,
                               estacion = x.estacion,
                               anticipos = x.anticipo,
                               sanciones = x.sancion,
                               prestamos = x.prestamo,
                           }).ToList();
                }
            }catch(Exception e)
            {
                throw;
            }

            return list;
        }
    }
}
