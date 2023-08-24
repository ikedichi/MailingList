using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class SuccessModel : PageModel
{
    private readonly MyDbContext _context;

    public List<Person> MailingList { get; set; } = new List<Person>();

    public SuccessModel(MyDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        MailingList = _context.MailingList.ToList();
    }
}



