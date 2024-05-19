using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WebApplication1_Proof_Of_Concept.Models;
using WebApplication1_Proof_Of_Concept.Database;

namespace WebApplication1_Proof_Of_Concept.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WoofDatabaseController : ControllerBase
    {

        /*
         ================= GET ROUTES =================
         */

        [HttpGet]
        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Users>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var query = HttpContext.Request.Query;
                Dictionary<string, object> filters = new Dictionary<string, object>();
                foreach (var item in query)
                {
                    filters.Add(item.Key, item.Value.ToString());
                }

                List<Users> result = Connection.GetUsers(filters);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving users.", error = ex.Message });
            }
        }

        [HttpGet]
        /// <summary>
        /// Gets all businesses from the database.
        /// </summary>
        /// <returns>A list of businesses.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Businesses>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetBusinesses()
        {
            try
            {
                var query = HttpContext.Request.Query;
                Dictionary<string, object> filters = new Dictionary<string, object>();
                foreach (var item in query)
                {
                    filters.Add(item.Key, item.Value.ToString());
                }

                List<Businesses> result = Connection.GetBusinesses(filters);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while retrieving businesses.", error = ex.Message });
            }

        }

        [HttpGet("{USERID}")]
        /// <summary>
        /// Gets all businesses owned by users from the database.
        /// </summary>
        /// <returns>A list of user owned businesses.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserOwnedBusiness>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetUsersBusinesses([FromRoute] int USERID)
        {
            try
            {
                var query = HttpContext.Request.Query;
                Dictionary<string, object> filters = new Dictionary<string, object>();
                foreach (var item in query)
                {
                    filters.Add(item.Key, item.Value.ToString());
                }

                List<UserOwnedBusiness> result = Connection.GetUserOwnedBusiness(USERID, filters);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while retrieving the user owned business", error = ex.Message });
            }
        }



        /*
         ================= POST ROUTES =================
        */

        [HttpPost]
        //<summary>
        // Adds account to the database
        //</summary>
        //<returns>JSON obj of user that was created</returns>
        [ProducesResponseType(typeof(Users), 201)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult NewUser([FromBody] Users newUser)
        { 
            if (newUser == null)
            {
                return BadRequest("Invalid User Data.");
            }

            try
            {
                var result = Connection.CreateUser(newUser);
                if (!result)
                {
                    return BadRequest(new { error = "Error executing query" });
                }

                var queryParams = new Dictionary<string, object>()
                {
                    {"username", newUser.Username }
                };
                var user = Connection.GetUsers(queryParams);
                if(user == null)
                {
                    return StatusCode(500, new { error = "An error occured retrieving acocunt from db" });
                }

                return CreatedAtAction(nameof(NewUser), user);
            }
            catch (Exception e)
            {
                return StatusCode(500, new {error = e.Message});
            }
            
        }


        [HttpPost]
        //<summary>
        // Adds account to the database
        //</summary>
        //<returns>JSON obj of business that was created</returns>
        [ProducesResponseType(typeof(Businesses), 201)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult NewBusiness([FromBody] Businesses newBusiness)
        {
            if (newBusiness == null)
            {
                return BadRequest("Invalid User Data.");
            }

            try
            {
                var result = Connection.CreateBusiness(newBusiness);
                if (!result)
                {
                    return BadRequest(new { error = "Error executing query" });
                }

                var queryParams = new Dictionary<string, object>()
                {
                    {"businessName", newBusiness.BusinessName }
                };
                var businesses = Connection.GetBusinesses(queryParams);
                if (businesses == null)
                {
                    return StatusCode(500, new { error = "An error occured retrieving acocunt from db" });
                }

                return CreatedAtAction(nameof(NewBusiness), businesses);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }

        }


        /*
         ================= PATCH ROUTES =================
        */

        [HttpPatch]
        //<summary>
        //updates table with new values using old columns and values
        //</summary>
        //<returns>Message saying success or failure</returns>
        [HttpPatch]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(500)]
        public IActionResult Update([FromBody] UpdateQuery uQuery)
        {
            if (uQuery == null)
            {
                return BadRequest("Invalid parameters");
            }

            try
            {
                var result = Connection.UpdateValues(uQuery);
                if (!result)
                {
                    return BadRequest(new { error = "Error executing update query" });
                }
                return StatusCode(200, new {message = $"{uQuery.TableName} Updated"});

            }
            catch (Exception e)
            {
                return StatusCode(500, new {error = e.Message});
            }
        }



        /*
         ================= DELETE ROUTES =================
        */

        [HttpDelete]
        //<summary>
        //deletes value from table 
        //</summary>
        //<returns>Message saying success or failure</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromBody] DeleteQuery dQuery)
        {
            if (dQuery == null)
            {
                return BadRequest("Invalid parameters");
            }

            try
            {
                var result = Connection.DeleteValues(dQuery);
                if (!result)
                {
                    return BadRequest(new { error = "Error executing update query" });
                }
                return StatusCode(200, new { message = $"{dQuery.TableName} {dQuery.ID} Deleted" });

            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
