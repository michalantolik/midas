namespace Data.Wallets
{
    public class WalletWithBalancesDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BalanceDto> Balances { get; set; }
    }
}
