namespace Application.Wallets.Commands.WithdrawRequest
{
    public interface IWithdrawRequestCommand
    {
        bool Execute(WithdrawRequestModel model);
    }
}
