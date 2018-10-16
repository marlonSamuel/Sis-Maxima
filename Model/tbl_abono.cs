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

    public partial class tbl_abono
    {
        [Key]
        public int id_abono { get; set; }

        public int id_prestamo { get; set; }

        public decimal monto { get; set; }

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

        public virtual tbl_prestamo tbl_prestamo { get; set; }

        //metodo para listar tipo permiso
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    agrid.Inicializar();

                    var query = ctx.tbl_abono.Where(x => x.id_abono > 0).Where(x => x.estado == "A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_abono)
                                                        : query.OrderBy(x => x.id_abono);

                    if (agrid.columna == "monto") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.monto)
                                                        : query.OrderBy(x => x.monto);



                    var abono = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    agrid.SetData(
                            from e in abono
                            select new
                            {
                                e.id_abono,
                                e.monto,
                              
                            }
                        ,
                        query.Count()
                    );
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();
        }

        //listar todos tipo permisos
        public List<tbl_abono> GetAll()
        {
            var list = new List<tbl_abono>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    list = ctx.tbl_abono.Where(x => x.estado == "A").ToList();
                }
            }
            catch
            {

            }
            return list;
        }

        //metodo para obtener tipo permisos por i

        //metodo para obtener tipo permisos por id
        public tbl_abono ObtenerPorId(int id)
        {
            var abono = new tbl_abono();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    abono = ctx.tbl_abono.Where(x => x.id_abono == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return abono;
        }

        //metodo para obtener tipo empleado por id
        public DtoAbono ObtenerPrestamoId(int id, string planilla)
        {
            var abono = new DtoAbono();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    abono = (from a in ctx.tbl_abono
                                 join p in ctx.tbl_prestamo on a.id_prestamo equals p.id_prestamo
                                 join e in ctx.tbl_empleado on p.id_empleado equals e.id_empleado
                                 join r in ctx.tbl_registro_pago on e.id_empleado equals r.id_empleado
                                 where r.num_planilla == planilla
                                 && r.estado == "A"
                                 select new
                                 {
                                     id_abono = a.id_abono,
                                     monto = a.monto,
                                     detalle = a.detalle
                                 }
                        ).Select(x => new DtoAbono
                        {
                            id_abono = x.id_abono,
                            monto = x.monto,
                            detalle = x.detalle
                        }).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return abono;
        }

        //metodo para insertar o actualizar
        public ResponseModel SaveAbono()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_abono > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el Abono");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarAbono(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var abono = ctx.tbl_abono.Where(x => x.id_abono == id).SingleOrDefault();
                    abono.estado = "I";

                    ctx.Entry(abono).State = EntityState.Modified;
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
  
