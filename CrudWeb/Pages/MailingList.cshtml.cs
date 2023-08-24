using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudWeb.Pages
{
    public class MailingListModel : PageModel
    {
        private readonly MyDbContext _context;

        public List<Person> MailingList { get; set; } = new List<Person>();

        [BindProperty]

        public Person NewPerson { get; set; } = new Person();
        public bool ShowSuccessToast { get; private set; }

        public MailingListModel(MyDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            MailingList = _context.MailingList.ToList();
            HttpContext.Session.Remove("FormSubmitted");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                
                
                // Handle the invalid state here, e.g., return a different page or set an error message.
                return Page();
            }

            _context.MailingList.Add(NewPerson);
            _context.SaveChanges();

            // Save to session after successful form submission
            HttpContext.Session.SetString("FormSubmitted", "true");
            Response.Cookies.Append("FormSubmitted", "true");


            
            return RedirectToPage("success"); 
        }

        public IActionResult OnPostUpdate(int id, string firstName, string lastName, string email)
        {
            var personToUpdate = _context.MailingList.FirstOrDefault(p => p.ID == id);
            if (personToUpdate == null)
            {
                
                return Page();
            }

            personToUpdate.FirstName = firstName;
            personToUpdate.LastName = lastName;
            personToUpdate.Email = email;
            _context.SaveChanges();

            return RedirectToPage("Success");
        }


        public IActionResult OnPostDelete(int id)
        {
            var personToDelete = _context.MailingList.FirstOrDefault(p => p.ID == id);
            if (personToDelete != null)
            {
                _context.MailingList.Remove(personToDelete);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }




    }
}

