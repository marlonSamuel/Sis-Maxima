namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_prestamo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_prestamo()
        {
            tbl_abono = new HashSet<tbl_abono>();
        }

        [Key]
        public int id_prestamo { get; set; }

        public int id_empleado { get; set; }

        public decimal monto { get; set; }

        public decimal saldo { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_abono> tbl_abono { get; set; }

        public virtual tbl_empleado tbl_empleado { get; set; }

        //metodo para listar
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    agrid.Inicializar();

                    var query = ctx.tbl_prestamo.Where(x => x.id_prestamo > 0).Where(x => x.estado == "A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_prestamo)
                                                        : query.OrderBy(x => x.id_prestamo);

                    if (agrid.columna == "id_empleado") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_empleado)
                                                        : query.OrderBy(x => x.id_empleado);

                    if (agrid.columna == "monto") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.monto)
                                                        : query.OrderBy(x => x.monto);
                    if (agrid.columna == "detalle") query = agrid.columna_orden == "DESC"
                                    ? query.OrderByDescending(x => x.detalle)
                                    : query.OrderBy(x => x.detalle);


                    var prestamos = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    var total = query.Count();

                    agrid.SetData(prestamos, total);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();
        }

        //metodo para obtener tipo empleado por id
        public tbl_prestamo ObtenerEmpleadoId(int id)
        {
            var prestamo = new tbl_prestamo();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    prestamo = ctx.tbl_prestamo.Where(x => x.id_empleado == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return prestamo;
        }

        //listar todos los prestamos
        public List<tbl_prestamo> GetAll()
        {
            var list = new List<tbl_prestamo>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    list = ctx.tbl_prestamo.Where(x => x.estado == "A").ToList();
                }
            }
            catch
            {

            }
            return list;
        }

        //metodo para obtener prestamos
        public tbl_prestamo ObtenerPorId(int id)
        {
            var prestamo = new tbl_prestamo();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    prestamo = ctx.tbl_prestamo.Where(x => x.id_prestamo == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return prestamo;
        }

        //metodo para insertar o actualizar
        public ResponseModel SavePrestamo()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_prestamo > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el cargo");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarPrestamo(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var prestamo = ctx.tbl_prestamo.Where(x => x.id_prestamo == id).SingleOrDefault();
                    prestamo.estado = "I";

                    ctx.Entry(prestamo).State = EntityState.Modified;
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
