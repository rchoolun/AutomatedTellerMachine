using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVisionIT.AutomatedTellerMachine.Service.Models
{
    public class UserModel
    {
        public int CardNumber { get; set; }

        public int PinNumber { get; set; }

        public int NumberOfAttempt { get; set; }

        public bool IsLoggedIn { get; set; }

        public UserMessage Message { get; set; }

        public CardStatus StatusOfCard { get; set; }

        public string ErrorMessage { get; set; }
    }
}
