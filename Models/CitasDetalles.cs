using TallerMVC.Data.Validaciones;

namespace TallerMVC.Models
{
    public class CitasDetalles
    {
        public int usuario_id { get; set; }
        public int vehiculo_id { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string descripcion { get; set; }
    }
}
