using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("CarParks")]
    public class CarParksModel
    {
        [Key]
        public int CarParkID { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
