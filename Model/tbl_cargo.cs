namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_cargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_cargo()
        {
            tbl_empleado = new HashSet<tbl_empleado>();
        }
   
        [Key]
        public int id_cargo { get; set; }
        public decimal? sueldo { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre_cargo { get; set; }

        [StringLength(200)]
        public string descripcion_cargo { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_empleado> tbl_empleado { get; set; }

        //metodo para listar
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    agrid.Inicializar();

                    var query = ctx.tbl_cargo.Where(x => x.id_cargo > 0).Where(x=>x.estado =="A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_cargo)
                                                        : query.OrderBy(x => x.id_cargo);

                    if (agrid.columna == "nombre_cargo") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.nombre_cargo)
                                                        : query.OrderBy(x => x.nombre_cargo);

                    if (agrid.columna == "descripcion_cargo") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.descripcion_cargo)
                                                        : query.OrderBy(x => x.descripcion_cargo);

                    if (agrid.columna == "sueldo") query = agrid.columna_orden == "DESC"
                                                       ? query.OrderByDescending(x => x.sueldo)
                                                       : query.OrderBy(x => x.sueldo);

                    var cargos = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    agrid.SetData(
                            from e in cargos
                            select new
                            {
                                e.id_cargo,
                                e.nombre_cargo,
                                e.descripcion_cargo,
                                e.sueldo
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

        //listar todos los cargos
        public List<tbl_cargo> GetAll()
        {
            var list = new List<tbl_cargo>();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    list = ctx.tbl_cargo.Where(x => x.estado == "A").ToList();
                }
            }
            catch
            {

            }
            return list;
        }

        //metodo para obtener cargo por id
        public tbl_cargo ObtenerPorId(int id)
        {
            var cargo = new tbl_cargo();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    cargo = ctx.tbl_cargo.Where(x => x.id_cargo == id).SingleOrDefault();
                }
            }
            catch(Exception)
            {
                throw;
            }
            return cargo;
        }

        //metodo para insertar o actualizar
        public ResponseModel SaveCargo()
        {
            var rm = new ResponseModel();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_cargo > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }catch(Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el cargo");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarCargo(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    var cargo = ctx.tbl_cargo.Where(x => x.id_cargo == id).SingleOrDefault();
                    cargo.estado = "I";

                    ctx.Entry(cargo).State = EntityState.Modified;
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
