using AutoMapper;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.Customers;

namespace STARC.Infra.CrossCutting.AutoMapper.Profiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // Insert
            CreateMap<Customer, CustomerToInsertViewModel>();
            CreateMap<CustomerToInsertViewModel, Customer>();

            // Update
            CreateMap<Customer, CustomerToUpdateViewModel>();
            CreateMap<CustomerToUpdateViewModel, Customer>();

            // Grid
            CreateMap<Customer, CustomerToQueryViewModel>();
            CreateMap<CustomerToQueryViewModel, Customer>();
        }
    }
}
