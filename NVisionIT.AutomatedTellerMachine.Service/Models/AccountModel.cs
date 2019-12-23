using System;

namespace NVisionIT.AutomatedTellerMachine.Service.Models
{
    public class AccountModel
    {
        public int CardNumber { get; set; }

        public int AccountNumber { get; set; }

        public int AmountAvailable { get; set; }

        public bool IsTransactionSuccessful { get; set; }

        public TransactionType Transaction { get; set; }

        public AccountType TypeOfAccount { get; set; }

        public UserMessage Message { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime TransactionDate { get; set; }

        public string NoteCounter { get; set; }
    }
}
