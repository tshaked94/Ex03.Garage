using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Customer
    {
        public enum eVehicleStatus { Paid, Repaired, Repairing};

        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_OwnerVehicle;

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
