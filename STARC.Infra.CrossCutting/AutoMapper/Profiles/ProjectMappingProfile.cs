using AutoMapper;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.Projects;

namespace STARC.Infra.CrossCutting.AutoMapper.Profiles
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            // Insert
            CreateMap<Project, ProjectToInsertViewModel>();
            CreateMap<ProjectToInsertViewModel, Project>();

            // Update
            CreateMap<Project, ProjectToUpdateViewModel>();
            CreateMap<ProjectToUpdateViewModel, Project>();

            // Grid
            CreateMap<Project, ProjectToQueryViewModel>();
            CreateMap<ProjectToQueryViewModel, Project>();
        }
    }
}
