using NVisionIT.AutomatedTellerMachine.Service.Models;

namespace NVisionIT.AutomatedTellerMachine.Service.Data
{
    public static class MockData
    {
        public static DbSet<UserModel> AllUsers()
        {
            return new DbSet<UserModel>() {
                new UserModel() { CardNumber = 12345, IsLoggedIn = false, NumberOfAttempt = 1, PinNumber = 1234, StatusOfCard = CardStatus.Active},
                new UserModel() { CardNumber = 67890, IsLoggedIn = false, NumberOfAttempt = 1, PinNumber = 1234, StatusOfCard = CardStatus.Lost },
                new UserModel() { CardNumber = 09876, IsLoggedIn = false, NumberOfAttempt = 1, PinNumber = 1234, StatusOfCard = CardStatus.Active },
                new UserModel() { CardNumber = 54321, IsLoggedIn = false, NumberOfAttempt = 1, PinNumber = 1234, StatusOfCard = CardStatus.Active}
            };

        }

        public static DbSet<AccountModel> GetAccounts()
        {
            return new DbSet<AccountModel>() {
                new AccountModel() { CardNumber = 12345, AccountNumber = 11111, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 12345, AccountNumber = 22222, AmountAvailable = 50000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Saving, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 23232, AccountNumber = 33333, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 76766, AccountNumber = 44444, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 87654, AccountNumber = 55555, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 90876, AccountNumber = 66666, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 09876, AccountNumber = 77777, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 09876, AccountNumber = 88888, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Saving, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 45673, AccountNumber = 99999, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
                new AccountModel() { CardNumber = 89097, AccountNumber = 23232, AmountAvailable = 10000, Transaction = TransactionType.Check, TypeOfAccount = AccountType.Current, Message = UserMessage.CheckAccounts },
            };

        }

        public static int[] AmountToDebitList = { 1000, 500, 200, 100, 50 };
    }
}
