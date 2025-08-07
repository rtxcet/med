using System.ComponentModel.DataAnnotations.Schema;

namespace med.Models
{
    public class Partner
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string? ImageUrl { get; set; }

    }
}
