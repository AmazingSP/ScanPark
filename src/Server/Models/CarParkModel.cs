using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class CarParkModel
    {
        [Key]
        public int CarParkId { get; set; }

        public string CarkParkName { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }
    }
}
