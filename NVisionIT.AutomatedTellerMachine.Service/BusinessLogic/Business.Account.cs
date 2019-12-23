using NVisionIT.AutomatedTellerMachine.Service.Data;
using NVisionIT.AutomatedTellerMachine.Service.DTO;
using NVisionIT.AutomatedTellerMachine.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NVisionIT.AutomatedTellerMachine.Service.BusinessLogic
{
    public partial class Business
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all the accounts associated with a card number (user)
        /// A user can have multiple accounts i.e a card number can have multiple accounts
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public List<AccountDto> GetAccounts(int cardNumber)
        {
            var dbContext = new DbContext();

            List<AccountDto> accountList = new List<AccountDto>();

            try
            {
                log.Info($"Getting accounts for card number: {cardNumber}");

                var accounts = dbContext.Accounts.Where(x => x.CardNumber == cardNumber);

                //Convert AccountModel to AccountDto
                foreach (var account in accounts)
                {
                    log.Info("Account: " + account.AccountNumber);

                    accountList.Add(account.MapProperties<AccountDto>());
                }

                return accountList;
            }
            catch (Exception ex)
            {
                //return an object with error message inside.
                //The consuming parties can read the message to know an error ocurred duing processing the request
                accountList.Add(new AccountDto { CardNumber = cardNumber, Message = UserMessage.AccountNotFound, ErrorMessage = ex.Message });
                log.Error($"Error retrieving Accounts for card number : {cardNumber}", ex);

                return accountList;
            }

        }

        /// <summary>
        /// Get a specific account byt the account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public AccountDto GetAccount(int accountNumber)
        {
            var dbContext = new DbContext();

            try
            {
                log.Info($"Getting accounts for account number: {accountNumber}");

                var account = dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

                log.Info("Retrieved account number: " + account.AccountNumber);

                return account.MapProperties<AccountDto>();
            }
            catch (Exception ex)
            {
                log.Error($"Error retrieving Account for account number : {accountNumber}", ex);
                return new AccountDto { AccountNumber = accountNumber, Message = UserMessage.AccountNotFound, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Allow withdrawal of cash by selecting from preselected amount
        /// </summary>
        /// <returns></returns>
        public AccountDto DebitAccountPreSelectAmount(int accountNumber, int amount)
        {
            var dbContext = new DbContext();

            try
            {
                log.Info($"Start debiting PRE-SELECT MODE account number: {accountNumber} by {amount} rupees");

                //Check if the amount requested is in the list else return amount not authorized
                var check = MockData.AmountToDebitList.Contains(amount);

                if (!check)
                {
                    log.Warn($"Debit amount requested ({amount}) not in list: {MockData.AmountToDebitList.ToString()}");
                    return new AccountDto { AccountNumber = accountNumber, IsTransactionSuccessful = false, Message = UserMessage.AmountRequestedNotAuthorized };
                }

                //Get the account info
                var account = dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

                log.Info($"Fetching account: {account.AccountNumber}");

                //Check if requested amount does not exceed the amount asked for
                if (amount > account.AmountAvailable)
                {
                    log.Warn($"Debit amount requested ({amount}) is more than amount available");
                    return new AccountDto { AccountNumber = accountNumber, IsTransactionSuccessful = false, Message = UserMessage.InsufficientFunds };
                }

                account.Transaction = TransactionType.Debit;
                account.AmountAvailable = account.AmountAvailable - amount;
                account.TransactionDate = DateTime.Now;
                account.Message = UserMessage.RequestedAmountDebited;
                account.IsTransactionSuccessful = true;

                log.Info($"Amount: {amount} has been debited successfully");

                dbContext.SaveChanges();

                return account.MapProperties<AccountDto>();

            }
            catch (Exception ex)
            {
                log.Error($"Error debitting Account number : {accountNumber}", ex);
                return new AccountDto { AccountNumber = accountNumber, Message = UserMessage.AmountRequestedNotAuthorized, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Debit the chosen about by user
        /// Check if amount is divisible by 10
        /// If amount exceeds amount available in account send error message
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public AccountDto DebitAccountUserSelectAmount(int accountNumber, int amount)
        {
            var dbContext = new DbContext();

            try
            {
                log.Info($"Start debiting USER-SELECT MODE account number: {accountNumber} by {amount} rupees");

                //Check if amount is divisible by 10
                var notesAllowed = MockData.AmountToDebitList;
                int[] noteCounter = new int[notesAllowed.Length];

                //Check if the amount requested is divisible by 10 or the amount is divisible by the smallest note amount found
                if (amount % 10 != 0 || amount % notesAllowed.Last() != 0)
                {
                    log.Warn($"Debit amount requested ({amount}) not divisible by 10 or not authorized");
                    return new AccountDto { AccountNumber = accountNumber, IsTransactionSuccessful = false, Message = UserMessage.AmountRequestedNotAuthorized };
                }

                //Get the account info
                var account = dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

                log.Info($"Fetching accound: {account.AccountNumber}");

                //Check if requested amount does not exceed the amount asked for
                if (amount > account.AmountAvailable)
                {
                    log.Warn($"Debit amount requested ({amount}) is more than amount available");
                    return new AccountDto { AccountNumber = accountNumber, IsTransactionSuccessful = false, Message = UserMessage.InsufficientFunds };
                }

                //Debit the account
                account.AmountAvailable = account.AmountAvailable - amount;
                account.Transaction = TransactionType.Debit;

                log.Info($"Note counter algorithm");

                // count notes
                for (int i = 0; i < notesAllowed.Length; i++)
                {
                    if (amount >= notesAllowed[i])
                    {
                        noteCounter[i] = amount / notesAllowed[i];
                        amount = amount - noteCounter[i] * notesAllowed[i];
                    }
                }

                var noteString = string.Empty;

                //Build the note counter string
                for (int i = 0; i < noteCounter.Length; i++)
                {
                    if(noteCounter[i] != 0)
                    {
                        noteString = noteString + ", " + noteCounter[i] + " X " + notesAllowed[i];
                    }
                   
                }

                account.NoteCounter = noteString.TrimStart(',').TrimStart();
                account.AmountAvailable = account.AmountAvailable - amount;
                account.TransactionDate = DateTime.Now;
                account.Message = UserMessage.RequestedAmountDebited;
                account.IsTransactionSuccessful = true;

                log.Info($"Amount: {amount} has been debited successfully");

                dbContext.SaveChanges();

                return account.MapProperties<AccountDto>();
            }
            catch (Exception ex)
            {
                log.Error($"Error debitting Account number : {accountNumber}", ex);
                return new AccountDto { AccountNumber = accountNumber, Message = UserMessage.AmountRequestedNotAuthorized, ErrorMessage = ex.Message };
            }
        }
    }
}
