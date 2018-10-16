namespace SCP_MAXIMA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_empleado()
        {
            tbl_anticipo = new HashSet<tbl_anticipo>();
            tbl_asueto = new HashSet<tbl_asueto>();
            tbl_permiso = new HashSet<tbl_permiso>();
            tbl_prestacion_empleado = new HashSet<tbl_prestacion_empleado>();
            tbl_prestacion = new HashSet<tbl_prestacion>();
            tbl_prestamo = new HashSet<tbl_prestamo>();
            tbl_registro_pago = new HashSet<tbl_registro_pago>();
            tbl_sancion = new HashSet<tbl_sancion>();
            tbl_vacacion = new HashSet<tbl_vacacion>();
        }

        [Key]
        public int id_empleado { get; set; }

        public int id_cargo { get; set; }

        public int id_estacion { get; set; }

        [Required]
        [StringLength(20)]
        public string primer_nombre { get; set; }

        [StringLength(20)]
        public string segundo_nombre { get; set; }

        [StringLength(20)]
        public string tercer_nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string primer_apellido { get; set; }

        [StringLength(20)]
        public string segundo_apellido { get; set; }

        [Required]
        [StringLength(50)]
        public string direccion { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_nac { get; set; }

        public int telefono { get; set; }

        [Required]
        [StringLength(10)]
        public string nit { get; set; }

        [StringLength(50)]
        public string correo { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public int dpi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_anticipo> tbl_anticipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_asueto> tbl_asueto { get; set; }

        public virtual tbl_cargo tbl_cargo { get; set; }

        public virtual tbl_estacion tbl_estacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_permiso> tbl_permiso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_prestacion_empleado> tbl_prestacion_empleado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_prestacion> tbl_prestacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_prestamo> tbl_prestamo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_registro_pago> tbl_registro_pago { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_sancion> tbl_sancion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_vacacion> tbl_vacacion { get; set; }
    }
}
