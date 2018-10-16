namespace SCP_MAXIMA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_prestacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_prestacion()
        {
            tbl_prestacion_empleado = new HashSet<tbl_prestacion_empleado>();
        }

        [Key]
        public int id_prestacion { get; set; }

        public int id_empleado { get; set; }

        public int id_tipo { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre_prestacion { get; set; }

        [StringLength(500)]
        public string detalle_prestacion { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public virtual tbl_empleado tbl_empleado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_prestacion_empleado> tbl_prestacion_empleado { get; set; }

        public virtual tbl_tipo_prestacion tbl_tipo_prestacion { get; set; }
    }
}
