using System.Data.SqlClient;
using System.Data;
using TallerMVC.Models;
using TallerMVC.Models.DTO;

namespace TallerMVC.Data
{
    public class CitasDatos
    {
        public List<CitasView> Listar(int id)
        {
            var oLista = new List<CitasView>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtener_estados_citas", conexion);
                cmd.Parameters.AddWithValue("usuario_id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new CitasView()
                        {
                            fecha = dr["fecha"].ToString(),
                            hora = dr["horario"].ToString(),
                            status = dr["estado"].ToString(),
                            statusDescripcion = dr["descripcion"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public Citas obtenerCita(int id)
        {
            var oCitas = new Citas();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spBuscarCitaPorId", conexion);
                cmd.Parameters.AddWithValue("id",id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oCitas.id = Convert.ToInt32(dr["id"]);
                        oCitas.usuario_id = Convert.ToInt32(dr["usuario_id"]);
                        oCitas.vehiculo_id = Convert.ToInt32(dr["vehiculo_id"]);
                    }
                }
            }
            return oCitas;
        }

        public bool Guardar(CitasDetalles citasDetalles)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spInsertarCitaConDetalles", conexion);
                    cmd.Parameters.AddWithValue("vehiculo_id", citasDetalles.vehiculo_id);
                    cmd.Parameters.AddWithValue("usuario_id", citasDetalles.usuario_id);
                    cmd.Parameters.AddWithValue("fecha", citasDetalles.fecha);
                    cmd.Parameters.AddWithValue("horario", citasDetalles.hora);
                    cmd.Parameters.AddWithValue("descripcion", citasDetalles.descripcion);
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

        public bool Editar(Usuarios ousuario)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("", conexion);
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
                    SqlCommand cmd = new SqlCommand("", conexion);
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
