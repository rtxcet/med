using System.ComponentModel.DataAnnotations.Schema;

namespace med.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string WpPhone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }

        public string GoogleMaps { get; set; }

        public string SiteMetaKeyword { get; set; }
        public string SiteMetaDescription { get; set; }
        public string SiteMailAdres { get; set; }
        public string SiteMailHost{ get; set; }
        public string GoogleA { get; set; }
        public string GoogleT { get; set; }


    }
}
