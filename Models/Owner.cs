using System;
using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;
using WebApplication5.Models.Domain;

namespace WebApplication5.Models
{
    public class Owner : Emploee
    {
        [Display(Name = "Департамент")]
        public int? depID { get; set; }
        public virtual Departament? Departament { get; set; }
        public virtual User? User { get; set; }

    }
}
