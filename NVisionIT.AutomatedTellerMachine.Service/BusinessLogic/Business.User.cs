using NVisionIT.AutomatedTellerMachine.Service.Data;
using NVisionIT.AutomatedTellerMachine.Service.DTO;
using NVisionIT.AutomatedTellerMachine.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVisionIT.AutomatedTellerMachine.Service.BusinessLogic
{
    public partial class Business
    {
        private static Business instance = null;

        /// <summary>
        /// Singleton implementation
        /// </summary>
        public static Business Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Business();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets user and card info
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public UserDto GetUser(UserDto userDto)
        {
            var dbContext = new DbContext();

            try
            {
                //Get the user with the card number provided
                var user = dbContext.Users.FirstOrDefault(x => x.CardNumber == userDto.CardNumber);

                //Check if object is null then return empty object
                if (user == null)
                {
                    return new UserDto() { Message = UserMessage.UserNotFound, StatusOfCard = CardStatus.Inactive };
                }

                //Check the status of the card provided
                if (user.StatusOfCard == CardStatus.Lost)
                {
                    return new UserDto() { CardNumber = user.CardNumber, Message = UserMessage.LostStolenRetainCard, StatusOfCard = CardStatus.Lost };
                }

                //Check if correct pin supplied. If wrong pin on third attempt hold the card
                if (user.PinNumber != userDto.PinNumber)
                {
                    switch (userDto.NumberOfAttempt)
                    {
                        case 0:
                            user.Message = UserMessage.WrongPasswordFirstAttempt;
                            break;
                        case 1:
                            user.Message = UserMessage.WrongPasswordSecondAttempt;
                            break;
                        default:
                            user.Message = UserMessage.WrongPasswordRetainCard;
                            break;
                    }

                    userDto.NumberOfAttempt = userDto.NumberOfAttempt + 1;
                    user.NumberOfAttempt = userDto.NumberOfAttempt;
                    user.PinNumber = 0;
                    user.IsLoggedIn = false;
                }
                else
                {
                    user.Message = UserMessage.LoginSuccessful;
                    user.IsLoggedIn = true;
                }

                return user.MapProperties<UserDto>();
            }
            catch(Exception ex)
            {
                return new UserDto() { Message = UserMessage.ErrorOccured, StatusOfCard = CardStatus.Inactive, ErrorMessage = $"Error Message: {ex.Message}" };
            }
        }
    }
}
