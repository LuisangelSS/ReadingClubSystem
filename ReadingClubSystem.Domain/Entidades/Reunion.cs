using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingClubSystem.Domain.Entities
{
    public class Reunion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}

