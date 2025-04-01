using CargoPay.Application.DTOS;
using CargoPay.Application.Exceptions;
using CargoPay.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoPay.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService service)
        {
            _cardService = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create() => Ok(await _cardService.CreateCardAsync());

        [HttpGet("balance/{cardNumber}")]
        public async Task<IActionResult> Balance(string cardNumber) =>
            Ok(await _cardService.GetBalanceAsync(cardNumber));
        
        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PayRequest request)
        {
            var payment = await _cardService.PayAsync(request.CardNumber, request.Amount);
            return Ok(payment);
        }
    }
}
