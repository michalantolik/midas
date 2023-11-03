using Microsoft.AspNetCore.Mvc;

namespace Application.Wallets.Commands.ConvertRequest
{
    public interface IConvertRequestCommand
    {
        IActionResult Execute(ConvertRequestModel model);
    }
}
