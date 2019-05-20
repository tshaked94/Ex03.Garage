using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public string RequestLicenseNumber()
        {
            string userLicenseNumber;
            Console.WriteLine("Please enter license number:");
            userLicenseNumber = Console.ReadLine();

            while (!isNumeric(userLicenseNumber))
            {
                Console.WriteLine("Invalid license number, please enter a license number including only numbers:");
                userLicenseNumber = Console.ReadLine();
            }

            return userLicenseNumber;
        }

        private bool isNumeric(string i_StringToCheck)
        {
            bool res = true;

            foreach (char character in i_StringToCheck)
            {
                if (character < '0' || character > '9')
                {
                    res = false;
                    break;
                }
            }

            return res;
        }
    }
}
