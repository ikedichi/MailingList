using System;
using System.ComponentModel.DataAnnotations;

namespace CrudWeb.Models
{
	public class Person
    {
	public int ID { get; set; }

      


        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

    }
}

