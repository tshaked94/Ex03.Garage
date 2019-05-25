using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Customer
    {
        public enum eVehicleStatus
        {
            Paid = 1,
            Repaired,
            Repairing
        }

        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_OwnerVehicle;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.Repairing;

        public Customer(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhone;
            m_OwnerVehicle = i_Vehicle;
        }

        public Vehicle Vehicle
        {
            get
            {

                return m_OwnerVehicle;
            }

            set
            {
                m_OwnerVehicle = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {

                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public string PhoneNumber
        {
            get
            {

                return m_OwnerPhoneNumber;
            }

            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        public string Name
        {
            get
            {

                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }
    }
}
