using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class DonorViewModel
    {
        public int DonorId { get; set; }

        public UserViewModel User { get; set; }

        public BloodViewModel Blood { get; set; }
    }
}