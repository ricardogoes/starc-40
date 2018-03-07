using AutoMapper;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.Users;

namespace STARC.Infra.CrossCutting.AutoMapper.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Insert
            CreateMap<User, UserToInsertViewModel>();
            CreateMap<UserToInsertViewModel, User>();

            // Update
            CreateMap<User, UserToUpdateViewModel>();
            CreateMap<UserToUpdateViewModel, User>();

            // Grid
            CreateMap<User, UserToQueryViewModel>();
            CreateMap<UserToQueryViewModel, User>();            
        }
    }
}
