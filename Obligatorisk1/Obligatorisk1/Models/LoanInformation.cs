using System;
using System.ComponentModel.DataAnnotations;

namespace Obligatorisk1.Models
{
    public class LoanInformation
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LoanDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }
        public string IsEmailSend { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReservationDate { get; set; }
        public User User { get; set; }
        public string ReservationId { get; set; }
    }
}