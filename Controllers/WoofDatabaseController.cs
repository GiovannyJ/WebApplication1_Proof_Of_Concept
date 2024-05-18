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
                string filter = query.ContainsKey("filter") ? query["filter"].ToString() : string.Empty;

                List<Users> result = Connection.GetUsers();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving users.", error = ex.Message });
            }
        }

        public IActionResult GetBusinesses() 
        {
            try
            {
                List<Businesses> result = Connection.GetBusinesses();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while retrieving businesses.", error = ex.Message });
            }

        }

       /* public IActionResult GetUsersBusinesses()
        {
            return null;
        }
*/





        /*
         ================= POST ROUTES =================
         */



        /*
         ================= PATCH ROUTES =================
         */

        /*
         ================= DELETE ROUTES =================
         */
    }
}
