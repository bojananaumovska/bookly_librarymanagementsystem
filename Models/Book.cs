using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Managment_Project.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 150 characters.")]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "HTML tags are not allowed.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN must be exactly 13 digits.")]
        [RegularExpression(@"^97[89]\d{10}$", ErrorMessage = "Invalid ISBN-13 format.")]
        public string ISBN { get; set; }
        [Required]
        [Range(1500, 2100, ErrorMessage = "Published year must be between 1500 and 2100.")]
        public int PublishedYear { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "HTML tags are not allowed.")]
        public string Publisher { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "HTML tags are not allowed.")]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        public ICollection<BookCopy> Copies { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        [StringLength(255)]
        public string ImagePath { get; set; }
    }
}