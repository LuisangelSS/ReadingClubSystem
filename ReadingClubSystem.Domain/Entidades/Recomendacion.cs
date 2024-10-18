using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingClubSystem.Domain.Entities
{
    public class Recomendacion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int LibroId { get; set; }
        public Libro Libro { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }

        public DateTime Fecha { get; set; }
    }
}

