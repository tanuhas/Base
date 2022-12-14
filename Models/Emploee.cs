using System.ComponentModel.DataAnnotations;
namespace WebApplication5.Models
{
    public class Emploee
    {
        public int ID { get; set; }

        [Display(Name = "Инициалы")]
        public string Name { get; set; }
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        [Display(Name = "Пользователь")]
        public string? UserID { get; set; }

    }
}
