namespace Model
{
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_usuario
    {
        [Key]
        public int id_usuario { get; set; }

    
        public int? id_estacion { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string contraseña { get; set; }

        [Required]
        [StringLength(50)]
        public string correo { get; set; }

        
        [StringLength(1)]
        public string estado { get; set; }

        public int id_rol { get; set; }

        public virtual tbl_rol tbl_rol { get; set; }

        //Login de usuario
        public ResponseModel Acceder(string email, string contraseña)
        {
            var rm = new ResponseModel();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    contraseña = HashHelper.SHA256(contraseña);

                    var user = ctx.tbl_usuario.Where(x => x.correo == email || x.nombre_usuario == email)
                                               .Where(x=>x.contraseña == contraseña)
                                               .Where(x=>x.estado == "A")
                                              .SingleOrDefault();
                    if(user != null)
                    {
                        SessionHelper.AddUserToSession(user.id_usuario.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Usuario y/o contraseña incorrecta");
                    }

                }

            }catch(Exception e)
            {
                throw e;
            }

            return rm;
        }

        //obtener usuario por Id
        public tbl_usuario ObtenerUsuario(int id)
        {
            var usuario = new tbl_usuario();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    usuario = ctx.tbl_usuario.Where(x => x.id_usuario == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return usuario;
        }
    }
}
