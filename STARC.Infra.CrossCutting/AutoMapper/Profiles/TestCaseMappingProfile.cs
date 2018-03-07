using AutoMapper;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.TestCase;

namespace STARC.Infra.CrossCutting.AutoMapper.Profiles
{
    public class TestCaseMappingProfile : Profile
    {
        public TestCaseMappingProfile()
        {
            // Insert
            CreateMap<TestCase, TestCaseToInsertViewModel>();
            CreateMap<TestCaseToInsertViewModel, TestCase>();

            // Update
            CreateMap<TestCase, TestCaseToUpdateViewModel>();
            CreateMap<TestCaseToUpdateViewModel, TestCase>();

            // Grid
            CreateMap<TestCase, TestCaseToQueryViewModel>();
            CreateMap<TestCaseToQueryViewModel, TestCase>();
        }
    }
}
