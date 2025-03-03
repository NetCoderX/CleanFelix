using Felix.Application.UseCases.Customer.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Felix.Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CustomerList([FromQuery] GetAllCustomerQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
