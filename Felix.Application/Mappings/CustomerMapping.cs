using AutoMapper;
using Felix.Application.Dtos.Customer;
using Felix.Domain.Entities;


namespace Felix.Application.Mappings
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, CustomerResponseDto>()
                .ForMember(x => x.CustomerId, x => x.MapFrom(y => y.Id))
                .ForMember(X => X.StateCustomer, x => x.MapFrom(y => y.State == 1 ? "ACTIVO" : "INACTIVO"))
                .ReverseMap();
            
        }
    }
}
