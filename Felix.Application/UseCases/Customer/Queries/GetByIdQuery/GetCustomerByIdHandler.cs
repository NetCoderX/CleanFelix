using AutoMapper;
using Felix.Application.Commons.Bases;
using Felix.Application.Dtos.Customer;
using Felix.Application.Interfaces.Services;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Felix.Application.UseCases.Customer.Queries.GetByIdQuery
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<CustomerByIdResponseDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CustomerByIdResponseDto>();

            try
            {
                var customer = await _unitOfWork.Customer.GetByIdAsync(request.CustomerId);

                if(customer is null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontraron registros.";
                    return response;
                }

                response.IsSuccess = true;
                response.Data = _mapper.Map<CustomerByIdResponseDto>(customer);
                response.Message = "Consulta exitosa";
                return response;

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
