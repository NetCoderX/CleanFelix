using AutoMapper;
using Felix.Application.Commons.Bases;
using Felix.Application.Dtos.Customer;
using Felix.Application.Interfaces.Services;
using MediatR;

namespace Felix.Application.UseCases.Customer.Queries.GetAllQuery
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, 
                                         BaseResponse<IEnumerable<CustomerResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
         }

        public async Task<BaseResponse<IEnumerable<CustomerResponseDto>>> Handle
        (GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<CustomerResponseDto>>();
             
            try
            {
                var customers = await _unitOfWork.Customer.GetAllAsync();

                if(customers is null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontraron registros.";
                    return response;
                }
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
                response.Message = "Consulta exitosa.";

            }
            catch(Exception  ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
