using System;
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
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_MaximumBatteryLife = k_MaximumBatteryLife;
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
        public override List<KeyValuePair<string, string>> Inforamtion()
        {
            int numberOfAttributes = 4;
            List<KeyValuePair<string, string>> informationList = new List<KeyValuePair<string, string>>(numberOfAttributes);

            informationList.Add(new KeyValuePair<string, string>("Battery life left", string.Format("{0} hours", m_BatteryLifeLeft)));
            informationList.Add(new KeyValuePair<string, string>("Maximum battery lifetime", string.Format("{0} hours", m_MaximumBatteryLife)));
            informationList.Add(new KeyValuePair<string, string>("License type", Enum.GetName(typeof(Utilities.eMotorbikeLicenseType), (int)m_LicenseType)));
            informationList.Add(new KeyValuePair<string, string>("Engine volume", m_EngineVolume.ToString()));

            return informationList;
        }

    }
}
