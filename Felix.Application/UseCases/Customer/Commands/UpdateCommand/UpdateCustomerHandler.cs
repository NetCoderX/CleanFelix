using AutoMapper;
using Felix.Application.Commons.Bases;
using Felix.Application.Interfaces.Services;
using MediatR;

namespace Felix.Application.UseCases.Customer.Commands.UpdateCommand
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var customer = _mapper.Map<Felix.Domain.Entities.Customer>(request);
                customer.Id = request.CustomerId;
                _unitOfWork.Customer.UpdateAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Registro actualizado.";
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
