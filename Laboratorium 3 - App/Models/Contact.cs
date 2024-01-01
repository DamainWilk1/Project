using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Laboratorium_3___App.Models
{
    public class Contact
    {
        [Display(Name = "Priorytet")]
        public Priority Priority { get; set; }
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Musisz podać imię!")]
        [StringLength(maximumLength: 50, ErrorMessage = "Imię zbyt długie maksymalnie 50 znaków")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Musissz podać poprawny email! (Brak znaku: @)")]
        public string Email { get; set; }
        [Display(Name = "Numer Telefonu")]
        [Phone(ErrorMessage = "Numer telefonu powinien zawierać cyfry")]
        public string Phone { get; set; }
        [Display(Name = "Data Urodzin")]
        [DataType(DataType.Date)]
        public DateTime? Birth { get; set; }

        public int? OrganizationId { get; set; }
        [ValidateNever]
        public List<SelectListItem> OrganizationsList { get; set; }
        

        [HiddenInput]
        public DateTime Created { get; set; }
    }
}