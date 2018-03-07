using AutoMapper;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.TestPlan;

namespace STARC.Infra.CrossCutting.AutoMapper.Profiles
{
    public class TestPlanMappingProfile : Profile
    {
        public TestPlanMappingProfile()
        {
            // Insert
            CreateMap<TestPlan, TestPlanToInsertViewModel>();
            CreateMap<TestPlanToInsertViewModel, TestPlan>();

            // Update
            CreateMap<TestPlan, TestPlanToUpdateViewModel>();
            CreateMap<TestPlanToUpdateViewModel, TestPlan>();

            // Grid
            CreateMap<TestPlan, TestPlanToQueryViewModel>();
            CreateMap<TestPlanToQueryViewModel, TestPlan>();
        }
    }
}
