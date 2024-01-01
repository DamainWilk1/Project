using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Laboratorium_3___App.Models
{
    public enum Priority
    {
        [Display(Name = "Niski")] Low,
        [Display(Name = "Normalny")] Normal,
        [Display(Name = "Wysoki")] High,
        [Display(Name = "Pilny")] Urgent
    }
}
