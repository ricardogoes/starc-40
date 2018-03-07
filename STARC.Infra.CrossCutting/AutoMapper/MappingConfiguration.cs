using AutoMapper;
using STARC.Infra.CrossCutting.AutoMapper.Profiles;

namespace STARC.Infra.CrossCutting.AutoMapper
{
    public class MappingConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<CustomerMappingProfile>();
                x.AddProfile<ProjectMappingProfile>();
                x.AddProfile<UserMappingProfile>();
                x.AddProfile<UsersInProjectsMappingProfile>();
                x.AddProfile<TestPlanMappingProfile>();
                x.AddProfile<TestCaseMappingProfile>();
                x.AddProfile<TestSuiteMappingProfile>();
            });
        }
    }
}
