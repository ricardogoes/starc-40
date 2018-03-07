using AutoMapper;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.TestSuite;

namespace STARC.Infra.CrossCutting.AutoMapper.Profiles
{
    public class TestSuiteMappingProfile : Profile
    {
        public TestSuiteMappingProfile()
        {
            // Insert
            CreateMap<TestSuite, TestSuiteToInsertViewModel>();
            CreateMap<TestSuiteToInsertViewModel, TestSuite>();

            // Update
            CreateMap<TestSuite, TestSuiteToUpdateViewModel>();
            CreateMap<TestSuiteToUpdateViewModel, TestSuite>();

            // Grid
            CreateMap<TestSuite, TestSuiteToQueryViewModel>();
            CreateMap<TestSuiteToQueryViewModel, TestSuite>();
        }
    }
}
