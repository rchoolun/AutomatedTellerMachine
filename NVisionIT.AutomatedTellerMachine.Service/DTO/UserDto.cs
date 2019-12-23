using NVisionIT.AutomatedTellerMachine.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NVisionIT.AutomatedTellerMachine.Service.DTO
{
    [DataContract]
    [Serializable]
    public class UserDto
    {
        [DataMember]
        public int CardNumber { get; set; }

        [DataMember]
        public int PinNumber { get; set; }

        [DataMember]
        public int NumberOfAttempt { get; set; }

        [DataMember]
        public bool IsLoggedIn { get; set; }

        [DataMember]
        public UserMessage Message { get; set; }

        [DataMember]
        public CardStatus StatusOfCard { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
