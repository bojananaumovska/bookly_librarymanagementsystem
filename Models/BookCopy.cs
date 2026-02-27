using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Managment_Project.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        [Required]

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public string InventoryNumber { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
        public BookCopy()
        {
            Loans = new HashSet<Loan>();
        }
    }
}