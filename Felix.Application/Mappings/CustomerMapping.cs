using AutoMapper;
using Felix.Application.Dtos.Customer;
using Felix.Application.UseCases.Customer.Commands.CreateCommand;
using Felix.Application.UseCases.Customer.Commands.UpdateCommand;
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

            CreateMap<Customer, CustomerByIdResponseDto>()
                                .ForMember(x => x.CustomerId, x => x.MapFrom(y => y.Id))
                                .ReverseMap();

            CreateMap<UseCases.Customer.Commands.CreateCommand.CreateCustomerCommand, Customer>();
            CreateMap<UseCases.Customer.Commands.UpdateCommand.UpdateCustomerCommand, Customer>();
        }
    }
}
