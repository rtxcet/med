using System.ComponentModel.DataAnnotations.Schema;

namespace med.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

    }
}
