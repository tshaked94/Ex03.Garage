using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_EnergyPercentage;
        protected List<Tire> m_VehicleTires = new List<Tire>();

        protected List<Tire> VehicleTires
        {
            get
            {

                return m_VehicleTires;
            }
        }

        protected float EnergyPercentage
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

        protected string LicenseNumber
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

        protected string ModelName
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
    }
}
