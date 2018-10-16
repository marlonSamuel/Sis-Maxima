namespace Model
{
    using Model.Dto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_registro_pago
    {
        [Key]
        public int id_registro_pago { get; set; }

        public int id_empleado { get; set; }

        public int no_recibo { get; set; }

        public decimal total { get; set; }

        public decimal faltante { get; set; }

        public int cierre { get; set; }
        [Required]
        [StringLength(10)]
        public string num_planilla { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        [StringLength(500)]
        public string detalle { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }


        public virtual tbl_empleado tbl_empleado { get; set; }

        public virtual tbl_planilla tbl_planilla { get; set; }


        //obtener info para la boleta



        //funcion para
        public DtoEmpleado Obtener(int id)
        {
            var empleado = new DtoEmpleado();

            try
            {
                using (var ctx = new MaximaContext())
                {
                    empleado = (
                      from p in ctx.tbl_empleado
                      join c in ctx.tbl_cargo on p.id_cargo equals c.id_cargo
                      join e in ctx.tbl_estacion on p.id_estacion equals e.id_estacion
                      where p.id_empleado == id
                      select new
                      {
                          id_empleado = p.id_empleado,
                          id_cargo = p.id_cargo,
                          id_estacion = p.id_estacion,
                          nombres = p.primer_nombre + " "+ p.segundo_nombre,
                          apellidos = p.primer_apellido + " "+p.segundo_apellido,
                          nit = p.nit,
                          correo = p.correo,
                          telefono = p.telefono,
                          direccion = p.direccion,
                          fecha_nac = p.fecha_nac,
                          cargo = c.nombre_cargo,
                          estacion = e.nombre_estacion,
                          sueldo = c.sueldo
                      }
                    ).Select(x => new DtoEmpleado
                    {
                        id_empleado = x.id_empleado,
                        id_cargo = x.id_cargo,
                        id_estacion = x.id_estacion,
                        nombres = x.nombres,
                        apellidos = x.apellidos,
                        nit = x.nit,
                        correo = x.correo,
                        telefono = x.telefono,
                        direccion = x.direccion,
                        fecha_nac = x.fecha_nac,
                        cargo = x.cargo,
                        estacion = x.estacion,
                        suelgo = x.sueldo
                    }).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return empleado;
        }


        public tbl_registro_pago ObtenerPorId(int id)
        {
            var registro = new tbl_registro_pago();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    registro = ctx.tbl_registro_pago.Where(x => x.estado == "A" && x.id_registro_pago == id).SingleOrDefault();
                }
            }
            catch
            {

            }

            return registro;
        }

        //metodo para insertar o actualizar
        public tbl_registro_pago SaveRegistro()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_registro_pago > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el registro");
            }

            return this;
        }


        //metodo para listar
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    agrid.Inicializar();
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var query = (

                          from r in ctx.tbl_registro_pago join
                          e in ctx.tbl_empleado on r.id_empleado equals e.id_empleado
                          join p in ctx.tbl_planilla on r.num_planilla equals p.num_planilla
                          where r.id_registro_pago > 0
                          && r.estado == "A"
                          && p.cerrada == 0
                          select new
                          {
                              id_registro_pago = r.id_registro_pago,
                              id_empleado = r.id_empleado,
                              num_planilla = r.num_planilla,
                              total = r.total,
                              empleado = e.primer_nombre + " " + e.primer_apellido,
                              faltante = r.faltante,
                              cierre = r.cierre
                          }).Select(x => new DtoRegistro {
                              id_empleado = x.id_empleado,
                              id_registro_pago = x.id_registro_pago,
                              empleado = x.empleado,
                              total = x.total,
                              faltante = x.faltante,
                              cierre = x.cierre,
                              num_planilla = x.num_planilla
                          });
                    

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_registro_pago)
                                                        : query.OrderBy(x => x.id_registro_pago);

                    if (agrid.columna == "num_planilla") query = agrid.columna_orden == "DESC"
                                    ? query.OrderByDescending(x => x.num_planilla)
                                    : query.OrderBy(x => x.num_planilla);

                    if (agrid.columna == "id_empleado") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_empleado)
                                                        : query.OrderBy(x => x.id_empleado);

                    if (agrid.columna == "faltante") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.faltante)
                                                        : query.OrderBy(x => x.faltante);

                    if (agrid.columna == "total") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.total)
                                                        : query.OrderBy(x => x.total);


                    if (agrid.columna == "empleado") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.empleado)
                                                        : query.OrderBy(x => x.empleado);

                    if (agrid.columna == "cierre") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.cierre)
                                                        : query.OrderBy(x => x.cierre);

                    var registros = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    var total = query.Count();

                    agrid.SetData(registros, total);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarRegistro(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var registro = ctx.tbl_registro_pago.Where(x => x.id_registro_pago == id).SingleOrDefault();
                    registro.estado = "I";

                    ctx.Entry(registro).State = EntityState.Modified;
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel CierrePlanilla(string num_planilla)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var planilla = ctx.tbl_planilla.Where(x => x.num_planilla == num_planilla).SingleOrDefault();
                    planilla.cerrada = 1;

                    ctx.Entry(planilla).State = EntityState.Modified;
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rm;
        }

    }
}
