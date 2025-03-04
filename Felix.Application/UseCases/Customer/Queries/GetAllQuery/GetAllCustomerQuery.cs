﻿using Felix.Application.Commons.Bases;
using Felix.Application.Dtos.Customer;
using MediatR;

namespace Felix.Application.UseCases.Customer.Queries.GetAllQuery
{
    public class GetAllCustomerQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<CustomerResponseDto>>>
    {
    }
}
