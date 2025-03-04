using AutoMapper;
using Felix.Application.Commons.Bases;
using Felix.Application.Interfaces.Services;
using MediatR;

namespace Felix.Application.UseCases.Customer.Commands.CreateCommand
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var customer = _mapper.Map< Felix.Domain.Entities.Customer >(request);
                await _unitOfWork.Customer.CreateAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Registro exitoso.";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }
    }
}
