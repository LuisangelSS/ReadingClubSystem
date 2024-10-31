namespace ReadingClubSystem.Domain.ViewModels
{
    public class RecomendacionDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
        public int ClubId { get; set; }
        public DateTime Fecha { get; set; }
    }
    
}

