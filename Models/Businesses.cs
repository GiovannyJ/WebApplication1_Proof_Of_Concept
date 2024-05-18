namespace WebApplication1_Proof_Of_Concept.Models
{
    public class Businesses
    {
        public int BusinessID { get; set; }
        public string? BusinessName { get; set; }
        public int OwnerUserID { get; set; }
        public string? BusinessType { get; set; }
        public string? Location { get; set; }
        public string? Contact { get; set; }
        public string? BusinessDescription { get; set; }
        public string? Events { get; set; }
        public string? Rating { get; set; }
        public string? DataLocation { get; set; }
        public int? ImgID { get; set; }
        public string? PetSizePref { get; set; }
        public bool LeashPolicy { get; set; }
        public bool DisabledFriendly { get; set; }
        public string? GeoLocation { get; set; }
    }
}
