using CargoPay.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeController : Controller
    {
        private readonly IFeeService _feeService;

        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCurrentFee()
        {
            var fee = _feeService.GetCurrentFee();
            return Ok(new { Fee = fee });
        }
    }
}
