using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingClubSystem.Domain.Entities
{
    public class Recomendacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Usuario es obligatorio.")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage = "El Libro es obligatorio.")]
        public int LibroId { get; set; }
        public virtual Libro Libro { get; set; }

        [Required(ErrorMessage = "El Club es obligatorio.")]
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }

        [Required(ErrorMessage = "La Fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}
