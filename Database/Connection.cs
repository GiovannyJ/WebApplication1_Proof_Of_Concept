using System.ComponentModel;
using System.Data;
using System.Security.Principal;
using MySql.Data.MySqlClient;
using WebApplication1_Proof_Of_Concept.Models;

namespace WebApplication1_Proof_Of_Concept.Database
{
    public class Connection
    {

        private static DataTable Connect(string query)
        {
            string connectionStr = $"server=aresfit.duckdns.org;port=5000;database=Woof_V2;uid=g_admin;pwd=dbGrbDzNNXX8gE7xL";

            using (var connection = new MySqlConnection(connectionStr))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[-]ERROR "+ ex.Message);
                    throw;
                }
            }
        }

        public static List<Users> GetUsers()
        {
            string sql = "SELECT * FROM user";

            DataTable result = Connect(sql);
            List<Users> users = new List<Users>();
            foreach (DataRow row in result.Rows)
            {
                Users user = new Users();
                user.UserID = row.Field<int>("userID");
                user.Username = row.Field<string>("username");
                user.Password = row.Field<string>("password");
                user.Email = row.Field<string>("email");
                user.ImgID = row.IsNull("imgID") ? (int?)null : row.Field<int>("imgID");
                user.AccountType = row.Field<string>("accountType");
                
                users.Add(user);
            }

            return users;
        }


        public static List<Businesses> GetBusinesses()
        {
            string sql = "SELECT * FROM businesses";
            List<Businesses> businesses = new List<Businesses>();
            DataTable resuslt = Connect(sql);
            foreach(DataRow row in resuslt.Rows)
            {
                Businesses business = new Businesses();
                business.BusinessID = row.Field<int>("businessID");
                business.BusinessName = row.Field<string>("businessName");
                business.OwnerUserID = row.Field<int>("ownerUserID");
                business.BusinessType = row.Field<string>("businessType");
                business.Location = row.Field<string>("location");
                business.Contact = row.Field<string>("contact");
                business.BusinessDescription = row.Field<string>("description");
                business.Events = row.Field<string>("events");
                business.Rating = row.Field<string>("rating");
                business.DataLocation = row.Field<string>("dataLocation");
                business.ImgID = row.IsNull("imgID") ? (int?)null : row.Field<int>("imgID");
                business.PetSizePref = row.Field<string>("petSizePref");
                business.LeashPolicy = row.Field<bool>("leashPolicy");
                business.DisabledFriendly = row.Field<bool>("disabledFriendly");
                business.GeoLocation = row.Field<string>("geolocation");

                businesses.Add(business);
            }

            return businesses;
        }
        
        
    }
}
