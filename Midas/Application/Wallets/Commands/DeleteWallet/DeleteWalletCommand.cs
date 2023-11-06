using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.DeleteWallet
{
    public class DeleteWalletCommand : ControllerBase, IDeleteWalletCommand
    {
        private readonly IDatabaseService _database;

        public DeleteWalletCommand(IDatabaseService database)
        {
            _database = database;
        }

        public IActionResult Execute(DeleteWalletModel model)
        {
            // Null model
            if (model == null)
            {
                return BadRequest($"Request data is null --> You must provide \"{nameof(DeleteWalletModel)} {nameof(model)}\"");
            }

            // Wallet not found
            var walletNotFound = _database.Wallets.SingleOrDefault(w => w.Id == model.Id) == null;
            if (walletNotFound)
            {
                return NotFound($"Cannot delete wallet with ID \"{model.Id}\". Wallet with such ID does not exist.");
            }

            try
            {
                // Deleting wallet
                var walletToRemove = _database.Wallets.Single(w => w.Id == model.Id);
                _database.Wallets.Remove(walletToRemove);
                _database.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error occurred while processing the request (deleting new wallet) --> Message: \"{ex.Message}\"");
            }
        }
    }
}
