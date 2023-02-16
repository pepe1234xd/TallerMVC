using TallerMVC.Models;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using TallerMVC.Models.DTO;

namespace TallerMVC.Data
{
    public class UsuarioDatos
    {
        public List<Usuarios> Listar()
        {
            var oLista = new List<Usuarios>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new Usuarios()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombres = dr["nombres"].ToString(),
                            apellidos = dr["apellidos"].ToString(),
                            email = dr["email"].ToString(),
                            contrasenia = dr["contrasenia"].ToString()
                        }); 
                    }
                }     
            }
            return oLista;
        }
        public Usuarios obtenerUsuarioId(int id)
        {
            var oUsuario = new Usuarios();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spBuscarUsuarioPorId", conexion);
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oUsuario.id = Convert.ToInt32(dr["id"]);
                        oUsuario.nombres = dr["nombres"].ToString();
                        oUsuario.apellidos = dr["apellidos"].ToString();
                        oUsuario.email = dr["email"].ToString();
                        oUsuario.contrasenia = dr["contrasenia"].ToString();
                    }
                }
            }
            return oUsuario;
        }
        public Usuarios obtenerUsuario(string email)
        {
            var oUsuario = new Usuarios();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spBuscarUsuarioPorEmail", conexion);
                cmd.Parameters.AddWithValue("email",email);
                cmd.CommandType=CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oUsuario.id = Convert.ToInt32(dr["id"]);
                        oUsuario.nombres = dr["nombres"].ToString();
                        oUsuario.apellidos = dr["apellidos"].ToString();
                        oUsuario.email = dr["email"].ToString();
                        oUsuario.contrasenia = dr["contrasenia"].ToString();
                       
                    }
                }  
            }

            if (oUsuario.email == null)
            {
                return null;
            }
            return oUsuario;
        }
        public bool Guardar(Usuarios ousuario)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spAgregarUsuario", conexion);
                    cmd.Parameters.AddWithValue("nombres", ousuario.nombres);
                    cmd.Parameters.AddWithValue("apellidos", ousuario.apellidos);
                    cmd.Parameters.AddWithValue("email", ousuario.email);
                    cmd.Parameters.AddWithValue("contrasenia", ousuario.contrasenia);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta= true;
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                rpta = false;
            }

            return rpta;
        }
        public bool Editar(Usuarios ousuario)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spEditarUsuario", conexion);
                    cmd.Parameters.AddWithValue("id", ousuario.id);
                    cmd.Parameters.AddWithValue("nombres", ousuario.nombres);
                    cmd.Parameters.AddWithValue("apellidos", ousuario.apellidos);
                    cmd.Parameters.AddWithValue("email", ousuario.email);
                    cmd.Parameters.AddWithValue("contrasenia", ousuario.contrasenia);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                rpta = false;
            }

            return rpta;
        }
        public bool Eliminar(int idUsuario)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spEliminarUsuario", conexion);
                    cmd.Parameters.AddWithValue("id", idUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                rpta = false;
            }
            return rpta;
        }

    }
}
