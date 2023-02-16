using System.Data.SqlClient;
using System.Data;
using TallerMVC.Models;

namespace TallerMVC.Data
{
    public class AdminDatos
    {
        public List<Admins> Listar()
        {
            var oLista = new List<Admins>();

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
                        oLista.Add(new Admins()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombres = dr["nombres"].ToString(),
                            apellidos = dr["apellidos"].ToString(),
                            email = dr["email"].ToString(),
                            contrasenia = dr["contrasenia"].ToString(),
                            puesto = dr["puesto"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public Admins obtenerAdmin(string email)
        {
            var oAdmin = new Admins();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spBuscarAdminPorEmail", conexion);
                cmd.Parameters.AddWithValue("email", email);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oAdmin.id = Convert.ToInt32(dr["id"]);
                        oAdmin.nombres = dr["nombres"].ToString();
                        oAdmin.apellidos = dr["apellidos"].ToString();
                        oAdmin.email = dr["email"].ToString();
                        oAdmin.contrasenia = dr["contrasenia"].ToString();
                        oAdmin.puesto = dr["puesto"].ToString();
                    }
                }
            }

            if (oAdmin.email == null)
            {
                return null;
            }
            return oAdmin;
        }

        public bool Guardar(Admins oAdmin)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spAgregarAdmin", conexion);
                    cmd.Parameters.AddWithValue("nombres", oAdmin.nombres);
                    cmd.Parameters.AddWithValue("apellidos", oAdmin.apellidos);
                    cmd.Parameters.AddWithValue("email", oAdmin.email);
                    cmd.Parameters.AddWithValue("contrasenia", oAdmin.contrasenia);
                    cmd.Parameters.AddWithValue("puesto", oAdmin.puesto);
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

        public bool Editar(Admins oAdmin)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("", conexion);
                    cmd.Parameters.AddWithValue("id", oAdmin.id);
                    cmd.Parameters.AddWithValue("nombres", oAdmin.nombres);
                    cmd.Parameters.AddWithValue("apellidos", oAdmin.apellidos);
                    cmd.Parameters.AddWithValue("email", oAdmin.email);
                    cmd.Parameters.AddWithValue("contrasenia", oAdmin.contrasenia);
                    cmd.Parameters.AddWithValue("puesto", oAdmin.puesto);
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

        public bool Eliminar(string email)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("", conexion);
                    cmd.Parameters.AddWithValue("email", email);
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
