namespace Server.Models
{
    public class BillsModel
    {
        public int BillID { get; set; }
        public int LicensePlateID { get; set; }
        public int Amount { get; set; }
        public int Paid { get; set; }
    }
}