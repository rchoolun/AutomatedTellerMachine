using NVisionIT.AutomatedTellerMachine.Service.Models;
using System;
using System.Runtime.Serialization;

namespace NVisionIT.AutomatedTellerMachine.Service.DTO
{
    [DataContract]
    [Serializable]
    public class AccountDto
    {
        [DataMember]
        public int CardNumber { get; set; }

        [DataMember]
        public int AccountNumber { get; set; }

        [DataMember]
        public int AmountAvailable { get; set; }

        [DataMember]
        public TransactionType Transaction { get; set; }

        [DataMember]
        public AccountType TypeOfAccount { get; set; }

        [DataMember]
        public UserMessage Message { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public bool IsTransactionSuccessful { get; set; }

        [DataMember]
        public DateTime TransactionDate { get; set; }

        [DataMember]
        public string NoteCounter { get; set; }

        public AccountDto()
        {
            Transaction = TransactionType.Default;
            TypeOfAccount = AccountType.Default;
            Message = UserMessage.Default;
            TransactionDate = DateTime.Now;
        }
    }
}
