using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingClubSystem.Domain.Entities
{
    public class Reunion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La Fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El Tema es obligatorio.")]
        [StringLength(100, ErrorMessage = "El tema no puede exceder los 100 caracteres.")]
        public string Tema { get; set; }

        [Required(ErrorMessage = "El Club es obligatorio.")]
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
    }
}
