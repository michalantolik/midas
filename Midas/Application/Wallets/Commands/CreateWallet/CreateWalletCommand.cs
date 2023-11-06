using Application.Interfaces;
using Domain.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.DepositRequest
{
    public class CreateWalletCommand : ControllerBase, ICreateWalletCommand
    {
        private readonly IDatabaseService _database;

        public CreateWalletCommand(IDatabaseService database)
        {
            _database = database;
        }

        public IActionResult Execute(CreateWalletModel model)
        {
            // Null model
            if (model == null)
            {
                return BadRequest($"Request data is null --> You must provide \"{nameof(CreateWalletModel)} {nameof(model)}\"");
            }

            // Incorrent name
            if (String.IsNullOrEmpty(model.WalletName) || !StartsWithLetter(model.WalletName) || !IsAlphanumeric(model.WalletName))
            {
                return BadRequest($"Request data is null --> \"{nameof(model.WalletName)}: {model.WalletName}\" --> Wallet name must be alphanumeric and must start from letter.");
            }

            // Forbidden wallet name
            if (model.WalletName.ToLower() == "string")
            {
                return BadRequest($"A wallet cannot be called \"{model.WalletName}\" --> This name is forbidden. Please provide other name.");
            }

            // Wallet already exists
            var walletAlreadyExists = _database.Wallets.SingleOrDefault(w => w.Name.ToLower() == model.WalletName.ToLower()) != null;
            if (walletAlreadyExists)
            {
                return Conflict($"Cannot create wallet with name \"{model.WalletName.ToLower()}\". Wallet with such name already exists!");
            }

            try
            {
                // Creating new wallet
                var newWallet = new Wallet { Name = model.WalletName };
                _database.Wallets.Add(newWallet);
                _database.Save();

                return CreatedAtRoute("", new { id = newWallet.Id }, newWallet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error occurred while processing the request (creating new wallet) --> Message: \"{ex.Message}\"");
            }
        }

        bool IsAlphanumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        bool StartsWithLetter(string input)
        {
            if(String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return char.IsLetter(input[0]);
        }
    }
}
