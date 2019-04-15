using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ReaderRestApi.Models;
using Serilog;

namespace CoreTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// get all users
        /// </summary>
        /// <returns>All users</returns>
        /// <response code="200">Returns all users</response>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            Log.Information("GET request for all users");
            List<User> _users = new List<User>();
            if (UserSource.GetAll(out _users))
            {
                Log.Debug("returned {0} users", _users.Count);
                return Ok(_users);
            }
            else
            {
                Log.Information("User not found");
                return NotFound();
            }

            
        }

        /// <summary>
        /// get user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>return user by id</returns>
        /// <response code="200">Returns the selected user</response>
        /// <response code="404">Not found user by id</response>
        [HttpGet("{userId}")]
        public ActionResult<string> Get(int userId)
        {
            Log.Information("GET request by id = {0}", userId);
            try
            {
                User _user = UserSource.GetUserById(userId);
                if (_user.UserId == 0)
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