using AutoMapper;
using BloodDonation.BloodDonationEntities;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserViewModel, User>();

            Mapper.CreateMap<Blood, BloodViewModel>();
            Mapper.CreateMap<BloodViewModel, Blood>();
        }
    }
}