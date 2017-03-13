using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class BillModel
    {
        [Key]
        public int BillId { get; set; }

        public string LicensePlate { get; set; }

        public string Date { get; set; }

        public string Entrence { get; set; }

        public string Exit { get; set; }

        public string Duration { get; set; }

        public string Amount { get; set; }

        public bool Paied { get; set; }
    }
}
