using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingClubSystem.Domain.Entities
{
    public class Club
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Relación con los usuarios del club
        public List<Usuario> Miembros { get; set; }
        public List<Reunion> Reuniones { get; set; }
    }
}

