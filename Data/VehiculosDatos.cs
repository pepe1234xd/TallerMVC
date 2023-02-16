using System.Data.SqlClient;
using System.Data;
using TallerMVC.Models;
using System.Reflection;

namespace TallerMVC.Data
{
    public class VehiculosDatos
    {
        public List<vehiculos> Listar(int id)
        {
            var oLista = new List<vehiculos>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spBuscarVehiculosPorUsuario", conexion);
                cmd.Parameters.AddWithValue("usuario_id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new vehiculos()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            usuario_id = id,
                            marca = dr["marca"].ToString(),
                            modelo = dr["modelo"].ToString(),
                            placas = dr["placas"].ToString(),
                        });
                    }
                }
            }
            return oLista;
        }

        public vehiculos obtenerVehiculo(int id)
        { 
            var oVehiculo = new vehiculos();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.cadenaConexion()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spBuscarVehiculoPorId", conexion);
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oVehiculo.id = Convert.ToInt32(dr["id"]);
                        oVehiculo.usuario_id = Convert.ToInt32(dr["usuario_id"]);
                        oVehiculo.marca = dr["marca"].ToString();
                        oVehiculo.modelo = dr["modelo"].ToString();
                        oVehiculo.placas = dr["placas"].ToString();
                    }
                }
            }

            if (oVehiculo.id == null)
            {
                return null;
            }
            return oVehiculo;
        }

        public bool Guardar(vehiculos ovehiculos)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("InsertarVehiculo", conexion);
                    cmd.Parameters.AddWithValue("usuario_id", ovehiculos.usuario_id);
                    cmd.Parameters.AddWithValue("marca", ovehiculos.marca);
                    cmd.Parameters.AddWithValue("modelo", ovehiculos.modelo);
                    cmd.Parameters.AddWithValue("placas", ovehiculos.placas);
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
