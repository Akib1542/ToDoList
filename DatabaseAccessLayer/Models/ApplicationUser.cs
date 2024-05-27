using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessLayer.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public int Name { get; set; }
        public string PostalCode { get; set; }

    }
}
