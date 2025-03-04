using Felix.Application.UseCases.Customer.Commands.CreateCommand;
using Felix.Application.UseCases.Customer.Commands.UpdateCommand;
using Felix.Application.UseCases.Customer.Queries.GetAllQuery;
using Felix.Application.UseCases.Customer.Queries.GetByIdQuery;
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

        [HttpGet("List")]
        public async Task<IActionResult> CustomerList([FromQuery] GetAllCustomerQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> CustomerById(int customerId)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery(){CustomerId = customerId });
            return Ok(response);
        }

        [HttpPost("resgistro")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
