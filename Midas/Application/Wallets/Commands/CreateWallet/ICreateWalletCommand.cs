using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.DepositRequest
{
    public interface ICreateWalletCommand
    {
        IActionResult Execute(CreateWalletModel model);
    }
}
