using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public enum eUserOptions
        {
            AddNewCustomer = 1,
            ShowLicenseNumbers,
            ChangeVehicleStatus,
            InflateVehicleTires,
            FuelEnginedVehicle,
            ChargeElectricVehicle,
            ShowVehicleDetails,
            Exit
        }

        private Garage m_Garage = new Garage();

        public void Run()
        {
            eUserOptions userChoice;
            do
            {
                showMenu();
                userChoice = getUserChoice();
                switch (userChoice)
                {
                    case eUserOptions.AddNewCustomer:
                        addNewCustomer();
                        break;
                    case eUserOptions.ShowLicenseNumbers:
                        showLicenseNumbers();
                        break;
                    case eUserOptions.ChangeVehicleStatus:
                        changeCustomerVehicleStatus();
                        break;
                    case eUserOptions.InflateVehicleTires:
                        inflateTiresToMaximum();
                        break;
                    case eUserOptions.FuelEnginedVehicle:
                        fuelEnginedVehicle();
                        break;
                    case eUserOptions.ChargeElectricVehicle:
                        chargeVehicle();
                        break;
                    case eUserOptions.ShowVehicleDetails:
                        showAllDetailsOfCustomer();
                        break;
                }
            }
            while (userChoice != eUserOptions.Exit);
        }

        private eUserOptions getUserChoice()
        {
            eUserOptions userChoice;
            string userChoiceString;
            int userChoiceInt;
            bool isChoiceValid;

            userChoiceString = Console.ReadLine();
            isChoiceValid = isValidChoice(userChoiceString, 8);
            while (!isChoiceValid)
            {
                Console.Write("Invalid input ");
                showMenu();
                userChoiceString = Console.ReadLine();
                isChoiceValid = isValidChoice(userChoiceString, 8);
            }

            // the parsing below will always a success, its validation occurs in the while loop
            userChoiceInt = int.Parse(userChoiceString);
            userChoice = (eUserOptions)userChoiceInt;

            return userChoice;
        }

        private void showMenu()
        {
            string[] menuOptions =
            {
                "1. Add a new vehicle to garage",
                "2. Display license numbers of the vehicles in the garage",
                "3. Change vehicle status",
                "4. Inflate car's tires to maximum",
                "5. Fuel engined vehicle",
                "6. Charge electric vehicle",
                "7. Display vehicle's data by license number",
                "8. Exit"
            };

            clear();
            Console.WriteLine("Please enter a number from the list below");
            foreach (string option in menuOptions)
            {
                Console.WriteLine(option);
            }
        }

        private void addNewCustomer()
        {
            // this method get customer and vehicle details from user and send details to logic.
            bool isVehicleFound;
            string licenseNumber, ownerName, ownerPhoneNumber;
            VehicleAllocator.eVehicleTypes vehicleType;
            VehicleAllocator vehicleAllocator;
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
                vehicleAllocator = new VehicleAllocator();
                requestOwnerDetails(out ownerName, out ownerPhoneNumber);
                vehicleType = requestVehicleTypeFromUser();
                vehicleCreated = vehicleAllocator.MakeNewVehicle(vehicleType);
                m_Garage.SetLicenseNumber(licenseNumber, vehicleCreated);
                requestVehicleDetails(vehicleCreated);
                m_Garage.AddNewCustomer(ownerName, ownerPhoneNumber, vehicleCreated);
                clear();
                Console.WriteLine("Customer added succesfully");
            }

            holdScreen();
        }

        private void requestVehicleDetails(Vehicle i_VehicleToSetDetails)
        {
            // this method gets the vehicle details from the user.
            string modelName, tiresManufacaturerName;
            float energyPercentageLeft, cargoVolume;
            Utilities.eCarColor carColor;
            Utilities.eMotorbikeLicenseType motorbikeLicenseType;
            int numberOfDoors, engineVolume;
            bool isContainingDangerousCargo;

            modelName = requestModelName();
            energyPercentageLeft = requestEnergyPercentageLeft();
            tiresManufacaturerName = requestTireManufacaturerName();
            m_Garage.SetVehicleDetails(i_VehicleToSetDetails, modelName, tiresManufacaturerName, energyPercentageLeft);
            assignValidInputToCurrentTirePressure(i_VehicleToSetDetails);
            if (i_VehicleToSetDetails is EnginedCar || i_VehicleToSetDetails is ElectricCar)
            {
                requestCarDetails(out carColor, out numberOfDoors);
                m_Garage.SetCarDetails(i_VehicleToSetDetails, carColor, numberOfDoors);
            }
            else if (i_VehicleToSetDetails is EnginedMotorbike || i_VehicleToSetDetails is ElectricMotorbike)
            {
                requestMotorbikeDetails(out engineVolume, out motorbikeLicenseType);
                m_Garage.SetMotorbikeDetails(i_VehicleToSetDetails, engineVolume, motorbikeLicenseType);
            }
            else if (i_VehicleToSetDetails is Truck)
            {
                requestTruckDetails(out cargoVolume, out isContainingDangerousCargo);
                m_Garage.SetTruckDetails(i_VehicleToSetDetails as Truck, cargoVolume, isContainingDangerousCargo);
            }
        }

        private void assignValidInputToCurrentTirePressure(Vehicle i_VehicleToSet)
        {
            float currentTirePressure;
            bool isValidInput;
            do
            {
                try
                {
                    currentTirePressure = requestCurrentTiresPressure();
                    m_Garage.SetTireCurrentPressure(i_VehicleToSet, currentTirePressure);
                    isValidInput = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a number between {0} to {1} represents current tire pressure", ex.MinValue, ex.MaxValue);
                    Console.WriteLine(exceptionMessage);
                    isValidInput = false;
                }
            }
            while (!isValidInput);
        }

        private void requestTruckDetails(out float o_CargoVolume, out bool o_IsContainingDangerousCargo)
        {
            // this method gets the truck details from the user.
            o_IsContainingDangerousCargo = requestDangerousCargoDetail();
            o_CargoVolume = requestCargoVolumeDetail();
        }

        private float requestCargoVolumeDetail()
        {
            // this method asks the user the cargo volume of the truck and return it as a float.
            string userChoiceString;
            bool isUserChoiceValid;
            float cargoVolumeFloat = 0f;

            clear();
            Console.WriteLine("Please enter the truck's cargo volume");
            do
            {
                try
                {
                    userChoiceString = Console.ReadLine();
                    clear();
                    cargoVolumeFloat = float.Parse(userChoiceString);
                    if (cargoVolumeFloat < 0)
                    {
                        throw new ValueOutOfRangeException(0);
                    }

                    isUserChoiceValid = true;
                }
                catch (ValueOutOfRangeException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a positive number represents cargo volume");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a float number represents cargo volume");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. the number you entered is not in range of the variable");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);

            return cargoVolumeFloat;
        }

        private bool requestDangerousCargoDetail()
        {
            // this method asking the user if the truck contains dangerous cargo and return it as bool.
            string userChoiceString;
            int userChoiceInt;
            bool isUserChoiceValid;
            bool isContainingDangerousCargo;

            clear();
            Console.WriteLine("Does the track contains dangerous cargo?{0}1.Yes{0}2.No", Environment.NewLine);
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(userChoiceString, 2);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input, please choose a number from the list below.{0}Does the track contains dangerous cargo?{0}1.Yes{0}2.No", Environment.NewLine);
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(userChoiceString, 2);
            }

            // the parsing below will always success. Its validation occurs in the while loop
            userChoiceInt = int.Parse(userChoiceString);
            if (userChoiceInt == 1)
            {
                isContainingDangerousCargo = true;
            }
            else
            {
                isContainingDangerousCargo = false;
            }

            return isContainingDangerousCargo;
        }

        private void requestMotorbikeDetails(out int o_EngineVolume, out Utilities.eMotorbikeLicenseType o_MotorbikeLicenseType)
        {
            // this method get motorbike details from the user
            o_EngineVolume = requestEngineVolume();
            o_MotorbikeLicenseType = requestMotorbikeLicenseType();
        }

        private Utilities.eMotorbikeLicenseType requestMotorbikeLicenseType()
        {
            // this method get motorbike license type from user.
            Utilities.eMotorbikeLicenseType userMotorbikeLicenseType;
            string userMotorbikeLicenseTypeString, MotorbikeLicenseTypes, message1, message2;
            int userMotorbikeLicenseTypeInt;
            bool isInputValid;

            MotorbikeLicenseTypes = string.Format("1. A{0}2. A1{0}3. A2{0}4. B", Environment.NewLine);
            message1 = string.Format("Please enter number of license type from the list below: {0}{1}", Environment.NewLine, MotorbikeLicenseTypes);
            message2 = string.Format("Invalid input, please enter valid number of license type from the list below: {0}{1}", Environment.NewLine, MotorbikeLicenseTypes);
            clear();
            Console.WriteLine(message1);
            userMotorbikeLicenseTypeString = Console.ReadLine();
            isInputValid = isValidChoice(userMotorbikeLicenseTypeString, 4);
            while (!isInputValid)
            {
                clear();
                Console.WriteLine(message2);
                userMotorbikeLicenseTypeString = Console.ReadLine();
                isInputValid = isValidChoice(userMotorbikeLicenseTypeString, 4);
            }

            // the parsing below will always a success, its validation occurs in the while loop
            userMotorbikeLicenseTypeInt = int.Parse(userMotorbikeLicenseTypeString);
            userMotorbikeLicenseType = (Utilities.eMotorbikeLicenseType)userMotorbikeLicenseTypeInt;
            return userMotorbikeLicenseType;
        }

        private int requestEngineVolume()
        {
            string engineVolumeString;
            int engineVolumeInt = 0;
            bool isUserChoiceValid;

            Console.WriteLine("Please enter the engine volume");
            do
            {
                try
                {
                    engineVolumeString = Console.ReadLine();
                    clear();
                    engineVolumeInt = int.Parse(engineVolumeString);
                    if (engineVolumeInt < 0)
                    {
                        throw new ValueOutOfRangeException(0);
                    }

                    isUserChoiceValid = true;
                }
                catch (ValueOutOfRangeException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a positive number represents the engine volume");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is an integer number only represents the engine volume");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. the number you entered is not in range of the variable");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);

            return engineVolumeInt;
        }

        private void requestCarDetails(out Utilities.eCarColor o_CarColor, out int o_NumOfDoors)
        {
            // this method get car details from the user
            o_CarColor = requestCarColor();
            o_NumOfDoors = requestNumOfDoors();
        }

        private int requestNumOfDoors()
        {
            // this method get num of doors in car from the user.
            string numOfDoorsString;
            int numOfDoorsInt = 0;
            bool isUserChoiceValid;

            clear();
            Console.WriteLine("Please enter number of doors in the car");
            do
            {
                try
                {
                    numOfDoorsString = Console.ReadLine();
                    clear();
                    numOfDoorsInt = int.Parse(numOfDoorsString);
                    if (numOfDoorsInt < 2 || numOfDoorsInt > 5)
                    {
                        throw new ValueOutOfRangeException(5, 2);
                    }

                    isUserChoiceValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format(@"Invalid input, please enter a number between {0} to {1}
                    represents the number of doors", ex.MinValue, ex.MaxValue);
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format(@"Invalid input, please enter a number
represents the number of doors");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format(@"Invalid input, the number you entered is not in range of the variable.
Please enter a number represents the number of doors");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);

            return numOfDoorsInt;
        }

        private Utilities.eCarColor requestCarColor()
        {
            // this method get car color from the user.
            // TODO: list of strings of car colors and print it instead of 1.red....
            string carColorString;
            bool isUserChoiceValid;
            Utilities.eCarColor carColor;
            int carColorInt;

            clear();
            Console.WriteLine("Please choose a number from the list below represents the car color:{0}1. Red{0}2. Blue{0}3. Black{0}4. Grey", Environment.NewLine);
            carColorString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(carColorString, 4);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input. Please choose a number from the list below represents the car color:{0}1. Red{0}2. Blue{0}3. Black{0}4. Grey", Environment.NewLine);
                carColorString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(carColorString, 4);
            }

            // the parsing below will always success. Its validation occurs in the while loop
            carColorInt = int.Parse(carColorString);
            carColor = (Utilities.eCarColor)carColorInt;

            return carColor;
        }

        private float requestCurrentTiresPressure()
        {
            // this method get vehicle's current tires pressure.
            string tirePressuerString;
            float tirePressureFloat = 0f;
            bool isUserChoiceValid;

            Console.WriteLine("Please enter the current tires pressure");
            do
            {
                try
                {
                    tirePressuerString = Console.ReadLine();
                    clear();
                    tirePressureFloat = float.Parse(tirePressuerString);
                    isUserChoiceValid = true;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. Please enter a float number represents current tire pressure");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format(@"Invalid input. the number you entered is not in range of the variable.
Please enter a float number represents the current tires pressure");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);

            return tirePressureFloat;
        }

        private string requestTireManufacaturerName()
        {
            // this method get the tire manufacaturer name.
            string manufacaturerName;
            bool isValidInput;

            clear();
            Console.WriteLine("Please enter the tire's manufacaturer name");
            manufacaturerName = Console.ReadLine();

            // this boolean expresion validate that the user input isn't empty
            isValidInput = !string.IsNullOrEmpty(manufacaturerName) && (isContainLetter(manufacaturerName) || isContainDigit(manufacaturerName));
            while (!isValidInput)
            {
                clear();
                Console.WriteLine(@"Invalid tire's manufacaturer name, empty input are not allowed.
Please enter valid tire's manufacaturer name:");
                manufacaturerName = Console.ReadLine();
                isValidInput = !string.IsNullOrEmpty(manufacaturerName) && (isContainLetter(manufacaturerName) || isContainDigit(manufacaturerName));
            }

            clear();

            return manufacaturerName;
        }

        private string requestModelName()
        {
            // this method gets the model name of the vehicle from the user.
            string modelName;
            bool isValidInput;

            clear();
            Console.WriteLine("Please enter the vehicle's model name");
            modelName = Console.ReadLine();

            // this boolean expresion validate that the user input isn't empty
            isValidInput = !string.IsNullOrEmpty(modelName) && (isContainLetter(modelName) || isContainDigit(modelName));
            while (!isValidInput)
            {
                clear();
                Console.WriteLine("Invalid model name, empty input are not allowed. Please enter valid model name:");
                modelName = Console.ReadLine();
                isValidInput = !string.IsNullOrEmpty(modelName) && (isContainLetter(modelName) || isContainDigit(modelName));
            }

            return modelName;
        }

        private float requestEnergyPercentageLeft()
        {
            // this method gets the percentage of energy left from the user and return it as float.
            string energyLeftString;
            float energyLeftFloat = 0f;
            bool isUserChoiceValid;

            clear();
            Console.WriteLine("Please enter the energy perecntage left in vehicle");
            do
            {
                try
                {
                    energyLeftString = Console.ReadLine();
                    clear();
                    energyLeftFloat = float.Parse(energyLeftString);
                    if (energyLeftFloat < 0 || energyLeftFloat > 100)
                    {
                        throw new ValueOutOfRangeException(100, 0);
                    }

                    isUserChoiceValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format(@"Invalid input, please enter a float number between {0} to {1}
                    represents the energy percentage left", ex.MinValue, ex.MaxValue);
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format(@"Invalid input, please enter a float number
                    represents the percentage of energy left");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format(@"Invalid input. the number you entered is not in range of the variable.
Please enter a float number represents the percentage of energy left");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);

            return energyLeftFloat;
        }

        private void vehicleStatusHasBeenChangedToRepairing()
        {
            clear();
            Console.WriteLine("Vehicle status has been changed to repairing.");
        }

        private void requestOwnerDetails(out string io_OwnerName, out string io_OwnerPhoneNumber)
        {
            // this method request user to enter his name and his phone number, it return this data by out parameters
            io_OwnerName = requestOwnerName();
            io_OwnerPhoneNumber = requestOwnerPhoneNumber();
        }

        private string requestOwnerPhoneNumber()
        {
            bool isPhoneNumberValid;
            string ownerPhoneNumber = null;
            int phoneNumberInt;

            clear();
            Console.WriteLine("Please enter your phone number:");
            do
            {
                try
                {
                    ownerPhoneNumber = Console.ReadLine();
                    clear();
                    phoneNumberInt = int.Parse(ownerPhoneNumber);
                    isPhoneNumberValid = true;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a phone number includes numbers only");
                    Console.WriteLine(exceptionMessage);
                    isPhoneNumberValid = false;
                }
                catch (OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. the number you entered is not in range of the variable");
                    Console.WriteLine(exceptionMessage);
                    isPhoneNumberValid = false;
                }
            }
            while (!isPhoneNumberValid);

            return ownerPhoneNumber;
        }

        private string requestOwnerName()
        {
            string ownerName = null;
            bool isInputValid;

            clear();
            Console.WriteLine("Please enter your name:");
            ownerName = Console.ReadLine();

            // validate that the user input isn't empty and include letters only
            isInputValid = isOwnerNameValid(ownerName) && !string.IsNullOrEmpty(ownerName);
            while (!isInputValid)
            {
                clear();
                Console.WriteLine(@"Invalid owner name, empty or non letters only input not allowed.
Please enter valid name:");
                ownerName = Console.ReadLine();
                isInputValid = isOwnerNameValid(ownerName) && !string.IsNullOrEmpty(ownerName);
            }

            return ownerName;
        }

        private void showLicenseNumbers()
        {
            // this method print out to console license numbers by user choice, filtered or not. 
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
            // this method change vehicle status and inform the user.
            Customer.eVehicleStatus newVehicleStatus;
            string licenseNumberOfVehicle, message;
            bool isVehicleFound;

            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                newVehicleStatus = requestNewVehicleStatus();
                m_Garage.ChangeCustomerVehicleStatus(licenseNumberOfVehicle, newVehicleStatus);
                clear();
                message = string.Format("Vehicle's no.{0} status has been changed succesfully to {1}", licenseNumberOfVehicle, newVehicleStatus.ToString());
                Console.WriteLine(message);
            }
            else
            {
                vehicleDoesNotExist();
            }

            holdScreen();
        }

        private void inflateTiresToMaximum()
        {
            string licenseNumberOfVehicle, message;
            bool isVehicleFound;

            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                m_Garage.InflateVehicleTiresToMaximum(licenseNumberOfVehicle);
                clear();
                message = string.Format("Vehicle's no. {0} tires has been sucsessfully inflated to maximum", licenseNumberOfVehicle);
                Console.WriteLine(message);
            }
            else
            {
                vehicleDoesNotExist();
            }

            holdScreen();
        }

        private void fuelEnginedVehicle()
        {
            try
            {
               fuelRegularEnginedVehicle();
            }
            catch (ArgumentException)
            {
                string exceptionMessage;

                clear();
                exceptionMessage = string.Format("Invalid input, you just choosed an electric vehicle to refuel gas");
                Console.WriteLine(exceptionMessage);
            }

            holdScreen();
        }

        private void fuelRegularEnginedVehicle()
        {
            string licenseNumberOfVehicle, message;
            bool isVehicleFound, isEngined;

            licenseNumberOfVehicle = requestLicenseNumber();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                isEngined = m_Garage.IsEnginedVehicle(licenseNumberOfVehicle);
                if (!isEngined)
                {
                    throw new ArgumentException();
                }
                else
                {
                    clear();
                    assignValidInputToCurrentFuelAmount(licenseNumberOfVehicle);
                    message = string.Format("Vehicle no. {0} has been fueled succesfully", licenseNumberOfVehicle);
                    Console.WriteLine(message);
                }
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private void assignValidInputToCurrentFuelAmount(string i_LicenseNumberOfVehicle)
        {
            bool isUserChoiceValid, isFuelTypeValid;
            EnginedVehicle.eFuelType fuelType;
            float amountOfFuelToAdd;

            fuelType = requestFuelType();
            isFuelTypeValid = m_Garage.areFuelTypesEquals(i_LicenseNumberOfVehicle, fuelType);
            do
            {
                try
                {
                    if (!isFuelTypeValid)
                    {
                        throw new ArgumentException();
                    }
                    else
                    {
                        amountOfFuelToAdd = requestAmountOfFuelToAdd();
                        m_Garage.FuelEnginedVehicle(i_LicenseNumberOfVehicle, fuelType, amountOfFuelToAdd);
                        isUserChoiceValid = true;
                    }
                }
                catch (ArgumentException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid fuel type, ");
                    Console.Write(exceptionMessage);
                    fuelType = requestFuelType();
                    isFuelTypeValid = m_Garage.areFuelTypesEquals(i_LicenseNumberOfVehicle, fuelType);

                    isUserChoiceValid = false;
                }
                catch (ValueOutOfRangeException ex)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a number between {0} to {1} represents the amout of fuel to add", ex.MinValue, ex.MaxValue);
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                 catch(OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. the number you entered is not in range of the variable");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);
            clear();
        }

        private float requestAmountOfFuelToAdd()
        {
            string fuelAmountToAddString;
            float fuelAmountToAdd = 0f;
            bool isUserChoiceValid;

            Console.WriteLine("Please enter fuel amount you would like to add");
            do
            {
                try
                {
                    fuelAmountToAddString = Console.ReadLine();
                    clear();
                    fuelAmountToAdd = float.Parse(fuelAmountToAddString);
                    isUserChoiceValid = true;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a float number represents the amount of fuel to add");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch(OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. the number you entered is not in range of the variable");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);

            return fuelAmountToAdd;
        }

        private EnginedVehicle.eFuelType requestFuelType()
        {
            EnginedVehicle.eFuelType fuelType;
            string userChoiceString;
            int userChoiceInt;
            bool isUserChoiceValid;

            Console.WriteLine("Please choose fuel type from the list below{0}1. Soler{0}2. Octan95{0}3. Octan96{0}4. Octan98", Environment.NewLine);
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(userChoiceString, 4);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid Input, please choose a number from the list below.{0}Please enter fuel type{0}1. Soler{0}2. Octan95{0}3. Octan96{0}4. Octan98", Environment.NewLine);
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(userChoiceString, 4);
            }

            // the parsing below will always success. Its validation occurs in the while loop
            userChoiceInt = int.Parse(userChoiceString);
            fuelType = (EnginedVehicle.eFuelType)userChoiceInt;
            clear();

            return fuelType;
        }

        private void chargeElectricVehicle()
        {
            string licenseNumberOfVehicle, message;
            bool isVehicleFound, isElectric, isUserChoiceValid;
            float amountOfMinutesToCharge;

            licenseNumberOfVehicle = requestLicenseNumber();
            clear();
            isVehicleFound = m_Garage.isVehicleInGarage(licenseNumberOfVehicle);
            if (isVehicleFound)
            {
                isElectric = m_Garage.IsElectricVehicle(licenseNumberOfVehicle);
                if (!isElectric)
                {
                    throw new ArgumentException();
                }
                else
                {
                    do
                    {
                        try
                        {
                            amountOfMinutesToCharge = requestAmountOfMinutesToCharge();
                            m_Garage.ChargeElectricVehicle(licenseNumberOfVehicle, amountOfMinutesToCharge);
                            isUserChoiceValid = true;
                            clear();
                            message = string.Format("Vehicle no. {0} has been charged succesfully", licenseNumberOfVehicle);
                            Console.WriteLine(message);
                        }
                        catch(ValueOutOfRangeException ex)
                        {
                            clear();
                            message = string.Format("Invalid input. A valid input is a float number between {0} to {1} represents the amount of minutes to charge", ex.MinValue, ex.MaxValue * 60);
                            isUserChoiceValid = false;
                            Console.WriteLine(message);
                        }
                    }
                    while (!isUserChoiceValid);
                }
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private void chargeVehicle()
        {
            try
            {
                chargeElectricVehicle();
            }
            catch (ArgumentException)
            {
                string exceptionMessage;

                clear();
                exceptionMessage = string.Format("Invalid input, you just choosed an engined vehicle to charge electric");
                Console.WriteLine(exceptionMessage);
            }

            holdScreen();
        }

        private float requestAmountOfMinutesToCharge()
        {
            string batteryAmountToAddString;
            float batteryAmountToAdd = 0f;
            bool isUserChoiceValid;

            Console.WriteLine("Please enter amount of minutes you would like to charge:");
            do
            {
                try
                {
                    batteryAmountToAddString = Console.ReadLine();
                    clear();
                    batteryAmountToAdd = float.Parse(batteryAmountToAddString);
                    isUserChoiceValid = true;
                }
                catch (FormatException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. A valid input is a float number represents the amount of charge minutes to add");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
                catch (OverflowException)
                {
                    string exceptionMessage;

                    clear();
                    exceptionMessage = string.Format("Invalid input. the number you entered is not in range of the variable");
                    Console.WriteLine(exceptionMessage);
                    isUserChoiceValid = false;
                }
            }
            while (!isUserChoiceValid);

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
                printAllCustomerDetails(customerToShowDetails);
            }
            else
            {
                vehicleDoesNotExist();
            }
        }
        
        private void printAllCustomerDetails(Customer i_CustomerToShowDetails)
        {
            List<KeyValuePair<string, string>> specificVehicleDetails = i_CustomerToShowDetails.Vehicle.VehicleInformationByType();

            printOwnerDetails(i_CustomerToShowDetails);
            printGeneralVehicleDetails(i_CustomerToShowDetails);
            foreach (KeyValuePair<string, string> attribute in specificVehicleDetails)
            {
                Console.WriteLine("      {0} : {1}", attribute.Key, attribute.Value);
            }

            holdScreen();
        }

        private void printOwnerDetails(Customer i_CustomerToShowDetails)
        {
            string ownerName, ownerPhoneNumber, vehicleStatus, ownerDetails;

            ownerName = i_CustomerToShowDetails.Name;
            ownerPhoneNumber = i_CustomerToShowDetails.PhoneNumber;
            vehicleStatus = i_CustomerToShowDetails.VehicleStatus.ToString();
            ownerDetails = string.Format(@"         Owner Details{0}
            Owner name: {1}{0}      Owner phone number: {2}{0}      Owner's vehicle status: {3}{0}", Environment.NewLine, ownerName, ownerPhoneNumber, vehicleStatus);
            Console.WriteLine(ownerDetails);
        }

        private void printGeneralVehicleDetails(Customer i_CustomerToShowDetails)
        {
            string modelName, licenseNumber, tireManufacturerName, generalVehicleDetails;
            float energyPercentageLeft, tireCurrentPressure, tireMaxPressure;

            modelName = i_CustomerToShowDetails.Vehicle.ModelName;
            licenseNumber = i_CustomerToShowDetails.Vehicle.LicenseNumber;
            energyPercentageLeft = i_CustomerToShowDetails.Vehicle.EnergyPercentage;
            tireManufacturerName = i_CustomerToShowDetails.Vehicle.Tires[0].ManufacaturerName;
            tireCurrentPressure = i_CustomerToShowDetails.Vehicle.Tires[0].CurrentPressure;
            tireMaxPressure = i_CustomerToShowDetails.Vehicle.Tires[0].MaximumPressure;
            generalVehicleDetails = string.Format(@"         Vehicle Details{0}{0}      License number: {1}{0}      Model name: {2}{0}      Energy percentage left: {3}%
            Manufacturer name: {4}{0}      Current tire pressure: {5}{0}      Maximum tire pressure: {6}", Environment.NewLine, licenseNumber, modelName, energyPercentageLeft, tireManufacturerName, tireCurrentPressure, tireMaxPressure);
            Console.WriteLine(generalVehicleDetails);
        }

        private void holdScreen()
        {
            Console.WriteLine("Press any key to go back to main menu...");
            Console.ReadKey();
            clear();
        }

        private Customer.eVehicleStatus requestNewVehicleStatus()
        {
            Customer.eVehicleStatus newVehicleStatus;
            string newVehicleStatusString;
            bool isUserChoiceValid;
            int newVehicleStatusInt;

            clear();
            Console.WriteLine("Please choose a number of the new vehicle status from the list below{0}1. Paid{0}2. Repaired{0}3. Repairing", Environment.NewLine);
            newVehicleStatusString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(newVehicleStatusString, 3);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid Input, please choose a number from the list below.{0}Please enter the new vehicle status{0}1. Paid{0}2. Repaired{0}3. Repairing", Environment.NewLine);
                newVehicleStatusString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(newVehicleStatusString, 3);
            }

            // the parsing below will always success. Its validation occurs in the while loop
            newVehicleStatusInt = int.Parse(newVehicleStatusString);
            newVehicleStatus = (Customer.eVehicleStatus)newVehicleStatusInt;

            return newVehicleStatus;
        }

        private void printLicenseNumbers(LinkedList<string> licenseNumbersToShow)
        {
            string message;
            int numOfLicense = 1;

            clear();
            if (licenseNumbersToShow.Count == 0)
            {
                Console.WriteLine("There are no vehicles with this status");
            }
            else
            {
                foreach (string licenseNumber in licenseNumbersToShow)
                {
                    message = string.Format("{0}. {1}", numOfLicense, licenseNumber);
                    Console.WriteLine(message);
                    numOfLicense++;
                }
            }

            holdScreen();
        }

        private Customer.eVehicleStatus requestStatusFilter()
        {
            Customer.eVehicleStatus userStatusFilter;
            string userStatusFilterString, message1, message2, statusFilters;
            bool isValidInput;
            int numberOfStatus, userStatusInt;

            numberOfStatus = Enum.GetValues(typeof(Customer.eVehicleStatus)).Length;
            statusFilters = string.Format("1. Paid{0}2. Repaired{0}3. Reapairing", Environment.NewLine);
            message1 = string.Format("Please enter status filter from the list below:{0}{1}", Environment.NewLine, statusFilters);
            message2 = string.Format("Invalid input, Please enter a correct number of status filter from the list below:{0}{1}", Environment.NewLine, statusFilters);
            clear();
            Console.WriteLine(message1);
            userStatusFilterString = Console.ReadLine();
            isValidInput = isValidChoice(userStatusFilterString, numberOfStatus);

            while (!isValidInput)
            {
                clear();
                Console.WriteLine(message2);
                userStatusFilterString = Console.ReadLine();
                isValidInput = isValidChoice(userStatusFilterString, numberOfStatus);
            }

            // the parsing below will always success. Its validation occurs in the while loop
            userStatusInt = int.Parse(userStatusFilterString);
            userStatusFilter = (Customer.eVehicleStatus)userStatusInt;

            return userStatusFilter;
        }

        private bool isValidChoice(string i_UserChoice, int i_MaxOption)
        {
            // this method return true if i_UserChoice is numeric and it between the range of user options
            // 1 <= i_UserChoice <= i_MaxOption
            bool isParsingSuccessed, res;
            int userChoiceInt;

            isParsingSuccessed = int.TryParse(i_UserChoice, out userChoiceInt);
            res = isParsingSuccessed && userChoiceInt >= 1 && userChoiceInt <= i_MaxOption;

            return res;
        }

        private bool askUserIfHeWantToFilterLicenseNumberByStatus()
        {
            // this method asks the user if he want to filter license numbers by their status and return the answer as bool.
            string userChoiceString;
            bool isUserChoiceValid, didUserChoseToFilter;
            int userChoiceInt;

            clear();
            Console.WriteLine("Do you want to filter vehicles by their status?{0}1. Yes{0}2. No", Environment.NewLine);
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(userChoiceString, 2);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid Input, Please enter a number from the list below.{0}Do you want to filter vehicles by their status?{0}1. Yes{0}2. No", Environment.NewLine);
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(userChoiceString, 2);
            }

            // the parsing below will always success, its validation occurs in the while loop
            userChoiceInt = int.Parse(userChoiceString);
            if (userChoiceInt == 1)
            {
                didUserChoseToFilter = true;
            }
            else
            {
                didUserChoseToFilter = false;
            }

            return didUserChoseToFilter;
        }

        private void vehicleDoesNotExist()
        {
            clear();
            Console.WriteLine("This vehicle does not exist.");
        }

        private VehicleAllocator.eVehicleTypes requestVehicleTypeFromUser()
        {
            VehicleAllocator.eVehicleTypes userVeichleType;
            string userVeichleTypeString, message, invalidMessage;
            bool isValidInput;
            int userVeichleTypeInt, numberOfVeicleType;

            StringBuilder vehicleTypes = new StringBuilder(100); // TODO: think other capacity, maybe const?

            numberOfVeicleType = 0;

            // this foreach loop passing all the vehicles supported in the garage and add its names to string.
            foreach (VehicleAllocator.eVehicleTypes vehicleType in (VehicleAllocator.eVehicleTypes[])Enum.GetValues(typeof(VehicleAllocator.eVehicleTypes)))
            {
                numberOfVeicleType++;
                vehicleTypes.AppendFormat("{0}. {1}{2}", numberOfVeicleType, vehicleType.ToString(), Environment.NewLine);
            }

            message = string.Format("Please enter vehicle type from the list below:{0}{1}", Environment.NewLine, vehicleTypes);
            invalidMessage = string.Format("Invalid input, Please enter a correct number of veichle from the list below:{0}{1}", Environment.NewLine, vehicleTypes);
            clear();
            Console.WriteLine(message);
            userVeichleTypeString = Console.ReadLine();
            isValidInput = isValidChoice(userVeichleTypeString, numberOfVeicleType);

            while (!isValidInput)
            {
                clear();
                Console.WriteLine(invalidMessage);
                userVeichleTypeString = Console.ReadLine();
                isValidInput = isValidChoice(userVeichleTypeString, numberOfVeicleType);
            }

            // the parsing below will always success. Its validation occurs in the while loop
            userVeichleTypeInt = int.Parse(userVeichleTypeString);
            userVeichleType = (VehicleAllocator.eVehicleTypes)userVeichleTypeInt;

            return userVeichleType;
        }

        private string requestLicenseNumber()
        {
            string userLicenseNumber;
            bool isValidInput;

            clear();
            Console.WriteLine("Please enter license number:");
            userLicenseNumber = Console.ReadLine();
            isValidInput = !string.IsNullOrEmpty(userLicenseNumber) && IsAllDigits(userLicenseNumber);
            while (!isValidInput)
            {
                clear();
                Console.WriteLine("Invalid license number, empty or non numric input are not allowed. Please enter valid license number");
                userLicenseNumber = Console.ReadLine();
                isValidInput = !string.IsNullOrEmpty(userLicenseNumber) && IsAllDigits(userLicenseNumber);
            }

            return userLicenseNumber;
        }

        private bool IsAllDigits(string i_StringToCheck)
        {
            bool res = true;

            foreach (char character in i_StringToCheck)
            {
                if (!char.IsDigit(character))
                {
                    res = false;
                    break;
                }
            }

            return res;
        }

        public bool isOwnerNameValid(string i_StringToCheck)
        {
            // this method return true if the owner name is valid, i.e. the name include at least one letter and space char can appear.
            bool isStringContainLetter, res, isCharLetterOrSpace;
            char spaceChar = ' ';

            isCharLetterOrSpace = true;
            isStringContainLetter = isContainLetter(i_StringToCheck);
            foreach (char character in i_StringToCheck)
            {
                if (!char.IsLetter(character) && character != spaceChar)
                {
                    isCharLetterOrSpace = false;
                    break;
                }
            }

            res = isStringContainLetter && isCharLetterOrSpace;

            return res;
        }

        private bool isContainLetter(string i_StringToCheck)
        {
            bool res = false;

            foreach (char character in i_StringToCheck)
            {
                if (char.IsLetter(character))
                {
                    res = true;
                    break;
                }
            }

            return res;
        }

        private bool isContainDigit(string i_StringToCheck)
        {
            bool res = false;

            foreach (char character in i_StringToCheck)
            {
                if (char.IsDigit(character))
                {
                    res = true;
                    break;
                }
            }

            return res;
        }

        private void clear()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }
    }
}