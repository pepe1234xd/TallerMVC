using System.Data.SqlClient;
using System.Data;
using TallerMVC.Models.DTO;
using TallerMVC.Models;

namespace TallerMVC.Data
{
    public class ValidarData
    {
        public int ValidarCita(string hora, DateTime dia)
        {
            int count = 0;  
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.cadenaConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("VerificarCoincidencia", conexion);
                    cmd.Parameters.AddWithValue("fecha",dia.Date);
                    cmd.Parameters.AddWithValue("hora",hora);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            count = Convert.ToInt32(dr["ExisteCoincidencia"]);
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return count;
        }
    }
}
