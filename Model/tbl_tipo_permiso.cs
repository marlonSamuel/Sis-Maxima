namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_tipo_permiso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_tipo_permiso()
        {
            tbl_permiso = new HashSet<tbl_permiso>();
        }

        [Key]
        public int id_tipo_permiso { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre { get; set; }

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
        public virtual ICollection<tbl_permiso> tbl_permiso { get; set; }

        //metodo para listar tipo permiso
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    agrid.Inicializar();

                    var query = ctx.tbl_tipo_permiso.Where(x => x.id_tipo_permiso > 0).Where(x => x.estado == "A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_tipo_permiso)
                                                        : query.OrderBy(x => x.id_tipo_permiso);

                    if (agrid.columna == "nombre") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.nombre)
                                                        : query.OrderBy(x => x.nombre);

                    if (agrid.columna == "detalle") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.detalle)
                                                        : query.OrderBy(x => x.detalle);


                    var tipo_permiso = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    agrid.SetData(
                            from e in tipo_permiso
                            select new
                            {
                                e.id_tipo_permiso,
                                e.nombre,
                                e.detalle
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
        public List<tbl_tipo_permiso> GetAll()
        {
            var list = new List<tbl_tipo_permiso>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    list = ctx.tbl_tipo_permiso.Where(x => x.estado == "A").ToList();
                }
            }
            catch
            {

            }
            return list;
        }

        //metodo para obtener tipo permisos por id
        public tbl_tipo_permiso ObtenerPorId(int id)
        {
            var tipo_permiso = new tbl_tipo_permiso();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    tipo_permiso = ctx.tbl_tipo_permiso.Where(x => x.id_tipo_permiso == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipo_permiso;
        }

        //metodo para insertar o actualizar
        public ResponseModel Savetipo_permiso()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_tipo_permiso > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el tipo de permiso");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel Eliminartipo_permiso(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var tipo_permiso = ctx.tbl_tipo_permiso.Where(x => x.id_tipo_permiso == id).SingleOrDefault();
                    tipo_permiso.estado = "I";

                    ctx.Entry(tipo_permiso).State = EntityState.Modified;
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
