using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessLayer.Models
{
    public class MyTask
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Please Enter a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter a description.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Please select a Category.")]
        public string CategoryId { get; set; } =  string.Empty;

        [ValidateNever]
        public string Category { get; set; } = null!;

        [Required(ErrorMessage = "Please select a Status.")]
        public string StatusId { get; set; } = string.Empty;
        [ValidateNever]
        public Status Status { get; set; } = null!;
        public bool Overdew => StatusId == "open" && DueDate < DateTime.Today;

        public bool IsActive {  get; set; }

    }
}
