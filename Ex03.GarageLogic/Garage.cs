using System;
using System.Collections.Generic;
using System.Text;
using Ex03.ConsoleUI;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<int, Customer> m_Customers;
        private UI m_UI = new UI();

        public void InflateVehicleTires()
        {
            string licenseNumberOfVehicleToInflate;
            int hashCode;
            bool isVehicleFound;
            Customer customer;

            licenseNumberOfVehicleToInflate = m_UI.RequestLicenseNumber();
            hashCode = licenseNumberOfVehicleToInflate.GetHashCode();
            isVehicleFound = m_Customers.TryGetValue(hashCode, out customer);
            if(isVehicleFound)
            {
                foreach(Tire tire in customer.Vehicle.Tires)
                {
                    tire.Inflate(tire.MaximumPressure - tire.CurrentPressure);
                }
            }
            else
            {
                m_UI.VehicleDoesNotExist();
            }
        }

        public void ChangeCustomerVehicleStatus()
        {
            Customer.eVehicleStatus newVehicleStatus;
            Customer customer;
            string licenseNumberOfVehicle;
            int hashCode;
            bool isVehicleFound;

            licenseNumberOfVehicle = m_UI.RequestLicenseNumber();
            hashCode = licenseNumberOfVehicle.GetHashCode();
            isVehicleFound = m_Customers.TryGetValue(hashCode, out customer);
            if(isVehicleFound)
            {
                newVehicleStatus = m_UI.RequestNewVehicleStatus();
                customer.VehicleStatus = newVehicleStatus;
            }
            else
            {
                m_UI.VehicleDoesNotExist();
            }
        }

        public void ShowLicenseNumbers()
        {
            Customer.eVehicleStatus vehicleStatusFilter;
            bool isFilterRequired;

            isFilterRequired = m_UI.AskUserIfHeWantToFilterLicenseNumberByStatus();
            if (isFilterRequired)
            {
                vehicleStatusFilter = m_UI.RequestStatusFilter();
                foreach (KeyValuePair<int,Customer> customer in m_Customers)
                {
                    if (customer.Value.VehicleStatus == vehicleStatusFilter)
                    {
                        m_UI.ShowLicenseNumber(customer.Value.Vehicle.LicenseNumber);
                    }
                }
            }
            else
            {
                foreach(KeyValuePair<int, Customer> customer in m_Customers)
                {
                    m_UI.ShowLicenseNumber(customer.Value.Vehicle.LicenseNumber);
                }
            }
        }

        public void AddNewVehicle()
        {
            bool isVehicleAlreadyInTheGarage;
            string licenseNumber;
            Customer newCustomer;
            int hashCode;

            licenseNumber = m_UI.RequestLicenseNumber();
            isVehicleAlreadyInTheGarage = isVehicleInGarage(licenseNumber);
            if (isVehicleAlreadyInTheGarage)
            {
                m_UI.InformUserVehicleIsAlreadyInTheGarage();
            }
            else
            {
                newCustomer = createCustomer();
                hashCode = newCustomer.Vehicle.LicenseNumber.GetHashCode();
                m_Customers.Add(hashCode, newCustomer);
            }
        }

        private bool isVehicleInGarage(string i_LicenseNumber)
        {
            bool isVehicleFound = false;

            isVehicleFound = m_Customers.ContainsKey(i_LicenseNumber.GetHashCode());

            return isVehicleFound;
        }

        private Customer createCustomer()
        {
            VehicleAllocator.eVehicleTypes vehicleType;
            string ownerName, ownerPhoneNumber;
            Vehicle newVehicle;
            Customer newCustomer;

            ownerName = m_UI.RequestOwnerName();
            ownerPhoneNumber = m_UI.RequestOwnerPhoneNumber();
            vehicleType = m_UI.RequestVehicleType();
            newVehicle = VehicleAllocator.AllocateVehicle(vehicleType);
            newCustomer = new Customer(ownerName, ownerPhoneNumber, newVehicle);

            return newCustomer;
        }

        public List<Customer> Customers
        {
            get
            {

                return m_Customers;
            }
        }

    }
}
