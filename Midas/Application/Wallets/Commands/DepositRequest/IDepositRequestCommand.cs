namespace Application.Wallets.Commands.DepositRequest
{
    public interface IDepositRequestCommand
    {
        bool Execute(DepositRequestModel model);
    }
}
