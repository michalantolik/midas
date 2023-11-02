using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.DepositRequest
{
    public interface IDepositRequestCommand
    {
        IActionResult Execute(DepositRequestModel model);
    }
}
