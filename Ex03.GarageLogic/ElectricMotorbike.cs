using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class ElectricMotorbike : ElectricVehicle
    {
        private Utilities.eMotorbikeLicenseType m_LicenseType;
        private int m_EngineVolume;

        public int EngineVolume
        {
            get
            {

                return m_EngineVolume;
            }

            set
            {
                m_EngineVolume = value;
            }
        }

        public Utilities.eMotorbikeLicenseType LicenseType
        {
            get
            {

                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }
        public ElectricMotorbike()
        {
            m_VehicleTires.Add(new Tire(33f));
            m_VehicleTires.Add(new Tire(33f));
            m_MaximumBatteryLife = 1.4f;
        }
    }
}
