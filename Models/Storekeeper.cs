using System.ComponentModel.DataAnnotations;
namespace WebApplication5.Models
{
    public class Storekeeper : Person
    {
        [Display(Name = "Департамент")]
        public int depID { get; set; }
        public virtual Departament? Departament { get; set; }
        public virtual User? User { get; set; }
    }
}
