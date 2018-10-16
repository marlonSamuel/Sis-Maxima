namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_tipo_prestacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_tipo_prestacion()
        {
            tbl_prestacion = new HashSet<tbl_prestacion>();
        }

        [Key]
        public int id_tipo { get; set; }

        [Required]
        [StringLength(20)]
        public string prestacion { get; set; }

        [StringLength(20)]
        public string detalle_prestacion { get; set; }

       
        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

       
        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

       
        [StringLength(1)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_prestacion> tbl_prestacion { get; set; }

        //metodo para listar
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    agrid.Inicializar();

                    var query = ctx.tbl_tipo_prestacion.Where(x => x.id_tipo > 0).Where(x => x.estado == "A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_tipo)
                                                        : query.OrderBy(x => x.id_tipo);

                    if (agrid.columna == "prestacion") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.prestacion)
                                                        : query.OrderBy(x => x.prestacion);

                    if (agrid.columna == "detalle_prestacion") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.detalle_prestacion)
                                                        : query.OrderBy(x => x.detalle_prestacion);


                    var tipo_prestacion = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    agrid.SetData(
                            from e in tipo_prestacion
                            select new
                            {
                                e.id_tipo,
                                e.prestacion,
                                e.detalle_prestacion
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

        //listar todos tipo de prestaciones
        public List<tbl_tipo_prestacion> GetAll()
        {
            var list = new List<tbl_tipo_prestacion>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    list = ctx.tbl_tipo_prestacion.Where(x => x.estado == "A").ToList();
                }
            }
            catch
            {

            }
            return list;
        }

        //metodo para obtener tipo prestacion por id
        public tbl_tipo_prestacion ObtenerPorId(int id)
        {
            var tipo_prestacion = new tbl_tipo_prestacion();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    tipo_prestacion = ctx.tbl_tipo_prestacion.Where(x => x.id_tipo == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipo_prestacion;
        }

        //metodo para insertar o actualizar
        public ResponseModel Savetipo_prestacion()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_tipo > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el tipo de prestacion");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel Eliminartipo_prestacion(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var tipo_prestacion = ctx.tbl_tipo_prestacion.Where(x => x.id_tipo == id).SingleOrDefault();
                    tipo_prestacion.estado = "I";

                    ctx.Entry(tipo_prestacion).State = EntityState.Modified;
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
