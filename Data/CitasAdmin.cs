using System.Data.SqlClient;
using System.Data;
using TallerMVC.Models.DTO;
using TallerMVC.Models;

namespace TallerMVC.Data
{
    public class CitasAdmin
    {
        public List<CitasView> Listar()
        {
            var oLista = new List<CitasView>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtenerCitasAdmin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new CitasView()
                        {
                            id = dr.GetInt32("id"),
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

        public CitasView obtenerCita(int id)
        {
            var oCitas = new CitasView();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spObtenerCitaAdminId", conexion);
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oCitas.id = dr.GetInt32("id");
                        oCitas.fecha = dr["fecha"].ToString();
                        oCitas.hora = dr["horario"].ToString();
                        oCitas.status = dr["estado"].ToString();
                        oCitas.statusDescripcion = dr["descripcion"].ToString();
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

        public bool Editar(CitasView citasView)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spEditarEstado", conexion);
                    cmd.Parameters.AddWithValue("id", citasView.id);
                    cmd.Parameters.AddWithValue("estado", citasView.status);
                    cmd.Parameters.AddWithValue("descripcion", citasView.statusDescripcion);
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

