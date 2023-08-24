using Microsoft.AspNetCore.Mvc;
using CrudWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace CrudWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailingListController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MailingListController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetMailingList(string lastName, string sortOrder = "asc")
        {
            var query = _context.MailingList.AsQueryable();

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(p => p.LastName == lastName);
            }

            if (sortOrder == "asc")
            {
                query = query.OrderBy(p => p.LastName);
            }
            else
            {
                query = query.OrderByDescending(p => p.LastName);
            }

            return Ok(query.ToList());
        }
    }
}
