using BloodDonation.BloodDonationEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Models
{
    public class DonateViewModel
    {
        public List<Blood> Blood { get; set; } = new List<Blood>();

        [Required]
        public int BloodId { get; set; }

        public UserViewModel User { get; set; }
    }
}