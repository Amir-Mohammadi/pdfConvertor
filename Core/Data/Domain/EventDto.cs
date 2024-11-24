namespace amandaReport.Core.Data.Domain
{
    public partial class EventDto
    {
        public int id { get; set; }
        public string object_type { get; set; }
        public int zone_id { get; set; }
        public int unit_id { get; set; }
        public string ip { get; set; }
        public string image { get; set; }
        public DateTime datetime { get; set; }
    }
}
