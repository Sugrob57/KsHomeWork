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
        public ActionResult<IEnumerable<Topic>> Get()
        {
            return TopicSource.All;
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
            try
            {
                Topic selectedTopic = TopicSource.All.Where(x => x.Id == userId).FirstOrDefault();
                if (selectedTopic == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(selectedTopic);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

    }
}