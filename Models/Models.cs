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
        public int? Rating { get; set; }
        public string? DataLocation { get; set; }
        public int? ImgID { get; set; }
        public string? PetSizePref { get; set; }
        public bool LeashPolicy { get; set; }
        public bool DisabledFriendly { get; set; }
        public string? GeoLocation { get; set; }
    }

    public class Users
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? AccountType { get; set; }
        public int? ImgID { get; set; }
    }

    public class UserOwnedBusiness
    {
        public Users? User { get; set; }
        public Businesses? Businesses { get; set; }
    }

    public class UpdateQuery
    {
        public string? TableName { get; set; }
        public List<string> ColumnsOld { get; set; }
        public List<object> ValuesOld { get; set; }
        public List<string> ColumnsNew { get; set; }
        public List<object> ValuesNew { get; set; }
    }

    public class DeleteQuery
    {
        public string? TableName { get; set; }
        public string? Column { get; set; }
        public int ID { get; set; }
    }
}
