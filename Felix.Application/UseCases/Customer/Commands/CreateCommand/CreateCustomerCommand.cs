using Felix.Application.Commons.Bases;
using MediatR;

namespace Felix.Application.UseCases.Customer.Commands.CreateCommand
{
    public class CreateCustomerCommand : IRequest<BaseResponse<bool>>
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
