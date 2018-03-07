using AutoMapper;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.UsersInProjects;

namespace STARC.Infra.CrossCutting.AutoMapper.Profiles
{
    public class UsersInProjectsMappingProfile : Profile
    {
        public UsersInProjectsMappingProfile()
        {
            // Insert
            CreateMap<UsersInProjects, UsersInProjectsToInsertViewModel>();
            CreateMap<UsersInProjectsToInsertViewModel, UsersInProjects>();

            // Grid
            CreateMap<UsersInProjects, UsersInProjectsToQueryViewModel>();
            CreateMap<UsersInProjectsToQueryViewModel, UsersInProjects>();
        }
    }
}
