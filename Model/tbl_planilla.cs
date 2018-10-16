namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_planilla
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_planilla()
        {
            tbl_registro_pago = new HashSet<tbl_registro_pago>();
        }

        [Key]
        [StringLength(10)]
        public string num_planilla { get; set; }

        public DateTime fecha { get; set; }

        public DateTime fecha_fin { get; set; }

        public int cerrada { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_registro_pago> tbl_registro_pago { get; set; }

        public List<tbl_planilla> ListarPlanilla()
        {
            var list = new List<tbl_planilla>();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    list = ctx.tbl_planilla.ToList();
                }
            }catch(Exception e)
            {
                throw e;
            }

            return list;
        }

        //metodo para obtener planilla por id
        public tbl_planilla ObtenerPlanilla()
        {
            var planilla = new tbl_planilla();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    planilla = ctx.tbl_planilla.Where(x => x.cerrada == 0).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return planilla;
        }

    }
}
