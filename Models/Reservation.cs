using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Managment_Project.Models
{
    public enum ReservationStatus
    {
        Active = 1,
        Cancelled = 2,
        Completed = 3,
        Expired = 4
    }
    public class Reservation
    {
        public int Id { get; set; }
        [Required]

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [Required]

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReservationDate { get; set; }
        [Required]
        public ReservationStatus Status { get; set; }
    }
}