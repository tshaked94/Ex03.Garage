using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<int, Customer> m_Customers = new Dictionary<int, Customer>();

        public void InflateVehicleTiresToMaximum(string i_LicenseNumber)
        {
            foreach(Tire tire in m_Customers[i_LicenseNumber.GetHashCode()].Vehicle.Tires)
            {
                tire.Inflate(tire.MaximumPressure - tire.CurrentPressure);
            }
        }

        public void AddNewCustomer(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleCreated)
        {
            Customer newCustomer;
            int hashCode;

            newCustomer = new Customer(i_OwnerName, i_OwnerPhoneNumber, i_VehicleCreated);
            hashCode = i_VehicleCreated.LicenseNumber.GetHashCode();
            m_Customers.Add(hashCode, newCustomer);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, Customer.eVehicleStatus i_VehicleStatusToChange)
        {
            m_Customers[i_LicenseNumber.GetHashCode()].VehicleStatus = i_VehicleStatusToChange;
        }

        private float ToHour(float i_AmountOfMinutes)
        {
            float minutesConvertedToHours;

            minutesConvertedToHours = i_AmountOfMinutes / 60;

            return minutesConvertedToHours;
        }

        public LinkedList<string> ShowLicenseNumbersByFilter(Customer.eVehicleStatus i_VehicleStatusFilter)
        {
            LinkedList<string> listOFNumbersToShow = new LinkedList<string>();
            
            foreach(Customer customer in m_Customers.Values)
            {
                if(customer.VehicleStatus == i_VehicleStatusFilter)
                {
                    listOFNumbersToShow.AddFirst(customer.Vehicle.LicenseNumber);
                }
            }

            return listOFNumbersToShow;
        }

        public LinkedList<string> ShowAllLicenseNumbers()
        {
            LinkedList<string> listOFNumbersToShow = new LinkedList<string>();

            foreach (Customer customer in m_Customers.Values)
            {
                listOFNumbersToShow.AddFirst(customer.Vehicle.LicenseNumber);
            }

            return listOFNumbersToShow;
        }

        public void SetCarDetails(Vehicle i_VehicleToSetDetails, Utilities.eCarColor carColor, int numberOfDoors)
        {
            // this method set the car details according to the parameters recieved
            // at this stage we know that the vehicle is EnginedCar or ElectricCar
            // TODO: check is as
            if(i_VehicleToSetDetails is EnginedCar)
            {
                (i_VehicleToSetDetails as EnginedCar).CarColor = carColor;
                (i_VehicleToSetDetails as EnginedCar).NumberOfDoors = numberOfDoors;
                (i_VehicleToSetDetails as EnginedCar).CurrentFuelAmount = ((i_VehicleToSetDetails as EnginedCar).MaximumFuelAmount * i_VehicleToSetDetails.EnergyPercentage) / 100;
            }

            if(i_VehicleToSetDetails is ElectricCar)
            {
                (i_VehicleToSetDetails as ElectricCar).CarColor = carColor;
                (i_VehicleToSetDetails as ElectricCar).NumberOfDoors = numberOfDoors;
                (i_VehicleToSetDetails as ElectricCar).BatteryLifeLeft = ((i_VehicleToSetDetails as ElectricCar).MaximumBatteryLife * i_VehicleToSetDetails.EnergyPercentage) / 100;
            }
        }

        public void SetTireCurrentPressure(Vehicle i_VehicleToSetDetails, float i_CurrentTirePressure)
        {
            float maximumTirePressure = i_VehicleToSetDetails.Tires[0].MaximumPressure;

            if (i_CurrentTirePressure > maximumTirePressure || i_CurrentTirePressure < 0)
            {
                throw new ValueOutOfRangeException(maximumTirePressure, 0);
            }

            foreach(Tire tire in i_VehicleToSetDetails.Tires)
            {
                tire.CurrentPressure = i_CurrentTirePressure;
            }
        }

        public void SetVehicleDetails(Vehicle i_VehicleToSetDetails, string i_ModelName, string i_TiresManufacaturerName, float i_EnergyPercentageLeft)
        {
            // this method set the vehicle details according to the parameters recieved
            i_VehicleToSetDetails.ModelName = i_ModelName;
            i_VehicleToSetDetails.EnergyPercentage = i_EnergyPercentageLeft;
            foreach(Tire tire in i_VehicleToSetDetails.Tires)
            {
                tire.ManufacaturerName = i_TiresManufacaturerName;
            }
        }

        public void SetMotorbikeDetails(Vehicle i_VehicleToSetDetails, int i_EngineVolume, Utilities.eMotorbikeLicenseType i_MotorbikeLicenseType)
        {
            // this method set the motorbike details according to the parameters recieved
            // at this stage we know that the vehicle is EnginedMotorbike or ElectricMotorbike
            // TODO: check is as
            if (i_VehicleToSetDetails is ElectricMotorbike)
            {
                (i_VehicleToSetDetails as ElectricMotorbike).EngineVolume = i_EngineVolume;
                (i_VehicleToSetDetails as ElectricMotorbike).BatteryLifeLeft = (i_VehicleToSetDetails.EnergyPercentage * (i_VehicleToSetDetails as ElectricMotorbike).MaximumBatteryLife) / 100;
                (i_VehicleToSetDetails as ElectricMotorbike).LicenseType = i_MotorbikeLicenseType;
            }

            if(i_VehicleToSetDetails is EnginedMotorbike)
            {
                (i_VehicleToSetDetails as EnginedMotorbike).EngineVolume = i_EngineVolume;
                (i_VehicleToSetDetails as EnginedMotorbike).CurrentFuelAmount = (i_VehicleToSetDetails.EnergyPercentage * (i_VehicleToSetDetails as EnginedMotorbike).MaximumFuelAmount) / 100;
                (i_VehicleToSetDetails as EnginedMotorbike).LicenseType = i_MotorbikeLicenseType;
            }
        }

        public void SetLicenseNumber(string i_LicenseNumber, Vehicle i_VehicleToSet)
        {
            i_VehicleToSet.LicenseNumber = i_LicenseNumber;
        }

        public void SetTruckDetails(Truck i_TruckToSetDetails, float cargoVolume, bool isContainingDangerousCargo)
        {
            // this method set the truck details according to the parameters recieved
            i_TruckToSetDetails.CargoVolume = cargoVolume;
            i_TruckToSetDetails.CurrentFuelAmount = (i_TruckToSetDetails.EnergyPercentage * i_TruckToSetDetails.MaximumFuelAmount) / 100;
            i_TruckToSetDetails.IsContainingDangerousMaterials = isContainingDangerousCargo;
        }

        public void ChangeCustomerVehicleStatus(string i_LicenseNumberOfVehicle, Customer.eVehicleStatus i_NewVehicleStatus)
        {
            m_Customers[i_LicenseNumberOfVehicle.GetHashCode()].VehicleStatus = i_NewVehicleStatus;
        }

        public bool isVehicleInGarage(string i_LicenseNumber)
        {
            bool isVehicleFound = false;

            isVehicleFound = m_Customers.ContainsKey(i_LicenseNumber.GetHashCode());

            return isVehicleFound;
        }

        public Dictionary<int, Customer> Customers
        {
            get
            {

                return m_Customers;
            }
        }

        public Customer CustomerDetails(string i_LicenseNumberOfVehicle)
        {
            Customer customerToShowDetails;

            customerToShowDetails = m_Customers[i_LicenseNumberOfVehicle.GetHashCode()];

            return customerToShowDetails;
        }

        public void FuelEnginedVehicle(string i_LicenseNumberOfVehicle, EnginedVehicle.eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            // maybe to return a bool to ui if success or not and send message to user according to situation
            // TODO: check is as part.
            Vehicle vehicleToFuel;

            vehicleToFuel = m_Customers[i_LicenseNumberOfVehicle.GetHashCode()].Vehicle;
            (vehicleToFuel as EnginedVehicle).Refuel(i_AmountOfFuelToAdd, i_FuelType);
        }

        public void ChargeElectricVehicle(string i_LicenseNumberOfVehicle, float i_AmountOfMinutesToCharge)
        {
            // maybe to return a bool to ui if success or not and send message to user according to situation
            // TODO: check is as part.
            Vehicle vehicleToCharge;

            vehicleToCharge = m_Customers[i_LicenseNumberOfVehicle.GetHashCode()].Vehicle;
            (vehicleToCharge as ElectricVehicle).Charge(i_AmountOfMinutesToCharge / 60);
        }

        public bool IsEnginedVehicle(string i_LicenseNumberOfVehicle)
        {
            bool isEngined;

            isEngined = m_Customers[i_LicenseNumberOfVehicle.GetHashCode()].Vehicle is EnginedVehicle;

            return isEngined;
        }

        public bool areFuelTypesEquals(string i_LicenseNumberOfVehicle, EnginedVehicle.eFuelType fuelType)
        {
            bool isFuelEquals;

            if ((m_Customers[i_LicenseNumberOfVehicle.GetHashCode()].Vehicle as EnginedVehicle).FuelType == fuelType)
            {
                isFuelEquals = true;
            }
            else
            {
                 isFuelEquals = false;
            }

            return isFuelEquals;
        }

        public bool IsElectricVehicle(string i_LicenseNumberOfVehicle)
        {
            bool isElectric;

            isElectric = m_Customers[i_LicenseNumberOfVehicle.GetHashCode()].Vehicle is ElectricVehicle;

            return isElectric;
        }
    }
}
