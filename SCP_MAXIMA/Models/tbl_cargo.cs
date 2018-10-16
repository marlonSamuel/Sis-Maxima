namespace SCP_MAXIMA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_cargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_cargo()
        {
            tbl_empleado = new HashSet<tbl_empleado>();
        }

        [Key]
        public int id_cargo { get; set; }

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

        public decimal? sueldo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_empleado> tbl_empleado { get; set; }
    }
}
