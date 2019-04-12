using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class DonorListViewModel
    {
        public UserViewModel User { get; set; }

        public BloodViewModel Blood { get; set; }
    }
}