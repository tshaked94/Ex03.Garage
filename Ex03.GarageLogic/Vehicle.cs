using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_EnergyPercentage;
        protected List<Tire> m_VehicleTires = new List<Tire>();

        public List<Tire> Tires
        {
            get
            {

                return m_VehicleTires;
            }
        }

        public float EnergyPercentage
        {
            get
            {

                return m_EnergyPercentage;
            }

            set
            {
                m_EnergyPercentage = value;
            }
        }

        public string LicenseNumber
        {
            get
            {

                return m_LicenseNumber;
            }
            
            set
            {
                m_LicenseNumber = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public int HashCode()
        {
            // TODO:Check if needed.
            return m_LicenseNumber.GetHashCode();
        }

        public abstract List<KeyValuePair<string, string>> VehicleInformationByType();
    }
}
