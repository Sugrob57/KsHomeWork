using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CoreTestApi.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ReaderRestApi.Models;
using Serilog;

namespace CoreTestApi.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// get all topics
        /// </summary>
        /// <returns>all Topics</returns>
        /// <response code="200">Returns all item</response>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            Log.Information("GET request for all users");
            List<User> _users = new List<User>();
            _users = UserSource.GetAll();
            //Log.Debug("returned {0} users", _users.Count);

            return _users;
        }

        /// <summary>
        /// get topic 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>return item from Topic</returns>
        /// <response code="200">Returns the selected item</response>
        /// <response code="404">Not found uservby id</response>
        [HttpGet("{userId}")]
        public ActionResult<string> Get(int userId)
        {
            Log.Information("GET request by id = {0}", userId);
            try
            {
                User _user = UserSource.GetUserById(userId);
                if (_user == null)
                {
                    Log.Information("User not found");
                    return NotFound();
                }
                else
                {
                    Log.Information("Found user: {0} {1} {2}", _user.UserId, _user.SecondName, _user.FirstName);
                    return Ok(_user);
                }
            }
            catch (Exception e)
            {
                Log.Error("GET byID request error: {0}", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

    }
}