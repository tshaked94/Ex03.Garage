﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorbike : ElectricVehicle
    {
        private Utilities.eMotorbikeLicenseType m_LicenseType;
        private int m_EngineVolume;
        public const int k_TirePressure = 33;
        public const float k_MaximumBatteryLife = 1.4f;

        public ElectricMotorbike()
        {
            // electric motorbike c'tor
            m_VehicleTires.Add(new Tire(k_TirePressure));
            m_VehicleTires.Add(new Tire(k_TirePressure));
            m_MaximumBatteryLife = k_MaximumBatteryLife;
        }

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

        public override List<KeyValuePair<string, string>> VehicleInformationByType()
        {
            // this method return a list of pairs of attributes as strings
            int numberOfAttributes = 5;
            List<KeyValuePair<string, string>> informationList = new List<KeyValuePair<string, string>>(numberOfAttributes);

            informationList.Add(new KeyValuePair<string, string>("Vehicle type", GetType().Name));
            informationList.Add(new KeyValuePair<string, string>("Battery life left", string.Format("{0} hours", m_BatteryLifeLeft)));
            informationList.Add(new KeyValuePair<string, string>("Maximum battery lifetime", string.Format("{0} hours", m_MaximumBatteryLife)));
            informationList.Add(new KeyValuePair<string, string>("License type", Enum.GetName(typeof(Utilities.eMotorbikeLicenseType), (int)m_LicenseType)));
            informationList.Add(new KeyValuePair<string, string>("Engine volume", m_EngineVolume.ToString()));

            return informationList;
        }
    }
}
