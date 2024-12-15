using Application.AccountFeatures;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("user/{userId}/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountApplication _accountApplication;

        public AccountController(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute] Guid userId, [FromRoute] Guid id, [FromBody] decimal value)
        {
            var result = await _accountApplication.DepositAsync(value, userId, id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw([FromRoute] Guid userId, [FromRoute] Guid id, [FromBody] decimal value)
        {
            var result = await _accountApplication.WithdrawAsync(value, userId, id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("{id}/transfer")]
        public async Task<IActionResult> Transfer([FromRoute] Guid userId, [FromRoute] Guid id, [FromBody] TransferRequestDto requestDto)
        {
            var result = await _accountApplication.TransferAsync(requestDto.Value, userId, id, requestDto.DestinationAccountId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactions([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            var result = await _accountApplication.GetTransactionsAsync(userId, id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetBalance([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            var result = await _accountApplication.GetBalanceAsync(userId, id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
