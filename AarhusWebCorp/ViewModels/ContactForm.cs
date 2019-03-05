using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebCorp.ViewModels
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name {get;set;}
        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email {get;set;}
        [Required(ErrorMessage = "Please enter a subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter a message")]
        public string Message { get; set; }

    }
}