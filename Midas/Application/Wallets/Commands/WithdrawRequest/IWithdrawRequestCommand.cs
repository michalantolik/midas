using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.WithdrawRequest
{
    public interface IWithdrawRequestCommand
    {
        IActionResult Execute(WithdrawRequestModel model);
    }
}
