using System.ComponentModel;
using System.Data;
using System.Security.Principal;
using Google.Protobuf;
using MySql.Data.MySqlClient;
using WebApplication1_Proof_Of_Concept.Models;

namespace WebApplication1_Proof_Of_Concept.Database
{
    public class Connection
    {
        private static readonly string _connectionString = $"server=aresfit.duckdns.org;port=5000;database=Woof_V2;uid=g_admin;pwd=dbGrbDzNNXX8gE7xL";

        /*
         Connection:
            connects to database taking query and any parameters (WHERE clauses)
        Parameters:
            query: the query to execute
            parameters: where clauses
        Returns:
            DataTable results of there query
         */
        private static DataTable Connect(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[-]ERROR " + ex.Message);
                    throw;
                }
            }
        }

        /*
         ============================= SELECT QUERIES =============================
         */

        /*
         Grabs all users from databases using filters (Where clause)
        returns: list of users
         */
        public static List<Users> GetUsers(Dictionary<string, object> filters)
        {
            string sql = "SELECT * FROM user";

            List<string> conditions = new List<string>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            foreach (var filter in filters)
            {
                conditions.Add($"{filter.Key} = @{filter.Key}");
                parameters.Add($"{filter.Key}", filter.Value);
            }

            if (conditions.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", conditions);
            }

            DataTable result = Connect(sql, parameters);
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

        /*
         Grabs all businesses from db using filters (Where clauses)
        returns: list of businesses
         */
        public static List<Businesses> GetBusinesses(Dictionary<string, object> filters)
        {
            string sql = "SELECT * FROM businesses";

            List<string> conditions = new List<string>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            foreach (var filter in filters)
            {
                conditions.Add($"{filter.Key} = @{filter.Key}");
                parameters.Add($"{filter.Key}", filter.Value);
            }

            if (conditions.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", conditions);
            }


            List<Businesses> businesses = new List<Businesses>();
            DataTable resuslt = Connect(sql, parameters);
            foreach (DataRow row in resuslt.Rows)
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
                business.Rating = row.IsNull("rating") ? (int?)null : row.Field<int>("rating");
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

        /*
        gets all joined table values for users owning business using filters and userID
        returns: list of user owned businesses
         */
        public static List<UserOwnedBusiness> GetUserOwnedBusiness(int userID, Dictionary<string, object> filters)
        {
            string sql = "SELECT u.userID, u.username, u.password, u.email, u.accountType, u.imgID, " +
                    "b.businessID, b.businessName, b.OwnerUserID, b.businessType, b.location, b.contact, b.description, " +
                    "b.events, b.rating, b.dataLocation, b.imgID, b.petSizePref, b.leashPolicy, b.disabledFriendly, b.geolocation " +
                    "FROM user u " +
                    "JOIN businesses b ON b.OwnerUserID = u.userID " +
                    $"WHERE u.userID = {userID}";

            List<string> conditions = new List<string>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            foreach (var filter in filters)
            {
                conditions.Add($"{filter.Key} = @{filter.Key}");
                parameters.Add($"{filter.Key}", filter.Value);
            }

            if (conditions.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", conditions);
            }



            List<UserOwnedBusiness> userOwnedBusinesses = new List<UserOwnedBusiness>();
            DataTable result = Connect(sql, parameters);
            foreach (DataRow row in result.Rows)
            {
                Businesses business = new Businesses();
                Users user = new Users();
                UserOwnedBusiness UoB = new UserOwnedBusiness();
                business.BusinessID = row.Field<int>("businessID");
                business.BusinessName = row.Field<string>("businessName");
                business.OwnerUserID = row.Field<int>("ownerUserID");
                business.BusinessType = row.Field<string>("businessType");
                business.Location = row.Field<string>("location");
                business.Contact = row.Field<string>("contact");
                business.BusinessDescription = row.Field<string>("description");
                business.Events = row.Field<string>("events");
                business.Rating = row.IsNull("rating") ? (int?)null : row.Field<int>("rating");
                business.DataLocation = row.Field<string>("dataLocation");
                business.ImgID = row.IsNull("imgID") ? (int?)null : row.Field<int>("imgID");
                business.PetSizePref = row.Field<string>("petSizePref");
                business.LeashPolicy = row.Field<bool>("leashPolicy");
                business.DisabledFriendly = row.Field<bool>("disabledFriendly");
                business.GeoLocation = row.Field<string>("geolocation");
                user.UserID = row.Field<int>("userID");
                user.Username = row.Field<string>("username");
                user.Password = row.Field<string>("password");
                user.Email = row.Field<string>("email");
                user.ImgID = row.IsNull("imgID") ? (int?)null : row.Field<int>("imgID");
                user.AccountType = row.Field<string>("accountType");

                UoB.Businesses = business;
                UoB.User = user;

                userOwnedBusinesses.Add(UoB);
            }

            return userOwnedBusinesses;

        }


        /*
         ============================= INSERT QUERIES =============================
         */

        /*
         inserts a new user into the database (NEED TO FIND BETTER WAY TO CHECK BOOL)
        returns: true if successful false if not
         */
        public static bool CreateUser(Users user)
        {
            string sql = "INSERT INTO user(username, password, email, accountType) " +
                $" VALUES('{user.Username}', '{user.Password}', '{user.Email}', '{user.AccountType}') ";
            _ = Connect(sql);

            return true;
        }


        /*
        inserts a new business into the database (NEED TO FIND A BETTER WAY TO CHECK BOOL)
        returns: true if successfull false if not
         */
        public static bool CreateBusiness(Businesses b)
        {
            string sql = "INSERT INTO businesses(businessName, OwnerUserID, businessType, location, contact, " +
                "description, dataLocation, petSizePref, leashPolicy, disabledFriendly, geolocation) " +
                $"VALUES('{b.BusinessName}', {b.OwnerUserID}, '{b.BusinessType}', '{b.Location}', '{b.Contact}', " +
                $"'{b.BusinessDescription}', '{b.DataLocation}', '{b.PetSizePref}', '{(b.LeashPolicy ? 1 : 0)}', '{(b.DisabledFriendly ? 1 : 0)}'," +
                $"'{b.GeoLocation}')";
            _ = Connect(sql);

            return true;
        }



        /*
         ============================= UPDATE QUERIES =============================
         */


        /*
         * updates value in table using list of columns and values
         * returns: true if successful false if not
         */
        public static bool UpdateValues(UpdateQuery q)
        {
            var setValues = new List<string>();
            var whereValues = new List<string>();

            for (int i = 0; i < q.ColumnsNew.Count; i++)
            {
                string columnName = q.ColumnsNew[i];
                object columnValue = q.ValuesNew[i];

                setValues.Add($"{columnName} = @NewValue{i}");
            }

            for (int i = 0; i < q.ColumnsOld.Count; i++)
            {
                string columnName = q.ColumnsOld[i];
                object columnValue = q.ValuesOld[i];

                whereValues.Add($"{columnName} = @OldValue{i}");
            }

            string sql = $"UPDATE {q.TableName} SET {string.Join(", ", setValues)} WHERE {string.Join(" AND ", whereValues)}";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        for (int i = 0; i < q.ColumnsNew.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@NewValue{i}", q.ValuesNew[i]);
                        }

                        for (int i = 0; i < q.ColumnsOld.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@OldValue{i}", q.ValuesOld[i]);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] ERROR: {ex.Message}");
                return false;
            }
        }


        /*
         ============================= DELETE QUERIES =============================
         */

        /*
         * deletes value in table using column and value
         * returns: true if successful false if not
         */
        public static bool DeleteValues(DeleteQuery q)
        {
            string sql;

            switch (q.TableName)
            {
                case "user":
                    sql = $@"
                        DELETE FROM attendance WHERE eventID IN (SELECT e.eventID FROM events e JOIN businesses b ON b.OwnerUserID = {q.ID} AND e.businessID = b.businessID);
                        DELETE FROM events WHERE businessID IN (SELECT businessID FROM businesses WHERE OwnerUserID = {q.ID});
                        DELETE FROM reviews WHERE userID = {q.ID};
                        DELETE FROM savedBusinesses WHERE userID = {q.ID};
                        DELETE FROM businesses WHERE OwnerUserID = {q.ID};
                        DELETE FROM user WHERE userID = {q.ID};";
                    break;

                case "businesses":
                    sql = $@"
                        DELETE FROM attendance WHERE eventID IN (SELECT eventID FROM events WHERE businessID = {q.ID});
                        DELETE FROM events WHERE businessID = {q.ID};
                        DELETE FROM reviews WHERE businessID = {q.ID};
                        DELETE FROM savedBusinesses WHERE businessID = {q.ID};
                        DELETE FROM businesses WHERE businessID = {q.ID};";
                    break;

                default:
                    sql = $"DELETE FROM {q.TableName} WHERE {q.Column} = {q.ID};";
                    break;
            }

            return ExecuteDelete(sql);
        }

        /*
         * helper method to exevute delete function
         */
        private static bool ExecuteDelete(string sql)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] ERROR: {ex.Message}");
                return false;
            }
        }

    }
}

