using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<int, Customer> m_Customers;

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
            if(vehicleToFuel is EnginedVehicle)
            {
                (vehicleToFuel as EnginedVehicle).Refuel(i_AmountOfFuelToAdd, i_FuelType);
            }
            else
            {
                // TODO: throw argument exception

            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumberOfVehicle, float i_AmountOfMinutesToCharge)
        {
            // maybe to return a bool to ui if success or not and send message to user according to situation
            // TODO: check is as part.
            Vehicle vehicleToCharge;

            vehicleToCharge = m_Customers[i_LicenseNumberOfVehicle.GetHashCode()].Vehicle;
            if (vehicleToCharge is ElectricVehicle)
            {
                (vehicleToCharge as ElectricVehicle).Charge(i_AmountOfMinutesToCharge);
            }
            else
            {
                // TODO: throw argument exception

            }
        }
    }
}
