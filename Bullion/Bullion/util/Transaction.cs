using BullionLibrary;

namespace Bullion.util
{
    public class Transaction
    {
        // Account Savings value is increased based on given savings.
        public double DepositSavings(Account account, double savings)
        {
            account.Savings += savings;
            return account.Savings;
        }

        // Account Cash value is increased based on given cash.
        public double DepositCash(Account account, double cash)
        {
            account.Cash += cash;
            return account.Cash;
        }

        // Account Crypto value is increased based on given crypto.
        public double DepositCrypto(Account account, double crypto)
        {
            account.Crypto += crypto;
            return account.Crypto;
        }

        // Account savings value is decreased based on given savings.
        public double WithdrawSavings(Account account, double savings)
        {
            account.Savings -= savings;
            return account.Savings;
        }

        // Account Cash value is decreased based on given cash.
        public double WithdrawCash(Account account, double cash)
        {
            account.Cash -= cash;
            return account.Cash;
        }

        // Account Crypto value is decreased based on given crypto.
        public double WithdrawCrypto(Account account, double crypto)
        {
            account.Crypto -= crypto;
            return account.Crypto;
        }
    }
}
