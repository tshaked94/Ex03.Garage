﻿using System;
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
                switch(userChoice)
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
                        fuelRegularEnginedVehicle();
                        break;
                    case eUserOptions.ChargeElectricVehicle:
                        chargeElectricVehicle();
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
                vehicleType = getVehicleTypeFromUser();
                vehicleCreated = VehicleAllocator.MakeNewVehicle(vehicleType);
                m_Garage.SetLicenseNumber(licenseNumber, vehicleCreated);
                getVehicleDetails(vehicleCreated);
                getOwnerDetails(out ownerName, out ownerPhoneNumber);
                m_Garage.AddNewCustomer(ownerName, ownerPhoneNumber, vehicleCreated);
            }


            holdScreen();
        }

        private void getVehicleDetails(Vehicle i_VehicleToSetDetails)
        {
            // this method gets the vehicle details from the user.
            string modelName, tiresManufacaturerName;
            float energyPercentageLeft, currentTirePressure, cargoVolume;
            Utilities.eCarColor carColor;
            int numberOfDoors, engineVolume;
            Utilities.eMotorbikeLicenseType motorbikeLicenseType;
            bool isContainingDangerousCargo;

            modelName = getModelName();
            energyPercentageLeft= getEnergyPercentageLeft();
            getVehicleTiresDetails(out tiresManufacaturerName, out currentTirePressure);
            m_Garage.SetVehicleDetails(i_VehicleToSetDetails, modelName, tiresManufacaturerName, currentTirePressure, energyPercentageLeft);
            if(i_VehicleToSetDetails is EnginedCar || i_VehicleToSetDetails is ElectricCar)
            {
                getCarDetails(out carColor, out numberOfDoors);
                m_Garage.SetCarDetails(i_VehicleToSetDetails, carColor, numberOfDoors);
            }
            else if (i_VehicleToSetDetails is EnginedMotorbike || i_VehicleToSetDetails is ElectricMotorbike)
            {
                getMotorbikeDetails(out engineVolume, out motorbikeLicenseType);
                m_Garage.SetMotorbikeDetails(i_VehicleToSetDetails, engineVolume, motorbikeLicenseType);
            }
            else if(i_VehicleToSetDetails is Truck)
            {
                getTruckDetails(out cargoVolume, out isContainingDangerousCargo);
                m_Garage.SetTruckDetails(i_VehicleToSetDetails as Truck, cargoVolume, isContainingDangerousCargo);
            }
        }

        private void getTruckDetails(out float o_CargoVolume, out bool o_IsContainingDangerousCargo)
        {
            // this method gets the truck details from the user.
            o_IsContainingDangerousCargo = getDangerousCargoDetail();
            o_CargoVolume = getCargoVolumeDetail();
        }

        private float getCargoVolumeDetail()
        {
            // this method asks the user the cargo volume of the truck and return it as a float.
            string userChoiceString;
            bool isUserChoiceValid;
            float cargoVolumeFloat;

            clear();
            Console.WriteLine("Please enter the truck's cargo volume");
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = float.TryParse(userChoiceString, out cargoVolumeFloat);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input, please enter a number represents the cargo volume");
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = float.TryParse(userChoiceString, out cargoVolumeFloat);
            }

            return cargoVolumeFloat;
        }

        private bool getDangerousCargoDetail()
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

        private void getMotorbikeDetails(out int o_EngineVolume, out Utilities.eMotorbikeLicenseType o_MotorbikeLicenseType)
        {
            // this method get motorbike details from the user
            o_EngineVolume = getEngineVolume();
            o_MotorbikeLicenseType = getMotorbikeLicenseType();
        }

        private Utilities.eMotorbikeLicenseType getMotorbikeLicenseType()
        {
            // this method get motorbike license type from user.
            Utilities.eMotorbikeLicenseType userMotorbikeLicenseType;
            string userMotorbikeLicenseTypeString, MotorbikeLicenseTypes, message1, message2;
            int userMotorbikeLicenseTypeInt;
            bool isInputValid;

            MotorbikeLicenseTypes = string.Format("1. A{0}2. A1{0}3. A2{0}4. B", Environment.NewLine);
            message1 = string.Format("Please enter license type from the list below: {0}{1}", Environment.NewLine, MotorbikeLicenseTypes);
            message2 = string.Format("Invalid input, please enter valid number of license type from the list below: {0}{1}", Environment.NewLine, MotorbikeLicenseTypes);
            clear();
            Console.WriteLine(message1);
            userMotorbikeLicenseTypeString = Console.ReadLine();
            isInputValid = isValidChoice(userMotorbikeLicenseTypeString, 4);
            while(!isInputValid)
            {
                clear();
                Console.WriteLine(message2);
                userMotorbikeLicenseTypeString = Console.ReadLine();
                isInputValid = isValidChoice(userMotorbikeLicenseTypeString, 4);
            }

            userMotorbikeLicenseTypeInt = int.Parse(userMotorbikeLicenseTypeString);
            userMotorbikeLicenseType = (Utilities.eMotorbikeLicenseType)userMotorbikeLicenseTypeInt;
            return userMotorbikeLicenseType;
        }

        private int getEngineVolume()
        {
            string engineVolumeString;
            int engineVolumeInt;
            bool isUserChoiceValid;

            clear();
            Console.WriteLine("Please enter the engine volume");
            engineVolumeString = Console.ReadLine();
            isUserChoiceValid = int.TryParse(engineVolumeString, out engineVolumeInt);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input, Please enter a number represents the engine volume");
                engineVolumeString = Console.ReadLine();
                isUserChoiceValid = int.TryParse(engineVolumeString, out engineVolumeInt);
            }

            return engineVolumeInt;
        }

        private void getCarDetails(out Utilities.eCarColor o_CarColor, out int o_NumOfDoors)
        {
            // this method get car details from the user
            o_CarColor = getCarColor();
            o_NumOfDoors = getNumOfDoors();
        }

        private int getNumOfDoors()
        {
            // this method get num of doors in car from the user.
            string numOfDoorsString;
            int numOfDoorsInt;
            bool isUserChoiceValid;

            clear();
            Console.WriteLine("Please enter number of doors in the car");
            numOfDoorsString = Console.ReadLine();
            isUserChoiceValid = int.TryParse(numOfDoorsString, out numOfDoorsInt);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input, Please enter a number represents the number of doors in the car");
                numOfDoorsString = Console.ReadLine();
                isUserChoiceValid = int.TryParse(numOfDoorsString, out numOfDoorsInt);
            }

            return numOfDoorsInt;
        }

        private Utilities.eCarColor getCarColor()
        {
            // this method get car color from the user.
            // TODO: list of strings of car colors and print it instead of 1.red....
            string carColorString;
            bool isUserChoiceValid;
            Utilities.eCarColor carColor;
            int carColorInt;

            clear();
            Console.WriteLine("Please choose car color from the list below:{0}1.Red{0}2.Blue{0}3.Black{0}4.Grey", Environment.NewLine);
            carColorString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(carColorString, 4);
            while(!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input. Please choose car color from the list below:{0}1.Red{0}2.Blue{0}3.Black{0}4.Grey", Environment.NewLine);
                carColorString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(carColorString, 4);
            }

            carColorInt = int.Parse(carColorString);
            carColor = (Utilities.eCarColor)carColorInt;


            return carColor;
        }

        private void getVehicleTiresDetails(out string o_TireManufacaturerName, out float o_CurrentTirePressure)
        {
            // this method gets the tires details from user
            o_TireManufacaturerName = getTireManufacaturerName();
            o_CurrentTirePressure = getCurrentTiresPressure();
        }

        private float getCurrentTiresPressure()
        {
            // this method get vehicle's current tires pressure.
            string tirePressuerString;
            float tirePressureFloat;
            bool isUserChoiceValid;

            clear();
            Console.WriteLine("Please enter the current tires pressure");
            tirePressuerString = Console.ReadLine();
            isUserChoiceValid = float.TryParse(tirePressuerString, out tirePressureFloat);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input, Please enter a number represents the current tires pressure");
                tirePressuerString = Console.ReadLine();
                isUserChoiceValid = float.TryParse(tirePressuerString, out tirePressureFloat);
            }

            return tirePressureFloat;
        }

        private string getTireManufacaturerName()
        {
            // this method get the tire manufacaturer name.
            string manufacaturerName;

            clear();
            Console.WriteLine("Please enter the tire's manufacaturer name");
            manufacaturerName = Console.ReadLine();

            return manufacaturerName;
        }

        private string getModelName()
        {
            // this method gets the model name of the vehicle from the user.
            string modelName;

            clear();
            Console.WriteLine("Please enter the vehicle's model name");
            modelName = Console.ReadLine();

            return modelName;
        }

        private float getEnergyPercentageLeft()
        {
            // this method gets the percentage of energy left from the user and return it as float.
            string energyLeftString;
            float energyLeftFloat;
            bool isUserChoiceValid;

            clear();
            Console.WriteLine("Please enter the energy perecntage left in vehicle");
            energyLeftString = Console.ReadLine();
            isUserChoiceValid = float.TryParse(energyLeftString, out energyLeftFloat) && isPercentage(energyLeftFloat);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid input, please enter a number represents the energy percentage left");
                energyLeftString = Console.ReadLine();
                isUserChoiceValid = float.TryParse(energyLeftString, out energyLeftFloat) && isPercentage(energyLeftFloat);
            }

            return energyLeftFloat;
        }

        private bool isPercentage(float energyLeftFloat)
        {
            // this method gets a num and checks if it represents a valid percentage between 0-100.
            bool isNumPercentage;

            isNumPercentage = (energyLeftFloat >= 0) && (energyLeftFloat <= 100);

            return isNumPercentage;
        }

        private void vehicleStatusHasBeenChangedToRepairing()
        {
            Console.WriteLine("Vehicle status has been changed to repairing.");
        }        

        private void getOwnerDetails(out string io_OwnerName, out string io_OwnerPhoneNumber)
        {
            // this method request user to enter his name and his phone number, it return this data by out parameters
            bool isNameValid, isPhoneNumberValid;

            clear();
            Console.WriteLine("Please enter your name:");
            io_OwnerName = Console.ReadLine();
            isNameValid = IsAllLetters(io_OwnerName);
            while (!isNameValid)
            {
                clear();
                Console.WriteLine("Name cannot include numbers, Please enter valid name:");
                io_OwnerName = Console.ReadLine();
                isNameValid = IsAllLetters(io_OwnerName);
            }

            clear();
            Console.WriteLine("Please enter your phone number:");
            io_OwnerPhoneNumber = Console.ReadLine();
            isPhoneNumberValid = IsAllDigits(io_OwnerPhoneNumber);
            while (!isPhoneNumberValid)
            {
                clear();
                Console.WriteLine("Phone number cannot include letters, Please enter valid phone number:");
                io_OwnerName = Console.ReadLine();
                isPhoneNumberValid = IsAllDigits(io_OwnerPhoneNumber);
            }
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
            // this method inform and change vehicle status.
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

            holdScreen();
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
                Console.WriteLine("Tires has been sucsessfully inflated to maximum");
                holdScreen();
            }
            else
            {
                vehicleDoesNotExist();
            }

            holdScreen();
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

            holdScreen();
        }

        private float requestAmountOfFuelToAdd()
        {
            string fuelAmountToAddString;
            float fuelAmountToAdd;
            bool isParsingSuccessed;

            // TODO: add Formatting exeption
            clear();
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

            clear();
            Console.WriteLine("Please enter fuel type{0}1.Soler{0}2.Octan95{0}3.Octan96{0}4.Octan98", Environment.NewLine);
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(userChoiceString, 4);
            while (!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid Input, please choose a number from the list below.{0}Please enter fuel type{0}1.Soler{0}2.Octan95{0}3.Octan96{0}4.Octan98", Environment.NewLine);
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(userChoiceString, 4);
            }

            userChoiceInt = int.Parse(userChoiceString);
            fuelType = (EnginedVehicle.eFuelType)userChoiceInt;

            return fuelType;
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

            holdScreen();
        }

        private float requestAmountOfMinutesToCharge()
        {
            string batteryAmountToAddString;
            float batteryAmountToAdd;
            bool isParsingSuccessed;


            // TODO: add Formatting exeption
            clear();
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
                printAllCustomerDetails(customerToShowDetails);
            }
            else
            {
                vehicleDoesNotExist();
            }
        }

        private void printAllCustomerDetails(Customer i_CustomerToShowDetails)
        {
            printOwnerDetails(i_CustomerToShowDetails);
            printGeneralVehicleDetails(i_CustomerToShowDetails);
            if(i_CustomerToShowDetails.Vehicle is ElectricVehicle)
            {
                printElectricVehicleDetails(i_CustomerToShowDetails.Vehicle as ElectricVehicle);
            }
            else if(i_CustomerToShowDetails.Vehicle is EnginedVehicle)
            {
                printEnginedVehicleDetails(i_CustomerToShowDetails.Vehicle as EnginedVehicle);
            }
      
            holdScreen();
        }

        private void printEnginedVehicleDetails(EnginedVehicle i_EnginedVehicle)
        {
            string fuelType, enginedVehicleDetails;
            float currentFuelAmount, maximumFuelAmount;

            fuelType = i_EnginedVehicle.FuelType.ToString();
            currentFuelAmount = i_EnginedVehicle.CurrentFuelAmount;
            maximumFuelAmount = i_EnginedVehicle.MaximumFuelAmount;
            enginedVehicleDetails = string.Format("Maximum fuel amount is: {1}{0}Current fuel amount is: {2}{0}Fuel type is: {3}", Environment.NewLine, maximumFuelAmount, currentFuelAmount, fuelType);
            Console.WriteLine(enginedVehicleDetails);
            if(i_EnginedVehicle is EnginedCar)
            {
                printEnginedCarDetails(i_EnginedVehicle as EnginedCar);
            }
            else if(i_EnginedVehicle is EnginedMotorbike)
            {
                printEnginedMotorbikeDetails(i_EnginedVehicle as EnginedMotorbike);
            }
            else if(i_EnginedVehicle is Truck)
            {
                printTruckDetails(i_EnginedVehicle as Truck);
            }
        }

        private void printTruckDetails(Truck truck)
        {
            string truckDetails, yesOrNo;
            float cargoVolume;
            bool isContainingDangerousCargo;

            isContainingDangerousCargo = truck.IsContainingDangerousMaterials;
            yesOrNo = isContainingDangerousCargo == true ? "Yes" : "No";
            cargoVolume = truck.CargoVolume;
            truckDetails = string.Format("Does the truck contains dangerous cargo?: {1}{0}The cargo volume is: {2}", Environment.NewLine, yesOrNo, cargoVolume);
            Console.WriteLine(truckDetails);
        }

        private void printEnginedMotorbikeDetails(EnginedMotorbike i_EnginedMotorbike)
        {
            string electricMotorBikeDetails, licenseTypeString;
            int engineVolume;

            licenseTypeString = i_EnginedMotorbike.LicenseType.ToString();
            engineVolume = i_EnginedMotorbike.EngineVolume;
            electricMotorBikeDetails = string.Format("The license Type is: {1}{0}The engine volume is: {2}", Environment.NewLine, licenseTypeString, engineVolume);
            Console.WriteLine(electricMotorBikeDetails);
        }

        private void printEnginedCarDetails(EnginedCar i_EnginedCar)
        {
            string carColor, electricCarDetails;
            int numOfDoors;

            carColor = i_EnginedCar.CarColor.ToString();
            numOfDoors = i_EnginedCar.NumberOfDoors;
            electricCarDetails = string.Format("The car color is: {1}{0}Number of doors: {2}", Environment.NewLine, carColor, numOfDoors);
            Console.WriteLine(electricCarDetails);
        }

        private void printElectricVehicleDetails(ElectricVehicle i_ElectricVehicleToShowDetails)
        {
            float batteryLifeLeft, maximumBatteryLife;
            string batteryLifeLeftString;

            batteryLifeLeft = i_ElectricVehicleToShowDetails.BatteryLifeLeft;
            maximumBatteryLife = i_ElectricVehicleToShowDetails.MaximumBatteryLife;
            batteryLifeLeftString = string.Format("Maximum battery life is: {1}{0} Battery life left is: {2}", Environment.NewLine, maximumBatteryLife, batteryLifeLeft);
            Console.WriteLine(batteryLifeLeftString);
            if(i_ElectricVehicleToShowDetails is ElectricCar)
            {
                printElectricCarDetails(i_ElectricVehicleToShowDetails as ElectricCar);
            }
            else if(i_ElectricVehicleToShowDetails is ElectricMotorbike)
            {
                printElectricMotorbikeDetails(i_ElectricVehicleToShowDetails as ElectricMotorbike);
            }
        }

        private void printElectricMotorbikeDetails(ElectricMotorbike i_ElectricMotorbike)
        {
            string electricMotorBikeDetails, licenseTypeString;
            int engineVolume;

            licenseTypeString = i_ElectricMotorbike.LicenseType.ToString();
            engineVolume = i_ElectricMotorbike.EngineVolume;
            electricMotorBikeDetails = string.Format("The license Type is: {1}{0}The engine volume is: {2}", Environment.NewLine, licenseTypeString, engineVolume);
            Console.WriteLine(electricMotorBikeDetails);
        }

        private void printElectricCarDetails(ElectricCar i_ElectricCar)
        {
            string carColor, electricCarDetails;
            int numOfDoors;

            carColor = i_ElectricCar.CarColor.ToString();
            numOfDoors = i_ElectricCar.NumberOfDoors;
            electricCarDetails = string.Format("The car color is: {1}{0}Number of doors: {2}", Environment.NewLine, carColor, numOfDoors);
            Console.WriteLine(electricCarDetails);
        }

        private void printOwnerDetails(Customer i_CustomerToShowDetails)
        {
            string ownerName, ownerPhoneNumber, vehicleStatus, ownerDetails;

            ownerName = i_CustomerToShowDetails.Name;
            ownerPhoneNumber = i_CustomerToShowDetails.PhoneNumber;
            vehicleStatus = i_CustomerToShowDetails.VehicleStatus.ToString();
            ownerDetails = string.Format("Owner name: {1}{0}Owner phone number: {2}{0}Owner's vehicle status: {3}", Environment.NewLine, ownerName, ownerPhoneNumber, vehicleStatus);
            Console.WriteLine(ownerDetails);
        }

        private void printGeneralVehicleDetails(Customer i_CustomerToShowDetails)
        {
            string manufacturerName, licenseNumber, tireManufacturerName, generalVehicleDetails;
            float energyPercentageLeft, tireCurrentPressure, tireMaxPressure;

            manufacturerName = i_CustomerToShowDetails.Vehicle.ModelName;
            licenseNumber = i_CustomerToShowDetails.Vehicle.LicenseNumber;
            energyPercentageLeft = i_CustomerToShowDetails.Vehicle.EnergyPercentage;
            tireManufacturerName = i_CustomerToShowDetails.Vehicle.Tires[0].ManufacaturerName;
            tireCurrentPressure = i_CustomerToShowDetails.Vehicle.Tires[0].CurrentPressure;
            tireMaxPressure = i_CustomerToShowDetails.Vehicle.Tires[0].MaximumPressure;
            generalVehicleDetails = string.Format(@"Manufacturer Name: {1}{0}License Number: {2}{0}Energy Percentage Left: {3}{0}
Tires{0}Manufacturer Name: {4}{0}Current Tire Pressure: {5}{0}Max Tire Pressure: {6}", Environment.NewLine, manufacturerName, licenseNumber, energyPercentageLeft, tireManufacturerName, tireCurrentPressure, tireMaxPressure);
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
            Console.WriteLine("Please enter the new vehicle status{0}1.Paid{0}2.Repaired{0}3.Repairing", Environment.NewLine);
            newVehicleStatusString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(newVehicleStatusString, 3);
            while(!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid Input, please choose a number from the list below.{0}Please enter the new vehicle status{0}1.Paid{0}2.Repaired{0}3.Repairing", Environment.NewLine);
                newVehicleStatusString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(newVehicleStatusString, 3);
            }

            newVehicleStatusInt = int.Parse(newVehicleStatusString);
            newVehicleStatus = (Customer.eVehicleStatus)newVehicleStatusInt;

            return newVehicleStatus;
        }

        private void printLicenseNumbers(LinkedList<string> licenseNumbersToShow)
        {
            string message;
            int numOfLicense = 1;

            clear();
            foreach (string licenseNumber in licenseNumbersToShow)
            {
                message = string.Format("{0}. {1}", numOfLicense, licenseNumber);
                Console.WriteLine(message);
                numOfLicense++;                
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
            statusFilters = string.Format("1. Paid{0}2. Repaired{0} 3. Reapairing", Environment.NewLine);
            message1 = string.Format("Please enter status filter from the list below:{0}{1}", Environment.NewLine, statusFilters);
            message2 = string.Format("Invalid input, Please enter a correct number of status filter from the list below:{0}{1}",Environment.NewLine, statusFilters);
            clear();
            Console.WriteLine(message1);
            userStatusFilterString = Console.ReadLine();
            isValidInput = isValidChoice(userStatusFilterString, numberOfStatus);

            while(!isValidInput)
            {
                clear();
                Console.WriteLine(message2);
                userStatusFilterString = Console.ReadLine();
                isValidInput = isValidChoice(userStatusFilterString, numberOfStatus);
            }

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
            res = isParsingSuccessed && 1 <= userChoiceInt && userChoiceInt <= i_MaxOption;

            return res;  
        }

        private bool askUserIfHeWantToFilterLicenseNumberByStatus()
        {
            string userChoiceString;
            bool isUserChoiceValid, didUserChoseToFilter;
            int userChoiceInt;

            clear();
            Console.WriteLine("Do you want to filter vehicles by their status?{0}1.Yes{0}2.No", Environment.NewLine);
            userChoiceString = Console.ReadLine();
            isUserChoiceValid = isValidChoice(userChoiceString, 2);
            while(!isUserChoiceValid)
            {
                clear();
                Console.WriteLine("Invalid Input, Please enter a number from the list below.{0}Do you want to filter vehicles by their status?{0}1.Yes{0}2.No", Environment.NewLine);
                userChoiceString = Console.ReadLine();
                isUserChoiceValid = isValidChoice(userChoiceString, 2);
            }

            userChoiceInt = int.Parse(userChoiceString);
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

        private void vehicleDoesNotExist()
        {
            Console.WriteLine("This vehicle does not exist.");
        }

        private VehicleAllocator.eVehicleTypes getVehicleTypeFromUser()
        {
            // TODO: think about way to disply all supported vehcile types 
            VehicleAllocator.eVehicleTypes userVeichleType;
            string userVeichleTypeString, message1, message2;
            bool isValidInput;     
            int userVeichleTypeInt, numberOfVeicleType;
            
            StringBuilder vehicleTypes = new StringBuilder(100); // TODO: think other capacity, maybe const?
            
            numberOfVeicleType = 1;
            foreach (string vehicleType in VehicleAllocator.VehicleTypes)
            {   
                vehicleTypes.AppendFormat("{0}. {1}{2}", numberOfVeicleType, vehicleType, Environment.NewLine);
                numberOfVeicleType++;
            }
            message1 = string.Format("Please enter veichle type from the list below:{0}{1}", Environment.NewLine, vehicleTypes);
            message2 = string.Format("Invalid input, Please enter a correct number of veichle from the list below:{0}{1}",Environment.NewLine, vehicleTypes);
            clear();
            Console.WriteLine(message1);
            userVeichleTypeString = Console.ReadLine();
            isValidInput = isValidChoice(userVeichleTypeString, numberOfVeicleType);

            while(!isValidInput)
            {
                clear();
                Console.WriteLine(message2);
                userVeichleTypeString = Console.ReadLine();
                isValidInput = isValidChoice(userVeichleTypeString, numberOfVeicleType);
            }

            userVeichleTypeInt = int.Parse(userVeichleTypeString);
            userVeichleType = (VehicleAllocator.eVehicleTypes)userVeichleTypeInt;

            return userVeichleType;
        }

        private string requestLicenseNumber()
        {
            string userLicenseNumber;

            clear();
            Console.WriteLine("Please enter license number:");
            userLicenseNumber = Console.ReadLine();

            while (!IsAllDigits(userLicenseNumber))
            {
                clear();
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

        public bool IsAllLetters(string i_StringToCheck)
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

        private void clear()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }
    }
}