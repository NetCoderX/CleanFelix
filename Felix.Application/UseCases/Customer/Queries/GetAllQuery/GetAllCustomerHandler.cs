using AutoMapper;
using Felix.Application.Commons.Bases;
using Felix.Application.Dtos.Customer;
using Felix.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Felix.Application.UseCases.Customer.Queries.GetAllQuery
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, 
                                         BaseResponse<IEnumerable<CustomerResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _orderingQuery = orderingQuery;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
         }

        public async Task<BaseResponse<IEnumerable<CustomerResponseDto>>> Handle
        (GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<CustomerResponseDto>>();
             
            try
            {
                var customers = _unitOfWork.Customer.GetAllQueryable();

                if(request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
                {
                    switch(request.NumFilter)
                    {
                        case 1:
                            customers = customers.Where(x => x.Name.Contains(request.TextFilter));
                            break;
                        case 2:
                            customers = customers.Where(x => x.LastName.Contains(request.TextFilter));
                            break;
                    }
                }

                if(request.StateFilter is not null)
                {
                    customers = customers.Where(x => x.State == request.StateFilter);
                }

                if(!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
                {
                    customers = customers.Where(x => x.AuditCreateDate > Convert.ToDateTime(request.StartDate)
                    && x.AuditCreateDate < Convert.ToDateTime(request.EndDate).AddDays(1));
                }

                request.Sort ??= "Id";

                var items = await _orderingQuery.Ordering(request, customers).ToListAsync(cancellationToken);

                response.IsSuccess = true;
                response.TotalRecords = await customers.CountAsync(cancellationToken);
                response.Data = _mapper.Map<IEnumerable<CustomerResponseDto>>(items);
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
