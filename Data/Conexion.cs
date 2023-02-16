using System.Data.SqlClient;

namespace TallerMVC.Data
{
    public class Conexion
    {
        private string _cadenasql;

        public Conexion()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _cadenasql = builder.GetSection("ConnectionStrings:db").Value;
        }

        public string cadenaConexion()
        {
            return _cadenasql;
        }
    }
}
