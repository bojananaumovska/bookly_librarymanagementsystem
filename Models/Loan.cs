using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Managment_Project.Models
{
    public class Loan
    {
        public enum LoanStatus
        {
            Active = 1,
            Returned = 2,
            Overdue = 3
        }

        public Loan()
        {
            LoanDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(14); 
            Status = LoanStatus.Active;
        }

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int BookCopyId { get; set; }

        [ForeignKey("BookCopyId")]
        public virtual BookCopy BookCopy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Loan Date")]
        public DateTime LoanDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        public LoanStatus Status { get; set; }
    }
}