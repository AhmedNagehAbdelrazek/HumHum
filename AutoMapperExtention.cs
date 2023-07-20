using AutoMapper;
using HumHum.Areas.Identity.Data;
using HumHum.Models;
using HumHum.ViewModels;

namespace HumHum
{
    public static class AutoMapperExtention
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<UserTable, ApplicationUser>();
                CreateMap<ApplicationUser, UserTable>();

                CreateMap<FoodItem,EditFoodItem>();
                CreateMap<EditFoodItem,FoodItem>();

            }
        }
    }
}
