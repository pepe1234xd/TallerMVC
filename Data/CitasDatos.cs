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
                            id = dr.GetInt32("id"),
                            fecha = (DateTime)dr["fecha"],
                            hora = dr["horario"].ToString(),
                            status = dr["estado"].ToString(),
                            statusDescripcion = dr["descripcion"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }
        public CitasDetalles obtenerCita(int id)
        {
            var oCitas = new CitasDetalles();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerDetallesCitas", conexion);
                cmd.Parameters.AddWithValue("cita_id",id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oCitas.id = dr.GetInt32("id");
                        oCitas.usuario_id = dr.GetInt32("usuario_id");
                        oCitas.vehiculo_id = dr.GetInt32("vehiculo_id");
                        oCitas.fecha = (DateTime)dr["fecha"];
                        oCitas.hora = dr["horario"].ToString();
                        oCitas.descripcion = dr["descripcion"].ToString();
                        
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
        public bool Editar(CitasDetalles citasDetalles)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spEditarCita", conexion);
                    cmd.Parameters.AddWithValue("id", citasDetalles.id);
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
        public bool Eliminar(int id)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spEliminarCitas", conexion);
                    cmd.Parameters.AddWithValue("id", id);
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
