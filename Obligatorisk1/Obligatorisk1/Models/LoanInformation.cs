﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obligatorisk1.Models;

namespace LabModel.Models
{
    public class LoanInformation
    {
        public int Id { get; set; }
        public Component Component { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string IsEmailSend { get; set; }
        public DateTime ReservationDate { get; set; }
        public User User { get; set; }
        public string ReservationId { get; set; }
    }
}