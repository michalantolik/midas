using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.DeleteWallet
{
    public interface IDeleteWalletCommand
    {
        IActionResult Execute(DeleteWalletModel model);
    }
}
