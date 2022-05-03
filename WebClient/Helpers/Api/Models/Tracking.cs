namespace WebClient.Helpers.Api.Models
{
    public class Track
    {
        public int trackingInfoId { get; set; }
        public string trackingCode { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public DateTime creationDate { get; set; }
    }

    public class TrackSimple
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}
