namespace Server.Models
{
    public class OccurencesModel
    {
        public int OccurenceID { get; set; }
        public int LicensePlateID { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int Duration { get; set; }
    }
}