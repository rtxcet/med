using System.ComponentModel.DataAnnotations;

namespace med.Models
{
    public class ChangeUsernameViewModel
    {
        [Required]
        [Display(Name = "Yeni Kullanıcı Adı")]
        public string NewUsername { get; set; }
    }
}
