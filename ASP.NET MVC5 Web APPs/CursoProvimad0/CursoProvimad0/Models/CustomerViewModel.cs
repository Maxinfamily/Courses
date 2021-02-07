using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MisDatos2;

namespace CursoProvimad0.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nanashi no customer")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Ciudad")]
        public string City { get; set; }
        [Display(Name = "Pais")]
        public string Country { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "No puedes llamar sin teléfono")]
        public string Phone { get; set; }
    }
}