namespace TallerMVC.Models.DTO
{
    public class UsuariosRegisterDTO
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string contrasenia { get; set; }
        public string confirmarContrasenia { get; set; }
    }
}
