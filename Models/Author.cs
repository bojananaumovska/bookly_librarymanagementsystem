using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Managment_Project.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name can contain only letters and spaces.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name can contain only letters and spaces.")]
        public string LastName { get; set; }
        [StringLength(1000, ErrorMessage = "Bio cannot exceed 1000 characters.")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "HTML tags are not allowed.")]
        public string Bio { get; set; }
        public ICollection<Book> Books { get; set; }
        [StringLength(255)]
        public string ImagePath { get; set; }
    }
}