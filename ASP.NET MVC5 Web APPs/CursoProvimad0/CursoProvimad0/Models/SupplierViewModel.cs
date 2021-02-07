using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoProvimad0.Models
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "No hay empresa")]
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "No hay pais para carcas")]
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int ProductsCount { get; set; }
        [Display(Name = "Disponibles")]
        public int AvailableProductsCount { get; set; }
    }
}