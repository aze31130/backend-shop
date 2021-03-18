using AutoMapper;
using backend_shop.DTO;
using backend_shop.Models;

namespace backend_shop.Utils
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}
