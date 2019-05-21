using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private Garage m_Garage;

        private void addNewCustomer()
        {
            bool isVehicleFound;
            string licenseNumber, ownerName, ownerPhoneNumber;
            VehicleAllocator.eVehicleTypes vehicleType;
            Vehicle vehicleCreated;

            licenseNumber = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumber);
            if (isVehicleFound)
            {
                vehicleStatusHasBeenChangedToRepairing();
                m_Garage.ChangeVehicleStatus(licenseNumber, Customer.eVehicleStatus.Repairing);
            }
            else
            {
                getOwnerDetails(out ownerName, out ownerPhoneNumber);
                vehicleType = getVehicleTypeFromUser();
                vehicleCreated = VehicleAllocator.MakeNewVehicle(vehicleType);
                m_Garage.AddNewCustomer(ownerName, ownerPhoneNumber, vehicleCreated);
            }
        }

        private void vehicleStatusHasBeenChangedToRepairing()
        {
            Console.WriteLine("Vehicle status has been changed to repairing.");
        }        

        private void getOwnerDetails(out string io_OwnerName, out string io_OwnerPhoneNumber)
        {
            bool isNameValid, isPhoneNumberValid;

            Console.WriteLine("Please enter your name:");
            io_OwnerName = Console.ReadLine();
            isNameValid = IsAllLetters(io_OwnerName);
            while (!isNameValid)
            {
                Console.WriteLine("Name cannot include numbers, Please enter valid name:");
                io_OwnerName = Console.ReadLine();
                isNameValid = IsAllLetters(io_OwnerName);
            }
            Console.WriteLine("Please enter your phone number:");
            io_OwnerPhoneNumber = Console.ReadLine();
            isPhoneNumberValid = IsAllDigits(io_OwnerPhoneNumber);
            while (!isPhoneNumberValid)
            {
                Console.WriteLine("Phone number cannot include letters, Please enter valid phone number:");
                io_OwnerName = Console.ReadLine();
                isPhoneNumberValid = IsAllDigits(io_OwnerPhoneNumber);
            }
        }

        private void showLicenseNumbers()
        {
            bool isFilterRequired;
            Customer.eVehicleStatus vehicleStatusFilter;
            LinkedList<string> licenseNumbersToShow;

            isFilterRequired = askUserIfHeWantToFilterLicenseNumberByStatus();
            if (isFilterRequired)
            {
                vehicleStatusFilter = requestStatusFilter();
                licenseNumbersToShow = m_Garage.ShowLicenseNumbersByFilter(vehicleStatusFilter);
            }
            else
            {
                licenseNumbersToShow = m_Garage.ShowAllLicenseNumbers();
            }
            printLicenseNumbers(licenseNumbersToShow);
        }

        private void changeCustomerVehicleStatus()
        {
            Customer.eVehicleStatus newVehicleStatus;
            string licenseNumberOfVehicle;
            bool isVehicleFound;

            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                newVehicleStatus = requestNewVehicleStatus();
                m_Garage.ChangeCustomerVehicleStatus(licenseNumberOfVehicle, newVehicleStatus);
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private void inflateTiresToMaximum()
        {
            string licenseNumberOfVehicle;
            bool isVehicleFound;

            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                m_Garage.InflateVehicleTiresToMaximum(licenseNumberOfVehicle);
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private void fuelRegularEnginedVehicle()
        {
            string licenseNumberOfVehicle;
            bool isVehicleFound;
            EnginedVehicle.eFuelType fuelType;
            float amountOfFuelToAdd;

            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if(isVehicleFound)
            {
                fuelType = requestFuelType();
                amountOfFuelToAdd = requestAmountOfFuelToAdd();
                m_Garage.FuelEnginedVehicle(licenseNumberOfVehicle, fuelType, amountOfFuelToAdd);
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private float requestAmountOfFuelToAdd()
        {
            string fuelAmountToAddString;
            float fuelAmountToAdd;
            bool isParsingSuccessed;

            // TODO: add Formatting exeption
            Console.WriteLine("Please enter fuel amount you would like to add:");
            fuelAmountToAddString = Console.ReadLine();
            isParsingSuccessed = float.TryParse(fuelAmountToAddString, out fuelAmountToAdd);

            return fuelAmountToAdd;
        }

        private EnginedVehicle.eFuelType requestFuelType()
        {
            EnginedVehicle.eFuelType fuelType;
            string userChoiceString;
            int userChoiceInt;
            bool isUserChoiceValid;

            Console.WriteLine("Please enter fuel type{0}1.Soler{0}2.Octan95{0}3.Octan96{0}4.Octan98", Environment.NewLine);
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = userChoiceString.Length == 1 && isFuelType(userChoiceString);
            while (!isUserChoiceValid)
            {
                Console.WriteLine("Invalid Input, please choose a number from the list below.{0}Please enter fuel type{0}1.Soler{0}2.Octan95{0}3.Octan96{0}4.Octan98", Environment.NewLine);
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = userChoiceString.Length == 1 && isFuelType(userChoiceString);
            }

            userChoiceInt = userChoiceString[0] - '0';
            fuelType = (EnginedVehicle.eFuelType)userChoiceInt;

            return fuelType;
        }

        private bool isFuelType(string inputToCheck)
        {
            bool isInputAFuelType;

            if(inputToCheck[0] >= '1' && inputToCheck[0] <= '4')
            {
                isInputAFuelType = true;
            }
            else
            {
                isInputAFuelType = false;
            }

            return isInputAFuelType;
        }

        private void chargeElectricVehicle()
        {
            string licenseNumberOfVehicle;
            bool isVehicleFound;
            float amountOfMinutesToCharge;

            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                amountOfMinutesToCharge = requestAmountOfMinutesToCharge();
                m_Garage.ChargeElectricVehicle(licenseNumberOfVehicle, amountOfMinutesToCharge);
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private float requestAmountOfMinutesToCharge()
        {
            string batteryAmountToAddString;
            float batteryAmountToAdd;
            bool isParsingSuccessed;


            // TODO: add Formatting exeption
            Console.WriteLine("Please enter amount of minutes you would like to charge:");
            batteryAmountToAddString = Console.ReadLine();
            isParsingSuccessed = float.TryParse(batteryAmountToAddString, out batteryAmountToAdd);

            return batteryAmountToAdd;
        }

        private void showAllDetailsOfCustomer()
        {
            string licenseNumberOfVehicle;
            bool isVehicleFound;
            Customer customerToShowDetails;
            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                customerToShowDetails = m_Garage.CustomerDetails(licenseNumberOfVehicle);
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private Customer.eVehicleStatus requestNewVehicleStatus()
        {
            Customer.eVehicleStatus newVehicleStatus;
            string newVehicleStatusString;
            bool isUserChoiceValid;
            int newVehicleStatusInt;

            Console.WriteLine("Please enter the new vehicle status{0}1.Paid{0}2.Repaired{0}3.Repairing", Environment.NewLine);
            newVehicleStatusString = Console.ReadLine();
            isUserChoiceValid = (newVehicleStatusString.Length) == 1 && (isVehicleStatus(newVehicleStatusString));
            while(!isUserChoiceValid)
            {
                Console.WriteLine("Invalid Input, please choose a number from the list below.{0}Please enter the new vehicle status{0}1.Paid{0}2.Repaired{0}3.Repairing", Environment.NewLine);
                newVehicleStatusString = Console.ReadLine();
                isUserChoiceValid = (newVehicleStatusString.Length) == 1 && (isVehicleStatus(newVehicleStatusString));
            }

            newVehicleStatusInt = newVehicleStatusString[0] - '0';
            newVehicleStatus = (Customer.eVehicleStatus)newVehicleStatusInt;

            return newVehicleStatus;
        }

        private void printLicenseNumbers(LinkedList<string> licenseNumbersToShow)
        {
            string message;
            int numOfLicense = 1;
            
            foreach(string licenseNumber in licenseNumbersToShow)
            {
                message = string.Format("{0}. {1}", NumOfLicense, licenseNumber);
                Console.WriteLine(message);
                numOfLicense++;                
            }
        }

        private Customer.eVehicleStatus requestStatusFilter()
        {
            // TODO !!


            // Customer.eVehicleStatus userStatusFilter;
            // string userStatusFilterString;
                        
            // Console.WriteLine("Please enter status filter from the list below:");
            // userStatusFilterString = Console.ReadLine();

        }

        private bool askUserIfHeWantToFilterLicenseNumberByStatus()
        {
            string userChoiceString;
            bool isUserChoiceValid, didUserChoseToFilter;
            int userChoiceInt;

            Console.WriteLine("Do you want to filter vehicles by their status?{0}1.Yes{0}2.No", Environment.NewLine);
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = (userChoiceString.Length) == 1 && (isYesOrNo(userChoiceString));
            while(!isUserChoiceValid)
            {
                Console.WriteLine("Invalid Input, Please enter a number from the list below.{0}Do you want to filter vehicles by their status?{0}1.Yes{0}2.No", Environment.NewLine);
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = (userChoiceString.Length) == 1 && (isYesOrNo(userChoiceString));
            }

            userChoiceInt = userChoiceString[0] - '0';
            if(userChoiceInt == 1)
            {
                didUserChoseToFilter = true;
            }
            else
            {
                didUserChoseToFilter = false;
            }

            return didUserChoseToFilter;
        }

        private bool isYesOrNo(string userChoiceString)
        {
            bool isUserInputYesOrNo;

            if(userChoiceString[0] == '1' || userChoiceString[0] == '2')
            {
                isUserInputYesOrNo = true;
            }
            else
            {
                isUserInputYesOrNo = false;
            }

            return isUserInputYesOrNo;
        }

        private void vehicleDoesNotExist()
        {
            throw new NotImplementedException();
        }

        private VehicleAllocator.eVehicleTypes getVehicleTypeFromUser()
        {
            // TODO: think about way to disply all supported vehcile types 
            string vehicleType;

            Console.WriteLine("Please choose vehicle type from the list below:");
            vehicleType = Console.ReadLine();

            return (VehicleAllocator.eVehicleTypes)vehicleType; // TODO
        }

        private string requestLicenseNumber()
        {
            string userLicenseNumber;
            Console.WriteLine("Please enter license number:");
            userLicenseNumber = Console.ReadLine();

            while (!IsAllDigits(userLicenseNumber))
            {
                Console.WriteLine("Invalid license number, please enter a license number including only numbers:");
                userLicenseNumber = Console.ReadLine();
            }

            return userLicenseNumber;
        }

        private bool IsAllDigits(string i_StringToCheck)
        {
            bool res = true;

            foreach (char character in i_StringToCheck)
            {
                if (!Char.IsDigit(character))
                {
                    res = false;
                    break;
                }
            }

            return res;
        }

        public static bool IsAllLetters(string i_StringToCheck)
        {
            bool res = true;

            foreach (char character in i_StringToCheck)
            {
                if (!Char.IsLetter(character))
                {
                    res = false;
                    break;
                }
            }

            return res;
        }
    }
}
