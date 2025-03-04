using Felix.Application.Commons.Bases;
using Felix.Application.Dtos.Customer;
using MediatR;

namespace Felix.Application.UseCases.Customer.Queries.GetByIdQuery
{
    public class GetCustomerByIdQuery : IRequest<BaseResponse<CustomerByIdResponseDto>>
    {
        public int CustomerId { get; set; }
    }
}
