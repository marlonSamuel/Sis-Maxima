namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MaximaContext : DbContext
    {
        public MaximaContext()
            : base("name=MaximaContext")
        {
        }
        
        public virtual DbSet<tbl_abono> tbl_abono { get; set; }
        public virtual DbSet<tbl_anticipo> tbl_anticipo { get; set; }
        public virtual DbSet<tbl_asueto> tbl_asueto { get; set; }
        public virtual DbSet<tbl_cargo> tbl_cargo { get; set; }
        public virtual DbSet<tbl_empleado> tbl_empleado { get; set; }
        public virtual DbSet<tbl_estacion> tbl_estacion { get; set; }
        public virtual DbSet<tbl_permiso> tbl_permiso { get; set; }
        public virtual DbSet<tbl_planilla> tbl_planilla { get; set; }
        public virtual DbSet<tbl_prestacion> tbl_prestacion { get; set; }
        public virtual DbSet<tbl_prestamo> tbl_prestamo { get; set; }
        public virtual DbSet<tbl_registro_pago> tbl_registro_pago { get; set; }
        public virtual DbSet<tbl_rol> tbl_rol { get; set; }
        public virtual DbSet<tbl_sancion> tbl_sancion { get; set; }
        public virtual DbSet<tbl_tipo_permiso> tbl_tipo_permiso { get; set; }
        public virtual DbSet<tbl_tipo_prestacion> tbl_tipo_prestacion { get; set; }
        public virtual DbSet<tbl_usuario> tbl_usuario { get; set; }
        public virtual DbSet<tbl_vacacion> tbl_vacacion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_abono>()
                .Property(e => e.monto)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_abono>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_abono>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_abono>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_abono>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_anticipo>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_anticipo>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_anticipo>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_anticipo>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_asueto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_asueto>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_asueto>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_asueto>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_cargo>()
                .Property(e => e.nombre_cargo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_cargo>()
                .Property(e => e.descripcion_cargo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_cargo>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_cargo>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_cargo>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_cargo>()
                .Property(e => e.sueldo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<tbl_cargo>()
                .HasMany(e => e.tbl_empleado)
                .WithRequired(e => e.tbl_cargo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.primer_nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.segundo_nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.tercer_nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.primer_apellido)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.segundo_apellido)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.nit)
                .IsFixedLength();

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.usarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_empleado>()
                .HasMany(e => e.tbl_anticipo)
                .WithRequired(e => e.tbl_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_empleado>()
                .HasMany(e => e.tbl_permiso)
                .WithRequired(e => e.tbl_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_empleado>()
                .HasMany(e => e.tbl_prestacion)
                .WithRequired(e => e.tbl_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_empleado>()
                .HasMany(e => e.tbl_prestamo)
                .WithRequired(e => e.tbl_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_empleado>()
                .HasMany(e => e.tbl_registro_pago)
                .WithRequired(e => e.tbl_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_empleado>()
                .HasMany(e => e.tbl_sancion)
                .WithRequired(e => e.tbl_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_empleado>()
                .HasMany(e => e.tbl_vacacion)
                .WithRequired(e => e.tbl_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_estacion>()
                .Property(e => e.nombre_estacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_estacion>()
                .Property(e => e.ubicacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_estacion>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_estacion>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_estacion>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_estacion>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_estacion>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_estacion>()
                .HasMany(e => e.tbl_empleado)
                .WithRequired(e => e.tbl_estacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_permiso>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_permiso>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_permiso>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_permiso>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_permiso>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_planilla>()
                .Property(e => e.num_planilla)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_planilla>()
                .HasMany(e => e.tbl_registro_pago)
                .WithRequired(e => e.tbl_planilla)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_prestacion>()
                .Property(e => e.nombre_prestacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestacion>()
                .Property(e => e.detalle_prestacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestacion>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestacion>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestacion>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestamo>()
                .Property(e => e.monto)
                .HasPrecision(10, 2);

            modelBuilder.Entity<tbl_prestamo>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestamo>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestamo>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestamo>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_prestamo>()
                .Property(e => e.saldo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<tbl_prestamo>()
                .HasMany(e => e.tbl_abono)
                .WithRequired(e => e.tbl_prestamo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_registro_pago>()
                .Property(e => e.num_planilla)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_registro_pago>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_registro_pago>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_registro_pago>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_registro_pago>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_registro_pago>()
                .Property(e => e.faltante)
                .HasPrecision(10, 0);

            modelBuilder.Entity<tbl_rol>()
                .Property(e => e.rol)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_rol>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_sancion>()
                .Property(e => e.nombre_sancion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_sancion>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_sancion>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_sancion>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_sancion>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_sancion>()
                .Property(e => e.monto)
                .HasPrecision(10, 2);

            modelBuilder.Entity<tbl_tipo_permiso>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_permiso>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_permiso>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_permiso>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_permiso>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_permiso>()
                .HasMany(e => e.tbl_permiso)
                .WithRequired(e => e.tbl_tipo_permiso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_tipo_prestacion>()
                .Property(e => e.prestacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_prestacion>()
                .Property(e => e.detalle_prestacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_prestacion>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_prestacion>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_prestacion>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_tipo_prestacion>()
                .HasMany(e => e.tbl_prestacion)
                .WithRequired(e => e.tbl_tipo_prestacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_usuario>()
                .Property(e => e.nombre_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_usuario>()
                .Property(e => e.contraseña)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_usuario>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_usuario>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_vacacion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_vacacion>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_vacacion>()
                .Property(e => e.usuarioCreo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_vacacion>()
                .Property(e => e.usuarioActualizo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_vacacion>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
